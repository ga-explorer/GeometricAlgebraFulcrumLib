using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;


public static class CGaFloat64LerpIpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat.Element(),
            blade2.DecodeIpnsFlat.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeIpnsFlat.Element(egaProbePoint),
            blade2.DecodeIpnsFlat.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat.Element(),
            blade2.DecodeIpnsFlat.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsFlat.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPoint3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeIpnsFlat.Element(egaProbePoint),
            blade2.DecodeIpnsFlat.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat.Element(),
            blade2.DecodeIpnsFlat.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsFlat.Element(egaProbeLine),
            blade2.DecodeIpnsFlat.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat.Element(),
            blade2.DecodeIpnsFlat.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsFlat.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsFlat.Element(egaProbeLine),
            blade2.DecodeIpnsFlat.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat.Element(),
            blade2.DecodeIpnsFlat.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeIpnsFlat.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpIpnsFlatPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsFlat.Element(egaProbePlane),
            blade2.DecodeIpnsFlat.Element(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}