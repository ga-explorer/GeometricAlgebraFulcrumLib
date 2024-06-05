using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsDirectionWeight<T>(this XGaConformalBlade<T> opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsDirectionWeight<T>(this XGaConformalBlade<T> opnsDirection, LinVector2D<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsDirectionWeight<T>(this XGaConformalBlade<T> opnsDirection, LinVector3D<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsDirectionWeight<T>(this XGaConformalBlade<T> opnsDirection, XGaVector<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsDirectionWeight<T>(this XGaConformalBlade<T> opnsDirection, XGaConformalBlade<T> egaProbePoint)
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
    public static XGaConformalBlade<T> DecodeOpnsDirectionEGaDirection<T>(this XGaConformalBlade<T> opnsDirection)
    {
        return opnsDirection.RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeOpnsDirection<T>(this XGaConformalBlade<T> opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeOpnsDirection<T>(this XGaConformalBlade<T> opnsDirection, LinVector2D<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeOpnsDirection<T>(this XGaConformalBlade<T> opnsDirection, LinVector3D<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeOpnsDirection<T>(this XGaConformalBlade<T> opnsDirection, LinVector<T> egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DecodeOpnsDirection<T>(this XGaConformalBlade<T> opnsDirection, XGaConformalBlade<T> egaProbePoint)
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

        return new XGaConformalDirection<T>(
            opnsDirection.ConformalSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsDirection<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsDirection<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsDirection<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsDirection<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}