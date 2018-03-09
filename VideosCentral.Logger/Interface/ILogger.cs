using System;

namespace VideosCentral.Logger.Interface
{
    public interface ILogger
    {
        void LogError(Exception e, string message = null);
        void LogWarning(string message);
        void LogInfo(string message); 
    }
}