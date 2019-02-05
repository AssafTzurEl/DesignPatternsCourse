using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton.v1
{
    /// <summary>
    /// Simple Singleton implementation, "translated" from GoF's C++ example.
    /// </summary>
    class Logger
    {
        public static Logger Instance()
        {
            // Not thread safe!!!
            if (_instance == null)
            {
                _instance = new Logger();
            }

            return _instance;
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

        private static Logger _instance;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = Logger.Instance();

            logger.Info("Inside Main()");

            //Logger evilTwin = new Logger();

            MyMethod();
        }

        static void MyMethod()
        {
            Logger l = Logger.Instance();

            l.Info("Inside MyMethod()");
        }
    }
}
