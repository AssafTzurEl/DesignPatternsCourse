using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bridge
{
    #region Implementor
    /// <summary>
    /// Represents an electric appliance (Implementor).
    /// </summary>
    interface IAppliance
    {
        /// <summary>
        /// Turns the appliance on.
        /// </summary>
        void TurnOn();

        /// <summary>
        /// Turns an appliance off.
        /// </summary>
        void TurnOff();

        /// <summary>
        /// Returns the appliance's current state.
        /// </summary>
        bool IsWorking { get; }
    }

    class LightBulb : IAppliance
    {
        public bool IsWorking => _circuitClosed;

        public void TurnOff()
        {
            if (_circuitClosed)
            {
                _circuitClosed = false;
            }
        }

        public void TurnOn()
        {
            if (!_circuitClosed)
            {
                _circuitClosed = true;
            }
        }

        private bool _circuitClosed; // Bulb is active when circuit is closed
    }

    class Projector : IAppliance
    {
        public bool IsWorking => _bulbTemperature >= WorkingTemperature;

        public void TurnOff()
        {
            while (_bulbTemperature > ColdTemperature)
            {
                // Cool down
                Thread.Sleep(1000);
                _bulbTemperature -= 10;
            }
        }

        public void TurnOn()
        {
            while (_bulbTemperature < WorkingTemperature)
            {
                // Warm up
                Thread.Sleep(1000);
                _bulbTemperature += 10;
            }
        }

        private const int WorkingTemperature = 100;
        private const int ColdTemperature = 30;
        private int _bulbTemperature = ColdTemperature;
    }
    #endregion

    #region Abstraction
    /// <summary>
    /// Represents a smart home control (Abstraction).
    /// </summary>
    interface ISmartHome
    {
        /// <summary>
        /// Activates a smart home control.
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivates a smart home control.
        /// </summary>
        void Deactivate();

        ControlState State { get; }
    }

    abstract class SmartHomeControl : ISmartHome
    {
        public virtual ControlState State
        {
            get
            {
                if (_appliances.All(app => app.IsWorking))
                {
                    return ControlState.On;
                }
                else if (_appliances.All(app => !app.IsWorking))
                {
                    return ControlState.Off;
                }
                else
                {
                    return ControlState.Error;
                }
            }
        }

        public virtual void Activate()
        {
            _appliances.ForEach(app => app.TurnOn());
        }

        public virtual void Deactivate()
        {
            _appliances.ForEach(app => app.TurnOff());
        }

        protected List<IAppliance> _appliances = new List<IAppliance>();
    }

    class Lights : SmartHomeControl
    {
        public Lights(IEnumerable<IAppliance> lights)
        {
            _appliances = lights.ToList();
        }
    }


    class HomeTheater : SmartHomeControl
    {
        public HomeTheater(IEnumerable<IAppliance> lights, Projector projector)
        {
            _appliances = lights.ToList();
            _projector = projector;
        }

        public override void Activate()
        {
            // Turn off the lights:
            base.Deactivate();
            // Turn on the projector:
            _projector.TurnOn();
        }

        public override void Deactivate()
        {
            // Turn off the projector:
            _projector.TurnOff();
            // Turn on the lights:
            base.Activate();
        }

        private Projector _projector;
    }
    #endregion

    enum ControlState
    {
        On,
        Off,
        Error
    }

    class Program
    {
        static void Main(string[] args)
        {
            var livingRoomLight = new LightBulb();
            var diningRoomLight = new LightBulb();
            var bedroomLight = new LightBulb();
            var livingRoomProjector = new Projector();

            var lights = new Lights(new[] { livingRoomLight, diningRoomLight, bedroomLight });
            var homeTheater = new HomeTheater(new[] { livingRoomLight, diningRoomLight }, livingRoomProjector);

            // Honey, I'm home!
            lights.Activate();

            // Good night!
            lights.Deactivate();

            // Let's watch a movie!
            homeTheater.Activate();

            // Movie ended
            homeTheater.Deactivate();
        }
    }
}
