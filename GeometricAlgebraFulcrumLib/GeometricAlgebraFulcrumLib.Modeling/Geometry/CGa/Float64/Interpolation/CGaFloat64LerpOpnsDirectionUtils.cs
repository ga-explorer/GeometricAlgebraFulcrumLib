﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Interpolation;

public static class CGaFloat64LerpOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection.Element(),
            blade2.DecodeOpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine2D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection.Element(egaProbeLine),
            blade2.DecodeOpnsDirection.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection.Element(),
            blade2.DecodeOpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionLine3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection.Element(egaProbeLine),
            blade2.DecodeOpnsDirection.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection.Element(),
            blade2.DecodeOpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.DecodeOpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade LerpOpnsDirectionPlane3D(this double t, CGaFloat64Blade blade1, CGaFloat64Blade blade2, CGaFloat64Blade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection.Element(egaProbePlane),
            blade2.DecodeOpnsDirection.Element(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}