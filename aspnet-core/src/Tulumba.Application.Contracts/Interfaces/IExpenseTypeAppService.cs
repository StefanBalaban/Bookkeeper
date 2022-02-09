using System;
using Tulumba.Application.Contracts.Dtos.ExpenseType;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IExpenseTypeAppService :
        ICrudAppService< //Defines CRUD methods
            ExpenseTypeDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateExpenseTypeDto> //Used to create/update a book
    {
    }
}