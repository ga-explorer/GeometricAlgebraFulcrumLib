using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

public static class LinVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<int, T> CreateLinVectorDictionary<T>(this IReadOnlyDictionary<int, T> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<int, T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key, value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<int, T> CreateValidLinVectorDictionary<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        var basisScalarDictionary = new Dictionary<int, T>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            basisScalarDictionary.Add(index, scalar);

            index++;
        }

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateZeroLinVector<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<int, T>)
            return scalarProcessor.CreateZeroLinVector();

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<int, T>)
            return scalarProcessor.CreateLinVector(basisScalarDictionary.First());

        return new LinVector<T>(scalarProcessor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IReadOnlyDictionary<int, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<int, T>)
            return scalarProcessor.CreateZeroLinVector();

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<int, T>)
            return scalarProcessor.CreateLinVector(basisScalarDictionary.First());

        return new LinVector<T>(scalarProcessor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarArray)
    {
        var scalarDictionary = scalarProcessor.CreateValidLinVectorDictionary(scalarArray);

        return new LinVector<T>(scalarProcessor, scalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        var scalarDictionary = scalarProcessor.CreateValidLinVectorDictionary(scalarList);

        return new LinVector<T>(scalarProcessor, scalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<LinVectorTerm<T>> indexScalarList)
    {
        return scalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(indexScalarList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<int, T>(index, scalarProcessor.OneValue);

        return new LinVector<T>(scalarProcessor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int index, T scalar)
    {
        if (scalarProcessor.IsZero(scalar))
            return new LinVector<T>(scalarProcessor);

        var basisScalarDictionary =
            new SingleItemDictionary<int, T>(index, scalar);

        return new LinVector<T>(scalarProcessor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, KeyValuePair<int, T> indexScalarPair)
    {
        return scalarProcessor.CreateLinVector(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this int index, Scalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;
        if (scalar.IsZero())
            return new LinVector<T>(scalarProcessor);

        var basisScalarDictionary =
            new SingleItemDictionary<int, T>(index, scalar.ScalarValue);

        return new LinVector<T>(scalarProcessor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, double> indexToScalarFunc)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetTerm(index, scalarProcessor.ScalarFromNumber(scalar));
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, string> indexToScalarFunc)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetTerm(index, scalarProcessor.ScalarFromText(scalar));
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, T> indexToScalarFunc)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetTerm(index, scalar);
        }

        return composer.GetVector();
    }



    public static LinVector<T> DiagonalToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            if (scalar is not null)
                composer.SetTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static LinVector<T> RowToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor, int row)
    {
        var columnCount = matrix.GetLength(1);
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var j = 0; j < columnCount; j++)
        {
            var scalar = matrix[row, j];

            if (scalar is not null)
                composer.SetTerm(j, scalar);
        }

        return composer.GetVector();
    }

    public static LinVector<T> ColumnToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor, int column)
    {
        var rowCount = matrix.GetLength(0);
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var i = 0; i < rowCount; i++)
        {
            var scalar = matrix[i, column];

            if (scalar is not null)
                composer.SetTerm(i, scalar);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector<T>> RowsToLinVectors<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
    {
        return matrix.GetLength(0).GetRange().Select(
            r => matrix.RowToLinVector(scalarProcessor, r)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector<T>> ColumnsToLinVectors<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
    {
        return matrix.GetLength(1).GetRange().Select(
            c => matrix.ColumnToLinVector(scalarProcessor, c)
        );
    }


    public static LinVector<T> DiagonalToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
    {
        var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetTerm(i, scalarProcessor.ScalarFromNumber(scalar));
        }

        return composer.GetVector();
    }

    public static LinVector<T> RowToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor, int row)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var j = 0; j < matrix.ColumnCount; j++)
            composer.SetTerm(
                j,
                scalarProcessor.ScalarFromNumber(matrix[row, j])
            );

        return composer.GetVector();
    }

    public static LinVector<T> ColumnToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor, int column)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetTerm(
                i,
                scalarProcessor.ScalarFromNumber(matrix[i, column])
            );

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector<T>> RowsToLinVectors<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
    {
        return matrix.RowCount.GetRange().Select(
            r => matrix.RowToLinVector(scalarProcessor, r)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinVector<T>> ColumnsToLinVectors<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
    {
        return matrix.ColumnCount.GetRange().Select(
            c => matrix.ColumnToLinVector(scalarProcessor, c)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorComposer<T> CreateLinVectorComposer<T>(this IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorComposer<T>(scalarProcessor);
    }



}