using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tulumba.Application.Contracts.Dtos.DailyEarning;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Entities.DailyCashFlow;
using Tulumba.Entities.DailyEarning;
using Tulumba.Entities.MonthlyCashFlow;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices
{
    public class DailyEarningAppService :
        CrudAppService<
            DailyEarning, //The Book entity
            DailyEarningDto, //Used to show books
            Guid, //Primary key of the book entity
            GetDailyEarningListDto, //Used for paging/sorting
            CreateUpdateDailyEarningDto>, //Used to create/update a book
        IDailyEarningAppService //implement the IBookAppService
    {

        private IRepository<DailyEarning, Guid> _dailyEarningRepository;
        AppServiceUtils _appServiceUtils;
        public DailyEarningAppService(IRepository<DailyEarning, Guid> dailyEarningRepository,
            AppServiceUtils appServiceUtils)
            : base(dailyEarningRepository)
        {
            GetPolicyName = TulumbaPermissions.DailyEarnings.Default;
            GetListPolicyName = TulumbaPermissions.DailyEarnings.Get;
            CreatePolicyName = TulumbaPermissions.DailyEarnings.Create;
            UpdatePolicyName = TulumbaPermissions.DailyEarnings.Edit;
            DeletePolicyName = TulumbaPermissions.DailyEarnings.Delete;

            _dailyEarningRepository = dailyEarningRepository;

            _appServiceUtils = appServiceUtils;
        }

        public override async Task<DailyEarningDto> CreateAsync(CreateUpdateDailyEarningDto input)
        {

            var entity = new DailyEarning
            {
                ShopId = input.ShopId,
                Date = input.Date,
                EarningAmount = input.EarningAmount
            };

            var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);
            var dailyCashFlow = await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);

            entity.DailyCashFlow = dailyCashFlow;

            return ObjectMapper.Map<DailyEarning, DailyEarningDto>(await _dailyEarningRepository.InsertAsync(entity));
        }

        public override async Task<DailyEarningDto> UpdateAsync(Guid id, CreateUpdateDailyEarningDto input)
        {

            var entity = await _dailyEarningRepository.FindAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            var dateChanged = entity.Date.Date != input.Date.Date;
            var shopChanged = entity.ShopId != input.ShopId;

            entity.ShopId = input.ShopId;
            entity.Date = input.Date;
            entity.EarningAmount = input.EarningAmount;

            if (dateChanged || shopChanged)
            {
                var monthlyCashFlow = await _appServiceUtils.FindOrCreateMonthlyCashFlow(input.ShopId, input.Date);
                var dailyCashFlow = await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);

                entity.DailyCashFlow = dailyCashFlow;
            }

            return ObjectMapper.Map<DailyEarning, DailyEarningDto>(await _dailyEarningRepository.InsertAsync(entity));
        }

        [Authorize(Permissions.TulumbaPermissions.DailyEarnings.Get)]
        public override async Task<PagedResultDto<DailyEarningDto>> GetListAsync(GetDailyEarningListDto input)
        {
            var dailyEarnings = new List<DailyEarning>();

            if (
                !input.ShopId.HasValue &&
                !input.DateGTE.HasValue &&
                !input.DateLTE.HasValue
                )
            {
                if (string.IsNullOrWhiteSpace(input.Sorting))
                {
                    input.Sorting = nameof(DailyEarning.Date);
                }
                dailyEarnings = await _dailyEarningRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);

            }
            else
            {
                dailyEarnings = (await _dailyEarningRepository.GetListAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE)))
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount).ToList();
            }

            var totalCount = await _dailyEarningRepository.CountAsync(x =>
                    (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
                    (!input.DateGTE.HasValue || x.Date <= input.DateGTE) &&
                    (!input.DateLTE.HasValue || x.Date >= input.DateLTE));

            return new PagedResultDto<DailyEarningDto>(
                totalCount,
                ObjectMapper.Map<List<DailyEarning>, List<DailyEarningDto>>(dailyEarnings)
            );
        }
    }
}