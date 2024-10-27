using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsTangent.Element(),
            blade2.Decode.OpnsTangent.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.OpnsTangent.Element(egaProbePoint),
            blade2.Decode.OpnsTangent.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsTangent.Element(),
            blade2.Decode.OpnsTangent.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.OpnsTangent.Element(egaProbePoint),
            blade2.Decode.OpnsTangent.Element(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsTangent.Element(),
            blade2.Decode.OpnsTangent.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.OpnsTangent.Element(egaProbeLine),
            blade2.Decode.OpnsTangent.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsTangent.Element(),
            blade2.Decode.OpnsTangent.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.OpnsTangent.Element(egaProbeLine),
            blade2.Decode.OpnsTangent.Element(egaProbeLine)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsTangent.Element(),
            blade2.Decode.OpnsTangent.Element()
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsTangent.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.OpnsTangent.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpOpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.OpnsTangent.Element(egaProbePlane),
            blade2.Decode.OpnsTangent.Element(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}