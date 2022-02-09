using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.Expense
{
    public class Expense : AuditedAggregateRoot<Guid> // Trošak
    {
        public Guid ExpenseTypeId { get; set; }

        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public Guid? MonthlyCashFlowId { get; set; }
        public Guid? DailyCashFlowId { get; set; }
        public Guid? EmployeeId {get; set;}
        public decimal Amount { get; set; }

        public ExpenseType.ExpenseType ExpenseType { get; set; }

        public MonthlyCashFlow.MonthlyCashFlow MonthlyCashFlow { get; set; }
        public DailyCashFlow.DailyCashFlow DailyCashFlow { get; set; }

        public Shop.Shop Shop { get; set; }
        public Employee.Employee Employee { get; set; }
    }
}