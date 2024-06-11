//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples.Immutable;

//namespace GeometricAlgebraFulcrumLib.Algebra.Parametric.Space2D.Curves.Harmonic
//{
//    public sealed class HarmonicCurveTerm2D
//    {
//        public int HarmonicFactor { get; }

//        public Float64Tuple2D MagnitudeVector { get; }


//        internal HarmonicCurveTerm2D(int harmonicFactor, Float64Tuple2D magnitudeVector)
//        {
//            if (harmonicFactor < 1)
//                throw new ArgumentOutOfRangeException(nameof(harmonicFactor));

//            if (!magnitudeVector.IsValid())
//                throw new ArgumentException(nameof(magnitudeVector));

//            HarmonicFactor = harmonicFactor;
//            MagnitudeVector = magnitudeVector;
//        }


//        internal Float64Tuple2D GetPoint(double parameterValue)
//        {
//            var w = 2d * Math.PI * HarmonicFactor;

//            return new Float64Tuple2D(
//                MagnitudeVector.X * (w * parameterValue).Cos(),
//                MagnitudeVector.Y * (w * (parameterValue + 1d / 3d)).Cos(),
//                MagnitudeVector.Z * (w * (parameterValue - 1d / 3d)).Cos()
//            );
//        }

//        internal Float64Tuple2D GetTangent(double parameterValue)
//        {
//            var w = 2d * Math.PI * HarmonicFactor;

//            return new Float64Tuple2D(
//                -MagnitudeVector.X * w * (w * parameterValue).Sin(),
//                -MagnitudeVector.Y * w * (w * (parameterValue + 1d / 3d)).Sin(),
//                -MagnitudeVector.Z * w * (w * (parameterValue - 1d / 3d)).Sin()
//            );
//        }

//        internal Float64Tuple2D GetSecondDerivative(double parameterValue)
//        {
//            var w = 2d * Math.PI * HarmonicFactor;
//            var w2 = w * w;

//            return new Float64Tuple2D(
//                -MagnitudeVector.X * w2 * (w * parameterValue).Cos(),
//                -MagnitudeVector.Y * w2 * (w * (parameterValue + 1d / 3d)).Cos(),
//                -MagnitudeVector.Z * w2 * (w * (parameterValue - 1d / 3d)).Cos()
//            );
//        }
//    }
//}