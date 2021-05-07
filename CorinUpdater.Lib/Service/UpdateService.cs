using CorinUpdater.Lib.Model;
using CorinUpdater.Lib.Util;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CorinUpdater.Lib.Service
{
    public class UpdateService
    {
        public async Task<GithubReleaseRes> GetLatestVerInfo(string url)
        {
            try
            {
                string res = await HttpUtil.Get(url);
                GithubReleaseRes githubRelease = JsonSerializer.Deserialize<GithubReleaseRes>(res);
                return githubRelease;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new GithubReleaseRes();
        }

        public bool IsHasNewVer(GithubReleaseRes info, string localInfoPath)
        {
            string content = FileUtil.ReadContent(localInfoPath);
            if(content == "")
            {
                return true;
            }
            GithubReleaseRes localInfo = JsonSerializer.Deserialize<GithubReleaseRes>(content);
            if (info.Assets[0].AssetCreatedAt.CompareTo(localInfo.Assets[0].AssetCreatedAt) > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DownloadNewVer(GithubReleaseRes info, string fullFileName)
        {
            if(FileUtil.ByteArrayToFile(fullFileName, await HttpUtil.DownloadFile(info.Assets[0].AssetBrowserDownloadUrl)))
            {
                return true;
            }
            return false;
        }
    }
}
