using System.IO;

namespace CorinUpdater.Lib.Util
{
    public class FileUtil
    {
        public static void DeleteWholeFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接刪除其中的文件   
                }
                else
                    DeleteWholeFolder(d);//遞規删除子文件夹   
            }
            Directory.Delete(dir);//刪除已空文件夹   
        }
    }
}
