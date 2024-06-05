using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Blades;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Encoding;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Conformal.Elements;

public static class XGaConformalDirectionComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionScalar<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, Scalar<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeScalar(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionScalar<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, IntegerSign direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeScalar(direction.ToInt32())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector2D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarProcessor.One,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector3D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarProcessor.One,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, LinVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarProcessor.One,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, XGaVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaVector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector2D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector3D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionLine<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaVector(direction)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, LinBivector2D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, LinBivector3D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, XGaBivector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinBivector2D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinBivector3D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionPlane<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaBivector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBivector(direction)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionVolume<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, LinTrivector3D<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaTrivector(direction)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirection<T>(this XGaConformalSpace<T> conformalSpace, XGaKVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirection<T>(this XGaConformalSpace<T> conformalSpace, XGaConformalBlade<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            conformalSpace.ScalarOne,
            direction
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirection<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaKVector<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            conformalSpace.EncodeEGaBlade(direction)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirection<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, XGaConformalBlade<T> direction)
    {
        return new XGaConformalDirection<T>(
            conformalSpace,
            weight,
            direction
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            conformalSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            conformalSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            conformalSpace.ScalarOne,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            conformalSpace.ScalarOne,
            directionVectors.Op(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineDirection(
            conformalSpace.ScalarOne,
            directionVectors.Op(conformalSpace.EuclideanProcessor)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector2D<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector3D<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params LinVector<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Select(v => v.ToXGaVector(conformalSpace.EuclideanProcessor)).Op(conformalSpace.Processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, params XGaVector<T>[] directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op(conformalSpace.EuclideanProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaConformalDirection<T> DefineDirectionFromVectors<T>(this XGaConformalSpace<T> conformalSpace, Scalar<T> weight, IEnumerable<XGaVector<T>> directionVectors)
    {
        return conformalSpace.DefineDirection(
            weight,
            directionVectors.Op(conformalSpace.EuclideanProcessor)
        );
    }
}