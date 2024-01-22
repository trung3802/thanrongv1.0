using System;
using TienKiemV2Remastered.Application.IO;
using TienKiemV2Remastered.DatabaseManager;
using Serilog;
using Serilog.Events;
using TienKiemV2Remastered.Application.Main;

namespace TienKiemV2Remastered.Logging
{
    public class ServerLogger : IServerLogger
    {
        private readonly ILogger _logger;

        public ServerLogger()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logging/log-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Debug(string message)
        {
            if(ConfigManager.gI().IsDebug) _logger.Information($"DEBUG ==> {message}");
        }
        public void PrintError(int setId,string message)
        {
            if (DragonBall.findErr) ServerUtils.WriteLog("login/"+setId,$"Find Error ==> {message}");
        }
        public void DebugColor(string message, string color)
        {
            if (ConfigManager.gI().IsDebug)
            {
                if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
                if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
                if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
                if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
                if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
                if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;

                if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
        public void PrintColor(string message, string color)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
            if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void Print(string message, string color)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;
            if (color == "manager") Console.ForegroundColor = ConsoleColor.DarkMagenta;

            if (color == "green") Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void PrintColorWithBackgroundColor(string message, string color, string bgColor)
        {
            if (color == "black") Console.ForegroundColor = ConsoleColor.Black;
            if (color == "red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "darkred") Console.ForegroundColor = ConsoleColor.DarkRed;
            if (color == "blue") Console.ForegroundColor = ConsoleColor.Blue;
            if (color == "cyan") Console.ForegroundColor = ConsoleColor.Cyan;
            if (color == "yellow") Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);
            Console.ResetColor();
        }
        //public void Print(string message)
        //{
        //    _logger.Information($"==> " + ServerUtils.TimeNow() + "| "+ message);
        //}
        public void Print(string message)
        {
            _logger.Information(message);
        }
        public void Info(string info)
        {
            _logger.Information(info);
        }

        public void Warning(string message, Exception exception = null)
        {
            _logger.Warning(message, exception);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}