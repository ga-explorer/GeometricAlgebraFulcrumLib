using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            cgaGeometricSpace
                .EncodeVGa.Vector(point2 - point1)
                .Op(cgaGeometricSpace.EncodeVGa.Vector(point3 - point2));

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            cgaGeometricSpace
                .EncodeVGa.Vector(point2 - point1)
                .Op(cgaGeometricSpace.EncodeVGa.Vector(point3 - point2));

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangent<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangent<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangent<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangent<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaTangent<T>(
            cgaGeometricSpace,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            cgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            cgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaTangent<T> DefineTangentFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }
}