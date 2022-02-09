using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tulumba.Application.Contracts.Dtos.DailyCashFlow;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class DailyCashFlowAppService :
        CrudAppService<
            DailyCashFlow, //The Book entity
            DailyCashFlowDto, //Used to show books
            Guid, //Primary key of the book entity
            GetDailyCashFlowListDto, //Used for paging/sorting
            CreateUpdateDailyCashFlowDto>, //Used to create/update a book
        IDailyCashFlowAppService //implement the IBookAppService
    {
        AppServiceUtils _appServiceUtils;
        IRepository<DailyCashFlow, Guid> _dailyCashFlowRepository;
        public DailyCashFlowAppService(
            IRepository<DailyCashFlow, Guid> dailyCashFlowRepository,
            IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository,
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

        [Authorize(Permissions.TulumbaPermissions.DailyCashFlows.Get)]
        public override async Task<PagedResultDto<DailyCashFlowDto>> GetListAsync(GetDailyCashFlowListDto input)
        {
            var dailyCashFlows = new List<DailyCashFlow>();

            if (
                !input.ShopId.HasValue &&
                !input.DateGTE.HasValue &&
                !input.DateLTE.HasValue
                )
            {
                if (string.IsNullOrWhiteSpace(input.Sorting))
                {
                    input.Sorting = nameof(DailyCashFlow.Date);
                }
                dailyCashFlows = await _dailyCashFlowRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            }
            else
            {
                dailyCashFlows = (await _dailyCashFlowRepository.GetListAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE)))
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount).ToList();
            }

            var totalCount = await _dailyCashFlowRepository.CountAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE));

            return new PagedResultDto<DailyCashFlowDto>(
                totalCount,
                ObjectMapper.Map<List<DailyCashFlow>, List<DailyCashFlowDto>>(dailyCashFlows)
            );
        }
    }
}