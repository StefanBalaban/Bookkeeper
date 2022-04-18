using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.MonthlyCashFlow
{
    public class MonthlyCashFlowDto : AuditedEntityDto<Guid>
    {
        public DateTime Date { get; set; }
        public Guid ShopId { get; set; }
        public decimal Sum { get; set; }
    }
}