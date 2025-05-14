using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> centerX, Scalar<T> centerY)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(centerX, centerY),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> centerX, Scalar<T> centerY, Scalar<T> centerZ)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(centerX, centerY, centerZ),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> center, Scalar<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPoint<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> center, Scalar<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.ScalarZero,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector2D<T> center, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector<T> center, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector<T> center, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector2D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector2D<T> center, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> normalDirection)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinVector3D<T> normalDirection)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, XGaVector<T> center, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, CGaBlade<T> center, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, CGaBlade<T> center, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector2D<T> center, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector3D<T> center, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, LinVector<T> center, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, XGaVector<T> center, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radiusSquared, XGaVector<T> center, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector2D<T> center, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector3D<T> center, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, LinVector<T> center, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radiusSquared, XGaVector<T> center, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector2D<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector3D<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params LinVector<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, params XGaVector<T>[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, IEnumerable<XGaVector<T>> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector2D<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector3D<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params LinVector<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, params XGaVector<T>[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRoundFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, IEnumerable<XGaVector<T>> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).Decode.OpnsRound.Element();
    }

}