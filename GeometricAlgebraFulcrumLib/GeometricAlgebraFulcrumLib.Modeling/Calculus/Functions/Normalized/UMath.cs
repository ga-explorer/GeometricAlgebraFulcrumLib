using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Normalized
{
    /// <summary>
    /// Normalized domain mathematical functions
    /// Any function is assumed to have a real input in the range [-1, 1] and must
    /// produce an output in the same range
    /// </summary>
    public static class UMath
    {
        public static double Clamp(double x)
        {
            return x switch
            {
                < -1 => -1,
                > 1 => 1,
                _ => x
            };
        }

        public static double Identity(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return x;
        }

        public static double Negative(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return -x;
        }

        public static double Reciprocal(double x)
        {
            const double zeroEpsilon = 1000;

            Debug.Assert(x is >= -1 and <= 1);

            var z = zeroEpsilon * x;

            if (z is >= -1 or <= 1) return z;

            return 1 / z;
        }

        public static double Abs(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return Math.Abs(x);
        }
        
        public static double Square(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return x * x;
        }
        
        public static double Cube(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return x * x * x;
        }
        
        public static double Sqrt(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return x >= 0 ? Math.Sqrt(x) : -Math.Sqrt(-x);
        }
        
        public static double Cbrt(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return Math.Cbrt(x);
        }

        public static double Cos(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);
            
            return Math.Cos(x * Math.PI);
        }
        
        public static double Sin(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);
            
            return Math.Sin(x * Math.PI);
        }
        
        public static double Tan(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);
            
            return Math.Tan(x * Math.PI / 4);
        }

        public static double Exp(double x)
        {
            Debug.Assert(x is >= -1 and <= 1);

            return Math.Exp(x) / Math.E;
        }
        
        public static double Mean(double x, double y)
        {
            Debug.Assert(x is >= -1 and <= 1);
            Debug.Assert(y is >= -1 and <= 1);

            return (x + y) * 0.5;
        }
        
        //public static double Subtract(double x, double y)
        //{
        //    return Add(x, -y);
        //}

        public static double Times(double x, double y)
        {
            Debug.Assert(x is >= -1 and <= 1);
            Debug.Assert(y is >= -1 and <= 1);

            return x * y;
        }
        
        //public static double TimesReciprocal(double x, double y)
        //{
        //    return Times(x, Reciprocal(y));
        //}

    }
}
