using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.MonthlyCashFlow
{
    public class GetMonthlyCashFlowListDto : PagedAndSortedResultRequestDto
    {
        public DateTime? DateGTE { get; set; }
        public DateTime? DateLTE { get; set; }

        public Guid? ShopId { get; set; }
    }
}