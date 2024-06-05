using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Conformal.Elements;

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
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, LinFloat64Vector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, LinFloat64Vector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, LinFloat64Vector direction)
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
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector3D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionLine(this RGaConformalSpace conformalSpace, double weight, LinFloat64Vector direction)
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
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, LinFloat64Bivector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            1,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, LinFloat64Bivector3D direction)
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
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, double weight, LinFloat64Bivector2D direction)
    {
        return new RGaConformalDirection(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionPlane(this RGaConformalSpace conformalSpace, double weight, LinFloat64Bivector3D direction)
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
    public static RGaConformalDirection DefineDirectionVolume(this RGaConformalSpace conformalSpace, double weight, LinFloat64Trivector3D direction)
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
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params LinFloat64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params LinFloat64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params LinFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineDirection(
            1,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params LinFloat64Vector2D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params LinFloat64Vector3D[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params LinFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToRGaFloat64Vector()).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, params RGaFloat64Vector[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaConformalDirection DefineDirectionFromVectors(this RGaConformalSpace conformalSpace, double weight, IEnumerable<RGaFloat64Vector> directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op(conformalSpace.Processor)
        );
    }
}