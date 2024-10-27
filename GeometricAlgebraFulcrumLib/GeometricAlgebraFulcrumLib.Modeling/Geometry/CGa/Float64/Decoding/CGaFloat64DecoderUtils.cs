﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;

public static class CGaFloat64DecoderUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar DecodeVGaBladeToRGaScalar(this RGaFloat64Scalar cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return cgaGeometricSpace.EuclideanProcessor.Scalar(
            cgaKVector.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector DecodeVGaBladeToLinVector(this RGaFloat64Vector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            LinFloat64VectorComposer.Create();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector DecodeVGaBladeToRGaVector(this RGaFloat64Vector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetVectorTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector DecodeVGaBladeToRGaBivector(this RGaFloat64Bivector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector DecodeVGaBladeToRGaKVector(this RGaFloat64KVector cgaKVector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id >> 2, scalar);

        return composer.GetKVector(cgaKVector.Grade);
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperSphereVGaCenter(this RGaFloat64Vector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return cgaGeometricSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart((int i) => i >= 2)
            .Divide(weight)
            .ToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Blade DecodeIpnsHyperSphereVGaCenter(this RGaFloat64Multivector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereVGaCenter(
            cgaGeometricSpace
        );
    }

    public static Tuple<double, CGaFloat64Blade> DecodeIpnsHyperSphereWeightVGaCenter(this RGaFloat64Vector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<double, CGaFloat64Blade>(
                0,
                cgaGeometricSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<double, CGaFloat64Blade>(
                weight,
                vector
                    .GetVectorPart((int i) => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<double, CGaFloat64Blade> DecodeIpnsHyperSphereWeightVGaCenter(this RGaFloat64Multivector vector, CGaFloat64GeometricSpace cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightVGaCenter(
            cgaGeometricSpace
        );
    }


}