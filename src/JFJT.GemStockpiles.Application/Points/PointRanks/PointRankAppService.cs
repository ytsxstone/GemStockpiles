using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Abp.UI;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JFJT.GemStockpiles.Helpers;
using JFJT.GemStockpiles.Sessions.Dto;
using JFJT.GemStockpiles.Authorization;
using JFJT.GemStockpiles.Models.Points;
using JFJT.GemStockpiles.Models.Configs;
using JFJT.GemStockpiles.Points.PointRanks.Dto;

namespace JFJT.GemStockpiles.Points.PointRanks
{
    [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks)]
    public class PointRankAppService : AsyncCrudAppService<PointRank, PointRankDto, Guid, PagedResultRequestDto, PointRankDto, PointRankDto>, IPointRankAppService
    {
        private readonly IRepository<PointRank, Guid> _pointRankRepository;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly UploadHelper uploadHelper;

        public PointRankAppService(IRepository<PointRank, Guid> pointRankRepository, IOptions<AppSettings> appSettings)
            : base(pointRankRepository)
        {
            _pointRankRepository = pointRankRepository;
            _appSettings = appSettings;
            uploadHelper = new UploadHelper(appSettings);
        }

        /// <summary>
        /// 新增积分等级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Create)]
        public override async Task<PointRankDto> Create(PointRankDto input)
        {
            CheckCreatePermission();

            CheckErrors(await CheckNameOrMinPointAsync(input.Id, input.Name, input.MinPoint));

            var entity = ObjectMapper.Map<PointRank>(input);

            entity = await _pointRankRepository.InsertAsync(entity);

            //移动文件
            if (!string.IsNullOrWhiteSpace(entity.Avatar))
            {
                uploadHelper.MoveFile(entity.Avatar, UploadType.PointAvatar, FileType.Image, AbpSession.UserId);
            }

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 修改积分等级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Edit)]
        public override async Task<PointRankDto> Update(PointRankDto input)
        {
            CheckUpdatePermission();

            var entity = await _pointRankRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            var oldAvatar = entity.Avatar;

            CheckErrors(await CheckNameOrMinPointAsync(input.Id, input.Name, input.MinPoint));

            MapToEntity(input, entity);

            entity = await _pointRankRepository.UpdateAsync(entity);

            //头像文件处理
            if (oldAvatar != entity.Avatar)
            {
                uploadHelper.MoveFile(entity.Avatar, UploadType.PointAvatar, FileType.Image, AbpSession.UserId);

                uploadHelper.DeleteFile(oldAvatar, UploadType.PointAvatar, FileType.Image);
            }

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 删除积分等级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_PointManagement_PointRanks_Delete)]
        public override async Task Delete(EntityDto<Guid> input)
        {
            CheckDeletePermission();

            var entity = await _pointRankRepository.GetAsync(input.Id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(PointRank), input.Id);

            await _pointRankRepository.DeleteAsync(entity);

            //删除头像文件
            if (!string.IsNullOrWhiteSpace(entity.Avatar))
            {
                uploadHelper.DeleteFile(entity.Avatar, UploadType.PointAvatar, FileType.Image);
            }
        }

        /// <summary>
        /// Dto模型映射
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pointRank"></param>
        protected override void MapToEntity(PointRankDto input, PointRank pointRank)
        {
            ObjectMapper.Map(input, pointRank);
        }

        /// <summary>
        /// 根据ID获取Entity模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected override async Task<PointRank> GetEntityByIdAsync(Guid id)
        {
            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(PointRank), id);
            }

            return entity;
        }

        /// <summary>
        /// GetAll查询排序条件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<PointRank> ApplySorting(IQueryable<PointRank> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.MinPoint);
        }

        /// <summary>
        /// 检测积分等级名称或最小积分值是否已存在
        /// </summary>
        /// <param name="expectedId"></param>
        /// <param name="name"></param>
        /// <param name="minPoint"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 异常描述本地化转换函数
        /// </summary>
        /// <param name="identityResult"></param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
