using System.ComponentModel.DataAnnotations;

namespace Tulumba.Application.Contracts.Dtos.Shop
{
    public class CreateUpdateShopDto
    {
        [Required] public string Name { get; set; }

        public string Address { get; set; }
    }
}