using System;

namespace BLL.LogFolder
{
    public interface ILogService
    {
        void Log(string level, Exception e, string format, params object [] args);     
    }
}