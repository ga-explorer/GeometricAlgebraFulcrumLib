using System;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Float32
{
    public sealed class GaScalarProcessorFloat32 
        : IGaScalarProcessor<float>
    {
        public float ZeroEpsilon { get; set; }
            = 1e-7f;


        public bool IsNumeric => true;

        public bool IsSymbolic => false;

        public float GetZeroScalar() => 0f;

        public float GetOneScalar() => 1f;

        public float GetMinusOneScalar() => -1f;

        public float GetPiScalar() => MathF.PI;
        public float[] GetZeroScalarArray1D(int count)
        {
            throw new NotImplementedException();
        }

        public float[,] GetZeroScalarArray2D(int count)
        {
            throw new NotImplementedException();
        }

        public float[,] GetZeroScalarArray2D(int count1, int count2)
        {
            throw new NotImplementedException();
        }


        public float Add(float scalar1, float scalar2)
        {
            return scalar1 + scalar2;
        }

        public float Subtract(float scalar1, float scalar2)
        {
            return scalar1 - scalar2;
        }

        public float Times(float scalar1, float scalar2)
        {
            return scalar1 * scalar2;
        }

        public float NegativeTimes(float scalar1, float scalar2)
        {
            return -scalar1 * scalar2;
        }

        public float Divide(float scalar1, float scalar2)
        {
            return scalar1 / scalar2;
        }

        public float NegativeDivide(float scalar1, float scalar2)
        {
            return -scalar1 / scalar2;
        }

        public float Positive(float scalar)
        {
            return scalar;
        }

        public float Negative(float scalar)
        {
            return -scalar;
        }

        public float Inverse(float scalar)
        {
            return 1f / scalar;
        }

        public float Abs(float scalar)
        {
            return MathF.Abs(scalar);
        }

        public float Sqrt(float scalar)
        {
            return MathF.Sqrt(scalar);
        }

        public float SqrtOfAbs(float scalar)
        {
            return MathF.Sqrt(MathF.Abs(scalar));
        }

        public float Exp(float scalar)
        {
            return MathF.Exp(scalar);
        }

        public float Log(float scalar)
        {
            return MathF.Log(scalar);
        }

        public float Log2(float scalar)
        {
            return MathF.Log2(scalar);
        }

        public float Log10(float scalar)
        {
            return MathF.Log10(scalar);
        }

        public float Log(float scalar, float baseScalar)
        {
            return MathF.Log(scalar, baseScalar);
        }

        public float Cos(float scalar)
        {
            return MathF.Cos(scalar);
        }

        public float Sin(float scalar)
        {
            return MathF.Sin(scalar);
        }

        public float Tan(float scalar)
        {
            return MathF.Tan(scalar);
        }

        public float ArcCos(float scalar)
        {
            return MathF.Acos(scalar);
        }

        public float ArcSin(float scalar)
        {
            return MathF.Asin(scalar);
        }

        public float ArcTan(float scalar)
        {
            return MathF.Atan(scalar);
        }

        public float ArcTan2(float scalarX, float scalarY)
        {
            return MathF.Atan2(scalarY, scalarX);
        }

        public float Cosh(float scalar)
        {
            return MathF.Cosh(scalar);
        }

        public float Sinh(float scalar)
        {
            return MathF.Sinh(scalar);
        }

        public float Tanh(float scalar)
        {
            return MathF.Tanh(scalar);
        }

        public bool IsValid(float scalar)
        {
            return !float.IsNaN(scalar);
        }

        public bool IsZero(float scalar)
        {
            return scalar == 0f;
        }

        public bool IsZero(float scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar > -ZeroEpsilon && scalar < ZeroEpsilon
                : scalar == 0f;
        }

        public bool IsNearZero(float scalar)
        {
            return scalar > -ZeroEpsilon && scalar < ZeroEpsilon;
        }

        public bool IsNotZero(float scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotZero(float scalar, bool nearZeroFlag)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNearZero(float scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsPositive(float scalar)
        {
            return scalar > 0;
        }

        public bool IsNegative(float scalar)
        {
            return scalar < 0;
        }

        public bool IsNotPositive(float scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNegative(float scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNearPositive(float scalar)
        {
            return scalar < -ZeroEpsilon;
        }

        public bool IsNotNearNegative(float scalar)
        {
            return scalar > ZeroEpsilon;
        }

        public float TextToScalar(string text)
        {
            return float.Parse(text);
        }

        public float IntegerToScalar(int value)
        {
            return value;
        }

        public float Float64ToScalar(double value)
        {
            return (float) value;
        }

        public float GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            return (float) (minValue + (maxValue - minValue) * randomGenerator.NextDouble());
        }

        public string ToText(float scalar)
        {
            return scalar.ToString("G");
        }
    }
}