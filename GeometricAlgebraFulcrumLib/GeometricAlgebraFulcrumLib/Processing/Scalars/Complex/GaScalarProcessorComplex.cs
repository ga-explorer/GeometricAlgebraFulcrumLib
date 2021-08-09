using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars.Complex
{
    public sealed class GaScalarProcessorComplex
        : IGaScalarProcessor<System.Numerics.Complex>
    {
        public double ZeroEpsilon { get; set; }
            = 1e-13d;


        public bool IsNumeric 
            => true;

        public bool IsSymbolic 
            => false;

        public System.Numerics.Complex ZeroScalar { get; } = System.Numerics.Complex.Zero;

        public System.Numerics.Complex OneScalar { get; } = System.Numerics.Complex.One;

        public System.Numerics.Complex MinusOneScalar { get; } = -System.Numerics.Complex.One;

        public System.Numerics.Complex PiScalar { get; } = Math.PI;


        public System.Numerics.Complex Add(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return scalar1 + scalar2;
        }

        public System.Numerics.Complex Add(params System.Numerics.Complex[] scalarsList)
        {
            var real = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Real
            );
            
            var imaginary = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Imaginary
            );
            
            return new System.Numerics.Complex(real, imaginary);
        }

        public System.Numerics.Complex Add(IEnumerable<System.Numerics.Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                System.Numerics.Complex.Zero, 
                (current, s) => current + s
            );
        }

        public System.Numerics.Complex Subtract(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return scalar1 - scalar2;
        }

        public System.Numerics.Complex Times(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return scalar1 * scalar2;
        }

        public System.Numerics.Complex Times(params System.Numerics.Complex[] scalarsList)
        {
            return scalarsList.Aggregate(
                System.Numerics.Complex.One, 
                (current, s) => current * s
            );
        }

        public System.Numerics.Complex Times(IEnumerable<System.Numerics.Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                System.Numerics.Complex.One, 
                (current, s) => current * s
            );
        }

        public System.Numerics.Complex NegativeTimes(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return -scalar1 * scalar2;
        }

        public System.Numerics.Complex NegativeTimes(params System.Numerics.Complex[] scalarsList)
        {
            return scalarsList.Aggregate(
                -System.Numerics.Complex.One, 
                (current, s) => current * s
            );
        }

        public System.Numerics.Complex NegativeTimes(IEnumerable<System.Numerics.Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                -System.Numerics.Complex.One, 
                (current, s) => current * s
            );
        }

        public System.Numerics.Complex Divide(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return scalar1 / scalar2;
        }

        public System.Numerics.Complex NegativeDivide(System.Numerics.Complex scalar1, System.Numerics.Complex scalar2)
        {
            return -scalar1 / scalar2;
        }

        public System.Numerics.Complex Positive(System.Numerics.Complex scalar)
        {
            return scalar;
        }

        public System.Numerics.Complex Negative(System.Numerics.Complex scalar)
        {
            return -scalar;
        }

        public System.Numerics.Complex Inverse(System.Numerics.Complex scalar)
        {
            return 1d / scalar;
        }

        public System.Numerics.Complex Abs(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Abs(scalar);
        }

        public System.Numerics.Complex Sqrt(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Sqrt(scalar);
        }

        public System.Numerics.Complex SqrtOfAbs(System.Numerics.Complex scalar)
        {
            return Math.Sqrt(System.Numerics.Complex.Abs(scalar));
        }

        public System.Numerics.Complex Exp(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Exp(scalar);
        }

        public System.Numerics.Complex Log(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Log(scalar);
        }

        public System.Numerics.Complex Log2(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Log(scalar, 2d);
        }

        public System.Numerics.Complex Log10(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Log10(scalar);
        }

        public System.Numerics.Complex Log(System.Numerics.Complex scalar, System.Numerics.Complex baseScalar)
        {
            return System.Numerics.Complex.Log(scalar, baseScalar.Real);
        }

        public System.Numerics.Complex Cos(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Cos(scalar);
        }

        public System.Numerics.Complex Sin(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Sin(scalar);
        }

        public System.Numerics.Complex Tan(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Tan(scalar);
        }

        public System.Numerics.Complex ArcCos(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Acos(scalar);
        }

        public System.Numerics.Complex ArcSin(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Asin(scalar);
        }

        public System.Numerics.Complex ArcTan(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Atan(scalar);
        }

        public System.Numerics.Complex ArcTan2(System.Numerics.Complex scalarX, System.Numerics.Complex scalarY)
        {
            throw new NotImplementedException();
        }

        public System.Numerics.Complex Cosh(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Cosh(scalar);
        }

        public System.Numerics.Complex Sinh(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Sinh(scalar);
        }

        public System.Numerics.Complex Tanh(System.Numerics.Complex scalar)
        {
            return System.Numerics.Complex.Tanh(scalar);
        }

        public bool IsValid(System.Numerics.Complex scalar)
        {
            return !scalar.IsNaN();
        }

        public bool IsZero(System.Numerics.Complex scalar)
        {
            return scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsZero(System.Numerics.Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon && 
                  scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon
                : scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsNearZero(System.Numerics.Complex scalar)
        {
            return scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon &&
                   scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon;
        }

        public bool IsPositive(System.Numerics.Complex scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNegative(System.Numerics.Complex scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNearPositive(System.Numerics.Complex scalar)
        {
            throw new NotImplementedException();
        }

        public bool IsNotNearNegative(System.Numerics.Complex scalar)
        {
            throw new NotImplementedException();
        }

        public System.Numerics.Complex TextToScalar(string text)
        {
            throw new NotImplementedException();
        }

        public System.Numerics.Complex IntegerToScalar(int value)
        {
            return new System.Numerics.Complex(value, 0);
        }

        public System.Numerics.Complex Float64ToScalar(double value)
        {
            return new System.Numerics.Complex(value, 0d);
        }

        public System.Numerics.Complex GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            var magnitude = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            var angle = 
                2d * Math.PI * randomGenerator.NextDouble();

            return System.Numerics.Complex.FromPolarCoordinates(magnitude, angle);
        }

        public string ToText(System.Numerics.Complex scalar)
        {
            return scalar.ToString("G");
        }
    }
}