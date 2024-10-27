using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpOpnsRoundUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsRound.Element(egaProbePoint),
            blade2.Decode.OpnsRound.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsRound.Element(egaProbePoint),
            blade2.Decode.OpnsRound.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair2D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.Decode.OpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair2D(
            blade1.Decode.OpnsRound.Element(egaProbePointPair),
            blade2.Decode.OpnsRound.Element(egaProbePointPair)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPointPair3D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.Decode.OpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbePointPair.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundPointPair3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePointPair)
    {
        return t.LerpPointPair3D(
            blade1.Decode.OpnsRound.Element(egaProbePointPair),
            blade2.Decode.OpnsRound.Element(egaProbePointPair)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle2D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.Decode.OpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle2D(
            blade1.Decode.OpnsRound.Element(egaProbeCircle),
            blade2.Decode.OpnsRound.Element(egaProbeCircle)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpCircle3D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.Decode.OpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbeCircle.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundCircle3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeCircle)
    {
        return t.LerpCircle3D(
            blade1.Decode.OpnsRound.Element(egaProbeCircle),
            blade2.Decode.OpnsRound.Element(egaProbeCircle)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpSphere3D(
            blade1.Decode.OpnsRound.Element(),
            blade2.Decode.OpnsRound.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.Decode.OpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsRound.Element(egaProbeSphere.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsRoundSphere3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeSphere)
    {
        return t.LerpSphere3D(
            blade1.Decode.OpnsRound.Element(egaProbeSphere),
            blade2.Decode.OpnsRound.Element(egaProbeSphere)
        ).EncodeOpnsBlade();
    }
}