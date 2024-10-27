using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsRound.Element(egaProbePoint),
            blade2.DecodeOpnsRound.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsRound.Element(egaProbePoint),
            blade2.DecodeOpnsRound.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.DecodeOpnsRound.Element(egaProbePointPair),
            blade2.DecodeOpnsRound.Element(egaProbePointPair)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundPointPair3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.DecodeOpnsRound.Element(egaProbePointPair),
            blade2.DecodeOpnsRound.Element(egaProbePointPair)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.DecodeOpnsRound.Element(egaProbeCircle),
            blade2.DecodeOpnsRound.Element(egaProbeCircle)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundCircle3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.DecodeOpnsRound.Element(egaProbeCircle),
            blade2.DecodeOpnsRound.Element(egaProbeCircle)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound.Element(),
            blade2.DecodeOpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsRoundSphere3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.DecodeOpnsRound.Element(egaProbeSphere),
            blade2.DecodeOpnsRound.Element(egaProbeSphere)
        ).EncodeOpnsBlade();
    }
}