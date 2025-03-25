using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Operations;

public static class CGaFloat64ProjectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Element ProjectOn(this CGaFloat64Element element1, CGaFloat64Element element2)
    {
        return element1.EncodeOpnsBlade()
            .ProjectOpnsOn(element2.EncodeOpnsBlade())
            .Decode.OpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ProjectOn(this CGaFloat64Element element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element2.ParameterRange,
            t => element1.ProjectOn(element2.GetElement(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ProjectOn(this CGaFloat64ParametricElement element1, CGaFloat64Element element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).ProjectOn(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64ParametricElement ProjectOn(this CGaFloat64ParametricElement element1, CGaFloat64ParametricElement element2)
    {
        return CGaFloat64ParametricElement.Create(
            element1.GeometricSpace,
            element1.ParameterRange.Intersect(element2.ParameterRange),
            t => element1.GetElement(t).ProjectOn(element2.GetElement(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectPositionOnFlat(this CGaFloat64Element element, CGaFloat64Flat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .Decode.OpnsFlat.VGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectPositionOnFlat2D(this CGaFloat64Element element, CGaFloat64Flat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .DecodeOpnsFlat.VGaPosition2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectPositionOnFlat3D(this CGaFloat64Element element, CGaFloat64Flat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .DecodeOpnsFlat.VGaPosition3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ProjectPositionOnFlat2D(this CGaFloat64Element element, CGaFloat64ParametricElement flat)
    {
        return Float64ComputedPath2D.Finite(
            flat.ParameterRange,
            t =>
            {
                var el = flat.GetElement(t);

                return el is CGaFloat64Flat flatElement
                    ? element.ProjectPositionOnFlat2D(flatElement)
                    : LinFloat64Vector2D.Zero;
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ProjectPositionOnFlat3D(this CGaFloat64Element element, CGaFloat64ParametricElement flat)
    {
        return Float64ComputedPath3D.Finite(
            flat.ParameterRange,
            t =>
            {
                var el = flat.GetElement(t);

                return el is CGaFloat64Flat flatElement
                    ? element.ProjectPositionOnFlat3D(flatElement)
                    : LinFloat64Vector3D.Zero;
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ProjectPositionOnFlat2D(this CGaFloat64ParametricElement element, CGaFloat64Flat flat)
    {
        return Float64ComputedPath2D.Finite(
            element.ParameterRange,
            t =>
                element.GetElement(t).ProjectPositionOnFlat2D(flat)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path2D ProjectPositionOnFlat2D(this CGaFloat64ParametricElement element, CGaFloat64ParametricElement flat)
    {
        return Float64ComputedPath2D.Finite(
            element.ParameterRange.Intersect(flat.ParameterRange),
            t =>
            {
                var el = flat.GetElement(t);

                return el is CGaFloat64Flat flatElement
                    ? element.GetElement(t).ProjectPositionOnFlat2D(flatElement)
                    : LinFloat64Vector2D.Zero;
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ProjectPositionOnFlat3D(this CGaFloat64ParametricElement element, CGaFloat64Flat flat)
    {
        return Float64ComputedPath3D.Finite(
            element.ParameterRange,
            t =>
                element.GetElement(t).ProjectPositionOnFlat3D(flat)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Path3D ProjectPositionOnFlat3D(this CGaFloat64ParametricElement element, CGaFloat64ParametricElement flat)
    {
        return Float64ComputedPath3D.Finite(
            element.ParameterRange.Intersect(flat.ParameterRange),
            t =>
            {
                var el = flat.GetElement(t);

                return el is CGaFloat64Flat flatElement
                    ? element.GetElement(t).ProjectPositionOnFlat3D(flatElement)
                    : LinFloat64Vector3D.Zero;
            }
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaFloat64Blade ProjectOn(this CGaFloat64Blade blade, RGaFloat64KVector subspace)
    {
        var projectedBlade =
            blade.InternalKVector
                .Fdp(subspace)
                .Gp(subspace)
                .GetKVectorPart(blade.Grade);

        var scalarFactor = subspace.SpSquared();

        var kVector = scalarFactor.IsNearZero()
            ? projectedBlade
            : projectedBlade / scalarFactor;

        return new CGaFloat64Blade(blade.GeometricSpace, kVector);
    }

    /// <summary>
    /// Project this EGA Blade on another EGA Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectVGaOn(this CGaFloat64Blade blade, CGaFloat64Blade subspace)
    {
        Debug.Assert(
            blade.IsVGaBlade() &&
            subspace.IsVGaBlade()
        );

        return blade.ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this PGA Blade on another PGA Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectPGaOn(this CGaFloat64Blade blade, CGaFloat64Blade subspace)
    {
        Debug.Assert(
            blade.IsPGaBlade() &&
            subspace.IsPGaBlade()
        );

        return blade.ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this CGA OPNS Blade on another CGA OPNS Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectOpnsOn(this CGaFloat64Blade blade, CGaFloat64Blade subspace)
    {
        return blade.ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this CGA IPNS Blade on another CGA IPNS Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectIpnsOn(this CGaFloat64Blade blade, CGaFloat64Blade subspace)
    {
        return blade.ProjectOn(subspace.InternalKVector);
    }

    /// <summary>
    /// Project this EGA Point on a IPNS Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade ProjectVGaPointOnIpns(this CGaFloat64Blade blade, CGaFloat64Blade subspace)
    {
        Debug.Assert(
            blade.IsVGaBlade()
        );

        return blade.VGaVectorToIpnsPoint().ProjectIpnsOn(subspace);
    }
}