using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Processing.Scalars
{
    public sealed class GaScalarProcessorComplex
        : IGaScalarProcessor<Complex>
    {
        public double ZeroEpsilon { get; set; }
            = 1e-13d;


        public Complex ZeroScalar { get; } = Complex.Zero;

        public Complex OneScalar { get; } = Complex.One;

        public Complex MinusOneScalar { get; } = -Complex.One;

        public Complex PiScalar { get; } = Math.PI;


        public Complex Add(Complex scalar1, Complex scalar2)
        {
            return scalar1 + scalar2;
        }

        public Complex Add(params Complex[] scalarsList)
        {
            var real = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Real
            );
            
            var imaginary = scalarsList.Aggregate(
                0d, 
                (current, s) => current + s.Imaginary
            );
            
            return new Complex(real, imaginary);
        }

        public Complex Add(IEnumerable<Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.Zero, 
                (current, s) => current + s
            );
        }

        public Complex Subtract(Complex scalar1, Complex scalar2)
        {
            return scalar1 - scalar2;
        }

        public Complex Times(Complex scalar1, Complex scalar2)
        {
            return scalar1 * scalar2;
        }

        public Complex Times(params Complex[] scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex Times(IEnumerable<Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex NegativeTimes(Complex scalar1, Complex scalar2)
        {
            return -scalar1 * scalar2;
        }

        public Complex NegativeTimes(params Complex[] scalarsList)
        {
            return scalarsList.Aggregate(
                -Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex NegativeTimes(IEnumerable<Complex> scalarsList)
        {
            return scalarsList.Aggregate(
                -Complex.One, 
                (current, s) => current * s
            );
        }

        public Complex Divide(Complex scalar1, Complex scalar2)
        {
            return scalar1 / scalar2;
        }

        public Complex NegativeDivide(Complex scalar1, Complex scalar2)
        {
            return -scalar1 / scalar2;
        }

        public Complex Positive(Complex scalar)
        {
            return scalar;
        }

        public Complex Negative(Complex scalar)
        {
            return -scalar;
        }

        public Complex Inverse(Complex scalar)
        {
            return 1d / scalar;
        }

        public Complex Abs(Complex scalar)
        {
            return Complex.Abs(scalar);
        }

        public Complex Sqrt(Complex scalar)
        {
            return Complex.Sqrt(scalar);
        }

        public Complex SqrtOfAbs(Complex scalar)
        {
            return Math.Sqrt(Complex.Abs(scalar));
        }

        public Complex Exp(Complex scalar)
        {
            return Complex.Exp(scalar);
        }

        public Complex Log(Complex scalar)
        {
            return Complex.Log(scalar);
        }

        public Complex Log2(Complex scalar)
        {
            return Complex.Log(scalar, 2d);
        }

        public Complex Log10(Complex scalar)
        {
            return Complex.Log10(scalar);
        }

        public Complex Log(Complex scalar, Complex baseScalar)
        {
            return Complex.Log(scalar, baseScalar.Real);
        }

        public Complex Cos(Complex scalar)
        {
            return Complex.Cos(scalar);
        }

        public Complex Sin(Complex scalar)
        {
            return Complex.Sin(scalar);
        }

        public Complex Tan(Complex scalar)
        {
            return Complex.Tan(scalar);
        }

        public Complex ArcCos(Complex scalar)
        {
            return Complex.Acos(scalar);
        }

        public Complex ArcSin(Complex scalar)
        {
            return Complex.Asin(scalar);
        }

        public Complex ArcTan(Complex scalar)
        {
            return Complex.Atan(scalar);
        }

        public Complex ArcTan2(Complex scalarX, Complex scalarY)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(Complex scalar)
        {
            return !scalar.IsNaN();
        }

        public bool IsZero(Complex scalar)
        {
            return scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsZero(Complex scalar, bool nearZeroFlag)
        {
            return nearZeroFlag
                ? scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon && 
                  scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon
                : scalar.Real == 0d && scalar.Imaginary == 0d;
        }

        public bool IsNearZero(Complex scalar)
        {
            return scalar.Real > -ZeroEpsilon && scalar.Real < ZeroEpsilon &&
                   scalar.Imaginary > -ZeroEpsilon && scalar.Imaginary < ZeroEpsilon;
        }

        public Complex TextToScalar(string text)
        {
            throw new NotImplementedException();
        }

        public Complex IntegerToScalar(int value)
        {
            return new(value, 0);
        }

        public Complex Float64ToScalar(double value)
        {
            return new Complex(value, 0d);
        }

        public Complex GetRandomScalar(System.Random randomGenerator, double minValue, double maxValue)
        {
            var magnitude = 
                minValue + (maxValue - minValue) * randomGenerator.NextDouble();

            var angle = 
                2d * Math.PI * randomGenerator.NextDouble();

            return Complex.FromPolarCoordinates(magnitude, angle);
        }

        public string ToText(Complex scalar)
        {
            return scalar.ToString("G");
        }
    }
}