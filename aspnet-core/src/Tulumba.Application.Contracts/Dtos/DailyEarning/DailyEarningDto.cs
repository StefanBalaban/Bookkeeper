using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.DailyEarning
{
    public class DailyEarningDto : AuditedEntityDto<Guid>
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public decimal EarningAmount { get; set; }
        public Guid? DailyCashFlowId { get; set; }
    }
}