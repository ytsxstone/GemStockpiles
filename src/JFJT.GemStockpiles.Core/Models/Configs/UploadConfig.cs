using System;

namespace JFJT.GemStockpiles.Models.Configs
{
    /// <summary>
    /// 文件上传配置
    /// </summary>
    public class UploadConfig
    {
        /// <summary>
        /// 公共上传文件配置
        /// </summary>
        public Common Common { get; set; }

        /// <summary>
        /// 积分头像文件上传配置
        /// </summary>
        public PointAvatar PointAvatar { get; set; }
    }

    /// <summary>
    /// 公共配置
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 文件上传根目录
        /// </summary>
        public string BasePath { get; set; }
        /// <summary>
        /// 文件上传临时目录
        /// </summary>
        public string TempFolderName { get; set; }
    }

    /// <summary>
    /// 积分头像
    /// </summary>
    public class PointAvatar
    {
        /// <summary>
        /// 文件目录
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// 文件扩展类型
        /// </summary>
        public string FileExtension { get; set; }
    }
}
