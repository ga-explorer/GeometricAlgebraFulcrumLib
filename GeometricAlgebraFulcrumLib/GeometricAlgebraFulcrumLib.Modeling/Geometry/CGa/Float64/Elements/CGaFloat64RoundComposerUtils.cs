using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
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
            cgaGeometricSpace.Encode.VGa.Vector(centerY, centerY),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double centerX, double centerY, double centerZ)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(centerY, centerY, centerZ),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D center, double direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            0,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.Scalar(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, XGaFloat64Vector center, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector center, LinFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundPointPair(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64Vector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector2D center, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Vector3D normalDirection)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(normalDirection.NormalToUnitDirection3D())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, XGaFloat64Vector center, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundCircle(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundSphere(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, LinFloat64Vector3D center, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, XGaFloat64Vector center, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            1,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
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
    public static CGaFloat64Round DefineRound(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, XGaFloat64Vector center, XGaFloat64KVector direction)
    {
        return new CGaFloat64Round(
            cgaGeometricSpace,
            weight,
            radiusSquared,
            cgaGeometricSpace.Encode.VGa.Vector(center),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, XGaFloat64Vector center, params XGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            1,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double radiusSquared, XGaFloat64Vector center, IEnumerable<XGaFloat64Vector> directionVectors)
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
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
            cgaGeometricSpace.Encode.VGa.Vector(center),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, XGaFloat64Vector center, params XGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineRound(
            weight,
            radiusSquared,
            center,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double radiusSquared, XGaFloat64Vector center, IEnumerable<XGaFloat64Vector> directionVectors)
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
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, params XGaFloat64Vector[] egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, IEnumerable<XGaFloat64Vector> egaPoints)
    {
        var kVector =
            egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector2D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector3D[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params XGaFloat64Vector[] egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Round DefineRoundFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IEnumerable<XGaFloat64Vector> egaPoints)
    {
        var kVector =
            weight * egaPoints.Select(p =>
                cgaGeometricSpace.Encode.IpnsRound.Point(p).InternalVector
            ).Op(cgaGeometricSpace.Processor);

        return kVector.EncodeVGaBlade(cgaGeometricSpace).DecodeOpnsRound.Element();
    }

}