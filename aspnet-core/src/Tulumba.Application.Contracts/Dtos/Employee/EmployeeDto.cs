using System;
using Tulumba.Entities.Employee;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.Employee
{
    public class EmployeeDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public SalaryType SalaryType { get; set; }
        public decimal SalaryAmount { get; set; }
        public bool Active { get; set; }
        public Guid ShopId { get; set; }
    }
}