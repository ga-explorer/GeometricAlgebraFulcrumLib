﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

public static class XGaVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, T> CreateVectorDictionary<T>(this IReadOnlyDictionary<int, T> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key.IndexToIndexSet(), value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<IIndexSet, T> CreateValidVectorDictionary<T>(XGaProcessor<T> processor, IEnumerable<T> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<T>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!processor.ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            if (!processor.ScalarProcessor.IsZero(scalar))
                basisScalarDictionary.Add(index.IndexToIndexSet(), scalar);

            index++;
        }

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateZeroVector<T>(this XGaProcessor<T> processor)
    {
        return new XGaVector<T>(processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<IIndexSet, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, T>)
            return processor.CreateZeroVector();

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, T>)
            return processor.CreateTermVector(basisScalarDictionary.First());

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IReadOnlyDictionary<int, T> basisScalarDictionary)
    {
        return new XGaVector<T>(processor, basisScalarDictionary.CreateVectorDictionary());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, params string[] scalarArray)
    {
        var scalarDictionary = CreateValidVectorDictionary(
            processor,
            scalarArray.Select(
                processor.ScalarProcessor.GetScalarFromText
            )
        );

        return processor.CreateVector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, params T[] scalarArray)
    {
        var scalarDictionary = CreateValidVectorDictionary(processor, scalarArray);

        return processor.CreateVector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IEnumerable<T> scalarList)
    {
        var scalarDictionary = CreateValidVectorDictionary(processor, scalarList);

        return processor.CreateVector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateXGaVector<T>(this IEnumerable<T> scalarList, XGaProcessor<T> processor)
    {
        var scalarDictionary = CreateValidVectorDictionary(processor, scalarList);

        return processor.CreateVector(
            scalarDictionary
        );
    }
        
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, int termsCount, Func<int, T> indexToScalarFunc)
    {
        var composer = processor.CreateComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetVectorTerm(index, scalar);
        }

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(
                index.IndexToIndexSet(),
                processor.ScalarProcessor.ScalarOne
            );

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, int index, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(
                index.IndexToIndexSet(),
                scalar
            );

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, int index, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(
                index.IndexToIndexSet(),
                scalar.ScalarValue
            );

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, KeyValuePair<int, T> indexScalarPair)
    {
        return processor.CreateTermVector(indexScalarPair.Key, indexScalarPair.Value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, ulong basisVectorId)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(basisVectorId.BitPatternToIndexSet(), processor.ScalarProcessor.ScalarOne);

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, ulong basisVectorId, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(basisVectorId.BitPatternToIndexSet(), scalar);

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, ulong basisVectorId, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(
                basisVectorId.BitPatternToIndexSet(),
                scalar.ScalarValue
            );

        return new XGaVector<T>(processor, basisScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, KeyValuePair<ulong, T> idScalarPair)
    {
        return processor.CreateTermVector(idScalarPair.Key.BitPatternToIndexSet(), idScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, IIndexSet basisVectorId)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(basisVectorId, processor.ScalarProcessor.ScalarOne);

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, IIndexSet basisVectorId, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(basisVectorId, scalar);

        return new XGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, IIndexSet basisVectorId, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new XGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<IIndexSet, T>(
                basisVectorId,
                scalar.ScalarValue
            );

        return new XGaVector<T>(processor, basisScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateTermVector<T>(this XGaProcessor<T> processor, KeyValuePair<IIndexSet, T> idScalarPair)
    {
        return processor.CreateTermVector(idScalarPair.Key, idScalarPair.Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateSymmetricVector<T>(this XGaProcessor<T> processor, int count)
    {
        return processor.CreateSymmetricVector(

            count,
            processor.ScalarProcessor.ScalarOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateSymmetricVector<T>(this XGaProcessor<T> processor, int count, T scalarValue)
    {
        return count switch
        {
            < 0 => throw new InvalidOperationException(),

            0 => new XGaVector<T>(
                processor,
                new EmptyDictionary<IIndexSet, T>()
            ),

            1 => new XGaVector<T>(
                processor,
                new SingleItemDictionary<IIndexSet, T>(0.IndexToUInt64IndexSet(), scalarValue)
            ),

            _ => new XGaVector<T>(
                processor,
                new XGaRepeatedScalarVectorDictionary<T>(count, scalarValue)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateSymmetricUnitVector<T>(this XGaProcessor<T> processor, int count)
    {
        return processor.CreateSymmetricVector(

            count,
            processor.ScalarProcessor.Inverse(processor.ScalarProcessor.Sqrt(count))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateUnitXGaVector<T>(this XGaProcessor<T> processor, T angle, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var scalar1 = processor.ScalarProcessor.Cos(angle);
        var scalar2 = processor.ScalarProcessor.Sin(angle);

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateXGaPhasor<T>(this XGaProcessor<T> processor, T magnitude, T angle, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var scalar1 = processor.ScalarProcessor.Times(magnitude, processor.ScalarProcessor.Cos(angle));
        var scalar2 = processor.ScalarProcessor.Times(magnitude, processor.ScalarProcessor.Sin(angle));

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, LinVector<T> vector)
    {
        return processor.CreateVector(
            vector.GetIndexScalarDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IFloat64Vector2D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IFloat64Vector3D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .SetTerm(4, processor.ScalarProcessor.GetScalarFromNumber(vector.Z))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> CreateVector<T>(this XGaProcessor<T> processor, IFloat64Vector4D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .SetTerm(4, processor.ScalarProcessor.GetScalarFromNumber(vector.Z))
            .SetTerm(8, processor.ScalarProcessor.GetScalarFromNumber(vector.W))
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this LinVector<T> vector, XGaProcessor<T> processor)
    {
        return processor.CreateVector(
            vector.GetIndexScalarDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this IFloat64Vector2D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this IFloat64Vector3D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .SetTerm(4, processor.ScalarProcessor.GetScalarFromNumber(vector.Z))
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> ToXGaVector<T>(this IFloat64Vector4D vector, XGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, processor.ScalarProcessor.GetScalarFromNumber(vector.X))
            .SetTerm(2, processor.ScalarProcessor.GetScalarFromNumber(vector.Y))
            .SetTerm(4, processor.ScalarProcessor.GetScalarFromNumber(vector.Z))
            .SetTerm(8, processor.ScalarProcessor.GetScalarFromNumber(vector.W))
            .GetVector();
    }


    public static XGaVector<T> DiagonalToXGaVector<T>(this T[,] matrix, XGaProcessor<T> processor)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = processor.CreateComposer();

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
        var composer = processor.CreateComposer();

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
        var composer = processor.CreateComposer();

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
        var composer = processor.CreateComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, processor.ScalarProcessor.GetScalarFromNumber(scalar));
        }

        return composer.GetVector();
    }

    public static XGaVector<T> RowToXGaVector<T>(this Matrix matrix, int row, XGaProcessor<T> processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.ColumnCount; i++)
            composer.SetVectorTerm(i, processor.ScalarProcessor.GetScalarFromNumber(matrix[row, i]));

        return composer.GetVector();
    }

    public static XGaVector<T> ColumnToXGaVector<T>(this Matrix matrix, int column, XGaProcessor<T> processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetVectorTerm(i, processor.ScalarProcessor.GetScalarFromNumber(matrix[i, column]));

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