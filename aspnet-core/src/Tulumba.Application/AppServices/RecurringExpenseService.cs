using System;
using System.Threading.Tasks;
using Tulumba.Application.Contracts.Dtos.RecurringExpense;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.ExpenseType;
using Tulumba.Entities.RecurringExpense;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class RecurringExpenseService :
    CrudAppService<
        RecurringExpense,
        RecurringExpenseDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateRecurringExpenseDto
    >, IRecurringExpenseService
{
    private readonly IRepository<ExpenseType, Guid> _expenseTypeRepository;
    private readonly IRepository<RecurringExpense, Guid> _repository;

    public RecurringExpenseService(IRepository<RecurringExpense, Guid> repository,
        IRepository<ExpenseType, Guid> expenseTypeRepository)
        : base(repository)
    {
        _repository = repository;
        _expenseTypeRepository = expenseTypeRepository;
        GetPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        GetListPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        CreatePolicyName = TulumbaPermissions.RecurringExpenses.Create;
        UpdatePolicyName = TulumbaPermissions.RecurringExpenses.Edit;
        DeletePolicyName = TulumbaPermissions.RecurringExpenses.Delete;
    }

    public override async Task<RecurringExpenseDto> CreateAsync(CreateUpdateRecurringExpenseDto input)
    {
        if (input.Amount < 0)
        {
            throw new Exception("Amount is less than 0.");
        }

        var expenseType = await _expenseTypeRepository.FirstOrDefaultAsync(x => x.Id == input.ExpenseTypeId);

        if (expenseType == null || expenseType.ExpensePeriod != ExpensePeriod.Mjesecni)
        {
            throw new Exception("Invalid Expense Type");
        }

        if (await _repository.AnyAsync(x => x.ShopId == input.ShopId && x.ExpenseTypeId == input.ExpenseTypeId))
        {
            throw new Exception("Recurring Expense already exists.");
        }

        return await base.CreateAsync(input);
    }

    public override async Task<RecurringExpenseDto> UpdateAsync(Guid id, CreateUpdateRecurringExpenseDto input)
    {
        if (input.Amount < 0)
        {
            throw new Exception("Amount is less than 0.");
        }

        var entity = await _repository.FindAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("Entity not found");
        }

        entity.Active = input.Active;
        entity.Amount = input.Amount;

        return ObjectMapper.Map<RecurringExpense, RecurringExpenseDto>(await _repository.UpdateAsync(entity));
    }
}