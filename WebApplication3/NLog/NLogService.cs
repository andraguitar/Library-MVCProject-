using System;
using BLL.LogFolder;
using NLog;

namespace WebApplication3.NLog
{
    public class NLogService : ILogService
    {
        public Logger Logger = LogManager.GetCurrentClassLogger();

        public void Log(string level, Exception e, string format, params object[] args)
        {
            Logger.Log(LogLevel.FromString(level), e, format, args);
        }
    }
}