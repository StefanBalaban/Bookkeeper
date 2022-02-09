using System;
using Tulumba.Application.Contracts.Dtos.DailyCashFlow;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IDailyCashFlowAppService :
        ICrudAppService< //Defines CRUD methods
            DailyCashFlowDto, //Used to show books
            Guid, //Primary key of the book entity
            GetDailyCashFlowListDto, //Used for paging/sorting
            CreateUpdateDailyCashFlowDto> //Used to create/update a book
    {
    }
}