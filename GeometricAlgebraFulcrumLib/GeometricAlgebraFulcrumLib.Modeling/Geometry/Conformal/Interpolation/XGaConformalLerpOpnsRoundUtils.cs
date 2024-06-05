using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound(egaProbePoint),
            blade2.DecodeOpnsRound(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound(egaProbePoint),
            blade2.DecodeOpnsRound(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound(egaProbePointPair),
            blade2.DecodeOpnsRound(egaProbePointPair)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound(egaProbePointPair),
            blade2.DecodeOpnsRound(egaProbePointPair)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound(egaProbeCircle),
            blade2.DecodeOpnsRound(egaProbeCircle)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound(egaProbeCircle),
            blade2.DecodeOpnsRound(egaProbeCircle)
        ).EncodeOpnsBlade();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(),
            blade2.DecodeOpnsRound()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound(egaProbeSphere),
            blade2.DecodeOpnsRound(egaProbeSphere)
        ).EncodeOpnsBlade();
    }
}