using System;
using Tulumba.Application.Contracts.Dtos.ExpenseType;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.ExpenseType;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class ExpenseTypeAppService :
        CrudAppService<
            ExpenseType, //The Book entity
            ExpenseTypeDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateExpenseTypeDto>, //Used to create/update a book
        IExpenseTypeAppService //implement the IBookAppService
    {
        public ExpenseTypeAppService(IRepository<ExpenseType, Guid> repository)
            : base(repository)
        {
            GetPolicyName = TulumbaPermissions.ExpenseTypes.Default;
            GetListPolicyName = TulumbaPermissions.ExpenseTypes.Default;
            CreatePolicyName = TulumbaPermissions.ExpenseTypes.Create;
            UpdatePolicyName = TulumbaPermissions.ExpenseTypes.Edit;
            DeletePolicyName = TulumbaPermissions.ExpenseTypes.Delete;
        }
    }
}