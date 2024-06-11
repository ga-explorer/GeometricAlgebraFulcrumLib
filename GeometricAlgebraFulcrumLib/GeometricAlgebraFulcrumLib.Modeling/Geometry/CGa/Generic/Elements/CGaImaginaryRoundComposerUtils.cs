using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaImaginaryRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared.Negative(),
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1.ToXGaVector(cgaGeometricSpace.Processor),
            point2.ToXGaVector(cgaGeometricSpace.Processor),
            point3.ToXGaVector(cgaGeometricSpace.Processor)
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1.ToXGaVector(cgaGeometricSpace.Processor),
            point2.ToXGaVector(cgaGeometricSpace.Processor),
            point3.ToXGaVector(cgaGeometricSpace.Processor)
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = cgaGeometricSpace.ScalarOne;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square().Negative(),
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square().Negative(),
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineImaginaryRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


}