using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.v2
{
    /// <summary>
    /// Adding thread-safety to basic Singleton implementation.
    /// </summary>
    class Logger
    {
        public static Logger Instance()
        {
            lock (_lockObj)
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }

                return _instance;
            }
        }

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

        private static object _lockObj = new object();
        private static Logger _instance;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = Logger.Instance();

            logger.Info("Inside Main()");
        }
    }
}
