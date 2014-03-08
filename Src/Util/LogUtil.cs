using System;
using System.Collections.Generic;
using System.Text;

namespace Hope.Util
{
    public class LogUtil
    {
        public static log4net.ILog _Log = log4net.LogManager.GetLogger("Log4NetHope.LogBase");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="e"></param>
        public static void Write(Type type, Exception e)
        {
            _Log.Error(type, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Write(string message)
        {
            _Log.Error(message);
        }

        /// <summary>
        /// fatal
        /// </summary>
        /// <param name="message"></param>
        public static void fatal(string message)
        {
            if (_Log.IsFatalEnabled)
            {
                _Log.Fatal(message);
            }
        }
        
        /// <summary>
        /// error
        /// </summary>
        /// <param name="message"></param>
        public static void error(string message)
        {
            if (_Log.IsErrorEnabled)
            {
                _Log.Error(message);
            }
        }
        
        /// <summary>
        /// warn
        /// </summary>
        /// <param name="message"></param>
        public static void warn(string message)
        {
            if (_Log.IsWarnEnabled)
            {
                _Log.Warn(message);
            }
        }

        /// <summary>
        /// info
        /// </summary>
        /// <param name="message"></param>
        public static void info(string message)
        {
            if (_Log.IsInfoEnabled)
            {
                _Log.Info(message);
            }
        }

        /// <summary>
        /// debug
        /// </summary>
        /// <param name="message"></param>
        public static void debug(string message)
        {
            if (_Log.IsDebugEnabled)
            {
                _Log.Debug(message);
            }
        }
    }
}
