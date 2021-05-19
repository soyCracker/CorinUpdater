using CorinUpdater.Lib.Model;
using CorinUpdater.Lib.Util;
using System;
using System.IO;
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
                LogUtil.Error(ex);
            }
            return new GithubReleaseRes();
        }

        public bool IsHasNewVer(GithubReleaseRes info, string localInfoPath)
        {
            try
            {
                string content = File.ReadAllText(localInfoPath);
                GithubReleaseRes localInfo = JsonSerializer.Deserialize<GithubReleaseRes>(content);
                //用時間比對
                if (info.Assets[0].AssetCreatedAt.CompareTo(localInfo.Assets[0].AssetCreatedAt) > 0)
                {
                    return true;
                }              
            }
            catch(Exception ex)
            {
                LogUtil.Debug("No Local Version Info");
                return true;
            }
            return false;
        }

        public async Task<bool> DownloadNewVer(GithubReleaseRes info, string dir, string fileName)
        {
            try
            {
                if(Directory.Exists(dir))
                {
                    LogUtil.Debug("New Version Folder is exist");
                    FileUtil.DeleteWholeFolder(dir);
                    Directory.CreateDirectory(dir);
                }
                File.WriteAllBytes(Path.Combine(dir, fileName), await HttpUtil.DownloadFile(info.Assets[0].AssetBrowserDownloadUrl));
                return true;
            }
            catch(Exception ex)
            {
                LogUtil.Debug("DownloadNewVer Fail");
                LogUtil.Error(ex);
            }
            return false;
        }

        public void SaveVerInfo(GithubReleaseRes info, string filePath, string infoFile)
        {
            if(File.Exists(Path.Combine(filePath, infoFile)))
            {
                File.Delete(Path.Combine(filePath, infoFile));
            }
            File.WriteAllText(Path.Combine(filePath, infoFile), JsonSerializer.Serialize(info));
        }
    }
}
