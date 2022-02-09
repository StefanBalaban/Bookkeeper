using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.DailyCashFlow
{
    public class DailyCashFlowDto : AuditedEntityDto<Guid>
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        
        public Guid? MonthlyCashFlowId { get; set; }
    }
}