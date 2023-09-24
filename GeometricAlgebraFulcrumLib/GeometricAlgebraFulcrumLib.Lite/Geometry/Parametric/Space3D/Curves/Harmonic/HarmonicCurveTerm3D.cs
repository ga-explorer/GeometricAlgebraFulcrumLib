using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Harmonic
{
    public sealed class HarmonicCurveTerm3D
    {
        public int HarmonicFactor { get; }

        public Float64Vector3D MagnitudeVector { get; }

        public Float64Vector3D ParameterShiftVector { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HarmonicCurveTerm3D(int harmonicFactor, Float64Vector3D magnitudeVector)
            : this(
                harmonicFactor, 
                magnitudeVector, 
                Float64Vector3D.Create(0, 1/3d, -1/3d)
            )
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HarmonicCurveTerm3D(int harmonicFactor, Float64Vector3D magnitudeVector, Float64Vector3D parameterShiftVector)
        {
            if (harmonicFactor < 1)
                throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

            if (!magnitudeVector.IsValid())
                throw new ArgumentException(nameof(magnitudeVector));

            HarmonicFactor = harmonicFactor;
            MagnitudeVector = magnitudeVector;
            ParameterShiftVector = parameterShiftVector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Vector3D GetPoint(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return Float64Vector3D.Create(
                MagnitudeVector.X * (w * (parameterValue + ParameterShiftVector.X)).Cos(),
                MagnitudeVector.Y * (w * (parameterValue + ParameterShiftVector.Y)).Cos(),
                MagnitudeVector.Z * (w * (parameterValue + ParameterShiftVector.Z)).Cos()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Vector3D GetTangent(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return Float64Vector3D.Create(
                -MagnitudeVector.X * w * (w * (parameterValue + ParameterShiftVector.X)).Sin(),
                -MagnitudeVector.Y * w * (w * (parameterValue + ParameterShiftVector.Y)).Sin(),
                -MagnitudeVector.Z * w * (w * (parameterValue + ParameterShiftVector.Z)).Sin()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Float64Vector3D GetSecondDerivative(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;
            var w2 = w * w;

            return Float64Vector3D.Create(
                -MagnitudeVector.X * w2 * (w * (parameterValue + ParameterShiftVector.X)).Cos(),
                -MagnitudeVector.Y * w2 * (w * (parameterValue + ParameterShiftVector.Y)).Cos(),
                -MagnitudeVector.Z * w2 * (w * (parameterValue + ParameterShiftVector.Z)).Cos()
            );
        }
    }
}