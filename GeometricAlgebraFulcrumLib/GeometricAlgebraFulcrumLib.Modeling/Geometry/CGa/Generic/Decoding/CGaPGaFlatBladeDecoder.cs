using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public sealed class CGaPGaFlatBladeDecoder<T> :
    CGaBladeDecoderBase<T>
{
    internal CGaPGaFlatBladeDecoder(CGaBlade<T> blade) 
        : base(blade)
    {
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> DecodePGaPoint2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var hgaPoint =
            Blade.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinVector2D<T>.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DecodePGaPoint3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var hgaPoint =
            Blade.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinVector3D<T>.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s,
            hgaPoint[4] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFlat<T> Element(CGaBlade<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Element(egaProbePoint);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
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
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade2D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement<T> Element<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector2D<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector3D<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(LinVector<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(XGaVector<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Weight(CGaBlade<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaDirection()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaNormalDirection()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaNormalDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(LinVector2D<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(LinVector3D<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(LinVector<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(XGaVector<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaBlade<T> VGaPosition(CGaBlade<T> egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

}