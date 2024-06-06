using System.Numerics;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Applications.Robotics;

public static class ComplexSnelliusPothenotProblem
{
    public static void BenchmarkComplexTriangulation()
    {
        var x1 = -4d; 
        var y1 = 3d;

        var x2 = 0d;
        var y2 = 5d;

        var x3 = 7d;
        var y3 = 4d;

        var alpha1 = 33 * Math.PI / 180;
        var alpha2 = 56 * Math.PI / 180;
        var alpha3 = 29 * Math.PI / 180;

        double x, y;

        ComplexTriangulation(
            out x, out y, 
            alpha1, alpha2, alpha3,
            x1, y1,
            x2, y2,
            x3, y3
        );
            
        var time1 = DateTime.Now;
        for (var i = 0; i < 1000000; i++)
        {
            ComplexTriangulation(
                out x, out y, 
                alpha1, alpha2, alpha3,
                x1, y1,
                x2, y2,
                x3, y3
            );
        }
        var time2 = DateTime.Now;

        Console.WriteLine(time2 - time1);

    }

    public static void ComplexTriangulation(out double x, out double y, double alpha1, double alpha2, double alpha3, double x1, double y1, double x2, double y2, double x3, double y3)
    {
        var i = Complex.ImaginaryOne;

        var alpha = alpha1 - alpha3;
        var beta  = alpha3 - alpha2;

        var pointA = new Complex(x1, y1);
        var pointB = new Complex(x2, y2);
        var pointC = new Complex(x3, y3);

        var v1 = pointA - pointB;
        var v2 = pointC - pointB;
        var d1 = v1 + v1 / Math.Tan(alpha) * i;
        var d2 = v2 - v2 / Math.Tan(beta) * i;
        var d = d2 - d1;
            
        var solution = -0.5 * (d1.Conjugate() * d2 - d1 * d2.Conjugate()) / d.Conjugate() + pointB;

        x = solution.Real;
        y = solution.Imaginary;
    }
}