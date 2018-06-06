using System;
using System.IO;
using Microsoft.Extensions.Options;
using JFJT.GemStockpiles.Models.Config;

namespace JFJT.GemStockpiles.Common
{
    public enum FileType
    {
        /// <summary>
        ///图片
        /// </summary>
        image = 1,
        /// <summary>
        /// 视频
        /// </summary>
        video = 2
    }

    public class SaveFileResult
    {
        public string SaveDriveFileName { get; set; }
        public string SaveFileName { get; set; }
        public string SaveDirectory { get; set; }
    }

    public class FileHelper
    {
        private readonly IOptions<AppSettings> _appSettings;

        public FileHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        //private string GetFileTypeFolder(FileType filetype, bool IsBase = false)
        //{
        //    string _path = IsBase ? BasePath : DriveFolder;
        //    switch (filetype)
        //    {
        //        case FileType.image:
        //            _path += ImageFileName;
        //            break;
        //        case FileType.video:
        //            _path += VideoFileName;
        //            break;
        //    }
        //    return _path;
        //}

        public SaveFileResult GetSaveFilePath(FileType fileType, string fileName)
        {
            string _folder = DateTime.Now.ToString("yyyy-MM") + "/";
            string driveFolder = ""; //GetFileTypeFolder(fileType) + _folder;
            string baseFolder = ""; //GetFileTypeFolder(fileType, true) + _folder;

            string bas =Path.Combine(Directory.GetCurrentDirectory() , "wwwroot");//获取服务器目录
            if (!Directory.Exists(bas + baseFolder))
            {
                Directory.CreateDirectory(bas + baseFolder);
            }

            string savefilename = Guid.NewGuid() + GetExtensionName(fileName);
            SaveFileResult result = new SaveFileResult()
            {
                SaveDriveFileName = driveFolder + savefilename,
                SaveFileName = baseFolder + savefilename,
                SaveDirectory= bas + baseFolder + savefilename
            };
            return result;
        }

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
        /// 上传文件验证
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileSize"></param>
        /// <param name="filetype"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Validate(string fileName, long fileSize, FileType filetype, out string Error)
        {
            string extensValue = "";
            long size = 0;
            Error = "";

            //switch (filetype)
            //{
            //    case FileType.image:
            //        extensValue = ImgExts;
            //        size = Convert.ToInt32(ImgFileLength) * 1024;
            //        break;
            //    case FileType.video:
            //        extensValue = VideoExts;
            //        size = Convert.ToInt32(VideoFileLength) * 1024;
            //        break;
            //}

            if (!ValidateExtension(extensValue, fileName))
            {
                Error = "文件类型错误";
                return false;
            }
            if (!ValidateSize(size, fileSize))
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
    }
}
