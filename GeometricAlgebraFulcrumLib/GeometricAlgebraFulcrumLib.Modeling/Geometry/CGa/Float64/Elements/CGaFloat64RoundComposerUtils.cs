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

public static class CGaFloat64RoundComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double centerX, double centerY)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(centerY, centerY),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double centerX, double centerY, double centerZ)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(centerY, centerY, centerZ),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeScalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector center, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, CGaFloat64Blade center, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, RGaFloat64Vector center, RGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, CGaFloat64Blade center, CGaFloat64Blade direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            center,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector center, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector center, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, RGaFloat64Vector center, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector center, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            cgaGeometricSpace.EncodeVGaVector(center),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, RGaFloat64Vector center, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, RGaFloat64Vector center, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector2D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IEnumerable<RGaFloat64Vector> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector2D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params RGaFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IEnumerable<RGaFloat64Vector> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.EncodeIpnsRoundPoint(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound();
    }

}