using CorinUpdater.Base;
using CorinUpdater.Lib.Model;
using CorinUpdater.Lib.Service;
using CorinUpdater.Lib.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CorinUpdater
{
    class Program
    {
        private static UpdateService updateService = new UpdateService();
        private static InstallService installService = new InstallService();

        static async Task Main(string[] args)
        {
            //Directory.SetCurrentDirectory(dir);

            //ProcessService processService = new ProcessService(Path.Combine(Environment.CurrentDirectory, ""));

            GithubReleaseRes info = await updateService.GetLatestVerInfo("https://api.github.com/repos/soyCracker/DelegationExporter/releases/latest");
            LogUtil.Debug("tag_name:" + info.TagName);
            LogUtil.Debug("download url:" + info.Assets[0].AssetBrowserDownloadUrl);
            LogUtil.Debug("file name:" + info.Assets[0].AssetName);

            //LogUtil.Debug(Environment.CurrentDirectory);
            //LogUtil.Debug(Path.GetFullPath(BaseConstant.VERSION_FILE_NAME));

            await DownloadNewerVer(info);

            InstallNewVer(info);

            /*if(!processService.IsProcessRun())
            {
                processService.StartProcess();
            }*/
        }

        private static async Task DownloadNewerVer(GithubReleaseRes info)
        {
            if (updateService.IsHasNewerVer(info, Path.Combine(BaseConstant.PROGRAM_FOLDER, BaseConstant.VERSION_FILE_NAME)) &&
                updateService.IsHasNewerVer(info, Path.Combine(BaseConstant.DOWNLOAD_TEMP_FOLDER, BaseConstant.VERSION_FILE_NAME)))
            {
                LogUtil.Debug("Has new ver, download start");
                if (await updateService.DownloadNewVer(info, BaseConstant.DOWNLOAD_TEMP_FOLDER, info.Assets[0].AssetName))
                {
                    LogUtil.Debug("Download...");
                    updateService.SaveVerInfo(info, BaseConstant.DOWNLOAD_TEMP_FOLDER, BaseConstant.VERSION_FILE_NAME);
                    LogUtil.Debug("Download Finish");

                    /*while(processService.IsProcessRun())
                    {
                        processService.StopProcess();
                        Thread.Sleep(1000);
                    }*/                   
                }
            }
        }

        private static void InstallNewVer(GithubReleaseRes info)
        {
            string zipPath = Path.Combine(BaseConstant.DOWNLOAD_TEMP_FOLDER, info.Assets[0].AssetName);
            installService.Inastall(zipPath, BaseConstant.PROGRAM_FOLDER);
            File.Copy(Path.Combine(BaseConstant.DOWNLOAD_TEMP_FOLDER, BaseConstant.VERSION_FILE_NAME), Path.Combine(BaseConstant.PROGRAM_FOLDER, BaseConstant.VERSION_FILE_NAME));
        }
    }
}
