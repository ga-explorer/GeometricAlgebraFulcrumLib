using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Polar;

/// <summary>
/// This is a parametric expressed as parametric polar coordinates
/// (r(t), theta(t)) 
/// </summary>
public class PolarCurve2D :
    IParametricC2Curve2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PolarCurve2D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, LinFloat64Angle thetaValue)
    {
        return new PolarCurve2D(
            parameterRange,
            rCurve,
            ConstantParametricScalar.Create(thetaValue.RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PolarCurve2D Create(Float64ScalarRange parameterRange, double rValue, IFloat64ParametricC2Scalar thetaCurve)
    {
        return new PolarCurve2D(
            parameterRange,
            ConstantParametricScalar.Create(rValue),
            thetaCurve
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PolarCurve2D Create(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, IFloat64ParametricC2Scalar thetaCurve)
    {
        return new PolarCurve2D(
            parameterRange,
            rCurve,
            thetaCurve
        );
    }
        

    public IFloat64ParametricC2Scalar RCurve { get; }

    public IFloat64ParametricC2Scalar ThetaCurve { get; }
        
    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PolarCurve2D(Float64ScalarRange parameterRange, IFloat64ParametricC2Scalar rCurve, IFloat64ParametricC2Scalar thetaCurve)
    {
        ParameterRange = parameterRange;
        RCurve = rCurve;
        ThetaCurve = thetaCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ParameterRange.IsValid() &&
               RCurve.IsValid() &&
               ThetaCurve.IsValid() &&
               RCurve.ParameterRange.Contains(ParameterRange) &&
               ThetaCurve.ParameterRange.Contains(ParameterRange);
    }

    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
            
        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);
            
        var x = r * thetaCos;
        var y = r * thetaSin;

        return LinFloat64Vector2D.Create(x, y);
    }

    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);

        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);
            
        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
            
        // x = r * thetaCos;
        var xDt1 = 
            rDt1 * thetaCos - r * thetaSin * thetaDt1;

        // y = r * thetaSin;
        var yDt1 = 
            rDt1 * thetaSin + r * thetaCos * thetaDt1;
            
        return LinFloat64Vector2D.Create(xDt1, yDt1);
    }
        
    public LinFloat64Vector2D GetDerivative2Point(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
            
        var thetaCos = Math.Cos(theta);
        var thetaSin = Math.Sin(theta);
            
        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
            
        var rDt2 = RCurve.GetDerivative2Value(parameterValue);
        var thetaDt2 = ThetaCurve.GetDerivative2Value(parameterValue);
            
        // xDt1 = rDt1 * thetaCos - r * thetaSin * thetaDt1
        var xDt2 = 
            rDt2 * thetaCos - 
            rDt1 * thetaSin * thetaDt1 - 
            rDt1 * thetaSin * thetaDt1 - 
            r * thetaCos * thetaDt1 * thetaDt1 - 
            r * thetaSin * thetaDt2;

        // yDt1 = rDt1 * thetaSin + r * thetaCos * thetaDt1
        var yDt2 = 
            rDt2 * thetaSin +
            rDt1 * thetaCos * thetaDt1 +
            rDt1 * thetaCos * thetaDt1 -
            r * thetaSin * thetaDt1 * thetaDt1 +
            r * thetaCos * thetaDt2;
            
        return LinFloat64Vector2D.Create(xDt2, yDt2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}