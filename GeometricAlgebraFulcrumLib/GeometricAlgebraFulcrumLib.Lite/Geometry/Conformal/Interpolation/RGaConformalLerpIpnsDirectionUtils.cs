using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(egaProbeLine),
            blade2.DecodeIpnsDirection(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(egaProbeLine),
            blade2.DecodeIpnsDirection(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(egaProbePlane),
            blade2.DecodeIpnsDirection(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}