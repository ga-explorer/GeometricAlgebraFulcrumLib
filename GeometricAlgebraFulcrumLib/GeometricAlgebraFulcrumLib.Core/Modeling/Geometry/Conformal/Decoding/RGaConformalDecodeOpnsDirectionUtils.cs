using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

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
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this RGaConformalBlade opnsDirection, LinFloat64Vector3D egaProbePoint)
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
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeOpnsDirection(this RGaConformalBlade opnsDirection, LinFloat64Vector egaProbePoint)
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