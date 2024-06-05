using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsTangentEGaPosition(this RGaConformalBlade ipnsTangent)
    {
        var conformalSpace = ipnsTangent.ConformalSpace;

        return ipnsTangent
            .Gp(conformalSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeIpnsTangentEGaDirection(this RGaConformalBlade ipnsTangent)
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
    public static double DecodeIpnsTangentWeight(this RGaConformalBlade ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight2D(this RGaConformalBlade ipnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight3D(this RGaConformalBlade ipnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight3(this RGaConformalBlade ipnsTangent, LinFloat64Vector egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight(this RGaConformalBlade ipnsTangent, RGaConformalBlade egaProbePoint)
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
    public static RGaConformalTangent DecodeIpnsTangent(this RGaConformalBlade ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeIpnsTangent2D(this RGaConformalBlade ipnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeIpnsTangent3D(this RGaConformalBlade ipnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeIpnsTangent(this RGaConformalBlade ipnsTangent, LinFloat64Vector egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static RGaConformalTangent DecodeIpnsTangent(this RGaConformalBlade ipnsTangent, RGaConformalBlade egaProbePoint)
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

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsTangent(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsTangent(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsTangent(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsTangent(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


}