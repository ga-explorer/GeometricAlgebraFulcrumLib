using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Elements;

public static class CGaRealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPairFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGa.Vector(center),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinVector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinVector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, LinVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundPointPair<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(LinBivector2D<T>.E12(cgaGeometricSpace.ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(LinBivector2D<T>.E12(cgaGeometricSpace.ScalarProcessor))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, LinBivector2D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinBivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircle<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaBivector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1.ToXGaVector(cgaGeometricSpace.Processor),
            point2.ToXGaVector(cgaGeometricSpace.Processor),
            point3.ToXGaVector(cgaGeometricSpace.Processor)
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector2D<T> point1, LinVector2D<T> point2, LinVector2D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector<T> point1, LinVector<T> point2, LinVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1.ToXGaVector(cgaGeometricSpace.Processor),
            point2.ToXGaVector(cgaGeometricSpace.Processor),
            point3.ToXGaVector(cgaGeometricSpace.Processor)
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundCircleFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Circle(
            point1,
            point2,
            point3
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphere<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, LinTrivector3D<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).Decode.OpnsRound.Element();

        round.Weight = cgaGeometricSpace.ScalarOne;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, LinVector3D<T> point1, LinVector3D<T> point2, LinVector3D<T> point3, LinVector3D<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundSphereFromPoints<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, XGaVector<T> point1, XGaVector<T> point2, XGaVector<T> point3, XGaVector<T> point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).Decode.OpnsRound.Element();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            cgaGeometricSpace.ScalarOne,
            radius.Square(),
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, XGaKVector<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            cgaGeometricSpace.EncodeVGa.Vector(position),
            cgaGeometricSpace.EncodeVGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRound<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, CGaBlade<T> position, CGaBlade<T> direction)
    {
        return new CGaRound<T>(
            cgaGeometricSpace,
            weight,
            radius.Square(),
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            cgaGeometricSpace.ScalarOne,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector2D<T> position, params LinVector2D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector3D<T> position, params LinVector3D<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, LinVector<T> position, params LinVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaVector(cgaGeometricSpace.Processor))
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, params XGaVector<T>[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaRound<T> DefineRealRoundFromVectors<T>(this CGaGeometricSpace<T> cgaGeometricSpace, Scalar<T> weight, Scalar<T> radius, XGaVector<T> position, IEnumerable<XGaVector<T>> directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

}