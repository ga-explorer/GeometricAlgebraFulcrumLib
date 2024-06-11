using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaDirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionScalar<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeScalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionScalar<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, IntegerSign direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeScalar(direction.ToInt32())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarProcessor.One,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinBivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaBivector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinBivector2D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinBivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaBivector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirectionVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinTrivector3D<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaDirection<T> DefineDirection<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaKVector<T> direction)
    {
        return new CGaDirection<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGaBlade(direction)
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
            cgaGeometricSpace.EncodeVGaBlade(direction)
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