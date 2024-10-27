using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat.Element(),
            blade2.DecodeOpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat.Element(egaProbePoint),
            blade2.DecodeOpnsFlat.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat.Element(),
            blade2.DecodeOpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat.Element(egaProbePoint),
            blade2.DecodeOpnsFlat.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat.Element(),
            blade2.DecodeOpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat.Element(egaProbeLine),
            blade2.DecodeOpnsFlat.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat.Element(),
            blade2.DecodeOpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat.Element(egaProbeLine),
            blade2.DecodeOpnsFlat.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat.Element(),
            blade2.DecodeOpnsFlat.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat.Element(egaProbePlane),
            blade2.DecodeOpnsFlat.Element(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}