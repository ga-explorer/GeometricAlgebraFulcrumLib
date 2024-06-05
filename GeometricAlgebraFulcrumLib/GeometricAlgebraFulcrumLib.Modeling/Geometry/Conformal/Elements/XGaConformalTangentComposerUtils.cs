using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public static class XGaConformalTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentLineFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = 
            conformalSpace
                .EncodeEGaVector(point2 - point1)
                .Op(conformalSpace.EncodeEGaVector(point3 - point2));

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            direction
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = 
            conformalSpace
                .EncodeEGaVector(point2 - point1)
                .Op(conformalSpace.EncodeEGaVector(point3 - point2));

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            direction
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentPlaneFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentVolume<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangent<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangent<T>(this XGaConformalSpace<T> conformalSpace, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangent<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangent<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
    {
        return new XGaConformalTangent<T>(
            conformalSpace,
            weight,
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            conformalSpace.ScalarOne,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineTangent(
            conformalSpace.ScalarOne,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalTangent<T> DefineTangentFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
}