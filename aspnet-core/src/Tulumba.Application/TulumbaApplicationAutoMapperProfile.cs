using AutoMapper;
using Tulumba.Application.Contracts.Dtos.DailyCashFlow;
using Tulumba.Application.Contracts.Dtos.DailyEarning;
using Tulumba.Application.Contracts.Dtos.Employee;
using Tulumba.Application.Contracts.Dtos.Expense;
using Tulumba.Application.Contracts.Dtos.ExpenseType;
using Tulumba.Application.Contracts.Dtos.MonthlyCashFlow;
using Tulumba.Application.Contracts.Dtos.RecurringExpense;
using Tulumba.Application.Contracts.Dtos.Shop;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.DailyEarning;
using Tulumba.Entities.Employee;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Entities.RecurringExpense;
using Tulumba.Entities.Shop;

namespace Tulumba
{
    public class TulumbaApplicationAutoMapperProfile : Profile
    {
        public TulumbaApplicationAutoMapperProfile()
        {
            CreateMap<Shop, ShopDto>();
            CreateMap<CreateUpdateShopDto, Shop>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateUpdateEmployeeDto, Employee>();
            CreateMap<DailyEarning, DailyEarningDto>();
            CreateMap<CreateUpdateDailyEarningDto, DailyEarning>();
            CreateMap<MonthlyCashFlow, MonthlyCashFlowDto>();
            CreateMap<CreateUpdateMonthlyCashFlowDto, MonthlyCashFlow>();
            CreateMap<ExpenseType, ExpenseTypeDto>();
            CreateMap<CreateUpdateExpenseTypeDto, ExpenseType>();
            CreateMap<Expense, ExpenseDto>();
            CreateMap<CreateUpdateExpenseDto, Expense>();
            CreateMap<DailyCashFlow, DailyCashFlowDto>();
            CreateMap<CreateUpdateDailyCashFlowDto, DailyCashFlow>();
            CreateMap<CreateUpdateRecurringExpenseDto, RecurringExpense>();
            CreateMap<RecurringExpense, RecurringExpenseDto>();
        }
    }
}