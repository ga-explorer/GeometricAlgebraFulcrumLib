using System;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Float64
{
    public sealed class GaScalarProcessorFloat64 
        : IGaScalarProcessor<double>
    {
        public static GaScalarProcessorFloat64 DefaultProcessor { get; }
            = new GaScalarProcessorFloat64();


        public double ZeroEpsilon { get; set; }
            = 1e-13d;


        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetZeroScalar()
        {
            return 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetOneScalar()
        {
            return 1d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetMinusOneScalar()
        {
            return -1d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetPiScalar()
        {
            return Math.PI;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetZeroScalarArray1D(int count)
        {
            return new double[count];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetZeroScalarArray2D(int count)
        {
            return new double[count, count];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetZeroScalarArray2D(int count1, int count2)
        {
            return new double[count1, count2];
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Add(double scalar1, double scalar2)
        {
            return scalar1 + scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Subtract(double scalar1, double scalar2)
        {
            return scalar1 - scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Times(double scalar1, double scalar2)
        {
            return scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeTimes(double scalar1, double scalar2)
        {
            return -scalar1 * scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Divide(double scalar1, double scalar2)
        {
            return scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double NegativeDivide(double scalar1, double scalar2)
        {
            return -scalar1 / scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Positive(double scalar)
        {
            return scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Negative(double scalar)
        {
            return -scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Inverse(double scalar)
        {
            return 1d / scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Abs(double scalar)
        {
            return Math.Abs(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sqrt(double scalar)
        {
            return Math.Sqrt(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double SqrtOfAbs(double scalar)
        {
            return Math.Sqrt(Math.Abs(scalar));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Exp(double scalar)
        {
            return Math.Exp(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double scalar)
        {
            return Math.Log(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log2(double scalar)
        {
            return Math.Log2(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log10(double scalar)
        {
            return Math.Log10(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Log(double scalar, double baseScalar)
        {
            return Math.Log(scalar, baseScalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cos(double scalar)
        {
            return Math.Cos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sin(double scalar)
        {
            return Math.Sin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tan(double scalar)
        {
            return Math.Tan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcCos(double scalar)
        {
            return Math.Acos(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcSin(double scalar)
        {
            return Math.Asin(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan(double scalar)
        {
            return Math.Atan(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ArcTan2(double scalarX, double scalarY)
        {
            return Math.Atan2(scalarY, scalarX);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cosh(double scalar)
        {
            return Math.Cosh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sinh(double scalar)
        {
            return Math.Sinh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tanh(double scalar)
        {
            return Math.Tanh(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid(double scalar)
        {
            return !double.IsNaN(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar)
        {
            return scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero(double scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        public bool IsNotZero(double scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotZero(double scalar, bool nearZeroFlag)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNearZero(double scalar)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double TextToScalar(string text)
        {
            return double.Parse(text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double IntegerToScalar(int value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Float64ToScalar(double value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return minValue + (maxValue - minValue) * randomGenerator.NextDouble();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToText(double scalar)
        {
            return scalar.ToString("G");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPositive(double scalar)
        {
            return scalar > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNegative(double scalar)
        {
            return scalar < 0;
        }

        public bool IsNotPositive(double scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNegative(double scalar)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearPositive(double scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNotNearNegative(double scalar)
        {
            return scalar > ZeroEpsilon;
        }
    }
}