using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsRound(egaProbeSphere.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere),
            blade2.DecodeIpnsRound(egaProbeSphere)
        ).EncodeIpnsBlade();
    }
}