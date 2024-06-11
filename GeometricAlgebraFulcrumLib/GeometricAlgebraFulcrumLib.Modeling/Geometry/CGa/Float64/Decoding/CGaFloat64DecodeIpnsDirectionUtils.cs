using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecodeIpnsDirectionUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this CGaFloat64Blade ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this CGaFloat64Blade ipnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this CGaFloat64Blade ipnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this CGaFloat64Blade ipnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirectionWeight(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double DecodeIpnsDirectionWeight(this CGaFloat64Blade ipnsDirection, CGaFloat64Blade egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        return egaProbePoint
            .VGaVectorToIpnsPoint()
            .Lcp(directionOpEi)
            .SpSquared()
            .SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsDirectionVGaDirection(this CGaFloat64Blade ipnsDirection)
    {
        return ipnsDirection.CGaUnDual().RemoveEi().DivideByNorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeIpnsDirection(this CGaFloat64Blade ipnsDirection)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.ZeroVectorBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeIpnsDirection(this CGaFloat64Blade ipnsDirection, LinFloat64Vector2D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeIpnsDirection(this CGaFloat64Blade ipnsDirection, LinFloat64Vector3D egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeIpnsDirection(this CGaFloat64Blade ipnsDirection, RGaFloat64Vector egaProbePoint)
    {
        return ipnsDirection.DecodeIpnsDirection(
            ipnsDirection.GeometricSpace.EncodeVGaVector(egaProbePoint)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DecodeIpnsDirection(this CGaFloat64Blade ipnsDirection, CGaFloat64Blade egaProbePoint)
    {
        Debug.Assert(
            ipnsDirection.IsCGaDirection() &&
            egaProbePoint.IsVGaVector()
        );

        var directionOpEi =
            ipnsDirection.CGaUnDual();

        var weight =
            egaProbePoint
                .VGaVectorToIpnsPoint()
                .Lcp(directionOpEi)
                .SpSquared()
                .SqrtOfAbs();

        return new CGaFloat64Direction(
            ipnsDirection.GeometricSpace,
            weight,
            directionOpEi.RemoveEi()
        );
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade2D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade3D blade)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection()
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade2D blade, IParametricCurve2D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaConformalParametricElement DecodeIpnsDirection(this RGaConformalParametricBlade3D blade, IParametricCurve3D egaProbePoint)
    //{
    //    if (!blade.Specs.IsIpnsDirection)
    //        throw new InvalidOperationException();

    //    return RGaConformalParametricElement.Create(
    //        blade.Specs,
    //        blade.ParameterRange,
    //        t => blade.GetBlade(t).DecodeIpnsDirection(
    //            egaProbePoint.GetPoint(t).EncodeVGaVectorBlade(blade.ConformalSpace)
    //        )
    //    );
    //}

}