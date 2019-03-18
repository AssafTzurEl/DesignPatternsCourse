using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Random experiments in code.
/// </summary>
namespace Playground
{
    class Base
    {
        public virtual void f(string s)
        {
            Console.WriteLine("Base.f " + s);
        }
    }

    class Der : Base
    {
        public override void f(string s)
        {
            Console.WriteLine("Der.f " + s);
        }
    }

    static class Ext
    {
        public static void Say(this Base b)
        {
            b.f("as Base");
        }

        public static void Say(this Der d)
        {
            d.f("as Der");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base();
            Der d = new Der();

            b.Say();
            d.Say();
            b = d;
            b.Say();
        }
    }
}
