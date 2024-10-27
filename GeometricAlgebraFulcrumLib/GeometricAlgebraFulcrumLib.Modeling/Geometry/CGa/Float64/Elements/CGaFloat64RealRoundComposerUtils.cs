using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64RealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPairFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1.ToRGaFloat64Vector(),
            point2.ToRGaFloat64Vector(),
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundCircleFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Circle(
            point1,
            point2,
            point3
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = 1;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundSphereFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = cgaGeometricSpace.Encode.OpnsRound.Sphere(
            point1,
            point2,
            point3,
            point4
        ).DecodeOpnsRound.Element();

        round.Weight = weight;

        return round;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            1,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            1,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            1,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            1,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            1,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRealRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

}