using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public static class RGaConformalTangentComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector2D position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector3D position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, Float64Vector position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, RGaFloat64Vector position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, double weight, Float64Vector position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPoint(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.OneScalarBlade
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, Float64Vector2D position, Float64Vector2D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, Float64Vector3D position, Float64Vector3D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, Float64Vector position, Float64Vector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D position, Float64Vector2D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D position, Float64Vector3D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector position, Float64Vector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLine(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position, RGaFloat64Vector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, Float64Vector point1, Float64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D point1, Float64Vector2D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D point1, Float64Vector3D point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector point1, Float64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentLineFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2)
    {
        var position = (point1 + point2) / 2;
        var direction = point2 - point1;

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, Float64Vector2D position, Float64Bivector2D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, Float64Vector3D position, Float64Bivector3D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D position, Float64Bivector2D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D position, Float64Bivector3D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlane(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position, RGaFloat64Bivector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector2D point1, Float64Vector2D point2, Float64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, Float64Vector point1, Float64Vector point2, Float64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = 
            conformalSpace
                .EncodeEGaVector(point2 - point1)
                .Op(conformalSpace.EncodeEGaVector(point3 - point2));

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            direction
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D point1, Float64Vector2D point2, Float64Vector2D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D point1, Float64Vector3D point2, Float64Vector3D point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, double weight, Float64Vector point1, Float64Vector point2, Float64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = 
            conformalSpace
                .EncodeEGaVector(point2 - point1)
                .Op(conformalSpace.EncodeEGaVector(point3 - point2));

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            direction
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentPlaneFromPoints(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector point1, RGaFloat64Vector point2, RGaFloat64Vector point3)
    {
        var position = (point1 + point2 + point3) / 3;
        var direction = (point2 - point1).Op(point3 - point2);

        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentVolume(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D position, Float64Trivector3D direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangent(this RGaConformalSpace conformalSpace, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangent(this RGaConformalSpace conformalSpace, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            1,
            position,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangent(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position, RGaFloat64KVector direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(position),
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangent(this RGaConformalSpace conformalSpace, double weight, RGaConformalBlade position, RGaConformalBlade direction)
    {
        return new RGaConformalTangent(
            conformalSpace,
            weight,
            position,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, Float64Vector2D position, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            1,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, Float64Vector3D position, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            1,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, Float64Vector position, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            1,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            1,
            position,
            directionVectors.Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineTangent(
            1,
            position,
            directionVectors.Op()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D position, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D position, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, double weight, Float64Vector position, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            conformalSpace.EncodeEGaVector(position),
            directionVectors
                .Select(v => v.ToRGaFloat64Vector())
                .Op()
                .EncodeEGaBlade(conformalSpace)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalTangent DefineTangentFromVectors(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector position, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineTangent(
            weight,
            position,
            directionVectors.Op()
        );
    }
}