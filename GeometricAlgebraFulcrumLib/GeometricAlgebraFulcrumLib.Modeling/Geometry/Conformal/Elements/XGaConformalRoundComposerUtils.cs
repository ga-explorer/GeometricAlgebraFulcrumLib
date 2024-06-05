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

public static class XGaConformalRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> centerX, Scalar<T> centerY)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(centerY, centerY),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(centerY, centerY, centerZ),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> center, Scalar<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPoint<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> center, Scalar<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            conformalSpace.ScalarZero,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector2D<T> center, LinVector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector<T> center, LinVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, LinVector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector<T> center, LinVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundPointPair<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector2D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector2D<T> center, LinBivector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> normalDirection)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, LinBivector2D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> normalDirection)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinBivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaBivector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinBivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundCircle<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaBivector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinTrivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundSphere<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinTrivector3D<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaKVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaConformalBlade<T> center, XGaConformalBlade<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaKVector<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRound<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaConformalBlade<T> center, XGaConformalBlade<T> direction)
    {
        return new XGaConformalRound<T>(
            conformalSpace,
            weight,
            radiusSquared,
            center,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector2D<T> center, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector3D<T> center, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, LinVector<T> center, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            conformalSpace.ScalarOne,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaVector<T> center, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            conformalSpace.ScalarOne,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> radiusSquared, XGaVector<T> center, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineRound(
            conformalSpace.ScalarOne,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector<T> center, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToXGaVector(conformalSpace.Processor))
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params LinVector2D<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params LinVector3D<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params LinVector<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, params XGaVector<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, IEnumerable<XGaVector<T>> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector2D<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector3D<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params XGaVector<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalRound<T> DefineRoundFromPoints<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, IEnumerable<XGaVector<T>> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

}