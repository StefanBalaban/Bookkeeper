using System;
using Volo.Abp.Application.Dtos;

namespace Tulumba.Application.Contracts.Dtos.Shop
{
    public class ShopDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}