using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using NumericalGeometryLib.BasicMath;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Interpolators
{
    public sealed class VectorFourierInterpolatorTerm
    {
        public IGeometricAlgebraProcessor<double> GeometricProcessor 
            => CosVector.GeometricProcessor;

        public GaVector<double> CosVector { get; private set; }

        public GaVector<double> SinVector { get; private set; }

        public double Frequency { get; }


        internal VectorFourierInterpolatorTerm([NotNull] GaVector<double> cosVector, [NotNull] GaVector<double> sinVector, [NotNull] double frequency)
        {
            CosVector = cosVector;
            SinVector = frequency >= 0 ? sinVector : -sinVector;
            Frequency = frequency.Abs();
        }


        internal VectorFourierInterpolatorTerm AddVectors([NotNull] GaVector<double> cosVector, [NotNull] GaVector<double> sinVector)
        {
            CosVector += cosVector;
            SinVector += sinVector;

            return this;
        }
        
        public GaVector<double> GetVector(double parameterValue)
        {
            var angle = Frequency * parameterValue;

            return CosVector * angle.Cos() + SinVector * angle.Sin();
        }
        
        public GaVector<double> GetVectorDt(double parameterValue, int degree = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return GetVector(parameterValue);

            var f = degree switch
            {
                1 => Frequency,
                2 => Frequency.Square(),
                3 => Frequency.Cube(),
                _ => Frequency.Power(degree)
            };

            var (cosVector, sinVector) = (degree % 4) switch
            {
                0 => new Pair<GaVector<double>>(f * CosVector, f * SinVector),
                1 => new Pair<GaVector<double>>(f * SinVector, -f * CosVector),
                2 => new Pair<GaVector<double>>(-f * CosVector, -f * SinVector),
                _ => new Pair<GaVector<double>>(-f * SinVector, f * CosVector)
            };

            var angle = Frequency * parameterValue;

            return cosVector * angle.Cos() + sinVector * angle.Sin();
        }

        public VectorFourierInterpolatorTerm GetTermDerivative(int degree = 1)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0)
                return this;

            var n = degree % 4;

            var f = degree switch
            {
                1 => Frequency,
                2 => Frequency.Square(),
                3 => Frequency.Cube(),
                _ => Frequency.Power(degree)
            };

            return n switch
            {
                0 => new VectorFourierInterpolatorTerm(
                    f * CosVector, 
                    f * SinVector, 
                    Frequency
                ),

                1 => new VectorFourierInterpolatorTerm(
                    f * SinVector, 
                    -f * CosVector, 
                    Frequency
                ),
                
                2 => new VectorFourierInterpolatorTerm(
                    -f * CosVector, 
                    -f * SinVector, 
                    Frequency
                ),
                
                _ => new VectorFourierInterpolatorTerm(
                    -f * SinVector, 
                    f * CosVector, 
                    Frequency
                )
            };
        }
    }
}