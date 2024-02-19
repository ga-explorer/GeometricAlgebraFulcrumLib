using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public static class RGaConformalImaginaryRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double radius, Float64Vector2D position, Float64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D position, Float64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double radius, Float64Vector position, Float64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, Float64Vector point1, Float64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D point1, Float64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D point1, Float64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector point1, Float64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector2D position, Float64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector3D position, Float64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector position, Float64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector2D position, Float64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D position, Float64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector2D position, Float64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector3D position, Float64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2, Float64Vector2D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, Float64Vector point1, Float64Vector point2, Float64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(), 
            point2.ToRGaFloat64Vector(), 
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D point1, Float64Vector2D point2, Float64Vector2D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector point1, Float64Vector point2, Float64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(), 
            point2.ToRGaFloat64Vector(), 
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = weight;
        round.RadiusSquared = -round.RadiusSquared;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D position, Float64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector3D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector3D position, Float64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundSphereFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3, Float64Vector3D point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
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
    public static RGaConformalRound DefineImaginaryRoundSphereFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
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
    public static RGaConformalRound DefineImaginaryRoundSphereFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3, Float64Vector3D point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
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
    public static RGaConformalRound DefineImaginaryRoundSphereFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
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
    public static RGaConformalRound DefineImaginaryRound(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRound(this RGaConformalSpace conformalSpace, double radius, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            -radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRound(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRound(this RGaConformalSpace conformalSpace, double weight, double radius, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            -radius * radius,
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, Float64Vector2D position, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            1,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, Float64Vector3D position, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            1,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, Float64Vector position, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            1,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            1,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector2D position, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector3D position, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, Float64Vector position, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            weight,
            radius,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineImaginaryRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineImaginaryRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
    
    
}