using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.RecurringExpense;

public class RecurringExpenseDto: AuditedEntityDto<Guid>
{
    public Guid ExpenseTypeId { get; set; }
    public Guid ShopId { get; set; }
    public decimal Amount { get; set; }
}