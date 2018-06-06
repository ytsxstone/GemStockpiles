using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Points.PointRanks.Dto;

namespace JFJT.GemStockpiles.Points.PointRanks
{
    [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks)]
    public class PointRankAppService : AsyncCrudAppService<PointRank, PointRankDto, Guid, PagedResultRequestDto, PointRankDto, PointRankDto>, IPointRankAppService
    {
        private readonly IRepository<PointRank, Guid> _pointRankRepository;

        public PointRankAppService(IRepository<PointRank, Guid> pointRankRepository)
            : base(pointRankRepository)
        {
            _pointRankRepository = pointRankRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Create)]
        public override async Task<PointRankDto> Create(PointRankDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckNameOrMinPointAsync(input.Id, input.Name, input.MinPoint));

            var entity = ObjectMapper.Map<PointRank>(input);

            entity = await _pointRankRepository.InsertAsync(entity);

            return MapToEntityDto(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Edit)]
        public override async Task<PointRankDto> Update(PointRankDto input)
        {
            CheckUpdatePermission();

            var entity = await _pointRankRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            CheckErrors(await CheckNameOrMinPointAsync(input.Id, input.Name, input.MinPoint));

            MapToEntity(input, entity);

            entity = await _pointRankRepository.UpdateAsync(entity);

            return MapToEntityDto(entity);
        }

        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _pointRankRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            await _pointRankRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// 积分头像上传
        /// </summary>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Create, PermissionNames.Pages_PointManagement_PointRanks_Edit)]
        public UploadAvatarDto UploadAvatar()
        {
            return new UploadAvatarDto { Name = "bc7521e033abdd1e92222d733590f104.jpg" };
        }

        protected override void MapToEntity(PointRankDto input, PointRank pointRank)
        {
            ObjectMapper.Map(input, pointRank);
        }

        protected override async Task<PointRank> GetEntityByIdAsync(Guid id)
        {
            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(PointRank), id);
            }

            return entity;
        }

        protected override IQueryable<PointRank> ApplySorting(IQueryable<PointRank> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.MinPoint);
        }

        protected async Task<IdentityResult> CheckNameOrMinPointAsync(Guid? expectedId, string name, int minPoint)
        {
            var entity = await _pointRankRepository.FirstOrDefaultAsync(b => b.Name == name);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException(name + " 等级名称已存在");
            }

            entity = await _pointRankRepository.FirstOrDefaultAsync(b => b.MinPoint == minPoint);
            if (entity != null && entity.Id != expectedId)
            {
                throw new UserFriendlyException(minPoint + " 最小积分已存在");
            }

            return IdentityResult.Success;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
