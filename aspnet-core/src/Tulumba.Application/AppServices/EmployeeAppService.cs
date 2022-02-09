using System;
using Tulumba.Application.Contracts.Dtos.Employee;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.Employee;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class EmployeeAppService :
        CrudAppService<
            Employee, //The Book entity
            EmployeeDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateEmployeeDto>, //Used to create/update a book
        IEmployeeAppService //implement the IBookAppService
    {
        public EmployeeAppService(IRepository<Employee, Guid> repository)
            : base(repository)
        {
            GetPolicyName = TulumbaPermissions.Employees.Default;
            GetListPolicyName = TulumbaPermissions.Employees.Default;
            CreatePolicyName = TulumbaPermissions.Employees.Create;
            UpdatePolicyName = TulumbaPermissions.Employees.Edit;
            DeletePolicyName = TulumbaPermissions.Employees.Delete;
        }
    }
}