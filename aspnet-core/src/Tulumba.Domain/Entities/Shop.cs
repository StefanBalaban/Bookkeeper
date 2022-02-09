using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.Shop
{
    public class Shop : AuditedAggregateRoot<Guid> // Radnja
    {
        public string Name { get; set; } // Naziv

        public string Address { get; set; } // Adresa
    }
}