using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsTangent.Element(),
            blade2.Decode.IpnsTangent.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.Decode.IpnsTangent.Element(egaProbePoint),
            blade2.Decode.IpnsTangent.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsTangent.Element(),
            blade2.Decode.IpnsTangent.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsTangent.Element(egaProbePoint.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPoint3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.Decode.IpnsTangent.Element(egaProbePoint),
            blade2.Decode.IpnsTangent.Element(egaProbePoint)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsTangent.Element(),
            blade2.Decode.IpnsTangent.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsTangent.Element(egaProbeLine),
            blade2.Decode.IpnsTangent.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsTangent.Element(),
            blade2.Decode.IpnsTangent.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsTangent.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsTangent.Element(egaProbeLine),
            blade2.Decode.IpnsTangent.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsTangent.Element(),
            blade2.Decode.IpnsTangent.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsTangent.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsTangent.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsTangentPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsTangent.Element(egaProbePlane),
            blade2.Decode.IpnsTangent.Element(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}