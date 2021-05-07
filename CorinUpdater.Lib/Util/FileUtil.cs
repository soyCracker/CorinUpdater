using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorinUpdater.Lib.Util
{
    public class FileUtil
    {
        public static bool Create()
        {

            return false;
        }

        public static string ReadContent(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch(Exception ex)
            {
                LogUtil.Error(ex);
            }
            return "";
        }

        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                File.WriteAllBytes(fileName, byteArray);
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
                return false;
            }
        }
    }
}
