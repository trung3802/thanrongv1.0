using System;

namespace TienKiemV2Remastered.Logging
{
    public interface IServerLogger
    {
        void PrintColor(string message, string color);
        void Debug(string message);
        void DebugColor(string message, string color);
        void Print(string message);
        void Print(string message, string color);
        void Info(string info);
        void Warning(string message, Exception exception);
        void Error(string message, Exception exception);
        void Error(string message);
        void PrintError(int setId,string message);
    }
    
}