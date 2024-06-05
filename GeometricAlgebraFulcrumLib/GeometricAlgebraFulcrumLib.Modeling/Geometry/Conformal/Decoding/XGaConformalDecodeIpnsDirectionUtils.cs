using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this XGaConformalBlade<T> ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this XGaConformalBlade<T> ipnsDirection, LinVector2D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this XGaConformalBlade<T> ipnsDirection, LinVector3D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this XGaConformalBlade<T> ipnsDirection, XGaVector<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this XGaConformalBlade<T> ipnsDirection, XGaConformalBlade<T> egaProbePoint)
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
    public static XGaConformalBlade<T> DecodeIpnsDirectionEGaDirection<T>(this XGaConformalBlade<T> ipnsDirection)
    {
        return ipnsDirection.CGaUnDual().RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeIpnsDirection<T>(this XGaConformalBlade<T> ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeIpnsDirection<T>(this XGaConformalBlade<T> ipnsDirection, LinVector2D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeIpnsDirection<T>(this XGaConformalBlade<T> ipnsDirection, LinVector3D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeIpnsDirection<T>(this XGaConformalBlade<T> ipnsDirection, XGaVector<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeIpnsDirection<T>(this XGaConformalBlade<T> ipnsDirection, XGaConformalBlade<T> egaProbePoint)
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

        return new XGaConformalDirection<T>(
            ipnsDirection.ConformalSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsDirection<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsDirection<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsDirection<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsDirection<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}