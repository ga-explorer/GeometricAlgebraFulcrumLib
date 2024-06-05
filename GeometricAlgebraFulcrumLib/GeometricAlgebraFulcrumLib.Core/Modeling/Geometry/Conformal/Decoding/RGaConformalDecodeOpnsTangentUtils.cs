using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;

public static class RGaConformalDecodeOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsTangentEGaPosition(this RGaConformalBlade opnsTangent)
    {
        var conformalSpace = opnsTangent.ConformalSpace;

        return opnsTangent
            .Gp(conformalSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereEGaCenter(conformalSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalBlade DecodeOpnsTangentEGaDirection(this RGaConformalBlade opnsTangent)
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
    public static double DecodeOpnsTangentWeight(this RGaConformalBlade opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight2D(this RGaConformalBlade opnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight3D(this RGaConformalBlade opnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight3(this RGaConformalBlade opnsTangent, LinFloat64Vector egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight(this RGaConformalBlade opnsTangent, RGaConformalBlade egaProbePoint)
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
    public static RGaConformalTangent DecodeOpnsTangent(this RGaConformalBlade opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeOpnsTangent2D(this RGaConformalBlade opnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeOpnsTangent3D(this RGaConformalBlade opnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DecodeOpnsTangent(this RGaConformalBlade opnsTangent, LinFloat64Vector egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.ConformalSpace.EncodeEGaVector(egaProbePoint)
        );
    }

    public static RGaConformalTangent DecodeOpnsTangent(this RGaConformalBlade opnsTangent, RGaConformalBlade egaProbePoint)
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

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            position,
            directionOpEi.RemoveEi()
        );
    }
    
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsTangent(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsTangent(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent()
    //    );
    //}
    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsTangent(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsTangent(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsTangent)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsTangent(
    //            egaProbePoint.GetPoint(t).EncodeEGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}