using System;
using Tulumba.Application.Contracts.Dtos.DailyEarning;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IDailyEarningAppService :
        ICrudAppService< //Defines CRUD methods
            DailyEarningDto, //Used to show books
            Guid, //Primary key of the book entity
            GetDailyEarningListDto, //Used for paging/sorting
            CreateUpdateDailyEarningDto> //Used to create/update a book
    {
    }
}