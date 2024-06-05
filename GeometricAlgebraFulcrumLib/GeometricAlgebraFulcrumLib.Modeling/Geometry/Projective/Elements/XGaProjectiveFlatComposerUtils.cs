using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Projective.Elements;

public static class XGaProjectiveElementComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPoint<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.OneScalarBlade
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLine<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementLineFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaVector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position, LinVector3D<T> normal)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, Scalar<T> distance, LinVector3D<T> normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlaneFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementVolume<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementVolume<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementPlane<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementVolume<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElement<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElement<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaProjectiveBlade<T> position, XGaProjectiveBlade<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            projectiveSpace.ScalarOne,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElement<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            projectiveSpace.EncodeEGaVector(position),
            projectiveSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElement<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaProjectiveBlade<T> position, XGaProjectiveBlade<T> direction)
    {
        return new XGaProjectiveElement<T>(
            projectiveSpace,
            weight,
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            projectiveSpace.ScalarOne,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            projectiveSpace.ScalarOne,
            position,
            directionVectors.Op(projectiveSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return projectiveSpace.DefineElement(
            projectiveSpace.ScalarOne,
            position,
            directionVectors.Op(projectiveSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            weight,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            weight,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            weight,
            projectiveSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(projectiveSpace.Processor))
                .Op(projectiveSpace.Processor)
                .EncodeEGaBlade(projectiveSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return projectiveSpace.DefineElement(
            weight,
            position,
            directionVectors.Op(projectiveSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromVectors<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return projectiveSpace.DefineElement(
            weight,
            position,
            directionVectors.Op(projectiveSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, params LinVector2D<T>[] egaPoints)
    {
        return projectiveSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(projectiveSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, params LinVector3D<T>[] egaPoints)
    {
        return projectiveSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(projectiveSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, params LinVector<T>[] egaPoints)
    {
        return projectiveSpace
            .EncodePGaElementFromPoints(
                egaPoints.SelectToArray(p => p.ToXGaVector(projectiveSpace.Processor))
            ).DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, params XGaVector<T>[] egaPoints)
    {
        return projectiveSpace
            .EncodePGaElementFromPoints(egaPoints)
            .DecodePGaElement();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaProjectiveElement<T> DefineElementFromPoints<T>(this XGaProjectiveSpace<T> projectiveSpace, Scalar<T> weight, IReadOnlyList<XGaVector<T>> egaPoints)
    {
        return projectiveSpace
            .EncodePGaElementFromPoints(egaPoints)
            .DecodePGaElement();
    }
}