using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeOpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this CGaFloat64Blade opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this CGaFloat64Blade opnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this CGaFloat64Blade opnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this CGaFloat64Blade opnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirectionWeight(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeOpnsDirectionWeight(this CGaFloat64Blade opnsDirection, CGaFloat64Blade egaProbePoint)
    {
        Debug.Assert(
            opnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            opnsDirection;

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeOpnsDirectionVGaDirection(this CGaFloat64Blade opnsDirection)
    {
        return opnsDirection.RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeOpnsDirection(this CGaFloat64Blade opnsDirection)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeOpnsDirection(this CGaFloat64Blade opnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeOpnsDirection(this CGaFloat64Blade opnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeOpnsDirection(this CGaFloat64Blade opnsDirection, LinFloat64Vector egaProbePoint)
    {
        return opnsDirection.DecodeOpnsDirection(
            opnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeOpnsDirection(this CGaFloat64Blade opnsDirection, CGaFloat64Blade egaProbePoint)
    {
        Debug.Assert(
            opnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            opnsDirection;

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaFloat64Direction(
            opnsDirection.GeometricSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeOpnsDirection(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsOpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeOpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}