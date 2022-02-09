using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.Employee
{
    public class Employee : AuditedAggregateRoot<Guid> // Radnik
    {
        public string Name { get; set; }
        public SalaryType SalaryType { get; set; }
        public decimal SalaryAmount { get; set; }
        public bool Active { get; set; }
        public Guid? ShopId { get; set; }
        public Shop.Shop Shop { get; set; }
    }
}