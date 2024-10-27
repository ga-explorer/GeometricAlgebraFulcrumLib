using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Interpolation;

public static class CGaLerpIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsDirection.Element(),
            blade2.Decode.IpnsDirection.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.Decode.IpnsDirection.Element(egaProbeLine),
            blade2.Decode.IpnsDirection.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsDirection.Element(),
            blade2.Decode.IpnsDirection.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsDirection.Element(egaProbeLine.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.Decode.IpnsDirection.Element(egaProbeLine),
            blade2.Decode.IpnsDirection.Element(egaProbeLine)
        ).EncodeIpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsDirection.Element(),
            blade2.Decode.IpnsDirection.Element()
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace)),
            blade2.Decode.IpnsDirection.Element(egaProbePlane.EncodeVGaVector(blade1.GeometricSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, CGaBlade<T> blade1, CGaBlade<T> blade2, CGaBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.Decode.IpnsDirection.Element(egaProbePlane),
            blade2.Decode.IpnsDirection.Element(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}