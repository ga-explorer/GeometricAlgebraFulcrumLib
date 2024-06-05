using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeIpnsDirection(egaProbeLine),
            blade2.DecodeIpnsDirection(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeIpnsDirection(egaProbeLine),
            blade2.DecodeIpnsDirection(egaProbeLine)
        ).EncodeIpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(),
            blade2.DecodeIpnsDirection()
        ).EncodeIpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeIpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeIpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpIpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeIpnsDirection(egaProbePlane),
            blade2.DecodeIpnsDirection(egaProbePlane)
        ).EncodeIpnsBlade();
    }
}