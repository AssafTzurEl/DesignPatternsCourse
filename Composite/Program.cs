using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int, int>> expr
                = (int x, int y, int z) => x + y * z;

            // Take a look at expr:
            // Expression   = Component
            // Leaf         = PrimitiveParameterExpression
            // Composite    = SimpleBinaryExpression
            Debugger.Break();
        }
    }
}
