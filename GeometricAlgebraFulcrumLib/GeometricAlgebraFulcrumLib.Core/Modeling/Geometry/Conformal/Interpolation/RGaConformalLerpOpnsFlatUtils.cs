using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpOpnsFlatUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsFlat(egaProbePoint),
            blade2.DecodeOpnsFlat(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsFlat(egaProbePoint),
            blade2.DecodeOpnsFlat(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsFlat(egaProbeLine),
            blade2.DecodeOpnsFlat(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsFlat(egaProbeLine),
            blade2.DecodeOpnsFlat(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(),
            blade2.DecodeOpnsFlat()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsFlat(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsFlatPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsFlat(egaProbePlane),
            blade2.DecodeOpnsFlat(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}