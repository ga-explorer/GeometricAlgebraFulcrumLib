using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsRound.Element(egaProbePoint),
            blade2.Decode.IpnsRound.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsRound.Element(egaProbePoint),
            blade2.Decode.IpnsRound.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair2D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.Decode.IpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.Decode.IpnsRound.Element(egaProbePointPair),
            blade2.Decode.IpnsRound.Element(egaProbePointPair)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair3D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.Decode.IpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.Decode.IpnsRound.Element(egaProbePointPair),
            blade2.Decode.IpnsRound.Element(egaProbePointPair)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle2D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.Decode.IpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.Decode.IpnsRound.Element(egaProbeCircle),
            blade2.Decode.IpnsRound.Element(egaProbeCircle)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle3D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.Decode.IpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.Decode.IpnsRound.Element(egaProbeCircle),
            blade2.Decode.IpnsRound.Element(egaProbeCircle)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpSphere3D(
            blade1.Decode.IpnsRound.Element(),
            blade2.Decode.IpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.Decode.IpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.Decode.IpnsRound.Element(egaProbeSphere),
            blade2.Decode.IpnsRound.Element(egaProbeSphere)
        ).EncodeIpnsBlade();
    }
}