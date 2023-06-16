using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Maps;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.PhCurves
{
    public sealed class PhCurve2DDegree5
    {
        public static PhCurve2DDegree5 Create(IFloat64Tuple2D point0, IFloat64Tuple2D tangent0, IFloat64Tuple2D point1, IFloat64Tuple2D tangent1)
        {
            return new PhCurve2DDegree5(point0, tangent0, point1, tangent1);
        }


        public Float64Vector2D Point0 { get; }

        public Float64Vector2D Point1 { get; }

        public Float64Vector2D Tangent0 { get; }

        public Float64Vector2D Tangent1 { get; }

        public double TangentLength0 { get; }

        public GaScaledPureRotor ScaledRotor { get; }

        public PhCurve2DDegree5Canonical CanonicalCurve { get; }


        private PhCurve2DDegree5(IFloat64Tuple2D point0, IFloat64Tuple2D tangent0, IFloat64Tuple2D point1, IFloat64Tuple2D tangent1)
        {
            Point0 = point0.ToLinVector2D();
            Point1 = point1.ToLinVector2D();
            Tangent0 = tangent0.ToLinVector2D();
            Tangent1 = tangent1.ToLinVector2D();
            TangentLength0 = Tangent0.ENorm();

            ScaledRotor = Float64Vector2D.E1.CreateEuclideanScaledPureRotor(tangent0);

            var scaledRotorInv = ScaledRotor.GetPureScaledRotorInverse();

            CanonicalCurve = PhCurve2DDegree5Canonical.Create(
                scaledRotorInv.OmMap(Point1 - Point0),
                scaledRotorInv.OmMap(Tangent1)
            );
        }


        public Float64Vector2D GetHodographPoint(double parameterValue)
        {
            return ScaledRotor.OmMap(
                CanonicalCurve.GetHodographPoint(parameterValue)
            );
        }

        public Float64Vector2D GetCurvePoint(double parameterValue)
        {
            return Point0 + ScaledRotor.OmMap(
                CanonicalCurve.GetCurvePoint(parameterValue)
            );
        }

        public double GetSigmaValue(double parameterValue)
        {
            return CanonicalCurve.GetSigmaValue(parameterValue) * TangentLength0;
        }

        public double GetLength(double parameterValue)
        {
            return CanonicalCurve.GetLength(parameterValue) * TangentLength0;
        }

        public double GetLength(double parameterValue1, double parameterValue2)
        {
            return CanonicalCurve.GetLength(parameterValue1, parameterValue2) * TangentLength0;
        }

        public double GetLength()
        {
            return CanonicalCurve.GetLength() * TangentLength0;
        }
    }
}