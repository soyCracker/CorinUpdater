using CorinUpdater.Base;
using CorinUpdater.Lib.Model;
using CorinUpdater.Lib.Service;
using CorinUpdater.Lib.Util;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CorinUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            UpdateService updateService = new UpdateService();
            GithubReleaseRes info = await updateService.GetLatestVerInfo("https://api.github.com/repos/soyCracker/DelegationExporter/releases/latest");
            LogUtil.Debug("tag_name:" + info.TagName);
            LogUtil.Debug("download url:" + info.Assets[0].AssetBrowserDownloadUrl);
            LogUtil.Debug("file name:" + info.Assets[0].AssetName);

            //LogUtil.Debug(Environment.CurrentDirectory);
            //LogUtil.Debug(Path.GetFullPath(BaseConstant.VERSION_FILE_NAME));
            
            if (updateService.IsHasNewVer(info, BaseConstant.VERSION_FILE_NAME))
            {
                LogUtil.Debug("Has new ver, download start");
                if(await updateService.DownloadNewVer(info, BaseConstant.DOWNLOAD_TEMP_FOLDER, info.Assets[0].AssetName))
                {
                    updateService.SaveVerInfo(info, BaseConstant.DOWNLOAD_TEMP_FOLDER, BaseConstant.VERSION_FILE_NAME);
                    LogUtil.Debug("Download Finish");
                }
            }

        }
    }
}
