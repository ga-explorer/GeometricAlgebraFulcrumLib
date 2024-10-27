using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaOpnsTangentBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaOpnsTangentBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return Blade
            .Gp(cgaGeometricSpace.Ei.Lcp(Blade).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDirection()
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(Blade)
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .GetVGaPart(true)
            .DivideByNorm();
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
    public Scalar<T> Weight(LinVector<T> egaProbePoint)
    {
        return Weight(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> Element(LinVector2D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> Element(LinVector3D<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaTangent<T> Element(LinVector<T> egaProbePoint)
    {
        return Element(
            Blade.GeometricSpace.EncodeVGa.Vector(egaProbePoint)
        );
    }

    public CGaTangent<T> Element(CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = Blade.GeometricSpace;

        var position =
            Blade
                .Gp(cgaGeometricSpace.Ei.Lcp(Blade).Inverse().Negative())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(Blade)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            position,
            directionOpEi.GetVGaPart(true)
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
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
    //    if (!blade.Specs.IsOpnsTangent)
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
    //    if (!blade.Specs.IsOpnsTangent)
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
    //    if (!blade.Specs.IsOpnsTangent)
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