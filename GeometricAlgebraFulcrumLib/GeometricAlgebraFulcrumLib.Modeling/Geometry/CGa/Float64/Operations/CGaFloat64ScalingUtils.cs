using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64ScalingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element ScaleBy(this CGaFloat64Element element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).Decode.OpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ScaleBy(this CGaFloat64Element element, IFloat64ParametricScalar scalingFactor)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            scalingFactor.ParameterRange,
            t => element.ScaleBy(scalingFactor.GetValue(t).ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ScaleBy(this CGaFloat64ParametricElement element, double scalingFactor)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange,
            t => element.GetElement(t).ScaleBy(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ScaleBy(this CGaFloat64ParametricElement element, IFloat64ParametricScalar scalingFactor)
    {
        return CGaFloat64ParametricElement.Create(
            element.GeometricSpace,
            element.ParameterRange.Intersect(scalingFactor.ParameterRange),
            t => element.GetElement(t).ScaleBy(scalingFactor.GetValue(t).ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction ScaleBy(this CGaFloat64Direction element, double scalingFactor)
    {
        return new CGaFloat64Direction(
            element.GeometricSpace,
            element.Weight,
            element.Direction.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent ScaleBy(this CGaFloat64Tangent element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsTangent.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat ScaleBy(this CGaFloat64Flat element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round ScaleBy(this CGaFloat64Round element, double scalingFactor)
    {
        return element.EncodeOpnsBlade().ScaleBy(scalingFactor).DecodeOpnsRound.Element();
    }


    /// <summary>
    /// Apply a uniform scaling to this CGA blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ScaleBy(this CGaFloat64Blade blade, double scalingFactor)
    {
        Debug.Assert(
            scalingFactor.IsValid() &&
            scalingFactor > 0
        );

        var g = 0.5 * scalingFactor.LogE();
        var s0 = Math.Cosh(g);
        var s2 = Math.Sinh(g) * blade.GeometricSpace.Eoi.InternalKVector;

        var kVector =
            (s0 + s2).Gp(blade.InternalKVector).Gp(s0 - s2).GetKVectorPart(blade.InternalKVector.Grade);

        return new CGaFloat64Blade(blade.GeometricSpace, kVector);
    }
}