using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tulumba.Application.Contracts.Dtos.MonthlyCashFlow;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Application.Extensions;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.DailyEarning;
using Tulumba.Entities.Expense;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class MonthlyCashFlowAppService :
    CrudAppService<
        MonthlyCashFlow, //The Book entity
        MonthlyCashFlowDto, //Used to show books
        Guid, //Primary key of the book entity
        GetMonthlyCashFlowListDto, //Used for paging/sorting
        CreateUpdateMonthlyCashFlowDto>, //Used to create/update a book
    IMonthlyCashFlowAppService //implement the IBookAppService
{
    private readonly AppServiceUtils _appServiceUtils;
    private readonly IRepository<MonthlyCashFlow, Guid> _monthlyCashFlowRepository;

    public MonthlyCashFlowAppService(
        IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository,
        AppServiceUtils appServiceUtils)
        : base(monthlyCashFlowRepository)
    {
        GetPolicyName = TulumbaPermissions.MonthlyCashFlows.Default;
        GetListPolicyName = TulumbaPermissions.MonthlyCashFlows.Get;
        CreatePolicyName = TulumbaPermissions.MonthlyCashFlows.Create;
        UpdatePolicyName = TulumbaPermissions.MonthlyCashFlows.Edit;
        DeletePolicyName = TulumbaPermissions.MonthlyCashFlows.Delete;

        _appServiceUtils = appServiceUtils;
        _monthlyCashFlowRepository = monthlyCashFlowRepository;
    }

    public override async Task<MonthlyCashFlowDto> CreateAsync(CreateUpdateMonthlyCashFlowDto input)
    {
        var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);

        return ObjectMapper.Map<MonthlyCashFlow, MonthlyCashFlowDto>(monthlyCashFlow);
    }

    [Authorize(TulumbaPermissions.MonthlyCashFlows.Get)]
    public override async Task<PagedResultDto<MonthlyCashFlowDto>> GetListAsync(GetMonthlyCashFlowListDto input)
    {
        var monthlyCashFlows = new List<MonthlyCashFlowDto>();

        if (string.IsNullOrWhiteSpace(input.Sorting))
        {
            input.Sorting = nameof(MonthlyCashFlow.Date) + " DESC";
        }

        var query = _monthlyCashFlowRepository
            .Where(x =>
                (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
                (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date));

        var sorting = input.Sorting.Split(' ');
        query = sorting[1].ToUpper().Equals("DESC")
                ? query.OrderByDescending(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)

                : query.OrderBy(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
            ;


        var totalCount = await _monthlyCashFlowRepository.CountAsync(x =>
            (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
            (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
            (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date));


        monthlyCashFlows = await query
            .Select(x => new MonthlyCashFlowDto
            {
                Id = x.Id,
                ShopId = x.ShopId,
                Date = x.Date,
                Sum =  
                    x.DailyCashFlows.Sum(y => y.DailyEarnings.Sum(z => z.EarningAmount)) 
                    - x.DailyCashFlows.Sum(y => y.Expenses.Sum(z => z.Amount))
                    - x.Expenses.Sum(y => y.Amount)
            })
            .ToListAsync();
        
        return new PagedResultDto<MonthlyCashFlowDto>(
            totalCount,
            monthlyCashFlows
        );
    }
}