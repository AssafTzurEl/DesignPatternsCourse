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

    // ToDo: Add C+ grade, for grades between 75 and 79

    class CHandler : TestHandler
    {
        public CHandler(ITestHandler nextHandler)
            : base(nextHandler)
        { }

        public override string ConvertToGrade(int testResult)
        {
            if (testResult >= 70) // I can handle it
            {
                return "C";
            }
            else // Can't handle it - maybe someone else along the chain
            {
                return _nextHandler?.ConvertToGrade(testResult);
            }
        }
    }

    sealed class DefaultHandler : ITestHandler
    {
        public string ConvertToGrade(int testResult)
        {
            return "F";
        }
    }

    class GradeManager
    {
        public GradeManager(ITestHandler testHandler)
        {
            _testHandler = testHandler;
        }

        public void PrintTestResults(int testResult)
        {
            Console.WriteLine("Your grade is: {0}", _testHandler.ConvertToGrade(testResult));
        }

        private ITestHandler _testHandler;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ToDo: Remove B grade - 80s are also considered C
            var testHandlerChain = new APlusHandler(new AHandler(new BHandler(new CHandler(new DefaultHandler()))));
            var gradeManager = new GradeManager(testHandlerChain);

            gradeManager.PrintTestResults(85);
            gradeManager.PrintTestResults(40);
        }
    }
}
