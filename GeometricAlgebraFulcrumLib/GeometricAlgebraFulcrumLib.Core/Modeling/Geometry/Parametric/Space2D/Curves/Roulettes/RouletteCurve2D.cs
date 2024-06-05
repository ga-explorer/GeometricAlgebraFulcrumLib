using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space2D;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Roulettes;

public class RouletteCurve2D :
    IParametricC2Curve2D
{
    public IArcLengthCurve2D FixedCurve { get; }

    public IArcLengthCurve2D MovingCurve { get; }

    public LinFloat64Vector2D GeneratorPoint { get; }
        
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
    public RouletteCurve2D(IArcLengthCurve2D fixedCurve, IArcLengthCurve2D movingCurve, ILinFloat64Vector2D generatorPoint, double parameterValueMax)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = generatorPoint.ToLinVector2D();
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
            movingFrame.GetAngle(fixedFrame).ToDirectedAngle()
        );
    }

    public LinFloat64Vector2D GetPoint(double parameterValue)
    {
        var t1 = MovingCurve.LengthToParameter(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToParameter(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var angle =
            movingFrame.GetAngle(fixedFrame);

        return fixedFrame.Point +
               angle.Rotate(GeneratorPoint - movingFrame.Point);
    }

    public LinFloat64Vector2D GetDerivative1Point(double parameterValue)
    {
        //TODO: May be add a caching mechanism to speed this more
        var fX =
            Differentiate.FirstDerivativeFunc(t => GetPoint(t).X);

        var fY =
            Differentiate.FirstDerivativeFunc(t => GetPoint(t).Y);
            
        return LinFloat64Vector2D.Create((Float64Scalar)fX(parameterValue),
            (Float64Scalar)fY(parameterValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParametricCurveLocalFrame2D GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }

    public LinFloat64Vector2D GetDerivative2Point(double parameterValue)
    {
        var fX =
            Differentiate.SecondDerivativeFunc(t => GetPoint(t).X);

        var fY =
            Differentiate.SecondDerivativeFunc(t => GetPoint(t).Y);
            
        return LinFloat64Vector2D.Create((Float64Scalar)fX(parameterValue),
            (Float64Scalar)fY(parameterValue));
    }
}