using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsFlat.Element(),
            blade2.Decode.OpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsFlat.Element(egaProbePoint),
            blade2.Decode.OpnsFlat.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsFlat.Element(),
            blade2.Decode.OpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsFlat.Element(egaProbePoint),
            blade2.Decode.OpnsFlat.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsFlat.Element(),
            blade2.Decode.OpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsFlat.Element(egaProbeLine),
            blade2.Decode.OpnsFlat.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsFlat.Element(),
            blade2.Decode.OpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsFlat.Element(egaProbeLine),
            blade2.Decode.OpnsFlat.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsFlat.Element(),
            blade2.Decode.OpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsFlat.Element(egaProbePlane),
            blade2.Decode.OpnsFlat.Element(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}