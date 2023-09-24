using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.Harmonic
{
    public sealed class HarmonicParametricScalarTerm
    {
        public int HarmonicFactor { get; }

        public Float64Scalar Magnitude { get; }

        public Float64Scalar ParameterShift { get; }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HarmonicParametricScalarTerm(int harmonicFactor, Float64Scalar magnitude)
            : this(
                harmonicFactor, 
                magnitude, 
                Float64Scalar.Zero
            )
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HarmonicParametricScalarTerm(int harmonicFactor, Float64Scalar magnitudeVector, Float64Scalar parameterShift)
        {
            if (harmonicFactor < 1)
                throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

            if (!magnitudeVector.IsValid())
                throw new ArgumentException(nameof(magnitudeVector));

            HarmonicFactor = harmonicFactor;
            Magnitude = magnitudeVector;
            ParameterShift = parameterShift;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Scalar GetPoint(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return Magnitude * (w * (parameterValue + ParameterShift)).Cos();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Scalar GetTangent(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return -Magnitude * w * (w * (parameterValue + ParameterShift)).Sin();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Scalar GetSecondDerivative(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;
            var w2 = w * w;

            return -Magnitude * w2 * (w * (parameterValue + ParameterShift)).Cos();
        }
    }
}