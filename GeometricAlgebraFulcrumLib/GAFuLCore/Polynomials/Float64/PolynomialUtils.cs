namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64;

public static class PolynomialUtils
{
    public static double NewtonCotes(Func<double, double> func, double a, double b, int n, int m)
    {
        var sum = 0d;
        var d = (b - a) / m;

        var a1 = a;
        var b1 = a + d;
        for (var i = 0; i < m; i++)
        {
            sum += n switch
            {
                1 => NewtonCotes1(func, a1, b1),
                2 => NewtonCotes2(func, a1, b1),
                3 => NewtonCotes3(func, a1, b1),
                4 => NewtonCotes4(func, a1, b1),
                5 => NewtonCotes5(func, a1, b1),
                6 => NewtonCotes6(func, a1, b1),
                _ => throw new ArgumentOutOfRangeException(nameof(n))
            };

            a1 = b1;
            b1 += d;
        }

        return sum;
    }

    public static double NewtonCotes1(Func<double, double> func, double a, double b)
    {
        var h = b - a;

        return (
            func(a) +
            func(b)
        ) * h / 2d;
    }

    public static double NewtonCotes2(Func<double, double> func, double a, double b)
    {
        var h = (b - a) / 2d;

        return (
            func(a) +
            4d * func(a + h) +
            func(b)
        ) * h / 3d;
    }

    public static double NewtonCotes3(Func<double, double> func, double a, double b)
    {
        var h = (b - a) / 3d;

        return (
            func(a) +
            3d * func(a + h) +
            3d * func(b - h) +
            func(b)
        ) * h * 3d / 8d;
    }

    public static double NewtonCotes4(Func<double, double> func, double a, double b)
    {
        var h = (b - a) / 4d;

        return (
            7d * func(a) +
            32d * func(a + h) +
            12d * func(a + 2d * h) +
            32d * func(b - h) +
            7d * func(b)
        ) * h * 2d / 45d;
    }

    public static double NewtonCotes5(Func<double, double> func, double a, double b)
    {
        var h = (b - a) / 5d;

        return (
            19d * func(a) +
            75d * func(a + h) +
            50d * func(a + 2d * h) +
            50d * func(b - 2d * h) +
            75d * func(b - h) +
            19d * func(b)
        ) * h * 5d / 288d;
    }

    public static double NewtonCotes6(Func<double, double> func, double a, double b)
    {
        var h = (b - a) / 6d;

        return (
            41d * func(a) +
            216d * func(a + h) +
            27d * func(a + 2 * h) +
            272d * func(a + 3 * h) +
            27d * func(b - 2 * h) +
            216d * func(b - h) +
            41d * func(b)
        ) * h / 140d;
    }

}