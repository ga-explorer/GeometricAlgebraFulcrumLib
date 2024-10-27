using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaIpnsDirectionBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaIpnsDirectionBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight()
    {
        return Weight(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector2D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector3D<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(XGaVector<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(CGaBlade<T> egaProbePoint)
    {
        Debug.Assert(
            Blade.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            Blade.CGaUnDual();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> VGaDirectionAsXGaKVector()
    {
        return Blade.CGaUnDual().GetVGaPartAsXGaKVector(true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDirectionAsBlade()
    {
        return Blade.CGaUnDual().GetVGaPart(true);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> VGaUnitDirectionAsXGaKVector()
    {
        return Blade.CGaUnDual().GetVGaPartAsXGaKVector(true).DivideByNorm();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaUnitDirectionAsBlade()
    {
        return Blade.CGaUnDual().GetVGaPart(true).DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaDirection<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaDirection<T> Element(LinVector2D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaDirection<T> Element(LinVector3D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaDirection<T> Element(XGaVector<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaDirection<T> Element(CGaBlade<T> egaProbePoint)
    {
        Debug.Assert(
            Blade.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            Blade.CGaUnDual();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaDirection<T>(
            Blade.GeometricSpace,
            weight,
            directionOpEi.GetVGaPart(true)
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGa.VectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}
}