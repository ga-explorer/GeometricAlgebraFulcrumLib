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

public static class RGaConformalRoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double centerX, double centerY)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(centerY, centerY),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector2D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double centerX, double centerY, double centerZ)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(centerY, centerY, centerZ),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector3D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector center)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, Float64Vector3D center, double direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector center)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D center, double direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            0,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeScalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D center, Float64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center, Float64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector center, Float64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector2D center, Float64Vector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center, Float64Vector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector center, Float64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundPointPair(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64Vector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D center, Float64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center, Float64Vector3D normalDirection)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector2D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector2D center, Float64Bivector2D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center, Float64Vector3D normalDirection)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center, Float64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center, Float64Bivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundCircle(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64Bivector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundSphere(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundSphere(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center, Float64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundSphere(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center, Float64Trivector3D direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRound(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRound(this RGaConformalSpace conformalSpace, double radiusSquared, RGaConformalBlade center, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            1,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRound(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64KVector direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRound(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaConformalBlade center, RGaConformalBlade direction)
    {
        return new RGaConformalRound(
            conformalSpace,
            weight,
            radiusSquared,
            center,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector2D center, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineRound(
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector3D center, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineRound(
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double radiusSquared, Float64Vector center, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineRound(
            1,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector center, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRound(
            1,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double radiusSquared, RGaFloat64Vector center, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineRound(
            1,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector2D center, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector3D center, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, Float64Vector center, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            conformalSpace.EncodeEGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(conformalSpace.Processor)
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaFloat64Vector center, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromVectors(this RGaConformalSpace conformalSpace, double weight, double radiusSquared, RGaFloat64Vector center, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, params Float64Vector2D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, params Float64Vector3D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, params Float64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, IEnumerable<RGaFloat64Vector> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, double weight, params Float64Vector2D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, double weight, params Float64Vector3D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, double weight, params Float64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, double weight, params RGaFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalRound DefineRoundFromPoints(this RGaConformalSpace conformalSpace, double weight, IEnumerable<RGaFloat64Vector> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                conformalSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(conformalSpace.Processor);

        return kVector.EncodeEGaBlade(conformalSpace).DecodeOpnsRound();
    }

}