using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.MonthlyCashFlow
{
    public class MonthlyCashFlow : AuditedAggregateRoot<Guid> // Mjesečni Promet
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public Shop.Shop Shop { get; set; }
    }
}