using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> DecodePGaPoint2D<T>(this XGaConformalBlade<T> pgaPoint)
    {
        Debug.Assert(pgaPoint.ConformalSpace.Is4D);

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
    public static LinVector3D<T> DecodePGaPoint3D<T>(this XGaConformalBlade<T> pgaPoint)
    {
        Debug.Assert(pgaPoint.ConformalSpace.Is5D);

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
    public static XGaConformalFlat<T> DecodePGaFlat<T>(this XGaConformalBlade<T> pgaFlat)
    {
        return pgaFlat.DecodePGaFlat(
            pgaFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalFlat<T> DecodePGaFlat<T>(this XGaConformalBlade<T> pgaFlat, XGaConformalBlade<T> egaProbePoint)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat, LinVector2D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat, LinVector3D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat, LinVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat, XGaVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodePGaFlatWeight<T>(this XGaConformalBlade<T> pgaFlat, XGaConformalBlade<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaDirection<T>(this XGaConformalBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaDirection();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaNormalDirection<T>(this XGaConformalBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaNormalDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat, LinVector2D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat, LinVector3D<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat, LinVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat, XGaVector<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodePGaFlatEGaPosition<T>(this XGaConformalBlade<T> pgaFlat, XGaConformalBlade<T> egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

}