using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeIpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsTangentVGaPosition(this CGaFloat64Blade ipnsTangent)
    {
        var cgaGeometricSpace = ipnsTangent.GeometricSpace;

        return ipnsTangent
            .Gp(cgaGeometricSpace.Ei.Lcp(ipnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsTangentVGaDirection(this CGaFloat64Blade ipnsTangent)
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
    public static double DecodeIpnsTangentWeight(this CGaFloat64Blade ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight2D(this CGaFloat64Blade ipnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight3D(this CGaFloat64Blade ipnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight3(this CGaFloat64Blade ipnsTangent, LinFloat64Vector egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangentWeight(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsTangentWeight(this CGaFloat64Blade ipnsTangent, CGaFloat64Blade egaProbePoint)
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
    public static CGaFloat64Tangent DecodeIpnsTangent(this CGaFloat64Blade ipnsTangent)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeIpnsTangent2D(this CGaFloat64Blade ipnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeIpnsTangent3D(this CGaFloat64Blade ipnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeIpnsTangent(this CGaFloat64Blade ipnsTangent, LinFloat64Vector egaProbePoint)
    {
        return ipnsTangent.DecodeIpnsTangent(
            ipnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaFloat64Tangent DecodeIpnsTangent(this CGaFloat64Blade ipnsTangent, CGaFloat64Blade egaProbePoint)
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

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}


}