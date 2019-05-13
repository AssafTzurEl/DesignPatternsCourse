using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", ID, Name, Age);
        }
    }

    class EmployeeNameAscComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    class EmployeeNameDescComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return -x.Name.CompareTo(y.Name);
        }
    }

    class EmployeeIDAscComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.ID - y.ID;
        }
    }

    class EmployeeIDDescComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return -x.ID + y.ID;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<Employee> se = new SortedSet<Employee>(new EmployeeNameAscComparer());
            Employee[] employees =
            {
                new Employee { ID = 1, Name = "Tom", Age = 30 },
                new Employee { ID = 2, Name = "Polina", Age = 25 },
                new Employee { ID = 3, Name = "Boris", Age = 27 }
            };

            Debugger.Break(); // Take a look at employees order

            Array.Sort<Employee>(employees, new EmployeeNameAscComparer());
            Debugger.Break(); // Take a look at employees order

            Array.Sort<Employee>(employees, new EmployeeNameDescComparer());
            Debugger.Break(); // Take a look at employees order

            Array.Sort<Employee>(employees, new EmployeeIDAscComparer());
            Debugger.Break(); // Take a look at employees order

            Array.Sort<Employee>(employees, new EmployeeIDDescComparer());
            Debugger.Break(); // Take a look at employees order
        }
    }
}
