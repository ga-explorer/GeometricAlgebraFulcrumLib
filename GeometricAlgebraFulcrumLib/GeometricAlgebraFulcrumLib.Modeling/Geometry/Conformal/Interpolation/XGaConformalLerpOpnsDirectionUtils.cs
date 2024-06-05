using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(egaProbeLine),
            blade2.DecodeOpnsDirection(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(egaProbeLine),
            blade2.DecodeOpnsDirection(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsDirectionPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(egaProbePlane),
            blade2.DecodeOpnsDirection(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}