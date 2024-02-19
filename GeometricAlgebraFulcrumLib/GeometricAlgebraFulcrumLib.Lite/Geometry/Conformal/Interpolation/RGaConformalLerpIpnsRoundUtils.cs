using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector2D egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, Float64Vector3D egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpIpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere),
            blade2.DecodeIpnsRound(egaProbeSphere)
        ).EncodeIpnsBlade();
    }
}