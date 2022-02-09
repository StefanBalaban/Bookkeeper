using System;
using System.ComponentModel.DataAnnotations;

namespace Tulumba.Application.Contracts.Dtos.DailyCashFlow
{
    public class CreateUpdateDailyCashFlowDto
    {
        [Required] public DateTime Date { get; set; }

        [Required] public Guid ShopId { get; set; }
    }
}