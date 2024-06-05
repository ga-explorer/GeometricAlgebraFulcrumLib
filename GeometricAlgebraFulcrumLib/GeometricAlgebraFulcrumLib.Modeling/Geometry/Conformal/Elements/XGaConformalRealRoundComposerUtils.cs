using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public static class XGaConformalRealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPairFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector2D<T> position)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(LinBivector2D<T>.E12(conformalSpace.ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(LinBivector2D<T>.E12(conformalSpace.ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToXGaVector(conformalSpace.Processor), 
            point2.ToXGaVector(conformalSpace.Processor), 
            point3.ToXGaVector(conformalSpace.Processor)
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToXGaVector(conformalSpace.Processor), 
            point2.ToXGaVector(conformalSpace.Processor), 
            point3.ToXGaVector(conformalSpace.Processor)
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundCircleFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector3D<T> position)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = conformalSpace.ScalarOne;

        return round;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundSphereFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radius.Square(),
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaConformalBlade<T> position, XGaConformalBlade<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radius.Square(),
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            conformalSpace.ScalarOne,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            conformalSpace.ScalarOne,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            conformalSpace.ScalarOne,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            conformalSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineRealRound(
            conformalSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRealRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
    
}