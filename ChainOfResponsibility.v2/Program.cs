using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility.v2
{
    interface ITestHandler
    {
        string ConvertToGrade(int testResult);
    }

    abstract class TestHandler : ITestHandler
    {
        public TestHandler(ITestHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public abstract string ConvertToGrade(int testResult);

        protected ITestHandler _nextHandler;
    }

    class APlusHandler : TestHandler
    {
        public APlusHandler(ITestHandler nextHandler)
            : base(nextHandler)
        { }

        public override string ConvertToGrade(int testResult)
        {
            if (testResult == 100) // I can handle it
            {
                return "A+";
            }
            else // Can't handle it - maybe someone else along the chain
            {
                return _nextHandler?.ConvertToGrade(testResult);
            }
        }
    }

    class AHandler : TestHandler
    {
        public AHandler(ITestHandler nextHandler)
            : base(nextHandler)
        { }

        public override string ConvertToGrade(int testResult)
        {
            if (testResult >= 90) // I can handle it
            {
                return "A";
            }
            else // Can't handle it - maybe someone else along the chain
            {
                return _nextHandler?.ConvertToGrade(testResult);
            }
        }
    }

    class BHandler : TestHandler
    {
        public BHandler(ITestHandler nextHandler)
            : base(nextHandler)
        { }

        public override string ConvertToGrade(int testResult)
        {
            if (testResult >= 80) // I can handle it
            {
                return "B";
            }
            else // Can't handle it - maybe someone else along the chain
            {
                return _nextHandler?.ConvertToGrade(testResult);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var testHandlerChain = new BHandler(new AHandler(new APlusHandler(null)));
            var testResult = 85;

            Console.WriteLine("Your grade is: {0}", testHandlerChain.ConvertToGrade(testResult));
        }
    }
}
