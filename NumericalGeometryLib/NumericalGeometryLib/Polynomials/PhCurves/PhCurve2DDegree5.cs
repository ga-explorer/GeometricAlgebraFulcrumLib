using System.Diagnostics.CodeAnalysis;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.GeometricAlgebra.Maps;

namespace NumericalGeometryLib.Polynomials.PhCurves
{
    public sealed class PhCurve2DDegree5
    {
        public static PhCurve2DDegree5 Create(ITuple2D point0, ITuple2D tangent0, ITuple2D point1, ITuple2D tangent1)
        {
            return new PhCurve2DDegree5(point0, tangent0, point1, tangent1);
        }


        public Tuple2D Point0 { get; }

        public Tuple2D Point1 { get; }

        public Tuple2D Tangent0 { get; }

        public Tuple2D Tangent1 { get; }

        public double TangentLength0 { get; }

        public GaScaledPureRotor ScaledRotor { get; }

        public PhCurve2DDegree5Canonical CanonicalCurve { get; }


        private PhCurve2DDegree5([NotNull] ITuple2D point0, [NotNull] ITuple2D tangent0, [NotNull] ITuple2D point1, [NotNull] ITuple2D tangent1)
        {
            Point0 = point0.ToTuple2D();
            Point1 = point1.ToTuple2D();
            Tangent0 = tangent0.ToTuple2D();
            Tangent1 = tangent1.ToTuple2D();
            TangentLength0 = Tangent0.GetLength();

            ScaledRotor = Tuple2D.E1.CreateEuclideanScaledPureRotor(tangent0);

            var scaledRotorInv = ScaledRotor.GetPureScaledRotorInverse();

            CanonicalCurve = PhCurve2DDegree5Canonical.Create(
                scaledRotorInv.OmMap(Point1 - Point0),
                scaledRotorInv.OmMap(Tangent1)
            );
        }


        public Tuple2D GetHodographPoint(double parameterValue)
        {
            return ScaledRotor.OmMap(
                CanonicalCurve.GetHodographPoint(parameterValue)
            );
        }

        public Tuple2D GetCurvePoint(double parameterValue)
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