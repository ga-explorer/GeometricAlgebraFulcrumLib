using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Modeling;

public static class DifferentialFunctionSamples
{
    public static void Example1()
    {
        var x = MathDf.X;

        var f = 1 - 2 * x + 3 * x.Square() - 4 * x.Cube();

        var fDt = f.GetDerivativeN(2);

        Console.WriteLine(f.ToString());
        Console.WriteLine(fDt.ToString());
        Console.WriteLine();
    }

    public static void Example2()
    {
        const int n = 7;
        var x = MathDf.X;
        var y = x.Sin().Power(n);

        var y1 = @$"TrigReduce[Sin[x]^{n}]".ToExpr().Evaluate();
        var y2 = y.ToString().ToExpr().Evaluate();

        var yDiff = Mfs.Subtract[y1, y2].FullSimplify();

        Console.WriteLine(y.ToString());
        Console.WriteLine(y1.ToString());
        Console.WriteLine(y2.ToString());
        Console.WriteLine(yDiff.ToString());
        Console.WriteLine();
    }

    public static void Example3()
    {
        const int n = 3;
        var x = MathDf.X;
        var g = x.Sin().Power(n);
        var h = x.Power(2) + x - 3;

        var y = 3 + g + h + h * 2.5 + 1.2 + g * 3;
        var yDt1 = y.GetDerivative1();
        var yDt2 = y.GetDerivative2();

        Console.WriteLine(y.ToString());
        Console.WriteLine(yDt1.ToString());
        Console.WriteLine(yDt2.ToString());
        Console.WriteLine();
    }

    public static void Example4()
    {
        const int n = 3;
        var x = MathDf.X;
        var g = x.Sin().Power(n);
        var h = x.Power(2) + x - 3;

        var y = 3 * g * h * h.Power(2.5) * 1.2 * g.Power(3);
        var yDt1 = y.GetDerivative1();
        var yDt2 = y.GetDerivative2();

        Console.WriteLine(y.ToString());
        Console.WriteLine(yDt1.ToString());
        Console.WriteLine(yDt2.ToString());
        Console.WriteLine();
    }
}