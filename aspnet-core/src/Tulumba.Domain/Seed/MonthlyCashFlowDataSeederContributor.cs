using System;
using System.Threading.Tasks;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class MonthlyCashFlowDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<MonthlyCashFlow, Guid> _monthlyCashFlowRepistory;

    private readonly IRepository<Shop, Guid> _shopRepository;

    public MonthlyCashFlowDataSeederContributor(IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository,
        IRepository<Shop, Guid> shopRepository)
    {
        _monthlyCashFlowRepistory = monthlyCashFlowRepository;
        _shopRepository = shopRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _monthlyCashFlowRepistory.GetCountAsync() <= 0)
        {
            var shop = await _shopRepository.InsertAsync(
                new Shop
                {
                    Name = "Mahala",
                    Address = "Mahalskih radnih brigada 9, Sarajevo 71000"
                },
                true
            );

            await _monthlyCashFlowRepistory.InsertAsync(
                new MonthlyCashFlow
                {
                    Date = DateTime.Now,
                    Shop = shop
                },
                true
            );
            await _monthlyCashFlowRepistory.InsertAsync(
                new MonthlyCashFlow
                {
                    Date = DateTime.Now.AddDays(-31),
                    Shop = shop
                },
                true
            );
        }
    }
}