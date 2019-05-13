using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    /// <summary>
    /// Product interface
    /// </summary>
    interface IVehicle
    { /* Vehicle properties here */ }

    /// <summary>
    /// Concrete product
    /// </summary>
    class Car: IVehicle
    { /* ... */ }

    /// <summary>
    /// Concrete product
    /// </summary>
    class Motorcycle : IVehicle
    { /* ... */ }

    /// <summary>
    /// Builder interface
    /// </summary>
    interface IVehicleBuilder
    {
        void BuildChassis();
        void BuildWheels();
        void BuildEngine();
        void Paint(ConsoleColor color);
        IVehicle Assemble();
    }

    /// <summary>
    /// ConcreteBuilder
    /// </summary>
    class CarBuilder : IVehicleBuilder
    {

        public void BuildChassis()
        {
            Console.WriteLine("Building car chassis");
        }

        public void BuildEngine()
        {
            Console.WriteLine("Building car engine");
        }

        public void BuildWheels()
        {
            Console.WriteLine("Building 4 wheels (plus replacement)");
        }

        public void Paint(ConsoleColor color)
        {
            Console.WriteLine("Painting car in {0}", color);
        }

        public IVehicle Assemble()
        {
            return new Car(/* chassis, wheels, engine, paint */);
        }
    }

    /// <summary>
    /// ConcreteBuilder
    /// </summary>
    class MotorcycleBuilder : IVehicleBuilder
    {

        public void BuildChassis()
        {
            Console.WriteLine("Building motorcycle chassis");
        }

        public void BuildEngine()
        {
            Console.WriteLine("Building motorcycle engine");
            throw new NotImplementedException();
        }

        public void BuildWheels()
        {
            Console.WriteLine("Building 2 wheels");
        }

        public void Paint(ConsoleColor color)
        {
            Console.WriteLine("Painting motorcycle in {0}", color);
        }

        public IVehicle Assemble()
        {
            return new Motorcycle(/* chassis, wheels, engine, paint */);
        }
    }

    /// <summary>
    /// Director
    /// </summary>
    class VehicleManufacturer
    {
        public VehicleManufacturer(IVehicleBuilder builder)
        {
            _builder = builder;
        }

        /// <summary>
        /// Construct()
        /// </summary>
        /// <param name="vehicleColor"></param>
        public void Manufacture(ConsoleColor vehicleColor)
        {
            // Known the right order and parameters
            _builder.BuildChassis();
            _builder.BuildWheels();
            _builder.BuildEngine();

            // Finishing touches
            _builder.Paint(vehicleColor);
        }

        private IVehicleBuilder _builder;
    }

    /// <summary>
    /// Client
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new CarBuilder();
            VehicleManufacturer bmw = new VehicleManufacturer(builder);

            bmw.Manufacture(ConsoleColor.Red);

            var myCar = builder.Assemble();
        }
    }
}
