using System;
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
    public RecurringExpenseService(IRepository<RecurringExpense, Guid> repository) 
        : base(repository)
    {
        GetPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        GetListPolicyName = TulumbaPermissions.RecurringExpenses.Default;
        CreatePolicyName = TulumbaPermissions.RecurringExpenses.Create;
        UpdatePolicyName = TulumbaPermissions.RecurringExpenses.Edit;
        DeletePolicyName = TulumbaPermissions.RecurringExpenses.Delete;
    }
}