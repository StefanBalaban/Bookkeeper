using System;
using Tulumba.Application.Contracts.Dtos.Shop;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IShopAppService :
        ICrudAppService< //Defines CRUD methods
            ShopDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateShopDto> //Used to create/update a book
    {
    }
}