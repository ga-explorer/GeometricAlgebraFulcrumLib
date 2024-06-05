using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsTangentEGaPosition<T>(this XGaConformalBlade<T> opnsTangent)
    {
        var conformalSpace = opnsTangent.ConformalSpace;

        return opnsTangent
            .Gp(conformalSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeOpnsTangentEGaDirection<T>(this XGaConformalBlade<T> opnsTangent)
    {
        var conformalSpace = opnsTangent.ConformalSpace;

        return conformalSpace.Ei
            .Lcp(opnsTangent)
            .Op(conformalSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight<T>(this XGaConformalBlade<T> opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight2D<T>(this XGaConformalBlade<T> opnsTangent, LinVector2D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight3D<T>(this XGaConformalBlade<T> opnsTangent, LinVector3D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight3<T>(this XGaConformalBlade<T> opnsTangent, LinVector<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight<T>(this XGaConformalBlade<T> opnsTangent, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = opnsTangent.ConformalSpace;

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(opnsTangent)
                .Op(conformalSpace.Ei)
                .Negative();

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeOpnsTangent<T>(this XGaConformalBlade<T> opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeOpnsTangent2D<T>(this XGaConformalBlade<T> opnsTangent, LinVector2D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeOpnsTangent3D<T>(this XGaConformalBlade<T> opnsTangent, LinVector3D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeOpnsTangent<T>(this XGaConformalBlade<T> opnsTangent, LinVector<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static XGaConformalTangent<T> DecodeOpnsTangent<T>(this XGaConformalBlade<T> opnsTangent, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = opnsTangent.ConformalSpace;

        var position =
            opnsTangent
                .Gp(conformalSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
                .DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(opnsTangent)
                .Op(conformalSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .EGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsTangent<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsTangent<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsTangent<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeOpnsTangent<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}