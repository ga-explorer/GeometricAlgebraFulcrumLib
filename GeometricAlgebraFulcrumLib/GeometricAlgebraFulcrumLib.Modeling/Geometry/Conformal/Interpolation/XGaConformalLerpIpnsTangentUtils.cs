using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(egaProbePoint),
            blade2.DecodeIpnsTangent(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(egaProbePoint),
            blade2.DecodeIpnsTangent(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(egaProbeLine),
            blade2.DecodeIpnsTangent(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(egaProbeLine),
            blade2.DecodeIpnsTangent(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(egaProbePlane),
            blade2.DecodeIpnsTangent(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}