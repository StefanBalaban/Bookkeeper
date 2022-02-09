using System;
using System.ComponentModel.DataAnnotations;
using Tulumba.Entities.Employee;

namespace Tulumba.Application.Contracts.Dtos.Employee
{
    public class CreateUpdateEmployeeDto
    {
        [Required] public string Name { get; set; }

        [Required] public SalaryType SalaryType { get; set; }
        public decimal SalaryAmount { get; set; }
        public bool Active { get; set; }
        [Required] public Guid ShopId { get; set; }
    }
}