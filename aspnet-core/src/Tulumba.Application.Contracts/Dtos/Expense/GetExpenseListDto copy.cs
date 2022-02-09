using System;
using System.ComponentModel.DataAnnotations;
using Tulumba.Entities.ExpenseType;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.Expense
{
    public class GetExpenseListDto : PagedAndSortedResultRequestDto
    {
        public Guid? ExpenseTypeId { get; set; }

        public DateTime? DateGTE { get; set; }
        public DateTime? DateLTE { get; set; }

        public Guid? ShopId { get; set; }

        public Guid? EmployeeId { get; set; }

        public decimal? Amount { get; set; }
        public ExpenseCategory? ExpenseCategory { get; set; }
    }
}