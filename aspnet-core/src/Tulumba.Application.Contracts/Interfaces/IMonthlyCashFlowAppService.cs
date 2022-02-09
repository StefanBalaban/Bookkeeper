using System;
using Tulumba.Application.Contracts.Dtos.MonthlyCashFlow;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IMonthlyCashFlowAppService :
        ICrudAppService< //Defines CRUD methods
            MonthlyCashFlowDto, //Used to show books
            Guid, //Primary key of the book entity
            GetMonthlyCashFlowListDto, //Used for paging/sorting
            CreateUpdateMonthlyCashFlowDto> //Used to create/update a book
    {
    }
}