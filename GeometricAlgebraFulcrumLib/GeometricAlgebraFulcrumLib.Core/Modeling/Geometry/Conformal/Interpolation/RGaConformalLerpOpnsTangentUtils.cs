using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Interpolation;

public static class RGaConformalLerpOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsTangent(),
            blade2.DecodeOpnsTangent()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint2D(
            blade1.DecodeOpnsTangent(egaProbePoint),
            blade2.DecodeOpnsTangent(egaProbePoint)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsTangent(),
            blade2.DecodeOpnsTangent()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsTangent(egaProbePoint.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPoint3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePoint)
    {
        return t.LerpPoint3D(
            blade1.DecodeOpnsTangent(egaProbePoint),
            blade2.DecodeOpnsTangent(egaProbePoint)
        ).EncodeOpnsBlade();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsTangent(),
            blade2.DecodeOpnsTangent()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector2D egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine2D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine2D(
            blade1.DecodeOpnsTangent(egaProbeLine),
            blade2.DecodeOpnsTangent(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsTangent(),
            blade2.DecodeOpnsTangent()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsTangent(egaProbeLine.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentLine3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbeLine)
    {
        return t.LerpLine3D(
            blade1.DecodeOpnsTangent(egaProbeLine),
            blade2.DecodeOpnsTangent(egaProbeLine)
        ).EncodeOpnsBlade();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsTangent(),
            blade2.DecodeOpnsTangent()
        ).EncodeOpnsBlade();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, LinFloat64Vector3D egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace)),
            blade2.DecodeOpnsTangent(egaProbePlane.EncodeEGaVectorBlade(blade1.ConformalSpace))
        ).EncodeOpnsBlade();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade LerpOpnsTangentPlane3D(this double t, RGaConformalBlade blade1, RGaConformalBlade blade2, RGaConformalBlade egaProbePlane)
    {
        return t.LerpPlane3D(
            blade1.DecodeOpnsTangent(egaProbePlane),
            blade2.DecodeOpnsTangent(egaProbePlane)
        ).EncodeOpnsBlade();
    }
}