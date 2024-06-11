using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodeIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsTangentVGaPosition<T>(this CGaBlade<T> ipnsTangent)
    {
        var cgaGeometricSpace = ipnsTangent.GeometricSpace;

        return ipnsTangent
            .Gp(cgaGeometricSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaBlade<T> DecodeIpnsTangentVGaDirection<T>(this CGaBlade<T> ipnsTangent)
    {
        var cgaGeometricSpace = ipnsTangent.GeometricSpace;

        return cgaGeometricSpace.Ei
            .Lcp(ipnsTangent.CGaUnDual())
            .Op(cgaGeometricSpace.Ei)
            .Negative()
            .RemoveEi()
            .DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight<T>(this CGaBlade<T> ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight2D<T>(this CGaBlade<T> ipnsTangent, LinVector2D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight3D<T>(this CGaBlade<T> ipnsTangent, LinVector3D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight3<T>(this CGaBlade<T> ipnsTangent, LinVector<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DecodeIpnsTangentWeight<T>(this CGaBlade<T> ipnsTangent, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsTangent.GeometricSpace;

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(ipnsTangent.CGaUnDual())
                .Op(cgaGeometricSpace.Ei)
                .Negative();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeIpnsTangent<T>(this CGaBlade<T> ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeIpnsTangent2D<T>(this CGaBlade<T> ipnsTangent, LinVector2D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeIpnsTangent3D<T>(this CGaBlade<T> ipnsTangent, LinVector3D<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DecodeIpnsTangent<T>(this CGaBlade<T> ipnsTangent, LinVector<T> egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaTangent<T> DecodeIpnsTangent<T>(this CGaBlade<T> ipnsTangent, CGaBlade<T> egaProbePoint)
    {
        var cgaGeometricSpace = ipnsTangent.GeometricSpace;

        var position =
            ipnsTangent
                .Gp(cgaGeometricSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
                .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);

        var directionOpEi =
            cgaGeometricSpace.Ei
                .Lcp(ipnsTangent.CGaUnDual())
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


}