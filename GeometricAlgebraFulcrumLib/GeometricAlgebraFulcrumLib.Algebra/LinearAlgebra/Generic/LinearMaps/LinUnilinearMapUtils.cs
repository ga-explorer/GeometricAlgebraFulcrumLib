using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;

public static class LinUnilinearMapUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] GetMapArray<T>(this LinUnilinearMap<T> map, int size)
    {
        return map.ToArray(size, size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<int, LinVector<T>>> GetColumns<T>(this LinUnilinearMap<T> map)
    {
        return map.GetMappedBasisVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetColumn<T>(this LinUnilinearMap<T> map, int colIndex)
    {
        return map.MapBasisVector(colIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetScaledColumn<T>(this LinUnilinearMap<T> map, int colIndex, T scalingFactor)
    {
        return map.MapBasisVector(colIndex).Times(scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetMappedColumn<T>(this LinUnilinearMap<T> map, int colIndex, Func<T, T> scalarMapping)
    {
        return map.MapBasisVector(colIndex).MapScalars(scalarMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> GetMappedColumn<T>(this LinUnilinearMap<T> map, int colIndex, Func<int, T, T> indexScalarMapping)
    {
        return map.MapBasisVector(colIndex).MapScalars(indexScalarMapping);
    }


    public static LinVector<T> CombineColumns<T>(this LinUnilinearMap<T> map, IReadOnlyList<T> scalarList, Func<T, LinVector<T>, LinVector<T>> scalingFunc, Func<LinVector<T>, LinVector<T>, LinVector<T>> reducingFunc)
    {
        var scalarProcessor = map.ScalarProcessor;
        var vector = scalarProcessor.CreateZeroLinVector();

        var count = scalarList.Count;
        for (var columnIndex = 0; columnIndex < count; columnIndex++)
        {
            if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
                continue;

            var scalingFactor = scalarList[columnIndex];
            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    public static LinVector<T> CombineColumns<T>(this LinUnilinearMap<T> map, LinVector<T> scalingVector, Func<T, LinVector<T>, LinVector<T>> scalingFunc, Func<LinVector<T>, LinVector<T>, LinVector<T>> reducingFunc)
    {
        var scalarProcessor = map.ScalarProcessor;
        var vector = scalarProcessor.CreateZeroLinVector();

        foreach (var (columnIndex, scalingFactor) in scalingVector)
        {
            if (!map.TryGetColumnVector(columnIndex, out var columnVector) || columnVector is null)
                continue;

            var scaledVector = scalingFunc(scalingFactor, columnVector);

            vector = reducingFunc(vector, scaledVector);
        }

        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CombineColumns<T>(this LinUnilinearMap<T> matrix1, LinUnilinearMap<T> matrix2, Func<T, LinVector<T>, LinVector<T>> scalingFunc, Func<LinVector<T>, LinVector<T>, LinVector<T>> reducingFunc)
    {
        var vectorsDictionary = new Dictionary<int, LinVector<T>>();

        foreach (var (index, vector) in matrix2.GetColumns())
            vectorsDictionary.Add(
                index,
                matrix1.CombineColumns(vector, scalingFunc, reducingFunc)
            );

        return vectorsDictionary.CreateLinUnilinearMap(matrix1.ScalarProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapVector<T>(this T[,] matrix, LinVector<T> vector)
    {
        var scalarProcessor = vector.ScalarProcessor;
        var composer = scalarProcessor.CreateLinVectorComposer();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        foreach (var (colIndex, scalar) in vector)
        {
            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalarProcessor.Times(
                        scalar,
                        matrix[rowIndex, colIndex]
                    )
                );
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapVector<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, IEnumerable<T> vector)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        var colIndex = 0;
        foreach (var scalar in vector)
        {
            if (colIndex >= colCount)
                return composer.GetVector();

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalarProcessor.Times(
                        scalar,
                        matrix[rowIndex, colIndex]
                    )
                );

            colIndex++;
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapVector<T>(this IScalarProcessor<T> scalarProcessor, T[,] matrix, IReadOnlyDictionary<int, T> vector)
    {
        var composer = scalarProcessor.CreateLinVectorComposer();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        foreach (var (colIndex, scalar) in vector)
        {
            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalarProcessor.Times(
                        scalar,
                        matrix[rowIndex, colIndex]
                    )
                );
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapVectorUsing<T>(this IReadOnlyList<T> vector, LinUnilinearMap<T> map)
    {
        return map.MapVector(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> MapVectorUsing<T>(this IReadOnlyDictionary<int, T> vector, LinUnilinearMap<T> map)
    {
        return map.MapVector(vector);
    }

    public static LinVector<T> MapVector<T>(this LinUnilinearMap<T> map, IReadOnlyList<T> vector)
    {
        var composer = map.ScalarProcessor.CreateLinVectorComposer();

        if (map.Count <= vector.Count)
        {
            foreach (var (index, mv) in map.IndexVectorPairs)
            {
                if (index >= vector.Count)
                    continue;

                composer.AddVector(mv, vector[index]);
            }
        }
        else
        {
            for (var index = 0; index < vector.Count; index++)
            {
                if (!map.TryGetVector(index, out var mv))
                    continue;

                composer.AddVector(mv, vector[index]);
            }
        }

        return composer.GetVector();
    }

    public static LinVector<T> MapVector<T>(this LinUnilinearMap<T> map, IReadOnlyDictionary<int, T> vector)
    {
        var composer = map.ScalarProcessor.CreateLinVectorComposer();

        if (map.Count <= vector.Count)
        {
            foreach (var (index, mv) in map.IndexVectorPairs)
            {
                if (!vector.TryGetValue(index, out var scalar))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }
        else
        {
            foreach (var (index, scalar) in vector)
            {
                if (!map.TryGetVector(index, out var mv))
                    continue;

                composer.AddVector(mv, scalar);
            }
        }

        return composer.GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateVectorToVectorRotationMatrix<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> sourceVector, IReadOnlyDictionary<int, T> targetVector, int basisVectorIndex, int vSpaceDimensions)
    {
        var matrix2 = scalarProcessor.CreateVectorToBasisRotationMap(sourceVector, basisVectorIndex, vSpaceDimensions);
        var matrix1 = scalarProcessor.CreateBasisToVectorRotationMap(basisVectorIndex, targetVector, vSpaceDimensions);

        return matrix1.Map(matrix2);
    }

    public static LinUnilinearMap<T> CreateBasisToVectorRotationMap<T>(this IScalarProcessor<T> scalarProcessor, int basisVectorIndex, IReadOnlyDictionary<int, T> unitVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = scalarProcessor.GetVectorTermScalar(unitVector, basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (scalarProcessor.IsOne(v1))
            return scalarProcessor.CreateIdentityLinUnilinearMap(vSpaceDimensions);

        // Special case: unitVector == -e_{basisVectorIndex}
        if (scalarProcessor.IsMinusOne(v1))
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            scalarProcessor.CreateLinUnilinearMapComposer();

        foreach (var (index, scalar) in unitVector)
        {
            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] =
                scalarProcessor.Negative(scalar).ScalarValue;

            Debug.Assert(v1 != null, nameof(v1) + " != null");

            map[index, index] =
                (scalarProcessor.One - scalarProcessor.Square(scalar) / v1).ScalarValue;

            foreach (var (index2, scalar2) in unitVector)
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] =
                    (scalarProcessor.NegativeTimes(scalar, scalar2) / v1).ScalarValue;
            }
        }

        return map.GetMap();
    }

    public static LinUnilinearMap<T> CreateVectorToBasisRotationMap<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> unitVector, int basisVectorIndex, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = scalarProcessor.GetVectorTermScalar(unitVector, basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (scalarProcessor.IsOne(v1))
            return scalarProcessor.CreateIdentityLinUnilinearMap(vSpaceDimensions);

        // Special case: unitVector == -e_{basisVectorIndex}
        if (scalarProcessor.IsMinusOne(v1))
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            scalarProcessor.CreateLinUnilinearMapComposer();

        foreach (var (index, scalar) in unitVector)
        {
            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] =
                scalarProcessor.Negative(scalar).ScalarValue;

            // Fill diagonal
            Debug.Assert(v1 != null, nameof(v1) + " != null");

            map[index, index] =
                (scalarProcessor.One - scalarProcessor.Square(scalar) / v1).ScalarValue;

            // Fill remaining items
            foreach (var (index2, scalar2) in unitVector)
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] =
                    (scalarProcessor.NegativeTimes(scalar, scalar2) / v1).ScalarValue;
            }
        }

        return map.GetMap();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> AbsScalars<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Abs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Sqrt<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Sqrt(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> SqrtOfAbs<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.SqrtOfAbs(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Exp<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Exp(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> LogE<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.LogE(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Log2<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Log2(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Log10<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Log10(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Log<T>(this LinUnilinearMap<T> v1, T scalar)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Log(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Log<T>(this T scalar, LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Log(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Power<T>(this LinUnilinearMap<T> v1, T scalar)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Power(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Power<T>(this T scalar, LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Log(scalar, s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Cos<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Cos(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Sin<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Sin(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Tan<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Tan(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ArcCos<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.ArcCos(scalar).RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ArcSin<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.ArcSin(scalar).RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> ArcTan<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.ArcTan(scalar).RadiansValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Cosh<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Cosh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Sinh<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Sinh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Tanh<T>(this LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(scalar => v1.ScalarProcessor.Tanh(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> CreateClarkeRotationMap<T>(this IScalarProcessor<T> processor, int vectorsCount)
    {
        return processor.CreateLinUnilinearMap(
            processor
                .CreateClarkeRotationArray(vectorsCount)
                .ColumnsToLinVectors(processor)
        );
    }
}