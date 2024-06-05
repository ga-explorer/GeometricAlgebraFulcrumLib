using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;

public static class XGaConformalDecodeIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsTangentEGaPosition<T>(this XGaConformalBlade<T> ipnsTangent)
    {
        var conformalSpace = ipnsTangent.ConformalSpace;

        return ipnsTangent
            .Gp(conformalSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalBlade<T> DecodeIpnsTangentEGaDirection<T>(this XGaConformalBlade<T> ipnsTangent)
    {
        var conformalSpace = ipnsTangent.ConformalSpace;

        return conformalSpace.Ei
            .Lcp(ipnsTangent.CGaUnDual())
            .Op(conformalSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight<T>(this XGaConformalBlade<T> ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight2D<T>(this XGaConformalBlade<T> ipnsTangent, LinVector2D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight3D<T>(this XGaConformalBlade<T> ipnsTangent, LinVector3D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight3<T>(this XGaConformalBlade<T> ipnsTangent, LinVector<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight<T>(this XGaConformalBlade<T> ipnsTangent, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsTangent.ConformalSpace;

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(ipnsTangent.CGaUnDual())
                .Op(conformalSpace.Ei)
                .Negative();

        return egaProbePoint
            .EGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeIpnsTangent<T>(this XGaConformalBlade<T> ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeIpnsTangent2D<T>(this XGaConformalBlade<T> ipnsTangent, LinVector2D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeIpnsTangent3D<T>(this XGaConformalBlade<T> ipnsTangent, LinVector3D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DecodeIpnsTangent<T>(this XGaConformalBlade<T> ipnsTangent, LinVector<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static XGaConformalTangent<T> DecodeIpnsTangent<T>(this XGaConformalBlade<T> ipnsTangent, XGaConformalBlade<T> egaProbePoint)
    {
        var conformalSpace = ipnsTangent.ConformalSpace;

        var position =
            ipnsTangent
                .Gp(conformalSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
                .DecodeIpnsHyperSphereEGaCenter(conformalSpace);

        var directionOpEi =
            conformalSpace.Ei
                .Lcp(ipnsTangent.CGaUnDual())
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
    //public static XGaConformalParametricElement<T> DecodeIpnsTangent<T>(this XGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsTangent<T>(this XGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsTangent<T>(this XGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static XGaConformalParametricElement<T> DecodeIpnsTangent<T>(this XGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return XGaConformalParametricElement<T>.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


}