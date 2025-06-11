using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;

public static class LinUnilinearMapUtils
{
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Log<T>(this T scalar, LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Log(scalar, s).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinUnilinearMap<T> Power<T>(this T scalar, LinUnilinearMap<T> v1)
    {
        return v1.MapScalars(s => v1.ScalarProcessor.Log(scalar, s).ScalarValue);
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
    public static LinUnilinearMap<T> CreateClarkeRotationMap<T>(this IScalarProcessor<T> processor, int vectorsCount)
    {
        return processor.CreateLinUnilinearMap(
            processor
                .CreateClarkeRotationArray(vectorsCount)
                .ColumnsToLinVectors(processor)
        );
    }
}