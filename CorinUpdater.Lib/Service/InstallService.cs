using CorinUpdater.Lib.Util;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorinUpdater.Lib.Service
{
    public class InstallService
    {
        private Process process = null;

        public ICollection<ZipEntry> Unzip(string zipDir, string zipFileName)
        {
            // 檢查檔案是否存在
            string fuulPath = Path.Combine(zipDir, zipFileName);
            if (File.Exists(fuulPath))
            {
                var options = new ReadOptions { StatusMessageWriter = Console.Out };
                using (ZipFile zip = ZipFile.Read(fuulPath, options))
                {
                    //zip.Password = password; // 解壓密碼
                    zip.ExtractAll(zipDir);  // 解壓全部
                    return zip.Entries;
                }
            }
            return null;
        }

        public bool Install(ICollection<ZipEntry> entries)
        {
            foreach(ZipEntry file in entries)
            {
                
            }
            return false;
        }

        public void StopProcess()
        {
            if(process!=null)
            {
                process.Kill();
            }
        }

        public bool StartProcess()
        {
            if(process==null || process.HasExited)
            {
                process.Start();
            }
            return false;
        }

        public void DeleteCurrent(ICollection<ZipEntry> entries)
        {
            foreach (ZipEntry file in entries)
            {
                File.Delete(file.FileName);
            }
        }
    }
}
