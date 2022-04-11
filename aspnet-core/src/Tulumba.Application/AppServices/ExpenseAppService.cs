using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Tulumba.Application.Contracts.Dtos.Expense;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Application.Extensions;
using Tulumba.Entities.Expense;
using Tulumba.Entities.ExpenseType;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class ExpenseAppService :
    CrudAppService<
        Expense, //The Book entity
        ExpenseDto, //Used to show books
        Guid, //Primary key of the book entity
        GetExpenseListDto, //Used for paging/sorting
        CreateUpdateExpenseDto>, //Used to create/update a book
    IExpenseAppService //implement the IBookAppService
{
    private readonly AppServiceUtils _appServiceUtils;
    private readonly IRepository<Expense, Guid> _expenseRepository;
    private readonly IRepository<ExpenseType, Guid> _expenseTypeRepository;

    public ExpenseAppService(
        IRepository<Expense, Guid> expenseRepository,
        IRepository<ExpenseType, Guid> expenseTypeRepository,
        AppServiceUtils appServiceUtils)
        : base(expenseRepository)
    {
        GetPolicyName = TulumbaPermissions.Expenses.Default;
        GetListPolicyName = TulumbaPermissions.Expenses.Default;
        CreatePolicyName = TulumbaPermissions.Expenses.Create;
        UpdatePolicyName = TulumbaPermissions.Expenses.Edit;
        DeletePolicyName = TulumbaPermissions.Expenses.Delete;

        _expenseRepository = expenseRepository;
        _expenseTypeRepository = expenseTypeRepository;
        _appServiceUtils = appServiceUtils;
    }

    public override async Task<ExpenseDto> CreateAsync(CreateUpdateExpenseDto input)
    {
        var expenseType = await _expenseTypeRepository.FindAsync(x => x.Id == input.ExpenseTypeId);
        if (expenseType == null)
        {
            throw new Exception("expenseType not found");
        }

        var dailyExpenseType = expenseType.ExpensePeriod == ExpensePeriod.Dnevni ? true : false;

        var entity = new Expense
        {
            ShopId = input.ShopId,
            Date = input.Date,
            ExpenseTypeId = input.ExpenseTypeId,
            Amount = input.Amount,
            EmployeeId = input.EmployeeId
        };

        var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);

        if (dailyExpenseType)
        {
            var dailyCashFlow =
                await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);

            entity.DailyCashFlow = dailyCashFlow;
        }
        else
        {
            entity.MonthlyCashFlow = monthlyCashFlow;
        }

        return ObjectMapper.Map<Expense, ExpenseDto>(await _expenseRepository.InsertAsync(entity));
    }

    public override async Task<ExpenseDto> UpdateAsync(Guid id, CreateUpdateExpenseDto input)
    {
        var entity = await _expenseRepository.FindAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        var expenseTypeChanged = entity.ExpenseTypeId != input.ExpenseTypeId;
        var dateChanged = entity.Date.Date != input.Date.Date;
        var shopChanged = entity.ShopId != input.ShopId;

        entity.ExpenseTypeId = input.ExpenseTypeId;
        entity.Date = input.Date;
        entity.Amount = input.Amount;
        entity.ShopId = input.ShopId;
        entity.EmployeeId = input.EmployeeId;


        if (expenseTypeChanged || dateChanged || shopChanged)
        {
            var dailyExpenseType =
                (await _expenseTypeRepository.FindAsync(entity.ExpenseTypeId)).ExpensePeriod == ExpensePeriod.Dnevni
                    ? true
                    : false;

            var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);

            if (dailyExpenseType)
            {
                var dailyCashFlow =
                    await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);
                entity.MonthlyCashFlow = null;
                entity.DailyCashFlow = dailyCashFlow;
            }
            else
            {
                entity.DailyCashFlow = null;
                entity.MonthlyCashFlow = monthlyCashFlow;
            }
        }

        return ObjectMapper.Map<Expense, ExpenseDto>(await _expenseRepository.UpdateAsync(entity));
    }

    [Authorize(TulumbaPermissions.Expenses.Get)]
    public override async Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpenseListDto input)
    {
        var expenses = new List<Expense>();

        if (string.IsNullOrWhiteSpace(input.Sorting))
        {
            input.Sorting = nameof(Expense.Date) + " DESC";
        }

        if (
            !input.ExpenseTypeId.HasValue &&
            !input.ShopId.HasValue &&
            !input.Amount.HasValue &&
            !input.EmployeeId.HasValue &&
            !input.DateGTE.HasValue &&
            !input.DateLTE.HasValue &&
            !input.ExpenseCategory.HasValue
        )
        {
            expenses = await _expenseRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        }
        else
        {
            var query = _expenseRepository.Where(x =>
                    (!input.ExpenseTypeId.HasValue || x.ExpenseTypeId == input.ExpenseTypeId) &&
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.Amount.HasValue || x.Amount == input.Amount) &&
                    (!input.EmployeeId.HasValue || x.EmployeeId == input.EmployeeId) &&
                    (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
                    (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date) &&
                    (!input.ExpenseCategory.HasValue || x.ExpenseType.ExpenseCategory == input.ExpenseCategory));

            var sorting = input.Sorting.Split(' ');
            Logger.LogInformation("Sort field: " + sorting[0]);
            Logger.LogInformation("Sort direction: " + sorting[1]);
            expenses = sorting[1].ToUpper().Equals("DESC")
                ? query.OrderByDescending(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList()
                : query.OrderBy(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();
        }

        var totalCount = await _expenseRepository.CountAsync(x =>
            (!input.ExpenseTypeId.HasValue || x.ExpenseTypeId == input.ExpenseTypeId) &&
            (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
            (!input.Amount.HasValue || x.Amount == input.Amount) &&
            (!input.EmployeeId.HasValue || x.EmployeeId == input.EmployeeId) &&
            (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
            (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date) &&
            (!input.ExpenseCategory.HasValue || x.ExpenseType.ExpenseCategory == input.ExpenseCategory));

        return new PagedResultDto<ExpenseDto>(
            totalCount,
            ObjectMapper.Map<List<Expense>, List<ExpenseDto>>(expenses)
        );
    }
}