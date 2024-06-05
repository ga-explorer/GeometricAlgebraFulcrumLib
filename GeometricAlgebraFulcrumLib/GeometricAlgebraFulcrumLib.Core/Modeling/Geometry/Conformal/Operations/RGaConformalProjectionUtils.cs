using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space3D.Curves;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Operations;

public static class RGaConformalProjectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalElement ProjectOn(this RGaConformalElement element1, RGaConformalElement element2)
    {
        return element1.EncodeOpnsBlade()
            .ProjectOpnsOn(element2.EncodeOpnsBlade())
            .DecodeOpnsElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ProjectOn(this RGaConformalElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element2.ParameterRange,
            t => element1.ProjectOn(element2.GetElement(t))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ProjectOn(this RGaConformalParametricElement element1, RGaConformalElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange,
            t => element1.GetElement(t).ProjectOn(element2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalParametricElement ProjectOn(this RGaConformalParametricElement element1, RGaConformalParametricElement element2)
    {
        return RGaConformalParametricElement.Create(
            element1.ConformalSpace,
            element1.ParameterRange.Intersect(element2.ParameterRange),
            t => element1.GetElement(t).ProjectOn(element2.GetElement(t))
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ProjectPositionOnFlat(this RGaConformalElement element, RGaConformalFlat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .DecodeOpnsFlatEGaPosition();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D ProjectPositionOnFlat2D(this RGaConformalElement element, RGaConformalFlat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .DecodeOpnsFlatEGaPosition2D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D ProjectPositionOnFlat3D(this RGaConformalElement element, RGaConformalFlat flat)
    {
        return element
            .PositionToOpnsFlatPoint()
            .ProjectOpnsOn(flat.EncodeOpnsBlade())
            .DecodeOpnsFlatEGaPosition3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D(this RGaConformalElement element, RGaConformalParametricElement flat)
    {
        return ComputedParametricCurve2D.Create(
            flat.ParameterRange,
            t =>
            {
                var el = flat.GetElement(t);

                return el is RGaConformalFlat flatElement
                    ? element.ProjectPositionOnFlat2D(flatElement)
                    : LinFloat64Vector2D.Zero;
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IParametricCurve3D ProjectPositionOnFlat3D(this RGaConformalElement element, RGaConformalParametricElement flat)
    {
        return ComputedParametricCurve3D.Create(
            flat.ParameterRange,
            t =>
            {
                var el = flat.GetElement(t);

                return el is RGaConformalFlat flatElement
                    ? element.ProjectPositionOnFlat3D(flatElement)
                    : LinFloat64Vector3D.Zero;
            }
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D(this RGaConformalParametricElement element, RGaConformalFlat flat)
    {
        return ComputedParametricCurve2D.Create(
            element.ParameterRange,
            t => 
                element.GetElement(t).ProjectPositionOnFlat2D(flat)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IFloat64ParametricCurve2D ProjectPositionOnFlat2D(this RGaConformalParametricElement element, RGaConformalParametricElement flat)
    {
        return ComputedParametricCurve2D.Create(
            element.ParameterRange.Intersect(flat.ParameterRange),
            t =>
            {
                var el = flat.GetElement(t);

                return el is RGaConformalFlat flatElement
                    ? element.GetElement(t).ProjectPositionOnFlat2D(flatElement)
                    : LinFloat64Vector2D.Zero;
            }
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IParametricCurve3D ProjectPositionOnFlat3D(this RGaConformalParametricElement element, RGaConformalFlat flat)
    {
        return ComputedParametricCurve3D.Create(
            element.ParameterRange,
            t => 
                element.GetElement(t).ProjectPositionOnFlat3D(flat)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IParametricCurve3D ProjectPositionOnFlat3D(this RGaConformalParametricElement element, RGaConformalParametricElement flat)
    {
        return ComputedParametricCurve3D.Create(
            element.ParameterRange.Intersect(flat.ParameterRange),
            t =>
            {
                var el = flat.GetElement(t);

                return el is RGaConformalFlat flatElement
                    ? element.GetElement(t).ProjectPositionOnFlat3D(flatElement)
                    : LinFloat64Vector3D.Zero;
            }
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static RGaConformalBlade ProjectOn(this RGaConformalBlade blade, RGaFloat64KVector subspace)
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

        return new RGaConformalBlade(blade.ConformalSpace, kVector);
    }

    /// <summary>
    /// Project this EGA Blade on another EGA Blade
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade ProjectEGaOn(this RGaConformalBlade blade, RGaConformalBlade subspace)
    {
        Debug.Assert(
            blade.IsEGaBlade() &&
            subspace.IsEGaBlade()
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
    public static RGaConformalBlade ProjectPGaOn(this RGaConformalBlade blade, RGaConformalBlade subspace)
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
    public static RGaConformalBlade ProjectOpnsOn(this RGaConformalBlade blade, RGaConformalBlade subspace)
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
    public static RGaConformalBlade ProjectIpnsOn(this RGaConformalBlade blade, RGaConformalBlade subspace)
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
    public static RGaConformalBlade ProjectEGaPointOnIpns(this RGaConformalBlade blade, RGaConformalBlade subspace)
    {
        Debug.Assert(
            blade.IsEGaBlade()
        );

        return blade.EGaVectorToIpnsPoint().ProjectIpnsOn(subspace);
    }
}