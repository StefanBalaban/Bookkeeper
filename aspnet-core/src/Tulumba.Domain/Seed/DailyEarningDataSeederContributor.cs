using System;
using System.Threading.Tasks;
using Tulumba.Entities.DailyEarning;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class DailyEarningDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<DailyEarning, Guid> _dailyEarningRepistory;
    private readonly IRepository<Shop, Guid> _shopRepository;

    public DailyEarningDataSeederContributor(IRepository<DailyEarning, Guid> dailyEarningRepository,
        IRepository<Shop, Guid> shopRepository)
    {
        _dailyEarningRepistory = dailyEarningRepository;
        _shopRepository = shopRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _dailyEarningRepistory.GetCountAsync() <= 0)
        {
            var shop = await _shopRepository.InsertAsync(
                new Shop
                {
                    Name = "Dobrinja",
                    Address = "Omladinskih radnih brigada 9, Sarajevo 71000"
                },
                true
            );
            await _dailyEarningRepistory.InsertAsync(
                new DailyEarning
                {
                    Date = DateTime.Now,
                    EarningAmount = 1000,
                    Shop = shop
                },
                true
            );
        }
    }
}