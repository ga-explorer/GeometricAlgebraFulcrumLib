using System;
using AngouriMath;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.AngouriMath
{
    public static class Sample1
    {
        public static void Execute()
        {
            //var expr = MathS.FromString("-a + b * c");
            var expr = MathS.FromString("tempVar_1");

            foreach (var node in expr.Nodes)
            {
                Console.WriteLine(node.GetType().Name);
                Console.WriteLine(node.ToString());
            }
        }
    }
}