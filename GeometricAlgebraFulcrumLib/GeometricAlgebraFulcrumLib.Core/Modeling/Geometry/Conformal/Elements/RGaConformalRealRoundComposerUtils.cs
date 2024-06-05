using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Decoding;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

public static class RGaConformalRealRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).ENormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPairFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var center = (point1 + point2) / 2;
        var direction = point2 - point1;
        var radiusSquared = (point1 - center).NormSquared();

        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector2D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector2D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(LinFloat64Bivector2D.E12)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(), 
            point2.ToRGaFloat64Vector(), 
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1, 
            point2, 
            point3
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
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
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
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
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var round = conformalSpace.EncodeOpnsRoundCircle(
            point1.ToRGaFloat64Vector(), 
            point2.ToRGaFloat64Vector(), 
            point3.ToRGaFloat64Vector()
        ).DecodeOpnsRound();

        round.Weight = weight;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundCircleFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
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
    public static RGaConformalRound DefineRealRoundSphere(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector3D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphere(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector3D position)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
    {
        var round = conformalSpace.EncodeOpnsRoundSphere(
            point1, 
            point2, 
            point3,
            point4
        ).DecodeOpnsRound();

        round.Weight = 1;

        return round;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3, LinFloat64Vector3D point4)
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
    public static RGaConformalRound DefineRealRoundSphereFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3, RGaFloat64Vector point4)
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
    public static RGaConformalRound DefineRealRound(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRound(this RGaConformalSpace conformalSpace, double radius, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radius * radius,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRound(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRound(this RGaConformalSpace conformalSpace, double weight, double radius, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radius * radius,
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            1,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineRealRound(
            1,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
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
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRealRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radius, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineRealRound(
            weight,
            radius,
            position,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
    
}