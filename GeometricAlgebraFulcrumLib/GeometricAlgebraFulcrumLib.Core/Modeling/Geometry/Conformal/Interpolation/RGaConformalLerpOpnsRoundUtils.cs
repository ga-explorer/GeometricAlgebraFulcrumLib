using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(egaProbePoint),
            blade2.DecodeOpnsRound(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(egaProbePoint),
            blade2.DecodeOpnsRound(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(egaProbePointPair),
            blade2.DecodeOpnsRound(egaProbePointPair)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundPointPair3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(egaProbePointPair),
            blade2.DecodeOpnsRound(egaProbePointPair)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(egaProbeCircle),
            blade2.DecodeOpnsRound(egaProbeCircle)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundCircle3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(egaProbeCircle),
            blade2.DecodeOpnsRound(egaProbeCircle)
        ).EncodeOpnsBlade();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsRoundSphere3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(egaProbeSphere),
            blade2.DecodeOpnsRound(egaProbeSphere)
        ).EncodeOpnsBlade();
    }
}