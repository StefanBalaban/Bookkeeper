using System;
using System.ComponentModel.DataAnnotations;

namespace Tulumba.Application.Contracts.Dtos.Expense
{
    public class CreateUpdateExpenseDto
    {
        [Required] public Guid ExpenseTypeId { get; set; }

        [Required] public DateTime Date { get; set; }

        [Required] public Guid ShopId { get; set; }

        public Guid? EmployeeId { get; set; }

        public decimal Amount { get; set; }
    }
}