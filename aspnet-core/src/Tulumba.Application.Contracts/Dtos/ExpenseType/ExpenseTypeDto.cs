using System;
using Tulumba.Entities.ExpenseType;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.ExpenseType
{
    public class ExpenseTypeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public ExpenseCategory ExpenseCategory { get; set; }
        public ExpensePeriod ExpensePeriod { get; set; }
    }
}