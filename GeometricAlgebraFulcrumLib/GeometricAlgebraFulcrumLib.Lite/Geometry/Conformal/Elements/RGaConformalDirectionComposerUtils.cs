using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Conformal.Elements;

public static class RGaConformalDirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionScalar(this RGaConformalSpace conformalSpace, double weight, double direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeScalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionScalar(this RGaConformalSpace conformalSpace, double weight, IntegerSign direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeScalar(direction.ToFloat64())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, Float64Vector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, Float64Vector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, Float64Vector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, RGaFloat64Vector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, Float64Vector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Vector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, Float64Bivector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, Float64Bivector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, RGaFloat64Bivector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, double weight, Float64Bivector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, double weight, Float64Bivector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, double weight, RGaFloat64Bivector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionVolume(this RGaConformalSpace conformalSpace, double weight, Float64Trivector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirection(this RGaConformalSpace conformalSpace, RGaFloat64KVector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirection(this RGaConformalSpace conformalSpace, RGaConformalBlade direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirection(this RGaConformalSpace conformalSpace, double weight, RGaFloat64KVector direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirection(this RGaConformalSpace conformalSpace, double weight, RGaConformalBlade direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Op()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params Float64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params Float64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params Float64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op()
        );
    }
}