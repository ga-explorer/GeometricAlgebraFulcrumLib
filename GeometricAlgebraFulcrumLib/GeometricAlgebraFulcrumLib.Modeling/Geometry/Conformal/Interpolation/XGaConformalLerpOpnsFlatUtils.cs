using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Interpolation;

public static class XGaConformalLerpOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(egaProbePoint),
            blade2.DecodeOpnsFlat(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPoint3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(egaProbePoint),
            blade2.DecodeOpnsFlat(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector2D<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine2D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(egaProbeLine),
            blade2.DecodeOpnsFlat(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatLine3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(egaProbeLine),
            blade2.DecodeOpnsFlat(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, LinVector3D<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> LerpOpnsFlatPlane3D<T>(this Scalar<T> t, XGaConformalBlade<T> blade1, XGaConformalBlade<T> blade2, XGaConformalBlade<T> egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(egaProbePlane),
            blade2.DecodeOpnsFlat(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}