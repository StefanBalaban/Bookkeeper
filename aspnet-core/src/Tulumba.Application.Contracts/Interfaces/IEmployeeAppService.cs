using System;
using Tulumba.Application.Contracts.Dtos.Employee;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tulumba.Application.Contracts.Interfaces
{
    public interface IEmployeeAppService :
        ICrudAppService< //Defines CRUD methods
            EmployeeDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateEmployeeDto> //Used to create/update a book
    {
    }
}