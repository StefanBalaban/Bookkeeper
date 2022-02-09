using System;
using Tulumba.Application.Contracts.Dtos.Expense;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IExpenseAppService :
        ICrudAppService< //Defines CRUD methods
            ExpenseDto, //Used to show books
            Guid, //Primary key of the book entity
            GetExpenseListDto, //Used for paging/sorting
            CreateUpdateExpenseDto> //Used to create/update a book
    {
    }
}