using System.Diagnostics;

namespace CorinUpdater.Lib.Service
{
    public class ProcessService
    {
        private Process process = new Process();

        public ProcessService(string appPath)
        {
            process.StartInfo.FileName = appPath;
        }

        public void StopProcess()
        {
            process.Kill();
        }

        public void StartProcess()
        {
            process.Start();
        }

        public bool IsProcessRun()
        {
            if(process == null || process.HasExited)
            {
                return false;
            }
            return true;
        }
    }
}
