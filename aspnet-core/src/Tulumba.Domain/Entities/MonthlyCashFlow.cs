using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.MonthlyCashFlow
{
    public class MonthlyCashFlow : AuditedAggregateRoot<Guid> // Mjesečni Promet
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public Shop.Shop Shop { get; set; }
        public List<DailyCashFlow.DailyCashFlow> DailyCashFlows { get; set; }
        public List<Expense.Expense> Expenses { get; set; }
    }
}