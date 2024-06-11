using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Float64.Elements;

public static class CGaFloat64DirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionScalar(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, double direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeScalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionScalar(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IntegerSign direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeScalar(direction.ToFloat64())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector2D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector3D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionLine(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Vector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Bivector2D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Bivector3D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionPlane(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64Bivector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionVolume(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, LinFloat64Trivector3D direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, RGaFloat64KVector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, CGaFloat64Blade direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            1,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, RGaFloat64KVector direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            cgaGeometricSpace.EncodeVGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirection(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, CGaFloat64Blade direction)
    {
        return new CGaFloat64Direction(
            cgaGeometricSpace,
            weight,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            1,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            1,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector2D[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector3D[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params LinFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, params RGaFloat64Vector[] directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CGaFloat64Direction DefineDirectionFromVectors(this CGaFloat64GeometricSpace cgaGeometricSpace, double weight, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return cgaGeometricSpace.DefineDirection(
            weight,
            directionVectors.Op(cgaGeometricSpace.Processor)
        );
    }
}