using CorinUpdater.Lib.Model;
using CorinUpdater.Lib.Service;
using System;
using System.Threading.Tasks;

namespace CorinUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            UpdateService updateService = new UpdateService();
            GithubReleaseRes info = await updateService.GetLatestVerInfo("https://api.github.com/repos/soyCracker/DelegationExporter/releases/latest");
            Console.WriteLine("tag_name:" + info.TagName);
            Console.WriteLine("download url:" + info.Assets[0].AssetBrowserDownloadUrl);
            Console.WriteLine("file name:" + info.Assets[0].AssetName);
            if (updateService.IsHasNewVer(info, "Local info path"))
            {
                Console.WriteLine("Has new ver");
                await updateService.DownloadNewVer(info, @"C:\Users\yuhuilai\OneDrive - A.S. Watson Group\桌面\temp\" + info.Assets[0].AssetName);
            }
        }
    }
}
