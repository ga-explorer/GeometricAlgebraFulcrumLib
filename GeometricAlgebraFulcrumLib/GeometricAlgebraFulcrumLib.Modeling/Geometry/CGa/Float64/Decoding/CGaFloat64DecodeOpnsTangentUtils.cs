using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeOpnsTangentUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsTangentVGaPosition(this CGaFloat64Blade opnsTangent)
    {
        var cgaGeometricSpace = opnsTangent.GeometricSpace;

        return opnsTangent
            .Gp(cgaGeometricSpace.Ei.Lcp(opnsTangent).Inverse().Negative())
            .DecodeIpnsHyperSphereVGaCenter(cgaGeometricSpace);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsTangentVGaDirection(this CGaFloat64Blade opnsTangent)
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
    public static double DecodeOpnsTangentWeight(this CGaFloat64Blade opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight2D(this CGaFloat64Blade opnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight3D(this CGaFloat64Blade opnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight3(this CGaFloat64Blade opnsTangent, LinFloat64Vector egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangentWeight(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsTangentWeight(this CGaFloat64Blade opnsTangent, CGaFloat64Blade egaProbePoint)
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
    public static CGaFloat64Tangent DecodeOpnsTangent(this CGaFloat64Blade opnsTangent)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeOpnsTangent2D(this CGaFloat64Blade opnsTangent, LinFloat64Vector2D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeOpnsTangent3D(this CGaFloat64Blade opnsTangent, LinFloat64Vector3D egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DecodeOpnsTangent(this CGaFloat64Blade opnsTangent, LinFloat64Vector egaProbePoint)
    {
        return opnsTangent.DecodeOpnsTangent(
            opnsTangent.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    public static CGaFloat64Tangent DecodeOpnsTangent(this CGaFloat64Blade opnsTangent, CGaFloat64Blade egaProbePoint)
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

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
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
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}