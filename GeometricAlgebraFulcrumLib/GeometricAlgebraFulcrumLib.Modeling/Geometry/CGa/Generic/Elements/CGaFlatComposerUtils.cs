using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaFlatComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLine<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatLineFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinVector3D<T> normal)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlaneFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatPlane<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatVolume<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlat<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlat<T>(this CGaGeometricSpace<T> cgaGeometricSpace, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlat<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlat<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaFlat<T>(
            cgaGeometricSpace,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            cgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            cgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector2D<T>[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlat.BladeFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(cgaGeometricSpace.Processor))
            ).Decode.OpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector3D<T>[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlat.BladeFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(cgaGeometricSpace.Processor))
            ).Decode.OpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector<T>[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlat.BladeFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(cgaGeometricSpace.Processor))
            )
            .Decode.OpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params XGaVector<T>[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlat.BladeFromPoints(egaPoints)
            .Decode.OpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFlat<T> DefineFlatFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, IReadOnlyList<XGaVector<T>> egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlat.BladeFromPoints(egaPoints)
            .Decode.OpnsFlat.Element();
    }
}