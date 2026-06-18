using System;
using System.IO;
using NLog;

namespace ET
{
    public class NLogger: ILog
    {
        private readonly NLog.Logger logger;

        private static string FindNLogConfig()
        {
            // 按优先级查找 NLog.config
            string[] candidates =
            {
                "../Config/NLog/NLog.config",                  // 原有相对路径
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Config", "NLog", "NLog.config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Config", "NLog", "NLog.config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "NLog", "NLog.config"),
                "NLog.config",                               // 当前目录
            };
            foreach (string p in candidates)
            {
                try
                {
                    string fullPath = Path.GetFullPath(p);
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
                catch { }
            }
            return null;
        }

        static NLogger()
        {
            string nlogPath = FindNLogConfig();
            if (nlogPath != null)
            {
                LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(nlogPath);
                LogManager.Configuration.Variables["currentDir"] = Environment.CurrentDirectory;
            }
            else
            {
                // 未找到配置文件时使用最小控制台配置，避免崩溃
                var config = new NLog.Config.LoggingConfiguration();
                config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, new NLog.Targets.ConsoleTarget("console"));
                LogManager.Configuration = config;
            }
        }

        public NLogger(string name, int process, int fiber)
        {
            this.logger = LogManager.GetLogger($"{(uint)process:000000}.{(uint)fiber:0000000000}.{name}");
        }

        public void Trace(string message)
        {
            this.logger.Trace(message);
        }

        public void Warning(string message)
        {
            this.logger.Warn(message);
        }

        public void Info(string message)
        {
            this.logger.Info(message);
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Error(Exception e)
        {
            this.logger.Error(e.ToString());
        }

#if DOTNET
        public void Trace(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Trace(message.ToStringAndClear());
        }

        public void Warning(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Warn(message.ToStringAndClear());
        }

        public void Info(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Info(message.ToStringAndClear());
        }

        public void Debug(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Debug(message.ToStringAndClear());
        }

        public void Error(ref System.Runtime.CompilerServices.DefaultInterpolatedStringHandler message)
        {
            this.logger.Error(message.ToStringAndClear());
        }
#endif
    }
}