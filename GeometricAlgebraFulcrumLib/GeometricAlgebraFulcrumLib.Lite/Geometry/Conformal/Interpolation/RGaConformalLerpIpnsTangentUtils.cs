using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsTangent(egaProbePoint),
            blade2.DecodeIpnsTangent(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsTangent(egaProbePoint),
            blade2.DecodeIpnsTangent(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsTangent(egaProbeLine),
            blade2.DecodeIpnsTangent(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsTangent(egaProbeLine),
            blade2.DecodeIpnsTangent(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(),
            blade2.DecodeIpnsTangent()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsTangent(egaProbePlane),
            blade2.DecodeIpnsTangent(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}