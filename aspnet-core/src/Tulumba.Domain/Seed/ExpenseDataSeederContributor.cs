using System;
using System.Threading.Tasks;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class ExpenseDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Expense, Guid> _expenseRepistory;

    private readonly IRepository<ExpenseType, Guid> _expenseTypeRepistory;
    private readonly IRepository<Shop, Guid> _shopRepository;

    public ExpenseDataSeederContributor(IRepository<Expense, Guid> expenseRepository,
        IRepository<Shop, Guid> shopRepository, IRepository<ExpenseType, Guid> expenseTypeRepository)
    {
        _expenseRepistory = expenseRepository;
        _shopRepository = shopRepository;
        _expenseTypeRepistory = expenseTypeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _expenseRepistory.GetCountAsync() <= 0)
        {
            var expenseType = await _expenseTypeRepistory.InsertAsync(new ExpenseType
            {
                Name = "Ulje",
                ExpenseCategory = ExpenseCategory.Materijalni,
                ExpensePeriod = ExpensePeriod.Dnevni
            });
            var shop = await _shopRepository.InsertAsync(
                new Shop
                {
                    Name = "Bašćaršija",
                    Address = "Adresa 1"
                },
                true
            );
            await _expenseRepistory.InsertAsync(
                new Expense
                {
                    Date = DateTime.Now,
                    Shop = shop,
                    ExpenseType = expenseType
                },
                true
            );
        }
    }
}