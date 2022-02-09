using System;
using System.Threading.Tasks;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class DailyCashFlowDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<DailyCashFlow, Guid> _dailyCashFlowRepistory;

    private readonly IRepository<Shop, Guid> _shopRepository;

    public DailyCashFlowDataSeederContributor(IRepository<DailyCashFlow, Guid> dailyCashFlowRepository,
        IRepository<Shop, Guid> shopRepository)
    {
        _dailyCashFlowRepistory = dailyCashFlowRepository;
        _shopRepository = shopRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _dailyCashFlowRepistory.GetCountAsync() <= 0)
        {
            var shop = await _shopRepository.InsertAsync(
                new Shop
                {
                    Name = "Ilidža",
                    Address = "Seijinih radnih brigada 9, Sarajevo 71000"
                },
                true
            );

            await _dailyCashFlowRepistory.InsertAsync(
                new DailyCashFlow
                {
                    Date = DateTime.Now,
                    Shop = shop
                },
                true
            );
            await _dailyCashFlowRepistory.InsertAsync(
                new DailyCashFlow
                {
                    Date = DateTime.Now.AddDays(-1),
                    Shop = shop
                },
                true
            );
        }
    }
}