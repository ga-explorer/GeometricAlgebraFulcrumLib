using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaDirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionScalar<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.Scalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionScalar<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, IntegerSign direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.Scalar(direction.ToInt32())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaBivector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinBivector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinBivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaBivector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinTrivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaKVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, CGaBlade<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            cgaGeometricSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            cgaGeometricSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            cgaGeometricSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            cgaGeometricSpace.ScalarOne,
            directionVectors.Op(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            cgaGeometricSpace.ScalarOne,
            directionVectors.Op(cgaGeometricSpace.EuclideanProcessor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(cgaGeometricSpace.EuclideanProcessor)).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Op(cgaGeometricSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Op(cgaGeometricSpace.EuclideanProcessor)
        );
    }
}