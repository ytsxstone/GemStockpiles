using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using JFJT.GemStockpiles.Models.Configs;

namespace JFJT.GemStockpiles.Helpers
{
    public class UploadHelper
    {
        private readonly IOptions<AppSettings> _appSettings;

        public UploadHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        #region 获取配置项

        /// <summary>
        /// 上传文件根目录
        /// </summary>
        private string BasePath
        {
            get
            {
                return _appSettings.Value.UploadConfig.Common.BasePath;
            }
        }
        /// <summary>
        /// 文件上传临时目录
        /// </summary>
        private string TempFolderName
        {
            get
            {
                return _appSettings.Value.UploadConfig.Common.TempFolderName;
            }
        }

        /// <summary>
        /// 上传文件类型对应的文件目录
        /// </summary>
        /// <param name="uploadType"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        private string GetFilePath(UploadType uploadType, FileType filetype)
        {
            switch (uploadType)
            {
                case UploadType.PointAvatar:
                    return _appSettings.Value.UploadConfig.PointAvatar.FolderName;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 上传文件大小
        /// </summary>
        /// <param name="uploadType"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        private string GetFileSize(UploadType uploadType, FileType filetype)
        {
            switch (uploadType)
            {
                case UploadType.PointAvatar:
                    return _appSettings.Value.UploadConfig.PointAvatar.FileSize;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 上传文件扩展类型
        /// </summary>
        /// <param name="uploadType"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        private string GetFileExtension(UploadType uploadType, FileType filetype)
        {
            switch (uploadType)
            {
                case UploadType.PointAvatar:
                    return _appSettings.Value.UploadConfig.PointAvatar.FileExtension;
                default:
                    return "";
            }
        }

        #endregion

        #region 文件验证

        /// <summary>
        /// 上传文件验证
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="uploadType">上传类型</param>
        /// <param name="filetype">文件类型</param>
        /// <param name="Error">错误消息</param>
        /// <returns></returns>
        public bool Validate(IFormFile file, UploadType uploadType, FileType filetype, out string Error)
        {
            string extensValue = GetFileExtension(uploadType, filetype);
            long size = Convert.ToInt32(GetFileSize(uploadType, filetype)) * 1024;
            Error = "";

            if (!ValidateExtension(extensValue, file.FileName))
            {
                Error = "文件类型错误";
                return false;
            }

            if (!ValidateSize(size, file.Length))
            {
                Error = "文件大小不可超过" + size / 1024 + "KB";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 验证文件类型
        /// </summary>
        /// <param name="ExtensionValue"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool ValidateExtension(string ExtensionValue, string fileName)
        {
            string ext = GetExtensionName(fileName).ToLower();

            return ExtensionValue.ToLower().Contains(ext);
        }

        /// <summary>
        /// 验证文件大小
        /// </summary>
        /// <param name="Size"></param>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private bool ValidateSize(long Size, long fileSize)
        {
            return fileSize <= Size;
        }

        #endregion

        #region 功能函数
        /// <summary>
        /// 返回文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetExtensionName(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        /// <summary>
        /// 获取文件保存信息
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="loginUserId">当前登录用户ID</param>
        /// <returns></returns>
        public async Task<SaveFileResult> SaveFile(IFormFile file, long? loginUserId)
        {
            //登录用户文件夹
            string loginUserFolder = "user" + loginUserId?.ToString();

            //临时存储的相对路径
            string relativePath = $"{BasePath}/{TempFolderName}/{loginUserFolder}";

            //获取服务器根目录
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //临时存储的绝对路径
            string filePath = $"{rootPath}/{relativePath}";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            //存储的文件名称
            string fileName = Guid.NewGuid() + GetExtensionName(file.FileName);

            //保存文件
            filePath = Path.Combine(filePath, fileName);
            using (FileStream fs = File.Create(filePath))
            {
                await file.CopyToAsync(fs);
                fs.Flush();
            }

            //返回存储结果
            SaveFileResult result = new SaveFileResult()
            {
                FileName = fileName,
                FilePath = filePath,
                RelativePath = $"{relativePath}/{fileName}"
            };
            return result;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="uploadType">上传类型</param>
        /// <param name="currentUserFolder">当前登录用户临时目录文件名</param>
        /// <returns></returns>
        public void MoveFile(string fileName, UploadType uploadType, FileType fileType, long? loginUserId)
        {
            //登录用户文件夹
            string loginUserFolder = "user" + loginUserId?.ToString();

            //临时相对路径
            string tempRelativePath = $"{BasePath}/{TempFolderName}/{loginUserFolder}";

            //存储相对路径
            string saveRelativePath = $"{BasePath}/{GetFilePath(uploadType, fileType)}";

            //获取服务器目录
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //判断文件是否存在
            string fileTempPath = $"{rootPath}/{tempRelativePath}/{fileName}";
            if (!File.Exists(fileTempPath))
            {
                return;
            }

            //文件保存路径
            string fileDestPath = $"{rootPath}/{saveRelativePath}";
            if (!Directory.Exists(fileDestPath))
            {
                Directory.CreateDirectory(fileDestPath);
            }

            fileDestPath = $"{fileDestPath}/{fileName}";
            //确认目标文件不存在
            if (File.Exists(fileDestPath))
            {
                File.Delete(fileDestPath);
            }
            //移动文件
            File.Move(fileTempPath, fileDestPath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="uploadType">上传类型</param>
        public void DeleteFile(string fileName, UploadType uploadType, FileType fileType)
        {
            //文件相对路径
            string fileRelativePath = $"{BasePath}/{GetFilePath(uploadType, fileType)}";

            //获取服务器目录
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //判断文件是否存在
            string filePath = $"{rootPath}/{fileRelativePath}/{fileName}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 删除登录用户临时目录
        /// </summary>
        /// <param name="loginUserId"></param>
        public void ClearTempDirectory(long? loginUserId)
        {
            //登录用户文件夹
            string loginUserFolder = "user" + loginUserId?.ToString();

            //临时相对路径
            string tempRelativePath = $"{BasePath}/{TempFolderName}/{loginUserFolder}";

            //获取服务器目录
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            //删除目录及子文件
            string tempDirectory = $"{rootPath}/{tempRelativePath}";
            if (Directory.Exists(tempDirectory))
            {
                Directory.Delete(tempDirectory, true);
            }
        }
        #endregion
    }

    /// <summary>
    /// 上传类型
    /// </summary>
    public enum UploadType
    {
        /// <summary>
        /// 积分头像
        /// </summary>
        PointAvatar = 10
    }

    /// <summary>
    /// 文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        ///图片
        /// </summary>
        Image = 10,
        /// <summary>
        /// 视频
        /// </summary>
        Video = 20
    }

    /// <summary>
    /// 文件存储结果
    /// </summary>
    public class SaveFileResult
    {
        //保存文件名称
        public string FileName { get; set; }
        //绝对路径
        public string FilePath { get; set; }
        //相对路径
        public string RelativePath { get; set; }
    }
}
