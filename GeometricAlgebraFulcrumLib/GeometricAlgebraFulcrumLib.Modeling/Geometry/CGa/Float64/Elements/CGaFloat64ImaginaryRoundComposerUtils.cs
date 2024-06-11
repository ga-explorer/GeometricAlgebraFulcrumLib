using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64ImaginaryRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
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
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
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
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
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
    public static CGaFloat64Round DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = cgaGeometricSpace.EncodeOpnsRoundSphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
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
    public static CGaFloat64Round DefineImaginaryRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
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
    public static CGaFloat64Round DefineImaginaryRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            -radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            -radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            1,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            1,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            1,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineImaginaryRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


}