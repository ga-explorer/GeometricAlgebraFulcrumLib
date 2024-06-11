using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Decoding;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64FlatComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Vector3D normal)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double distance, LinFloat64Vector3D normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double distance, LinFloat64Vector3D normal)
    {
        var position = normal.SetLength(distance);
        var direction = normal.NormalToUnitDirection3D();

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector2D[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlatFromPoints(
                egaPoints.SelectToArray(p => p.ToRGaFloat64Vector())
            ).DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector3D[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlatFromPoints(
                egaPoints.SelectToArray(p => p.ToRGaFloat64Vector())
            ).DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlatFromPoints(
                egaPoints.SelectToArray(p => p.ToRGaFloat64Vector())
            )
            .DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params RGaFloat64Vector[] egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlatFromPoints(egaPoints)
            .DecodeOpnsFlat();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IReadOnlyList<RGaFloat64Vector> egaPoints)
    {
        return cgaGeometricSpace
            .EncodeOpnsFlatFromPoints(egaPoints)
            .DecodeOpnsFlat();
    }
}