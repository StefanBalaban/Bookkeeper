using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.Employee;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.MonthlyCashFlow;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class AppServiceUtils : ITransientDependency
    {
        IRepository<MonthlyCashFlow, Guid> _monthlyCashFlowRepository;
        IRepository<DailyCashFlow, Guid> _dailyCashFlowRepository;
        IRepository<Expense, Guid> _expenseRepository;
        IRepository<Employee, Guid> _employeeRepository;
        IRepository<ExpenseType, Guid> _expenseTypeRepository;
        public AppServiceUtils(
            IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository,
            IRepository<DailyCashFlow, Guid> dailyCashFlowRepository,
            IRepository<Expense, Guid> expenseRepository,
            IRepository<Employee, Guid> employeeRepository,
            IRepository<ExpenseType, Guid> expenseTypeRepository)
        {
            _monthlyCashFlowRepository = monthlyCashFlowRepository;
            _dailyCashFlowRepository = dailyCashFlowRepository;
            _expenseRepository = expenseRepository;
            _employeeRepository = employeeRepository;
            _expenseTypeRepository = expenseTypeRepository;
        }
        public async Task<MonthlyCashFlow> FindOrCreateMonthlyCashFlow(Guid shopId, DateTime date)
        {
            var monthlyCashFlow = await _monthlyCashFlowRepository.FindAsync(x => x.Date.Month == date.Month && date.Year == x.Date.Year && x.ShopId == shopId);

            if (monthlyCashFlow == null)
            {
                monthlyCashFlow = (await _monthlyCashFlowRepository.InsertAsync(new MonthlyCashFlow
                {
                    Date = date,
                    ShopId = shopId
                }));
                await CreateMonthlySalaryExpenses(monthlyCashFlow);
            }

            return monthlyCashFlow;
        }

        public async Task<DailyCashFlow> FindOrCreateDailyCashFlow(Guid shopId, DateTime date, MonthlyCashFlow monthlyCashFlow)
        {
            var dailyCashFlow = await _dailyCashFlowRepository.FindAsync(x => x.Date.Date == date.Date && x.ShopId == shopId);

            if (dailyCashFlow == null)
            {
                dailyCashFlow = (await _dailyCashFlowRepository.InsertAsync(new DailyCashFlow
                {
                    Date = date,
                    ShopId = shopId,
                    MonthlyCashFlow = monthlyCashFlow
                }));
            }

            return dailyCashFlow;
        }

        private async Task CreateMonthlySalaryExpenses(MonthlyCashFlow monthlyCashFlow)
        {
            var expenseTypeId = (await _expenseTypeRepository.FindAsync(x => x.ExpensePeriod == ExpensePeriod.Mjesecni && x.ExpenseCategory == ExpenseCategory.Plata)).Id;
            var employeesExpenses =
                (await _employeeRepository.GetListAsync())
                .Where(x => x.Active && x.ShopId == monthlyCashFlow.ShopId && x.SalaryType == SalaryType.Mjesecno)
                .Select(x =>
                    new Expense
                    {
                        EmployeeId = x.Id,
                        Amount = x.SalaryAmount,
                        ShopId = x.ShopId.HasValue ? (Guid)x.ShopId : EmployeeWithoutShopId(x),
                        Date = monthlyCashFlow.Date,
                        MonthlyCashFlow = monthlyCashFlow,
                        ExpenseTypeId = expenseTypeId
                    });

            await _expenseRepository.InsertManyAsync(employeesExpenses, true);
        }

        private Guid EmployeeWithoutShopId(Employee x)
        {
            throw new Exception("Employee without ShopId assigned");
        }
    }
}