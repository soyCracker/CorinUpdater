using CorinUpdater.Lib.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorinUpdater.Lib.Util
{
    public class LogUtil
    {
        public static void Debug(string msg)
        {
            Console.WriteLine(LibConstant.PROJECT + " " + msg);
        }

        public static void Error(Exception ex)
        {
            Console.WriteLine(LibConstant.PROJECT + " ERROR ********");
            Console.WriteLine(ex.Message);
        }
    }
}
