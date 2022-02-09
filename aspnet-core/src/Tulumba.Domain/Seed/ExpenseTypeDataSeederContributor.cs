using System;
using System.Threading.Tasks;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.Shop;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

public class ExpenseTypeDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<ExpenseType, Guid> _expenseTypeRepistory;

    public ExpenseTypeDataSeederContributor(IRepository<ExpenseType, Guid> expenseTypeRepository,
        IRepository<Shop, Guid> shopRepository)
    {
        _expenseTypeRepistory = expenseTypeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _expenseTypeRepistory.GetCountAsync() <= 0)
        {
            await _expenseTypeRepistory.InsertAsync(
                new ExpenseType
                {
                    Name = "Šećer",
                    ExpensePeriod = ExpensePeriod.Dnevni,
                    ExpenseCategory = ExpenseCategory.Materijalni
                },
                true
            );
            await _expenseTypeRepistory.InsertAsync(
                new ExpenseType
                {
                    Name = "Gorivo",
                    ExpensePeriod = ExpensePeriod.Mjesecni,
                    ExpenseCategory = ExpenseCategory.Transport
                },
                true
            );
            await _expenseTypeRepistory.InsertAsync(
                new ExpenseType
                {
                    Name = "Plata",
                    ExpensePeriod = ExpensePeriod.Mjesecni,
                    ExpenseCategory = ExpenseCategory.Transport
                },
                true
            );
            await _expenseTypeRepistory.InsertAsync(
                new ExpenseType
                {
                    Name = "Dnevnica",
                    ExpensePeriod = ExpensePeriod.Dnevni,
                    ExpenseCategory = ExpenseCategory.Transport
                },
                true
            );
        }
    }
}