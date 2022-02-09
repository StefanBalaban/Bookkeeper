using System;
using System.ComponentModel.DataAnnotations;

namespace Tulumba.Application.Contracts.Dtos.MonthlyCashFlow
{
    public class CreateUpdateMonthlyCashFlowDto
    {
        [Required] public DateTime Date { get; set; }

        [Required] public Guid ShopId { get; set; }
    }
}