using System;
using System.Threading.Tasks;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class ShopDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Shop, Guid> _shopRepository;

    public ShopDataSeederContributor(IRepository<Shop, Guid> shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _shopRepository.GetCountAsync() <= 0)
        {
            await _shopRepository.InsertAsync(
                new Shop
                {
                    Name = "Dobrinja",
                    Address = "Omladinskih radnih brigada 9, Sarajevo 71000"
                },
                true
            );
        }
    }
}