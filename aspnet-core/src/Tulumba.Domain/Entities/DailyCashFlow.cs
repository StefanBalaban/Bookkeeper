using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.DailyCashFlow
{
    public class DailyCashFlow : AuditedAggregateRoot<Guid> // Dnevni Promet
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public Guid? MonthlyCashFlowId { get; set; }
        public MonthlyCashFlow.MonthlyCashFlow MonthlyCashFlow { get; set; }
        public List<DailyEarning.DailyEarning> DailyEarnings { get; set; }
        public List<Expense.Expense> Expenses { get; set; }
        public Shop.Shop Shop { get; set; }
    }
}