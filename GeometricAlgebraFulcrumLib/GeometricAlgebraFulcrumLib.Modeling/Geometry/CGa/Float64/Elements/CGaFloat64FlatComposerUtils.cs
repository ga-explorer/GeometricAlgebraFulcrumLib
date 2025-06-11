using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position, XGaFloat64Vector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector point1, XGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Vector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Vector3D normal)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(normal.NormalToUnitDirection3D())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector point1, XGaFloat64Vector point2, XGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position, XGaFloat64Bivector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Bivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Trivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
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
    public static CGaFloat64Flat DefineFlat(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position, XGaFloat64KVector direction)
    {
        return new CGaFloat64Flat(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            cgaGeometricSpace.Encode.VGa.Blade(direction)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            1,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
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
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            cgaGeometricSpace.Encode.VGa.Vector(position),
            directionVectors
                .Select(v => v.ToXGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position, params XGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineFlat(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, XGaFloat64Vector position, IEnumerable<XGaFloat64Vector> directionVectors)
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
            .Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaFloat64Vector())
            ).DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector3D[] egaPoints)
    {
        return cgaGeometricSpace
            .Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaFloat64Vector())
            ).DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector[] egaPoints)
    {
        return cgaGeometricSpace
            .Encode.OpnsFlat.BladeFromPoints(
                (IReadOnlyList<XGaFloat64Vector>)egaPoints.SelectToArray(p => p.ToXGaFloat64Vector())
            )
            .DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params XGaFloat64Vector[] egaPoints)
    {
        return cgaGeometricSpace
            .Encode.OpnsFlat.BladeFromPoints((IReadOnlyList<XGaFloat64Vector>)egaPoints)
            .DecodeOpnsFlat.Element();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Flat DefineFlatFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IReadOnlyList<XGaFloat64Vector> egaPoints)
    {
        return cgaGeometricSpace
            .Encode.OpnsFlat.BladeFromPoints(egaPoints)
            .DecodeOpnsFlat.Element();
    }
}