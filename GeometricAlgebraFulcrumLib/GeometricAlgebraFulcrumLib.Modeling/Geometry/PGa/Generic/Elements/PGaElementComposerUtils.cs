using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.PGa.Generic.Elements;

public static class PGaElementComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPoint<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> position, LinVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position, XGaVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector<T> position, LinVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLine<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementLineFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position, LinVector3D<T> normal)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlaneFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementVolume<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementVolume<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementPlane<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementVolume<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElement<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElement<T>(this PGaGeometricSpace<T> pgaGeometricSpace, PGaBlade<T> position, PGaBlade<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            pgaGeometricSpace.ScalarOne,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElement<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            pgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElement<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, PGaBlade<T> position, PGaBlade<T> direction)
    {
        return new PGaElement<T>(
            pgaGeometricSpace,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            pgaGeometricSpace.ScalarOne,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            pgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(pgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            pgaGeometricSpace.ScalarOne,
            position,
            directionVectors.Op(pgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            weight,
            pgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(pgaGeometricSpace.Processor))
                .Op(pgaGeometricSpace.Processor)
                .EncodeVGaBlade(pgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            weight,
            position,
            directionVectors.Op(pgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromVectors<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return pgaGeometricSpace.DefineElement(
            weight,
            position,
            directionVectors.Op(pgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, params LinVector2D<T>[] egaPoints)
    {
        return pgaGeometricSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(pgaGeometricSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, params LinVector3D<T>[] egaPoints)
    {
        return pgaGeometricSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(pgaGeometricSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, params LinVector<T>[] egaPoints)
    {
        return pgaGeometricSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(pgaGeometricSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, params XGaVector<T>[] egaPoints)
    {
        return pgaGeometricSpace
            .EncodePGaElementFromPoints(egaPoints)
            .DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PGaElement<T> DefineElementFromPoints<T>(this PGaGeometricSpace<T> pgaGeometricSpace, Scalar<T> weight, IReadOnlyList<XGaVector<T>> egaPoints)
    {
        return pgaGeometricSpace
            .EncodePGaElementFromPoints(egaPoints)
            .DecodePGaElement();
    }
}