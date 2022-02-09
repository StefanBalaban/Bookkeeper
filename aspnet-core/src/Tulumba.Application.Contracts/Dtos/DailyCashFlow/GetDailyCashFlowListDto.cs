using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.DailyCashFlow
{
    public class GetDailyCashFlowListDto : PagedAndSortedResultRequestDto
    {
        public DateTime? DateGTE { get; set; }
        public DateTime? DateLTE { get; set; }

        public Guid? ShopId { get; set; }
    }
}