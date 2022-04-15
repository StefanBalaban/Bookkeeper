using System;

namespace Tulumba.Application.Contracts.Dtos.RecurringExpense;

public class CreateUpdateRecurringExpenseDto
{
    public Guid ExpenseTypeId { get; set; }
    public Guid ShopId { get; set; }
    public decimal Amount { get; set; }
    public bool Active { get; set; }
}