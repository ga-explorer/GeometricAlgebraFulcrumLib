using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodePGaFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D DecodePGaPoint2D(this RGaConformalBlade pgaPoint)
    {
        Debug.Assert(pgaPoint.ConformalSpace.Is4D);

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
    public static LinFloat64Vector3D DecodePGaPoint3D(this RGaConformalBlade pgaPoint)
    {
        Debug.Assert(pgaPoint.ConformalSpace.Is5D);

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
    public static RGaConformalFlat DecodePGaFlat(this RGaConformalBlade pgaFlat)
    {
        return pgaFlat.DecodePGaFlat(
            pgaFlat.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalFlat DecodePGaFlat(this RGaConformalBlade pgaFlat, RGaConformalBlade egaProbePoint)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat, LinFloat64Vector2D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat, LinFloat64Vector3D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat, LinFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat, RGaFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodePGaFlatWeight(this RGaConformalBlade pgaFlat, RGaConformalBlade egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatWeight(egaProbePoint);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaDirection(this RGaConformalBlade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaDirection();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaNormalDirection(this RGaConformalBlade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaNormalDirection();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat, LinFloat64Vector2D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat, LinFloat64Vector3D egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat, LinFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat, RGaFloat64Vector egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodePGaFlatEGaPosition(this RGaConformalBlade pgaFlat, RGaConformalBlade egaProbePoint)
    {
        return pgaFlat.PGaToIpns().DecodeIpnsFlatEGaPosition(egaProbePoint);
    }

}