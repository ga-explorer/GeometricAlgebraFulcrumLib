using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Maps;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.PolynomialAlgebra.PhCurves
{
    public sealed class PhCurve3DDegree5
    {
        public static PhCurve3DDegree5 Create(IFloat64Tuple3D point0, IFloat64Tuple3D tangent0, IFloat64Tuple3D point1, IFloat64Tuple3D tangent1)
        {
            return new PhCurve3DDegree5(
                point0, tangent0, point1, tangent1, 0d, 0d
            );
        }

        public static PhCurve3DDegree5 Create(IFloat64Tuple3D point0, IFloat64Tuple3D tangent0, IFloat64Tuple3D point1, IFloat64Tuple3D tangent1, double theta1, double theta2)
        {
            return new PhCurve3DDegree5(
                point0, tangent0, point1, tangent1, theta1, theta2
            );
        }


        public RGaFloat64Processor BasisBladeSet { get; }

        public Float64Vector3D Point0 { get; }

        public Float64Vector3D Point1 { get; }

        public Float64Vector3D Tangent0 { get; }

        public Float64Vector3D Tangent1 { get; }

        public double TangentLength0 { get; }

        public Float64PlanarAngle Theta1 
            => CanonicalCurve.Theta1;

        public Float64PlanarAngle Theta2 
            => CanonicalCurve.Theta2;

        public GaScaledPureRotor ScaledRotor { get; }

        public PhCurve3DDegree5Canonical CanonicalCurve { get; }


        private PhCurve3DDegree5(IFloat64Tuple3D point0, IFloat64Tuple3D tangent0, IFloat64Tuple3D point1, IFloat64Tuple3D tangent1, double theta1, double theta2)
        {
            BasisBladeSet = RGaFloat64Processor.Euclidean;
            Point0 = point0.ToVector3D();
            Point1 = point1.ToVector3D();
            Tangent0 = tangent0.ToVector3D();
            Tangent1 = tangent1.ToVector3D();
            TangentLength0 = Tangent0.ENorm();
            ScaledRotor = Float64Vector3D.E1.CreateEuclideanScaledPureRotor(tangent0);

            var scaledRotorInv = tangent0.CreateEuclideanScaledPureRotor(Float64Vector3D.E1);

            CanonicalCurve = PhCurve3DDegree5Canonical.Create(
                scaledRotorInv.OmMap(Point1 - Point0), 
                scaledRotorInv.OmMap(tangent1),
                theta1,
                theta2
            );
        }

        
        public Float64Vector3D GetHodographPoint(double parameterValue)
        {
            return ScaledRotor.OmMap(
                CanonicalCurve.GetHodographPoint(parameterValue)
            );
        }

        public IFloat64Tuple3D GetCurvePoint(double parameterValue)
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