using System.ComponentModel.DataAnnotations;
using Tulumba.Entities.ExpenseType;

namespace Tulumba.Application.Contracts.Dtos.ExpenseType
{
    public class CreateUpdateExpenseTypeDto
    {
        [Required] public string Name { get; set; }

        [Required] public ExpenseCategory ExpenseCategory { get; set; }

        [Required] public ExpensePeriod ExpensePeriod { get; set; }
    }
}