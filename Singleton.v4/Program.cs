using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.v4
{
    /// <summary>
    /// Thread-safety via .NET.
    /// </summary>
    class Logger
    {
        //public static Logger Instance()
        //{
        //    return _instance;
        //}

        public static Logger Instance => _instance;

        public void Info(string message)
        {
            // Write to file
            Console.WriteLine("Info: {0}", message);
        }

        public void Warning(string message)
        {
            // Write to file
            Console.WriteLine("Warning: {0}", message);
        }

        public void Error(string message)
        {
            // Write to file
            Console.WriteLine("Error: {0}", message);
        }

        private Logger()
        {
            // Open log file for writing
        }

        private static Logger _instance = new Logger();

        /// <summary>
        /// Used to ensure thread-safety AND lazy initialization. See ECMA-334.
        /// </summary>
        static Logger()
        { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Logger logger = Logger.Instance;

            //logger.Info("Inside Main()");
            Logger.Instance.Info("Inside Main()");
        }
    }
}

