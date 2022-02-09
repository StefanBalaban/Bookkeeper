using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tulumba.Entities.ExpenseType
{
    public class ExpenseType : AuditedAggregateRoot<Guid> // Tip Troška
    {
        public string Name { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public ExpensePeriod ExpensePeriod { get; set; }
    }
}