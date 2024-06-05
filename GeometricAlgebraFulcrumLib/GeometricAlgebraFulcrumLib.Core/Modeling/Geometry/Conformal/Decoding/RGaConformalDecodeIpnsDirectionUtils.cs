using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this RGaConformalBlade ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this RGaConformalBlade ipnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this RGaConformalBlade ipnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this RGaConformalBlade ipnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this RGaConformalBlade ipnsDirection, RGaConformalBlade egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsEGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsDirectionEGaDirection(this RGaConformalBlade ipnsDirection)
    {
        return ipnsDirection.CGaUnDual().RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeIpnsDirection(this RGaConformalBlade ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeIpnsDirection(this RGaConformalBlade ipnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeIpnsDirection(this RGaConformalBlade ipnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeIpnsDirection(this RGaConformalBlade ipnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DecodeIpnsDirection(this RGaConformalBlade ipnsDirection, RGaConformalBlade egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsEGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        var weight =
            egaProbePoint
                .EGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new RGaConformalDirection(
            ipnsDirection.ConformalSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}