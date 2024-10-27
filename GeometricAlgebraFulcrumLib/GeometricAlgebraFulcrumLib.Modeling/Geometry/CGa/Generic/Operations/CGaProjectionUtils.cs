using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Operations;

public static class CGaProjectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaElement<T> ProjectOn<T>(this CGaElement<T> element1, CGaElement<T> element2)
    {
        return element1.EncodeOpnsBlade()
            .ProjectOpnsOn(element2.EncodeOpnsBlade())
            .Decode.OpnsElement();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ProjectOn<T>(this XGaConformalElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element2.ParameterRange,
    //        t => element1.ProjectOn(element2.GetElement(t))
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ProjectOn<T>(this XGaConformalParametricElement<T> element1, XGaConformalElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange,
    //        t => element1.GetElement(t).ProjectOn(element2)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> ProjectOn<T>(this XGaConformalParametricElement<T> element1, XGaConformalParametricElement<T> element2)
    //{
    //    return XGaConformalParametricElement<T>.Create(
    //        element1.ConformalSpace,
    //        element1.ParameterRange.Intersect(element2.ParameterRange),
    //        t => element1.GetElement(t).ProjectOn(element2.GetElement(t))
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ProjectPositionOnFlat<T>(this CGaElement<T> element, CGaFlat<T> flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .Decode.OpnsFlat.VGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> ProjectPositionOnFlat2D<T>(this CGaElement<T> element, CGaFlat<T> flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .Decode.OpnsFlat.VGaPositionAsVector2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> ProjectPositionOnFlat3D<T>(this CGaElement<T> element, CGaFlat<T> flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .Decode.OpnsFlat.VGaPositionAsVector3D();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D<T>(this XGaConformalElement<T> element, XGaConformalParametricElement<T> flat)
    //{
    //    return ComputedParametricCurve2D.Create(
    //        flat.ParameterRange,
    //        t =>
    //        {
    //            var el = flat.GetElement(t);

    //            return el is XGaConformalFlat<T> flatElement
    //                ? element.ProjectPositionOnFlat2D(flatElement)
    //                : LinVector2D<T>.Zero;
    //        }
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IParametricCurve3D ProjectPositionOnFlat3D<T>(this XGaConformalElement<T> element, XGaConformalParametricElement<T> flat)
    //{
    //    return ComputedParametricCurve3D.Create(
    //        flat.ParameterRange,
    //        t =>
    //        {
    //            var el = flat.GetElement(t);

    //            return el is XGaConformalFlat<T> flatElement
    //                ? element.ProjectPositionOnFlat3D(flatElement)
    //                : LinVector3D<T>.Zero;
    //        }
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D<T>(this XGaConformalParametricElement<T> element, XGaConformalFlat<T> flat)
    //{
    //    return ComputedParametricCurve2D.Create(
    //        element.ParameterRange,
    //        t => 
    //            element.GetElement(t).ProjectPositionOnFlat2D(flat)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D<T>(this XGaConformalParametricElement<T> element, XGaConformalParametricElement<T> flat)
    //{
    //    return ComputedParametricCurve2D.Create(
    //        element.ParameterRange.Intersect(flat.ParameterRange),
    //        t =>
    //        {
    //            var el = flat.GetElement(t);

    //            return el is XGaConformalFlat<T> flatElement
    //                ? element.GetElement(t).ProjectPositionOnFlat2D(flatElement)
    //                : LinVector2D<T>.Zero;
    //        }
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IParametricCurve3D ProjectPositionOnFlat3D<T>(this XGaConformalParametricElement<T> element, XGaConformalFlat<T> flat)
    //{
    //    return ComputedParametricCurve3D.Create(
    //        element.ParameterRange,
    //        t => 
    //            element.GetElement(t).ProjectPositionOnFlat3D(flat)
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static IParametricCurve3D ProjectPositionOnFlat3D<T>(this XGaConformalParametricElement<T> element, XGaConformalParametricElement<T> flat)
    //{
    //    return ComputedParametricCurve3D.Create(
    //        element.ParameterRange.Intersect(flat.ParameterRange),
    //        t =>
    //        {
    //            var el = flat.GetElement(t);

    //            return el is XGaConformalFlat<T> flatElement
    //                ? element.GetElement(t).ProjectPositionOnFlat3D(flatElement)
    //                : LinVector3D<T>.Zero;
    //        }
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaBlade<T> ProjectOn<T>(this CGaBlade<T> blade, XGaKVector<T> subspace)
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

        return new CGaBlade<T>(blade.GeometricSpace, kVector);
    }

    /// <summary>
    /// Project this EGA Blade on another EGA Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> ProjectVGaOn<T>(this CGaBlade<T> blade, CGaBlade<T> subspace)
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
    public static CGaBlade<T> ProjectPGaOn<T>(this CGaBlade<T> blade, CGaBlade<T> subspace)
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
    public static CGaBlade<T> ProjectOpnsOn<T>(this CGaBlade<T> blade, CGaBlade<T> subspace)
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
    public static CGaBlade<T> ProjectIpnsOn<T>(this CGaBlade<T> blade, CGaBlade<T> subspace)
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
    public static CGaBlade<T> ProjectVGaPointOnIpns<T>(this CGaBlade<T> blade, CGaBlade<T> subspace)
    {
        Debug.Assert(
            blade.IsVGaBlade()
        );

        return blade.VGaVectorToIpnsPoint().ProjectIpnsOn(subspace);
    }
}