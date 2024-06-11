using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars.Harmonic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Curves.Harmonic;

public class SimpleHarmonicCurve3D :
    IParametricC2Curve3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicCurve3D Create(SimpleHarmonicParametricScalar xCurve, SimpleHarmonicParametricScalar yCurve, SimpleHarmonicParametricScalar zCurve)
    {
        return new SimpleHarmonicCurve3D(xCurve, yCurve, zCurve);
    }


    public SimpleHarmonicParametricScalar XCurve { get; }

    public SimpleHarmonicParametricScalar YCurve { get; }

    public SimpleHarmonicParametricScalar ZCurve { get; }

    public Float64ScalarRange ParameterRange
        => XCurve.ParameterRange;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SimpleHarmonicCurve3D(SimpleHarmonicParametricScalar xCurve, SimpleHarmonicParametricScalar yCurve, SimpleHarmonicParametricScalar zCurve)
    {
        XCurve = xCurve;
        YCurve = yCurve;
        ZCurve = zCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return XCurve.IsValid() &&
               YCurve.IsValid() &&
               ZCurve.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetPoint(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetValue(parameterValue),
            YCurve.GetValue(parameterValue),
            ZCurve.GetValue(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative1Point(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetDerivative1Value(parameterValue),
            YCurve.GetDerivative1Value(parameterValue),
            ZCurve.GetDerivative1Value(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetDerivative2Point(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetDerivative2Value(parameterValue),
            YCurve.GetDerivative2Value(parameterValue),
            ZCurve.GetDerivative2Value(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}