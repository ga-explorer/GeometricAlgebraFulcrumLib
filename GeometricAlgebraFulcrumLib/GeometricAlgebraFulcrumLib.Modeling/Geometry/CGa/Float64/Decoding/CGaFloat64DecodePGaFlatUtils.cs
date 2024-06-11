using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodePGaPoint2D(this CGaFloat64Blade pgaPoint)
    {
        Debug.Assert(pgaPoint.GeometricSpace.Is4D);

        var hgaPoint =
            pgaPoint.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinFloat64Vector2D.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D DecodePGaPoint3D(this CGaFloat64Blade pgaPoint)
    {
        Debug.Assert(pgaPoint.GeometricSpace.Is5D);

        var hgaPoint =
            pgaPoint.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinFloat64Vector3D.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s,
            hgaPoint[4] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodePGaFlat(this CGaFloat64Blade pgaFlat)
    {
        return pgaFlat.DecodePGaFlat(
            pgaFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DecodePGaFlat(this CGaFloat64Blade pgaFlat, CGaFloat64Blade egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlat(egaProbePoint);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodePGaFlat(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodePGaFlat(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodePGaFlat(this RGaConformalParametricBlade2D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodePGaFlat(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat, LinFloat64Vector2D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat, LinFloat64Vector3D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat, LinFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat, RGaFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this CGaFloat64Blade pgaFlat, CGaFloat64Blade egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaDirection(this CGaFloat64Blade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaNormalDirection(this CGaFloat64Blade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaNormalDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat, LinFloat64Vector2D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat, LinFloat64Vector3D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat, LinFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat, RGaFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodePGaFlatVGaPosition(this CGaFloat64Blade pgaFlat, CGaFloat64Blade egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

}