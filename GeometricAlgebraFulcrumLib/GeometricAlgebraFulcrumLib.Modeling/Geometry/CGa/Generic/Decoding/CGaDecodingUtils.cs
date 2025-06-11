using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;

public static class CGaDecodingUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaScalar<T> DecodeVGaBladeToXGaScalar<T>(this XGaScalar<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return cgaGeometricSpace.EuclideanProcessor.Scalar(
            cgaKVector.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static LinVector<T> DecodeVGaBladeToLinVector<T>(this XGaVector<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer = cgaGeometricSpace.ScalarProcessor.CreateLinVectorComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaVector<T> DecodeVGaBladeToXGaVector<T>(this XGaVector<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateVectorComposer();

        foreach (var (index, scalar) in cgaKVector.IndexScalarPairs)
            composer.SetVectorTerm(index - 2, scalar);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaBivector<T> DecodeVGaBladeToXGaBivector<T>(this XGaBivector<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateBivectorComposer();

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static XGaKVector<T> DecodeVGaBladeToXGaKVector<T>(this XGaKVector<T> cgaKVector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        Debug.Assert(
            cgaGeometricSpace.IsValidVGaElement(cgaKVector)
        );

        var composer =
            cgaGeometricSpace.EuclideanProcessor.CreateKVectorComposer(cgaKVector.Grade);

        foreach (var (id, scalar) in cgaKVector.IdScalarPairs)
            composer.SetTerm(id.ShiftIndices(-2), scalar);

        return composer.GetKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaBlade<T> DecodeIpnsHyperSphereVGaCenter<T>(this XGaVector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return cgaGeometricSpace.ZeroVectorBlade;

        return vector
            .GetVectorPart(i => i >= 2)
            .Divide(weight)
            .ToConformalBlade(cgaGeometricSpace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static CGaBlade<T> DecodeIpnsHyperSphereVGaCenter<T>(this XGaMultivector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereVGaCenter(
            cgaGeometricSpace
        );
    }
    
    internal static Tuple<Scalar<T>, CGaBlade<T>> DecodeIpnsHyperSphereWeightVGaCenter<T>(this XGaVector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        var weight = vector[0] + vector[1];

        if (weight.IsNearZero())
            return new Tuple<Scalar<T>, CGaBlade<T>>(
                cgaGeometricSpace.ScalarProcessor.ScalarFromValue(cgaGeometricSpace.ScalarProcessor.OneValue),
                cgaGeometricSpace.ZeroVectorBlade
            );

        return weight.IsNearZero()
            ? throw new InvalidOperationException()
            : new Tuple<Scalar<T>, CGaBlade<T>>(
                weight,
                vector
                    .GetVectorPart(i => i >= 2)
                    .Divide(weight)
                    .ToConformalBlade(cgaGeometricSpace)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Tuple<Scalar<T>, CGaBlade<T>> DecodeIpnsHyperSphereWeightVGaCenter<T>(this XGaMultivector<T> vector, CGaGeometricSpace<T> cgaGeometricSpace)
    {
        return vector.GetVectorPart().DecodeIpnsHyperSphereWeightVGaCenter(
            cgaGeometricSpace
        );
    }


}