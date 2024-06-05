using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Harmonic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Harmonic;

public class SimpleHarmonicCurve2D :
    IParametricC2Curve2D
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SimpleHarmonicCurve2D Create(SimpleHarmonicParametricScalar xCurve, SimpleHarmonicParametricScalar yCurve)
    {
        return new SimpleHarmonicCurve2D(xCurve, yCurve);
    }


    public SimpleHarmonicParametricScalar XCurve { get; }

    public SimpleHarmonicParametricScalar YCurve { get; }
    
    public Float64ScalarRange ParameterRange 
        => XCurve.ParameterRange;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SimpleHarmonicCurve2D(SimpleHarmonicParametricScalar xCurve, SimpleHarmonicParametricScalar yCurve)
    {
        XCurve = xCurve;
        YCurve = yCurve;
        
        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return XCurve.IsValid() &&
               YCurve.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetValue(parameterValue),
            YCurve.GetValue(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetDerivative1Value(parameterValue),
            YCurve.GetDerivative1Value(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetDerivative2Point(double parameterValue)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetDerivative2Value(parameterValue),
            YCurve.GetDerivative2Value(parameterValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}