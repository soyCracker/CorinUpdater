using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace CorinUpdater.Lib.Service
{
    public class InstallService
    {
        public ICollection<ZipEntry> GetZipEntries(string zipPath)
        {
            var options = new ReadOptions { StatusMessageWriter = Console.Out };
            using (ZipFile zip = ZipFile.Read(zipPath, options))
            {              
                return zip.Entries;
            }
        }

        public void Inastall(string zipPath, string appDir)
        {
            var options = new ReadOptions { StatusMessageWriter = Console.Out };
            
            using (ZipFile zip = ZipFile.Read(zipPath, options))
            {
                //zip.Password = password; // 解壓密碼
                zip.ExtractAll(appDir, ExtractExistingFileAction.OverwriteSilently);  // 解壓全部
            }
        }
    }
}
