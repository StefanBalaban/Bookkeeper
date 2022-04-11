using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Tulumba.Application.Contracts.Dtos.DailyCashFlow;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Application.Extensions;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class DailyCashFlowAppService :
    CrudAppService<
        DailyCashFlow, //The Book entity
        DailyCashFlowDto, //Used to show books
        Guid, //Primary key of the book entity
        GetDailyCashFlowListDto, //Used for paging/sorting
        CreateUpdateDailyCashFlowDto>, //Used to create/update a book
    IDailyCashFlowAppService //implement the IBookAppService
{
    private readonly AppServiceUtils _appServiceUtils;
    private readonly IRepository<DailyCashFlow, Guid> _dailyCashFlowRepository;

    public DailyCashFlowAppService(
        IRepository<DailyCashFlow, Guid> dailyCashFlowRepository,
        AppServiceUtils appServiceUtils)
        : base(dailyCashFlowRepository)
    {
        GetPolicyName = TulumbaPermissions.DailyCashFlows.Default;
        GetListPolicyName = TulumbaPermissions.DailyCashFlows.Get;
        CreatePolicyName = TulumbaPermissions.DailyCashFlows.Create;
        UpdatePolicyName = TulumbaPermissions.DailyCashFlows.Edit;
        DeletePolicyName = TulumbaPermissions.DailyCashFlows.Delete;

        _appServiceUtils = appServiceUtils;
        _dailyCashFlowRepository = dailyCashFlowRepository;
    }

    public override async Task<DailyCashFlowDto> CreateAsync(CreateUpdateDailyCashFlowDto input)
    {
        var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);
        var dailyCashFlow = await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);

        return ObjectMapper.Map<DailyCashFlow, DailyCashFlowDto>(dailyCashFlow);
    }

    [Authorize(TulumbaPermissions.DailyCashFlows.Get)]
    public override async Task<PagedResultDto<DailyCashFlowDto>> GetListAsync(GetDailyCashFlowListDto input)
    {
        var dailyCashFlows = new List<DailyCashFlow>();
        
        if (string.IsNullOrWhiteSpace(input.Sorting))
        {
            input.Sorting = nameof(DailyCashFlow.Date) + " DESC";
        }
        
        if (
            !input.ShopId.HasValue &&
            !input.DateGTE.HasValue &&
            !input.DateLTE.HasValue
        )
        {

            dailyCashFlows =
                await _dailyCashFlowRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        }
        else
        {

            var query = _dailyCashFlowRepository.Where(x =>
                (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                (!input.DateGTE.HasValue || x.Date.Date.Date <= input.DateGTE.Value.Date) &&
                (!input.DateLTE.HasValue || x.Date.Date.Date >= input.DateLTE.Value.Date));
            
            var sorting = input.Sorting.Split(' ');
            dailyCashFlows = sorting[1].ToUpper().Equals("DESC")
                ? query.OrderByDescending(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList()
                : query.OrderBy(sorting[0])
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();
        }

        var totalCount = await _dailyCashFlowRepository.CountAsync(x =>
            (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
            (!input.DateGTE.HasValue || x.Date.Date.Date <= input.DateGTE.Value.Date) &&
            (!input.DateLTE.HasValue || x.Date.Date.Date >= input.DateLTE.Value.Date));

        return new PagedResultDto<DailyCashFlowDto>(
            totalCount,
            ObjectMapper.Map<List<DailyCashFlow>, List<DailyCashFlowDto>>(dailyCashFlows)
        );
    }
}