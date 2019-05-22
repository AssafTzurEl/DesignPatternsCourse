using System;
using System.Collections.Generic;

namespace Visitor
{
    /// <summary>
    /// Element
    /// </summary>
    class Employee
    {
        public Employee(string name, int salary, int seniority)
        {
            Name = name;
            BaseSalary = salary;
            Seniority = seniority;
        }

        public string Name { get; set; }
        public int BaseSalary { get; set; }
        public int Seniority { get; set; }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public virtual int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    /// <summary>
    /// ConcreteElement
    /// </summary>
    class Developer : Employee
    {
        public Developer(string name, int salary, int seniority)
            : base(name, salary, seniority)
        { }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public override int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    /// <summary>
    /// ConcreteElement
    /// </summary>
    class Ceo : Employee
    {
        public Ceo(string name, int salary, int seniority)
            : base(name, salary, seniority)
        { }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public override int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }

    /// <summary>
    /// Visitor
    /// </summary>
    abstract class IVisitor
    {
        public abstract int Visit(Employee e);

        public abstract int Visit(Developer dev);

        public abstract int Visit(Ceo ceo);
    }

    /// <summary>
    /// ConcreteVisitor
    /// </summary>
    class SalaryVisitor : IVisitor
    {
        public override int Visit(Employee e)
        {
            return e.BaseSalary;
        }

        public override int Visit(Developer dev)
        {
            // 10% raise for each year serving the company
            return dev.BaseSalary + 100 * dev.Seniority;
        }

        public override int Visit(Ceo ceo)
        {
            // 10% raise for each year serving the company
            return ceo.BaseSalary + (ceo.BaseSalary * ceo.Seniority / 10);
        }
    }

    /// <summary>
    /// ConcreteVisitor
    /// </summary>
    class BonusSalaryVisitor : SalaryVisitor
    {
        public override int Visit(Employee e)
        {
            return e.BaseSalary + 100;
        }

        public override int Visit(Ceo e)
        {
            // 10% raise for each year serving the company
            return base.Visit(e) * 2;
        }
    }

    class LetterHeaderVisitor : IVisitor
    {
        public override int Visit(Employee e)
        {
            Console.WriteLine($"Hi {e.Name},");

            return 0;
        }

        public override int Visit(Developer dev)
        {
            Console.WriteLine($"Dear {dev.Name},");

            return 0;
        }

        public override int Visit(Ceo ceo)
        {
            Console.WriteLine($"Greetings, your excellence!");

            return 0;
        }
    }

    class Program
    {
        static void ProcessSalaries(IEnumerable<Employee> employees,
            IVisitor salaryCalculator)
        {
            Console.WriteLine("{0,-12}{1,7}", "Employee", "Salary");
            foreach (var employee in employees)
            {
                Console.WriteLine("{0,-12}{1,7}", employee.Name,
                    employee.Accept(salaryCalculator));
            }
        }

        static void Main(string[] args)
        {
            var employees = new List<Employee>
            {
                new Employee("Yosefa", 5000, 2),
                new Developer("Yossi", 20000, 5),
                new Ceo("Yosef", 100000, 3)
            };

            var currentMonth = DateTime.Now.Month;

            if (currentMonth != 12)
            {
                // normal month
                ProcessSalaries(employees, new SalaryVisitor());
            }
            else
            {
                // last month of the year - bonus time!
                ProcessSalaries(employees, new BonusSalaryVisitor());
            }

            Console.WriteLine();

            var letterWriter = new LetterHeaderVisitor();

            foreach (var emp in employees)
            {
                emp.Accept(letterWriter);
            }
        }
    }
}
