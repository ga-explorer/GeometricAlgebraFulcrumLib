using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Curves.Harmonic
{
    public sealed class HarmonicCurveTerm3D
    {
        public int HarmonicFactor { get; }

        public Float64Vector3D MagnitudeVector { get; }


        internal HarmonicCurveTerm3D(int harmonicFactor, Float64Vector3D magnitudeVector)
        {
            if (harmonicFactor < 1)
                throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

            if (!magnitudeVector.IsValid())
                throw new ArgumentException(nameof(magnitudeVector));

            HarmonicFactor = harmonicFactor;
            MagnitudeVector = magnitudeVector;
        }


        internal Float64Vector3D GetPoint(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return Float64Vector3D.Create(MagnitudeVector.X * (w * parameterValue).Cos(),
                MagnitudeVector.Y * (w * (parameterValue + 1d / 3d)).Cos(),
                MagnitudeVector.Z * (w * (parameterValue - 1d / 3d)).Cos());
        }

        internal Float64Vector3D GetTangent(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;

            return Float64Vector3D.Create(-MagnitudeVector.X * w * (w * parameterValue).Sin(),
                -MagnitudeVector.Y * w * (w * (parameterValue + 1d / 3d)).Sin(),
                -MagnitudeVector.Z * w * (w * (parameterValue - 1d / 3d)).Sin());
        }

        internal Float64Vector3D GetSecondDerivative(double parameterValue)
        {
            var w = 2d * Math.PI * HarmonicFactor;
            var w2 = w * w;

            return Float64Vector3D.Create(-MagnitudeVector.X * w2 * (w * parameterValue).Cos(),
                -MagnitudeVector.Y * w2 * (w * (parameterValue + 1d / 3d)).Cos(),
                -MagnitudeVector.Z * w2 * (w * (parameterValue - 1d / 3d)).Cos());
        }
    }
}