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

public static class CGaDecodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodePGaPoint2D<T>(this CGaBlade<T> pgaPoint)
    {
        Debug.Assert(pgaPoint.GeometricSpace.Is4D);

        var hgaPoint =
            pgaPoint.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinVector2D<T>.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> DecodePGaPoint3D<T>(this CGaBlade<T> pgaPoint)
    {
        Debug.Assert(pgaPoint.GeometricSpace.Is5D);

        var hgaPoint =
            pgaPoint.PGaDual();

        var s = 1d / (hgaPoint[0] + hgaPoint[1]);

        //Debug.Assert(IsValidHGaPoint(hgaPoint));

        return LinVector3D<T>.Create(
            hgaPoint[2] * s,
            hgaPoint[3] * s,
            hgaPoint[4] * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodePGaFlat<T>(this CGaBlade<T> pgaFlat)
    {
        return pgaFlat.DecodePGaFlat(
            pgaFlat.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DecodePGaFlat<T>(this CGaBlade<T> pgaFlat, CGaBlade<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlat(egaProbePoint);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodePGaFlat<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodePGaFlat<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodePGaFlat<T>(this XGaConformalParametricBlade2D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodePGaFlat<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsPGaFlat)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodePGaFlat(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat, LinVector2D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat, LinVector3D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat, LinVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat, XGaVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this CGaBlade<T> pgaFlat, CGaBlade<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaDirection<T>(this CGaBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaNormalDirection<T>(this CGaBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaNormalDirection();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat, LinVector2D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat, LinVector3D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat, LinVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat, XGaVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodePGaFlatVGaPosition<T>(this CGaBlade<T> pgaFlat, CGaBlade<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatVGaPosition(egaProbePoint);
    }

}