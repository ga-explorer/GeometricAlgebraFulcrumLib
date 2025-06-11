using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarTransformerUtils
{
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> MapScalar<T>(this IScalar<T> scalar, ScalarTransformer<T> transformer)
    {
        var processor = scalar.ScalarProcessor;

        return processor.ScalarFromValue(
            transformer.MapScalarValue(scalar.ScalarValue)
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

    
        
}