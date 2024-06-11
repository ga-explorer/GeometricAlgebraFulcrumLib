using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsTangentVGaPosition<T>(this CGaBlade<T> opnsTangent)
    {
        var cgaGeometricSpace = opnsTangent.GeometricSpace;

        return opnsTangent
            .Gp(cgaGeometricSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeOpnsTangentVGaDirection<T>(this CGaBlade<T> opnsTangent)
    {
        var cgaGeometricSpace = opnsTangent.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(opnsTangent)
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight<T>(this CGaBlade<T> opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight2D<T>(this CGaBlade<T> opnsTangent, LinVector2D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight3D<T>(this CGaBlade<T> opnsTangent, LinVector3D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight3<T>(this CGaBlade<T> opnsTangent, LinVector<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeOpnsTangentWeight<T>(this CGaBlade<T> opnsTangent, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = opnsTangent.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(opnsTangent)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeOpnsTangent<T>(this CGaBlade<T> opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeOpnsTangent2D<T>(this CGaBlade<T> opnsTangent, LinVector2D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeOpnsTangent3D<T>(this CGaBlade<T> opnsTangent, LinVector3D<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeOpnsTangent<T>(this CGaBlade<T> opnsTangent, LinVector<T> egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaTangent<T> DecodeOpnsTangent<T>(this CGaBlade<T> opnsTangent, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = opnsTangent.GeometricSpace;

        var position =
            opnsTangent
                .Gp(cgaGeometricSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(opnsTangent)
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaTangent<T>(
            cgaGeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}