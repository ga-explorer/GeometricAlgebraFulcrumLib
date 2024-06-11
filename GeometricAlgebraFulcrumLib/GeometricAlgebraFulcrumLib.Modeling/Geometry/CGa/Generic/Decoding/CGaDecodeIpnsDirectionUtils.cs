using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this CGaBlade<T> ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this CGaBlade<T> ipnsDirection, LinVector2D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this CGaBlade<T> ipnsDirection, LinVector3D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this CGaBlade<T> ipnsDirection, XGaVector<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsDirectionWeight<T>(this CGaBlade<T> ipnsDirection, CGaBlade<T> egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsDirectionVGaDirection<T>(this CGaBlade<T> ipnsDirection)
    {
        return ipnsDirection.CGaUnDual().RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DecodeIpnsDirection<T>(this CGaBlade<T> ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DecodeIpnsDirection<T>(this CGaBlade<T> ipnsDirection, LinVector2D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DecodeIpnsDirection<T>(this CGaBlade<T> ipnsDirection, LinVector3D<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DecodeIpnsDirection<T>(this CGaBlade<T> ipnsDirection, XGaVector<T> egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DecodeIpnsDirection<T>(this CGaBlade<T> ipnsDirection, CGaBlade<T> egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaDirection<T>(
            ipnsDirection.GeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}