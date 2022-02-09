using System;
using System.ComponentModel.DataAnnotations;

namespace Tulumba.Application.Contracts.Dtos.DailyEarning
{
    public class CreateUpdateDailyEarningDto
    {
        [Required] public DateTime Date { get; set; }

        [Required] public Guid ShopId { get; set; }

        [Required] public decimal EarningAmount { get; set; }
    }
}