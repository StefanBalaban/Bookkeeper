using System;
using Tulumba.Application.Contracts.Dtos.RecurringExpense;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces;

public interface IRecurringExpenseService : 
    ICrudAppService<
        RecurringExpenseDto, 
        Guid, 
        PagedAndSortedResultRequestDto,
        CreateUpdateRecurringExpenseDto>
{
}