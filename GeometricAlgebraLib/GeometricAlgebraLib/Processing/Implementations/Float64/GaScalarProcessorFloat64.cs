using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Implementations.Float64
{
    public sealed class GaScalarProcessorFloat64 
        : IGaScalarProcessor<double>
    {
        public static GaScalarProcessorFloat64 DefaultProcessor { get; }
            = new();

        public double ZeroEpsilon { get; set; }
            = 1e-13d;

        
        public double ZeroScalar => 0d;
        
        public double OneScalar => 1d;

        public double MinusOneScalar => -1d;

        public double PiScalar => Math.PI;


        //Will this be better? [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double scalar1, double scalar2)
        {
            return scalar1 + scalar2;
        }

        public double Add(params double[] scalarsList)
        {
            return scalarsList.Sum();
        }

        public double Add(IEnumerable<double> scalarsList)
        {
            return scalarsList.Sum();
        }

        public double Subtract(double scalar1, double scalar2)
        {
            return scalar1 - scalar2;
        }

        public double Times(double scalar1, double scalar2)
        {
            return scalar1 * scalar2;
        }

        public double Times(params double[] scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        public double Times(IEnumerable<double> scalarsList)
        {
            return scalarsList.Aggregate(
                1d, 
                (current, scalar) => current * scalar
            );
        }

        public double NegativeTimes(double scalar1, double scalar2)
        {
            return -scalar1 * scalar2;
        }

        public double NegativeTimes(params double[] scalarsList)
        {
            return scalarsList.Aggregate(
                -1d, 
                (current, scalar) => current * scalar
            );
        }

        public double NegativeTimes(IEnumerable<double> scalarsList)
        {
            return scalarsList.Aggregate(
                -1d, 
                (current, scalar) => current * scalar
            );
        }

        public double Divide(double scalar1, double scalar2)
        {
            return scalar1 / scalar2;
        }

        public double NegativeDivide(double scalar1, double scalar2)
        {
            return -scalar1 / scalar2;
        }

        public double Positive(double scalar)
        {
            return scalar;
        }

        public double Negative(double scalar)
        {
            return -scalar;
        }

        public double Inverse(double scalar)
        {
            return 1d / scalar;
        }

        public double Abs(double scalar)
        {
            return Math.Abs(scalar);
        }

        public double Sqrt(double scalar)
        {
            return Math.Sqrt(scalar);
        }

        public double SqrtOfAbs(double scalar)
        {
            return Math.Sqrt(Math.Abs(scalar));
        }

        public double Exp(double scalar)
        {
            return Math.Exp(scalar);
        }

        public double Log(double scalar)
        {
            return Math.Log(scalar);
        }

        public double Log2(double scalar)
        {
            return Math.Log2(scalar);
        }

        public double Log10(double scalar)
        {
            return Math.Log10(scalar);
        }

        public double Log(double scalar, double baseScalar)
        {
            return Math.Log(scalar, baseScalar);
        }

        public double Cos(double scalar)
        {
            return Math.Cos(scalar);
        }

        public double Sin(double scalar)
        {
            return Math.Sin(scalar);
        }

        public double Tan(double scalar)
        {
            return Math.Tan(scalar);
        }

        public double ArcCos(double scalar)
        {
            return Math.Acos(scalar);
        }

        public double ArcSin(double scalar)
        {
            return Math.Asin(scalar);
        }

        public double ArcTan(double scalar)
        {
            return Math.Atan(scalar);
        }

        public double ArcTan2(double scalarX, double scalarY)
        {
            return Math.Atan2(scalarY, scalarX);
        }

        public bool IsValid(double scalar)
        {
            return !double.IsNaN(scalar);
        }

        public bool IsZero(double scalar)
        {
            return scalar == 0d;
        }

        public bool IsZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0d;
        }

        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        public double TextToScalar(string text)
        {
            return double.Parse(text);
        }

        public double IntegerToScalar(int value)
        {
            return value;
        }

        public double Float64ToScalar(double value)
        {
            return value;
        }

        public double GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        public string ToText(double scalar)
        {
            return scalar.ToString("G");
        }
    }
}