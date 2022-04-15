using System;
using System.Threading.Tasks;
using Tulumba.Application.Contracts.Dtos.RecurringExpense;
using Tulumba.Application.Contracts.Interfaces;
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
    private readonly IRepository<RecurringExpense, Guid> _repository;

    public RecurringExpenseService(IRepository<RecurringExpense, Guid> repository)
        : base(repository)
    {
        _repository = repository;
        GetPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        GetListPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        CreatePolicyName = TulumbaPermissions.RecurringExpenses.Create;
        UpdatePolicyName = TulumbaPermissions.RecurringExpenses.Edit;
        DeletePolicyName = TulumbaPermissions.RecurringExpenses.Delete;
    }

    public override async Task<RecurringExpenseDto> UpdateAsync(Guid id, CreateUpdateRecurringExpenseDto input)
    {
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