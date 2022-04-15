using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.RecurringExpense;

public class RecurringExpense: AuditedAggregateRoot<Guid>
{
    public Guid ExpenseTypeId { get; set; }
    public Guid ShopId { get; set; }
    public decimal Amount { get; set; }

    public bool Active { get; set; }
    
    public ExpenseType.ExpenseType ExpenseType { get; set; }
    public Shop.Shop Shop { get; set; }
}