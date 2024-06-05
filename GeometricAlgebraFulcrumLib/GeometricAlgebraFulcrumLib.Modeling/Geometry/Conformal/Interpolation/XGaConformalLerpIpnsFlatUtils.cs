using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;


public static class XGaConformalLerpIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat(),
            blade2.DecodeIpnsFlat()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat(egaProbePoint),
            blade2.DecodeIpnsFlat(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat(),
            blade2.DecodeIpnsFlat()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat(egaProbePoint),
            blade2.DecodeIpnsFlat(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat(),
            blade2.DecodeIpnsFlat()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat(egaProbeLine),
            blade2.DecodeIpnsFlat(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat(),
            blade2.DecodeIpnsFlat()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat(egaProbeLine),
            blade2.DecodeIpnsFlat(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat(),
            blade2.DecodeIpnsFlat()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat(egaProbePlane),
            blade2.DecodeIpnsFlat(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}