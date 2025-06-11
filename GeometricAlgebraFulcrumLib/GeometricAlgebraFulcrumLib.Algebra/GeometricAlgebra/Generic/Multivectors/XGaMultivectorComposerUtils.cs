using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public static class XGaMultivectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IndexSet, T> CreateVectorDictionary<T>(this IReadOnlyDictionary<int, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.ToUnitIndexSet(), value);

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<IndexPair, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.ToPairIndexSet(), value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IndexSet, T> CreateBivectorDictionary<T>(this IReadOnlyDictionary<Int32Pair, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.ToPairIndexSet(), value);

        return basisScalarDictionary;
    }
    
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> Vector<T>(this IEnumerable<T> scalarList, XGaProcessor<T> processor)
    {
        var scalarDictionary = processor.CreateValidVectorDictionary(scalarList);

        return processor.Vector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinFloat64Vector2D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, processor.ScalarProcessor.ScalarFromNumber(vector.X))
            .SetVectorTerm(1, processor.ScalarProcessor.ScalarFromNumber(vector.Y))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinFloat64Vector3D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, processor.ScalarProcessor.ScalarFromNumber(vector.X))
            .SetVectorTerm(1, processor.ScalarProcessor.ScalarFromNumber(vector.Y))
            .SetVectorTerm(2, processor.ScalarProcessor.ScalarFromNumber(vector.Z))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinFloat64Vector4D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, processor.ScalarProcessor.ScalarFromNumber(vector.X))
            .SetVectorTerm(1, processor.ScalarProcessor.ScalarFromNumber(vector.Y))
            .SetVectorTerm(2, processor.ScalarProcessor.ScalarFromNumber(vector.Z))
            .SetVectorTerm(3, processor.ScalarProcessor.ScalarFromNumber(vector.W))
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinVector2D<T> vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X.ScalarValue)
            .SetVectorTerm(1, vector.Y.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinVector3D<T> vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X.ScalarValue)
            .SetVectorTerm(1, vector.Y.ScalarValue)
            .SetVectorTerm(2, vector.Z.ScalarValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this ILinVector4D<T> vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateVectorComposer()
            .SetVectorTerm(0, vector.X.ScalarValue)
            .SetVectorTerm(1, vector.Y.ScalarValue)
            .SetVectorTerm(2, vector.Z.ScalarValue)
            .SetVectorTerm(3, vector.W.ScalarValue)
            .GetVector();
    }


    public static XGaVector<T> DiagonalToXGaVector<T>(this T[,] matrix, XGaProcessor<T> processor)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            if (scalar is not null)
                composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static XGaVector<T> RowToXGaVector<T>(this T[,] matrix, int row, XGaProcessor<T> processor)
    {
        var columnCount = matrix.GetLength(1);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < columnCount; i++)
        {
            var scalar = matrix[i, row];

            if (scalar is not null)
                composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static XGaVector<T> ColumnToXGaVector<T>(this T[,] matrix, int column, XGaProcessor<T> processor)
    {
        var rowCount = matrix.GetLength(0);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < rowCount; i++)
        {
            var scalar = matrix[column, i];

            if (scalar is not null)
                composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> RowsToXGaVectors<T>(this T[,] matrix, XGaProcessor<T> processor)
    {
        return matrix.GetLength(0).GetRange().Select(
            r => matrix.RowToXGaVector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> ColumnsToXGaVectors<T>(this T[,] matrix, XGaProcessor<T> processor)
    {
        return matrix.GetLength(1).GetRange().Select(
            c => matrix.ColumnToXGaVector(c, processor)
        );
    }


    public static XGaVector<T> DiagonalToXGaVector<T>(this Matrix matrix, XGaProcessor<T> processor)
    {
        var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, processor.ScalarProcessor.ScalarFromNumber(scalar));
        }

        return composer.GetVector();
    }

    public static XGaVector<T> RowToXGaVector<T>(this Matrix matrix, int row, XGaProcessor<T> processor)
    {
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < matrix.ColumnCount; i++)
            composer.SetVectorTerm(i, processor.ScalarProcessor.ScalarFromNumber(matrix[row, i]));

        return composer.GetVector();
    }

    public static XGaVector<T> ColumnToXGaVector<T>(this Matrix matrix, int column, XGaProcessor<T> processor)
    {
        var composer = processor.CreateVectorComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetVectorTerm(i, processor.ScalarProcessor.ScalarFromNumber(matrix[i, column]));

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> RowsToXGaVectors<T>(this Matrix matrix, XGaProcessor<T> processor)
    {
        return matrix.RowCount.GetRange().Select(
            r => matrix.RowToXGaVector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<XGaVector<T>> ColumnsToXGaVectors<T>(this Matrix matrix, XGaProcessor<T> processor)
    {
        return matrix.ColumnCount.GetRange().Select(
            c => matrix.ColumnToXGaVector(c, processor)
        );
    }

}