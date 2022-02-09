using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tulumba.Application.Contracts.Dtos.MonthlyCashFlow;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class MonthlyCashFlowAppService :
        CrudAppService<
            MonthlyCashFlow, //The Book entity
            MonthlyCashFlowDto, //Used to show books
            Guid, //Primary key of the book entity
            GetMonthlyCashFlowListDto, //Used for paging/sorting
            CreateUpdateMonthlyCashFlowDto>, //Used to create/update a book
        IMonthlyCashFlowAppService //implement the IBookAppService
    {
        private AppServiceUtils _appServiceUtils;
        IRepository<MonthlyCashFlow, Guid> _monthlyCashFlowRepository;
        public MonthlyCashFlowAppService(IRepository<MonthlyCashFlow, Guid> monthlyCashFlowRepository, AppServiceUtils appServiceUtils)
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

        [Authorize(Permissions.TulumbaPermissions.MonthlyCashFlows.Get)]
        public override async Task<PagedResultDto<MonthlyCashFlowDto>> GetListAsync(GetMonthlyCashFlowListDto input)
        {
            var monthlyCashFlows = new List<MonthlyCashFlow>();

            if (
                !input.ShopId.HasValue &&
                !input.DateGTE.HasValue &&
                !input.DateLTE.HasValue
                )
            {
                if (string.IsNullOrWhiteSpace(input.Sorting))
                {
                    input.Sorting = nameof(MonthlyCashFlow.Date);
                }
                monthlyCashFlows = await _monthlyCashFlowRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            }
            else
            {
                monthlyCashFlows = (await _monthlyCashFlowRepository.GetListAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE)))
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount).ToList();
            }

            var totalCount = await _monthlyCashFlowRepository.CountAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE));

            return new PagedResultDto<MonthlyCashFlowDto>(
                totalCount,
                ObjectMapper.Map<List<MonthlyCashFlow>, List<MonthlyCashFlowDto>>(monthlyCashFlows)
            );
        }
    }
}