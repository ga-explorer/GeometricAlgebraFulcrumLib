using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public sealed class CGaFloat64PGaFlatBladeDecoder :
    CGaFloat64BladeDecoderBase
{
    internal CGaFloat64PGaFlatBladeDecoder(CGaFloat64Blade blade) 
        : base(blade)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Point2D()
    {
        Debug.Assert(Blade.GeometricSpace.Is4D);

        var hgaPoint =
            Blade.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinFloat64Vector2D.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Point3D()
    {
        Debug.Assert(Blade.GeometricSpace.Is5D);

        var hgaPoint =
            Blade.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinFloat64Vector3D.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s,
            hgaPoint[4] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element()
    {
        return Element(
            Blade.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Flat Element(CGaFloat64Blade egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Element(egaProbePoint);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade2D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaConformalParametricElement Element(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).Element(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector2D egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector3D egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(LinFloat64Vector egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(XGaFloat64Vector egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double Weight(CGaFloat64Blade egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.Weight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaDirection()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaNormalDirection()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaNormalDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition()
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(LinFloat64Vector2D egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(LinFloat64Vector3D egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(LinFloat64Vector egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(XGaFloat64Vector egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CGaFloat64Blade VGaPosition(CGaFloat64Blade egaProbePoint)
    {
        return Blade.PGaToIpns().Decode.IpnsFlat.VGaPosition(egaProbePoint);
    }

}