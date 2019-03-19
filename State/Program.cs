using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    /// <summary>
    /// State
    /// </summary>
    interface IDevice
    {
        void Action1();
        void Action2();
    }

    /// <summary>
    /// ConcreteState
    /// </summary>
    class Clock : IDevice
    {
        public void Action1()
        {
            // Display basic clock
            Console.Write("\r{0,-30}", DateTime.Now.ToString("HH:mm"));
        }

        public void Action2()
        {
            // Display seconds
            Console.Write("\r{0,-30}", DateTime.Now.ToString("ss"));
        }
    }

    class Calendar : IDevice
    {
        public void Action1()
        {
            // Display date
            Console.Write("\r{0,-30}", DateTime.Now.ToShortDateString());
        }

        public void Action2()
        {
            // Display DST status
            Console.Write("\r{0,-30}", DateTime.Now.IsDaylightSavingTime() ? "DST" : "STD");
        }
    }

    class StopWatch : IDevice
    {
        public void Action1()
        {
            _stopwatch = Stopwatch.StartNew();
            Console.Write("\r{0,-30}", "Stopwatch started...");
        }

        public void Action2()
        {
            Console.Write("\r{0,-30}", _stopwatch.Elapsed);
        }

        Stopwatch _stopwatch;
    }

    /// <summary>
    /// Context
    /// </summary>
    class Watch
    {
        public Watch()
        {
            _devices = new List<IDevice> { new Clock(), new Calendar(), new StopWatch() };
            _index = 0;
            _state = _devices[_index];
            Console.Write("\r{0,-30}", $"Starting in {_state.GetType().Name} mode");
        }

        public void Button1()
        {
            _state.Action1();
        }

        public void Button2()
        {
            _state.Action2();
        }

        public void Button3()
        {
            _index = (_index + 1) % _devices.Count;
            _state = _devices[_index];
            Console.Write("\r{0,-30}", $"Now in {_state.GetType().Name} mode");
        }

        List<IDevice> _devices;
        int _index;
        IDevice _state;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Watch watch = new Watch();

            Debugger.Break(); // Continue step-by-step
            // Clock functions:
            watch.Button1();
            watch.Button2();

            // Switch to calendar:
            watch.Button3();
            watch.Button1();
            watch.Button2();

            // Switch to stopwatch:
            watch.Button3();
            watch.Button1();
            watch.Button2();

            // Back to clock:
            watch.Button3();
            watch.Button1();
            watch.Button2();
        }
    }
}
