using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsDirection(egaProbeLine),
            blade2.DecodeOpnsDirection(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsDirection(egaProbeLine),
            blade2.DecodeOpnsDirection(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(),
            blade2.DecodeOpnsDirection()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsDirection(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsDirectionPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsDirection(egaProbePlane),
            blade2.DecodeOpnsDirection(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}