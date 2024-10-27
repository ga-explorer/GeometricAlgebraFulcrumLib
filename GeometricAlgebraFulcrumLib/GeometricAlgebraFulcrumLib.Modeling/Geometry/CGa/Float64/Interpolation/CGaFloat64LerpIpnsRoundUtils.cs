using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpIpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsRound.Element(egaProbePoint),
            blade2.DecodeIpnsRound.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsRound.Element(egaProbePoint),
            blade2.DecodeIpnsRound.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeIpnsRound.Element(egaProbePointPair),
            blade2.DecodeIpnsRound.Element(egaProbePointPair)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeIpnsRound.Element(egaProbePointPair),
            blade2.DecodeIpnsRound.Element(egaProbePointPair)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeIpnsRound.Element(egaProbeCircle),
            blade2.DecodeIpnsRound.Element(egaProbeCircle)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeIpnsRound.Element(egaProbeCircle),
            blade2.DecodeIpnsRound.Element(egaProbeCircle)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound.Element(),
            blade2.DecodeIpnsRound.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeIpnsRound.Element(egaProbeSphere),
            blade2.DecodeIpnsRound.Element(egaProbeSphere)
        ).EncodeIpnsBlade();
    }
}