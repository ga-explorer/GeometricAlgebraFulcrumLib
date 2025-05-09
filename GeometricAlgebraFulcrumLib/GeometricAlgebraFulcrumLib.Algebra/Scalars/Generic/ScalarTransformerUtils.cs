using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarTransformerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> MapScalar<T>(this ScalarTransformer<T> transformer, IScalar<T> scalar)
    {
        var processor = scalar.ScalarProcessor;

        return processor.ScalarFromValue(
            transformer.MapScalarValue(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> MapAngleRadians<T>(this ScalarTransformer<T> transformer, LinPolarAngle<T> angle)
    {
        var processor = angle.ScalarProcessor;

        return processor.CreatePolarAngleFromRadians(
            transformer.MapScalarValue(angle.RadiansValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> MapAngleRadians<T>(this ScalarTransformer<T> transformer, LinDirectedAngle<T> angle)
    {
        var processor = angle.ScalarProcessor;

        return processor.CreateDirectedAngleFromRadians(
            transformer.MapScalarValue(angle.RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<T> MapComponents<T>(this ScalarTransformer<T> transformer, IPair<T> scalarValuePair)
    {
        return new Pair<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<T> MapComponents<T>(this ScalarTransformer<T> transformer, ITriplet<T> scalarValuePair)
    {
        return new Triplet<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<T> MapComponents<T>(this ScalarTransformer<T> transformer, IQuad<T> scalarValuePair)
    {
        return new Quad<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<T> MapComponents<T>(this ScalarTransformer<T> transformer, IQuint<T> scalarValuePair)
    {
        return new Quint<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4),
            transformer.MapScalarValue(scalarValuePair.Item5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<T> MapComponents<T>(this ScalarTransformer<T> transformer, IHexad<T> scalarValuePair)
    {
        return new Hexad<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4),
            transformer.MapScalarValue(scalarValuePair.Item5),
            transformer.MapScalarValue(scalarValuePair.Item6)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> MapScalars<T>(this ScalarTransformer<T> transformer, ILinVector2D<T> vector)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector2D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> MapScalars<T>(this ScalarTransformer<T> transformer, ILinVector3D<T> vector)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector3D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue),
            transformer.MapScalarValue(vector.Z.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> MapScalars<T>(this ScalarTransformer<T> transformer, ILinVector4D<T> vector)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector4D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue),
            transformer.MapScalarValue(vector.Z.ScalarValue),
            transformer.MapScalarValue(vector.W.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] MapScalars<T>(this ScalarTransformer<T> transformer, T[] scalarArray)
    {
        return scalarArray.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] MapScalars<T>(this ScalarTransformer<T> transformer, T[,] scalarArray)
    {
        return scalarArray.MapScalars(transformer.MapScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaScalar<T> mv)
    {
        var processor = mv.Processor;

        return processor.Scalar(
            transformer.MapScalarValue(mv.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaBivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaHigherKVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaKVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaGradedMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaUniformMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, RGaMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaScalar<T> mv)
    {
        var processor = mv.Processor;

        return processor.Scalar(
            transformer.MapScalarValue(mv.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaBivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaHigherKVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaKVector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaGradedMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaUniformMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this ScalarTransformer<T> transformer, XGaMultivector<T> mv)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> MapScalar<T>(this IScalar<T> scalar, ScalarTransformer<T> transformer)
    {
        var processor = scalar.ScalarProcessor;

        return processor.ScalarFromValue(
            transformer.MapScalarValue(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> MapAngleRadians<T>(this LinPolarAngle<T> angle, ScalarTransformer<T> transformer)
    {
        var processor = angle.ScalarProcessor;

        return processor.CreatePolarAngleFromRadians(
            transformer.MapScalarValue(angle.RadiansValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinDirectedAngle<T> MapAngleRadians<T>(this LinDirectedAngle<T> angle, ScalarTransformer<T> transformer)
    {
        var processor = angle.ScalarProcessor;

        return processor.CreateDirectedAngleFromRadians(
            transformer.MapScalarValue(angle.RadiansValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<T> MapComponents<T>(this IPair<T> scalarValuePair, ScalarTransformer<T> transformer)
    {
        return new Pair<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Triplet<T> MapComponents<T>(this ITriplet<T> scalarValuePair, ScalarTransformer<T> transformer)
    {
        return new Triplet<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quad<T> MapComponents<T>(this IQuad<T> scalarValuePair, ScalarTransformer<T> transformer)
    {
        return new Quad<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quint<T> MapComponents<T>(this IQuint<T> scalarValuePair, ScalarTransformer<T> transformer)
    {
        return new Quint<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4),
            transformer.MapScalarValue(scalarValuePair.Item5)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Hexad<T> MapComponents<T>(this IHexad<T> scalarValuePair, ScalarTransformer<T> transformer)
    {
        return new Hexad<T>(
            transformer.MapScalarValue(scalarValuePair.Item1),
            transformer.MapScalarValue(scalarValuePair.Item2),
            transformer.MapScalarValue(scalarValuePair.Item3),
            transformer.MapScalarValue(scalarValuePair.Item4),
            transformer.MapScalarValue(scalarValuePair.Item5),
            transformer.MapScalarValue(scalarValuePair.Item6)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> MapScalars<T>(this ILinVector2D<T> vector, ScalarTransformer<T> transformer)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector2D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3D<T> MapScalars<T>(this ILinVector3D<T> vector, ScalarTransformer<T> transformer)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector3D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue),
            transformer.MapScalarValue(vector.Z.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4D<T> MapScalars<T>(this ILinVector4D<T> vector, ScalarTransformer<T> transformer)
    {
        var processor = vector.ScalarProcessor;

        return processor.Vector4D(
            transformer.MapScalarValue(vector.X.ScalarValue),
            transformer.MapScalarValue(vector.Y.ScalarValue),
            transformer.MapScalarValue(vector.Z.ScalarValue),
            transformer.MapScalarValue(vector.W.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] MapScalars<T>(this T[] scalarArray, ScalarTransformer<T> transformer)
    {
        return scalarArray.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] MapScalars<T>(this T[,] scalarArray, ScalarTransformer<T> transformer)
    {
        return scalarArray.MapScalars(transformer.MapScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> MapScalars<T>(this RGaScalar<T> mv, ScalarTransformer<T> transformer)
    {
        var processor = mv.Processor;

        return processor.Scalar(
            transformer.MapScalarValue(mv.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> MapScalars<T>(this RGaVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaBivector<T> MapScalars<T>(this RGaBivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaHigherKVector<T> MapScalars<T>(this RGaHigherKVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> MapScalars<T>(this RGaKVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> MapScalars<T>(this RGaGradedMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> MapScalars<T>(this RGaUniformMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> MapScalars<T>(this RGaMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> MapScalars<T>(this XGaScalar<T> mv, ScalarTransformer<T> transformer)
    {
        var processor = mv.Processor;

        return processor.Scalar(
            transformer.MapScalarValue(mv.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> MapScalars<T>(this XGaVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> MapScalars<T>(this XGaBivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> MapScalars<T>(this XGaHigherKVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> MapScalars<T>(this XGaKVector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this XGaGradedMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> MapScalars<T>(this XGaUniformMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> MapScalars<T>(this XGaMultivector<T> mv, ScalarTransformer<T> transformer)
    {
        return mv.MapScalars(transformer.MapScalarValue);
    }
        
}