using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsDirection.Element(),
            blade2.Decode.OpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsDirection.Element(egaProbeLine),
            blade2.Decode.OpnsDirection.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsDirection.Element(),
            blade2.Decode.OpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsDirection.Element(egaProbeLine),
            blade2.Decode.OpnsDirection.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsDirection.Element(),
            blade2.Decode.OpnsDirection.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsDirection.Element(egaProbePlane),
            blade2.Decode.OpnsDirection.Element(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}