using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbePoint.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound(egaProbePoint),
            blade2.DecodeIpnsRound(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbePointPair.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound(egaProbePointPair),
            blade2.DecodeIpnsRound(egaProbePointPair)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbeCircle.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound(egaProbeCircle),
            blade2.DecodeIpnsRound(egaProbeCircle)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(),
            blade2.DecodeIpnsRound()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere.EncodeVGaVectorBlade(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound(egaProbeSphere.EncodeVGaVectorBlade(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound(egaProbeSphere),
            blade2.DecodeIpnsRound(egaProbeSphere)
        ).EncodeIpnsBlade();
    }
}