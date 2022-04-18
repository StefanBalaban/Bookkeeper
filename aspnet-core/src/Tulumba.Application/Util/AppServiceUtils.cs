using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.Employee;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Entities.RecurringExpense;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class AppServiceUtils : ITransientDependency
{
    private readonly IRepository<DailyCashFlow, Guid> _dailyCashFlowRepository;
    private readonly IRepository<Employee, Guid> _employeeRepository;
    private readonly IRepository<Expense, Guid> _expenseRepository;
    private readonly IRepository<ExpenseType, Guid> _expenseTypeRepository;
    private readonly IRepository<MonthlyCashFlow, Guid> _monthlyCashFlowRepository;
    private readonly IRepository<RecurringExpense, Guid> _recurringExpenseRepository;

    public AppServiceUtils(
        IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository,
        IRepository<DailyCashFlow, Guid> dailyCashFlowRepository,
        IRepository<Expense, Guid> expenseRepository,
        IRepository<Employee, Guid> employeeRepository,
        IRepository<ExpenseType, Guid> expenseTypeRepository,
        IRepository<RecurringExpense, Guid> recurringExpenseRepository)
    {
        _monthlyCashFlowRepository = monthlyCashFlowRepository;
        _dailyCashFlowRepository = dailyCashFlowRepository;
        _expenseRepository = expenseRepository;
        _employeeRepository = employeeRepository;
        _expenseTypeRepository = expenseTypeRepository;
        _recurringExpenseRepository = recurringExpenseRepository;
    }

    public async Task<MonthlyCashFlow> FindOrCreateMonthlyCashFlow(Guid shopId, DateTime date)
    {
        var monthlyCashFlow = await _monthlyCashFlowRepository.FindAsync(x =>
            x.Date.Month == date.Month && date.Year == x.Date.Year && x.ShopId == shopId);

        if (monthlyCashFlow == null)
        {
            monthlyCashFlow = await _monthlyCashFlowRepository.InsertAsync(new MonthlyCashFlow
            {
                Date = date,
                ShopId = shopId
            });
            await CreateMonthlySalaryExpenses(monthlyCashFlow);
            await CreateMonthlyRecurringExpenses(monthlyCashFlow);
        }

        return monthlyCashFlow;
    }

    public async Task<DailyCashFlow> FindOrCreateDailyCashFlow(Guid shopId, DateTime date,
        MonthlyCashFlow monthlyCashFlow)
    {
        var dailyCashFlow =
            await _dailyCashFlowRepository.FindAsync(x => x.Date.Date == date.Date && x.ShopId == shopId);

        if (dailyCashFlow == null)
        {
            dailyCashFlow = await _dailyCashFlowRepository.InsertAsync(new DailyCashFlow
            {
                Date = date,
                ShopId = shopId,
                MonthlyCashFlow = monthlyCashFlow
            });
        }

        return dailyCashFlow;
    }

    private async Task CreateMonthlySalaryExpenses(MonthlyCashFlow monthlyCashFlow)
    {
        var expenseTypeId = (await _expenseTypeRepository.FindAsync(x =>
            x.ExpensePeriod == ExpensePeriod.Mjesecni && x.ExpenseCategory == ExpenseCategory.Plata)).Id;
        var employeesExpenses =
            await _employeeRepository
                .Where(x => x.Active && x.ShopId == monthlyCashFlow.ShopId && x.SalaryType == SalaryType.Mjesecno)
                .Select(x =>
                    new Expense
                    {
                        EmployeeId = x.Id,
                        Amount = x.SalaryAmount,
                        ShopId = x.ShopId.HasValue ? (Guid) x.ShopId : EmployeeWithoutShopId(x),
                        Date = monthlyCashFlow.Date,
                        MonthlyCashFlow = monthlyCashFlow,
                        ExpenseTypeId = expenseTypeId
                    })
                .ToListAsync();

        await _expenseRepository.InsertManyAsync(employeesExpenses, true);
    }

    private async Task CreateMonthlyRecurringExpenses(MonthlyCashFlow monthlyCashFlow)
    {
        var expenses = await _recurringExpenseRepository
            .Where(x => x.Active)
            .Where(x => x.ShopId == monthlyCashFlow.ShopId)
            .Select(x =>
                new Expense
                {
                    ExpenseTypeId = x.ExpenseTypeId,
                    ShopId = x.ShopId,
                    Amount = x.Amount,
                    Date = monthlyCashFlow.Date,
                    MonthlyCashFlow = monthlyCashFlow
                })
            .ToListAsync();

        await _expenseRepository.InsertManyAsync(expenses, true);
    }

    private static Guid EmployeeWithoutShopId(Employee x)
    {
        throw new Exception($"Employee without ShopId assigned. EmployeeId {x.Id}");
    }
}