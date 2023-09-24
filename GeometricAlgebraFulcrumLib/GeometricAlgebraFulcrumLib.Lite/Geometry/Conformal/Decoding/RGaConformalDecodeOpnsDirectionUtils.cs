using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, Float64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, Float64Vector3D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, RGaConformalBlade egaProbePoint)
    {
        Debug.Assert(
            opnsDirection.IsCGaDirection() &&
            egaProbePoint.IsEGaVector()
        );

        var directionOpEi =
            opnsDirection;

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsDirectionEGaDirection(this RGaConformalBlade opnsDirection)
    {
        return opnsDirection.RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, Float64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, Float64Vector3D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, Float64Vector egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, RGaConformalBlade egaProbePoint)
    {
        Debug.Assert(
            opnsDirection.IsCGaDirection() &&
            egaProbePoint.IsEGaVector()
        );

        var directionOpEi =
            opnsDirection;

        var weight =
            egaProbePoint
                .EGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new RGaConformalDirection(
            opnsDirection.ConformalSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}