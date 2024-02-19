using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Roulettes;

public class RouletteCurve2D :
    IParametricC2Curve2D
{
    public IArcLengthCurve2D FixedCurve { get; }

    public IArcLengthCurve2D MovingCurve { get; }

    public Float64Vector2D GeneratorPoint { get; }
        
    public Float64ScalarRange ParameterRange { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RouletteCurve2D(IArcLengthCurve2D fixedCurve, IArcLengthCurve2D movingCurve, double parameterValueMax)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = movingCurve.GetPoint(movingCurve.ParameterRange.MinValue);
        ParameterRange = Float64ScalarRange.Create(0, parameterValueMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RouletteCurve2D(IArcLengthCurve2D fixedCurve, IArcLengthCurve2D movingCurve, IFloat64Vector2D generatorPoint, double parameterValueMax)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = generatorPoint.ToVector2D();
        ParameterRange = Float64ScalarRange.Create(0, parameterValueMax);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return FixedCurve.IsValid() &&
               MovingCurve.IsValid();
    }

    public RouletteMap2D GetRouletteMap(double parameterValue)
    {
        var t1 = MovingCurve.LengthToParameter(parameterValue);
        var movingFrame = MovingCurve.GetTangent(t1);

        var t2 = FixedCurve.LengthToParameter(parameterValue);
        var fixedFrame = FixedCurve.GetTangent(t2);
            
        return new RouletteMap2D(
            FixedCurve.GetPoint(parameterValue),
            MovingCurve.GetPoint(parameterValue),
            movingFrame.GetVectorsAngle(fixedFrame)
        );
    }

    public Float64Vector2D GetPoint(double parameterValue)
    {
        var t1 = MovingCurve.LengthToParameter(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToParameter(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var angle =
            movingFrame.GetVectorsAngle(fixedFrame);

        return fixedFrame.Point +
               angle.Rotate(GeneratorPoint - movingFrame.Point);
    }

    public Float64Vector2D GetDerivative1Point(double parameterValue)
    {
        //TODO: May be add a caching mechanism to speed this more
        var fX =
            Differentiate.FirstDerivativeFunc(t => GetPoint(t).X);

        var fY =
            Differentiate.FirstDerivativeFunc(t => GetPoint(t).Y);
            
        return Float64Vector2D.Create((Float64Scalar)fX(parameterValue),
            (Float64Scalar)fY(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }

    public Float64Vector2D GetDerivative2Point(double parameterValue)
    {
        var fX =
            Differentiate.SecondDerivativeFunc(t => GetPoint(t).X);

        var fY =
            Differentiate.SecondDerivativeFunc(t => GetPoint(t).Y);
            
        return Float64Vector2D.Create((Float64Scalar)fX(parameterValue),
            (Float64Scalar)fY(parameterValue));
    }
}