using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Maps;

using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.PhCurves;

public sealed class PhCurve3DDegree5
{
    public static PhCurve3DDegree5 Create(ILinFloat64Vector3D point0, ILinFloat64Vector3D tangent0, ILinFloat64Vector3D point1, ILinFloat64Vector3D tangent1)
    {
        return new PhCurve3DDegree5(
            point0,
            tangent0,
            point1,
            tangent1,
            LinFloat64PolarAngle.Angle0,
            LinFloat64PolarAngle.Angle0
        );
    }

    public static PhCurve3DDegree5 Create(ILinFloat64Vector3D point0, ILinFloat64Vector3D tangent0, ILinFloat64Vector3D point1, ILinFloat64Vector3D tangent1, LinFloat64Angle theta1, LinFloat64Angle theta2)
    {
        return new PhCurve3DDegree5(
            point0, tangent0, point1, tangent1, theta1, theta2
        );
    }


    public XGaFloat64Processor BasisBladeSet { get; }

    public LinFloat64Vector3D Point0 { get; }

    public LinFloat64Vector3D Point1 { get; }

    public LinFloat64Vector3D Tangent0 { get; }

    public LinFloat64Vector3D Tangent1 { get; }

    public double TangentLength0 { get; }

    public LinFloat64Angle Theta1
        => CanonicalCurve.Theta1;

    public LinFloat64Angle Theta2
        => CanonicalCurve.Theta2;

    public GaScaledPureRotor ScaledRotor { get; }

    public PhCurve3DDegree5Canonical CanonicalCurve { get; }


    private PhCurve3DDegree5(ILinFloat64Vector3D point0, ILinFloat64Vector3D tangent0, ILinFloat64Vector3D point1, ILinFloat64Vector3D tangent1, LinFloat64Angle theta1, LinFloat64Angle theta2)
    {
        BasisBladeSet = XGaFloat64Processor.Euclidean;
        Point0 = point0.ToLinVector3D();
        Point1 = point1.ToLinVector3D();
        Tangent0 = tangent0.ToLinVector3D();
        Tangent1 = tangent1.ToLinVector3D();
        TangentLength0 = Tangent0.VectorENorm();
        ScaledRotor = LinFloat64Vector3D.E1.CreateEuclideanScaledPureRotor(tangent0);

        var scaledRotorInv = tangent0.CreateEuclideanScaledPureRotor(LinFloat64Vector3D.E1);

        CanonicalCurve = PhCurve3DDegree5Canonical.Create(
            scaledRotorInv.OmMap(Point1 - Point0),
            scaledRotorInv.OmMap(tangent1),
            theta1,
            theta2
        );
    }


    public LinFloat64Vector3D GetHodographPoint(double parameterValue)
    {
        return ScaledRotor.OmMap(
            CanonicalCurve.GetHodographPoint(parameterValue)
        );
    }

    public ILinFloat64Vector3D GetCurvePoint(double parameterValue)
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