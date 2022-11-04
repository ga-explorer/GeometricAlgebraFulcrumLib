using System;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Mapped;

public class GrParametricRotatedNormalsCurve3D :
    IGraphicsC1ParametricCurve3D
{
    public IGraphicsC1ParametricCurve3D BaseCurve { get; }

    public Func<double, PlanarAngle> AngleFunction { get; }


    public GrParametricRotatedNormalsCurve3D(IGraphicsC1ParametricCurve3D baseCurve, Func<double, PlanarAngle> angleFunction)
    {
        BaseCurve = baseCurve;
        AngleFunction = angleFunction;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return BaseCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetPoint(double parameterValue)
    {
        return BaseCurve.GetPoint(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetTangent(double parameterValue)
    {
        return BaseCurve.GetTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple3D GetUnitTangent(double parameterValue)
    {
        return BaseCurve.GetUnitTangent(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
    {
        return BaseCurve
            .GetFrame(parameterValue)
            .RotateNormalsBy(
                AngleFunction(parameterValue)
            );
    }
}