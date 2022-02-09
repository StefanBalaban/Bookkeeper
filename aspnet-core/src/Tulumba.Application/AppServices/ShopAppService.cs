using System;
using Tulumba.Application.Contracts.Dtos.Shop;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.Shop;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class ShopAppService :
        CrudAppService<
            Shop, //The Book entity
            ShopDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateShopDto>, //Used to create/update a book
        IShopAppService //implement the IBookAppService
    {
        public ShopAppService(IRepository<Shop, Guid> repository)
            : base(repository)
        {
            GetPolicyName = TulumbaPermissions.Shops.Default;
            GetListPolicyName = TulumbaPermissions.Shops.Default;
            CreatePolicyName = TulumbaPermissions.Shops.Create;
            UpdatePolicyName = TulumbaPermissions.Shops.Edit;
            DeletePolicyName = TulumbaPermissions.Shops.Delete;
        }
    }
}