using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Spherical;

/// <summary>
/// This is a parametric expressed as parametric spherical coordinates
/// (r(t), theta(t), phi(t)) 
/// </summary>
public class SphericalCurve3D :
    IParametricC2Curve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SphericalCurve3D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, LinFloat64DirectedAngle thetaValue, LinFloat64DirectedAngle phiValue)
    {
        return new SphericalCurve3D(
            parameterRange,
            rCurve,
            ConstantParametricScalar.Create(thetaValue.RadiansValue),
            ConstantParametricScalar.Create(phiValue.RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SphericalCurve3D Create(Float64ScalarRange parameterRange, double rValue, IFloat64ParametricC2Scalar thetaCurve, IFloat64ParametricC2Scalar phiCurve)
    {
        return new SphericalCurve3D(
            parameterRange,
            ConstantParametricScalar.Create(rValue),
            thetaCurve,
            phiCurve
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SphericalCurve3D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, IFloat64ParametricC2Scalar thetaCurve, LinFloat64DirectedAngle phiValue)
    {
        return new SphericalCurve3D(
            parameterRange,
            rCurve,
            thetaCurve,
            ConstantParametricScalar.Create(phiValue.RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SphericalCurve3D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, LinFloat64DirectedAngle thetaValue, IFloat64ParametricC2Scalar phiCurve)
    {
        return new SphericalCurve3D(
            parameterRange,
            rCurve,
            ConstantParametricScalar.Create(thetaValue.RadiansValue),
            phiCurve
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SphericalCurve3D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, IFloat64ParametricC2Scalar thetaCurve, IFloat64ParametricC2Scalar phiCurve)
    {
        return new SphericalCurve3D(
            parameterRange,
            rCurve,
            thetaCurve,
            phiCurve
        );
    }


    public IFloat64ParametricC2Scalar RCurve { get; }

    public IFloat64ParametricC2Scalar ThetaCurve { get; }

    public IFloat64ParametricC2Scalar PhiCurve { get; }

    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SphericalCurve3D(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, IFloat64ParametricC2Scalar thetaCurve, IFloat64ParametricC2Scalar phiCurve)
    {
        ParameterRange = parameterRange;
        RCurve = rCurve;
        ThetaCurve = thetaCurve;
        PhiCurve = phiCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               RCurve.IsValid() &&
               ThetaCurve.IsValid() &&
               PhiCurve.IsValid() &&
               RCurve.ParameterRange.Contains(ParameterRange) &&
               ThetaCurve.ParameterRange.Contains(ParameterRange) &&
               PhiCurve.ParameterRange.Contains(ParameterRange);
    }

    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);

        var phiCos = Math.Cos(phi);
        var phiSin = Math.Sin(phi);

        var x = r * thetaCos * phiCos;
        var y = r * thetaCos * phiSin;
        var z = r * thetaSin;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);

        var phiCos = Math.Cos(phi);
        var phiSin = Math.Sin(phi);

        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
        var phiDt1 = PhiCurve.GetDerivative1Value(parameterValue);

        // x = r * thetaCos * phiCos;
        var x =
            rDt1 * thetaCos * phiCos -
            r * thetaSin * thetaDt1 * phiCos -
            r * thetaCos * phiSin * phiDt1;

        // y = r * thetaCos * phiSin;
        var y =
            rDt1 * thetaCos * phiSin -
            r * thetaSin * thetaDt1 * phiSin +
            r * thetaCos * phiCos * phiDt1;

        // z = r * thetaSin;
        var z =
            rDt1 * thetaSin +
            r * thetaCos * thetaDt1;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);

        var phiCos = Math.Cos(phi);
        var phiSin = Math.Sin(phi);

        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
        var phiDt1 = PhiCurve.GetDerivative1Value(parameterValue);

        var rDt2 = RCurve.GetDerivative2Value(parameterValue);
        var thetaDt2 = ThetaCurve.GetDerivative2Value(parameterValue);
        var phiDt2 = PhiCurve.GetDerivative2Value(parameterValue);

        var x =
            -phiCos * thetaCos * r * phiDt1 * phiDt1 -
            2 * thetaCos * phiSin * phiDt1 * rDt1 +
            2 * r * phiSin * thetaSin * phiDt1 * thetaDt1 -
            2 * phiCos * thetaSin * rDt1 * thetaDt1 -
            phiCos * thetaCos * r * thetaDt1 * thetaDt1 -
            thetaCos * r * phiSin * phiDt2 +
            phiCos * thetaCos * rDt2 -
            phiCos * r * thetaSin * thetaDt2;

        var y =
            -thetaCos * r * phiSin * phiDt1 * phiDt1 +
            2 * phiCos * thetaCos * phiDt1 * rDt1 -
            2 * phiCos * r * thetaSin * phiDt1 * thetaDt1 -
            2 * phiSin * thetaSin * rDt1 * thetaDt1 -
            thetaCos * r * phiSin * thetaDt1 * thetaDt1 +
            phiCos * thetaCos * r * phiDt2 +
            thetaCos * phiSin * rDt2 -
            r * phiSin * thetaSin * thetaDt2;

        var z =
            2 * thetaCos * rDt1 * thetaDt1 -
            r * thetaSin * thetaDt1 * thetaDt1 +
            thetaSin * rDt2 +
            thetaCos * r * thetaDt2;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}