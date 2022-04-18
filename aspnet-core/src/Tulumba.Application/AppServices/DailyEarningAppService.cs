using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Tulumba.Application.Contracts.Dtos.DailyEarning;
using Tulumba.Application.Contracts.Interfaces;
using Tulumba.Application.Extensions;
using Tulumba.Entities.DailyEarning;
using Tulumba.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tulumba.Application.AppServices;

public class DailyEarningAppService :
    CrudAppService<
        DailyEarning, //The Book entity
        DailyEarningDto, //Used to show books
        Guid, //Primary key of the book entity
        GetDailyEarningListDto, //Used for paging/sorting
        CreateUpdateDailyEarningDto>, //Used to create/update a book
    IDailyEarningAppService //implement the IBookAppService
{
    private readonly AppServiceUtils _appServiceUtils;

    private readonly IRepository<DailyEarning, Guid> _dailyEarningRepository;

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
            var dailyCashFlow =
                await _appServiceUtils.FindOrCreateDailyCashFlow(input.ShopId, input.Date, monthlyCashFlow);

            entity.DailyCashFlow = dailyCashFlow;
        }

        return ObjectMapper.Map<DailyEarning, DailyEarningDto>(await _dailyEarningRepository.InsertAsync(entity));
    }

    [Authorize(TulumbaPermissions.DailyEarnings.Get)]
    public override async Task<PagedResultDto<DailyEarningDto>> GetListAsync(GetDailyEarningListDto input)
    {
        var dailyEarnings = new List<DailyEarning>();


        if (string.IsNullOrWhiteSpace(input.Sorting))
        {
            input.Sorting = nameof(DailyEarning.Date) + " DESC";
        }

        var query = _dailyEarningRepository.Where(x =>
            (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
            (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
            (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date));

        var sorting = input.Sorting.Split(' ');
        dailyEarnings = sorting[1].ToUpper().Equals("DESC")
            ? await query.OrderByDescending(sorting[0])
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync()
            : await query.OrderBy(sorting[0])
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

        var totalCount = await _dailyEarningRepository.CountAsync(x =>
            (!input.ShopId.HasValue || x.ShopId == input.ShopId) &&
            (!input.DateGTE.HasValue || x.Date.Date <= input.DateGTE.Value.Date) &&
            (!input.DateLTE.HasValue || x.Date.Date >= input.DateLTE.Value.Date));

        return new PagedResultDto<DailyEarningDto>(
            totalCount,
            ObjectMapper.Map<List<DailyEarning>, List<DailyEarningDto>>(dailyEarnings)
        );
    }
}