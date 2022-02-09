using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.Expense
{
    public class ExpenseDto : AuditedEntityDto<Guid>
    {
        public Guid ExpenseTypeId { get; set; }

        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public Guid? MonthlyCashFlowId { get; set; }
        public Guid? DailyCashFlowId { get; set; }
        public decimal Amount {get; set;}
        public Guid? EmployeeId { get; set; }
    }
}