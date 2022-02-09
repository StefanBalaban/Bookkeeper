using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.DailyEarning
{
    public class DailyEarning : AuditedAggregateRoot<Guid> // Dnevna Zarada
    {
        public DateTime Date { get; set; }
        public Shop.Shop Shop { get; set; }
        public Guid ShopId { get; set; }
        public decimal EarningAmount { get; set; }
        public Guid? DailyCashFlowId { get; set; } // DnevniPromet
        public DailyCashFlow.DailyCashFlow DailyCashFlow { get; set; } 
    }
}