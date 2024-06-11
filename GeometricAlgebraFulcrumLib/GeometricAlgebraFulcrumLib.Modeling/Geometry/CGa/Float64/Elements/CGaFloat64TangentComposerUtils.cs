﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64TangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPoint(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, LinFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentLineFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            cgaGeometricSpace
                .EncodeVGaVector(point2 - point1)
                .Op(cgaGeometricSpace.EncodeVGaVector(point3 - point2));

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D point1, LinFloat64Vector2D point2, LinFloat64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D point1, LinFloat64Vector3D point2, LinFloat64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector point1, LinFloat64Vector point2, LinFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction =
            cgaGeometricSpace
                .EncodeVGaVector(point2 - point1)
                .Op(cgaGeometricSpace.EncodeVGaVector(point3 - point2));

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            direction
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentPlaneFromPoints(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangent(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangent(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            1,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangent(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangent(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade position, CGaFloat64Blade direction)
    {
        return new CGaFloat64Tangent(
            cgaGeometricSpace,
            weight,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            1,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            1,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            1,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D position, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D position, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector position, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            cgaGeometricSpace.EncodeVGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op(cgaGeometricSpace.Processor)
                .EncodeVGaBlade(cgaGeometricSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Tangent DefineTangentFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }
}