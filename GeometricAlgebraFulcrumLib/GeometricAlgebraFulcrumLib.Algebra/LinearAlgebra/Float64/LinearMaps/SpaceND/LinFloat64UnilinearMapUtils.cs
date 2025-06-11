using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

public static class LinFloat64UnilinearMapUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Log(this double scalar, LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => Math.Log(scalar, s));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Power(this double scalar, LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => Math.Log(scalar, s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVector(this double[,] matrix, LinFloat64Vector vector)
    {
        var composer = LinFloat64VectorComposer.Create();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        foreach (var (colIndex, scalar) in vector)
        {
            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalar * matrix[rowIndex, colIndex]
                );
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVector(this double[,] matrix, IEnumerable<double> vector)
    {
        var composer = LinFloat64VectorComposer.Create();

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
                    scalar * matrix[rowIndex, colIndex]
                );

            colIndex++;
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVector(this double[,] matrix, IReadOnlyDictionary<int, double> vector)
    {
        var composer = LinFloat64VectorComposer.Create();

        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);

        foreach (var (colIndex, scalar) in vector)
        {
            if (colIndex >= colCount)
                continue;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
                composer.AddTerm(
                    rowIndex,
                    scalar * matrix[rowIndex, colIndex]
                );
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVectorUsing(this IReadOnlyList<double> vector, LinFloat64UnilinearMap map)
    {
        return map.MapVector(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVectorUsing(this IReadOnlyDictionary<int, double> vector, LinFloat64UnilinearMap map)
    {
        return map.MapVector(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap CreateVectorToVectorRotationMatrix(this IReadOnlyDictionary<int, double> sourceVector, IReadOnlyDictionary<int, double> targetVector, int basisVectorIndex, int vSpaceDimensions)
    {
        var map2 = sourceVector.CreateVectorToBasisRotationMap(basisVectorIndex, vSpaceDimensions);
        var map1 = basisVectorIndex.CreateBasisToVectorRotationMap(targetVector, vSpaceDimensions);

        return map1.Map(map2);
    }

    public static LinFloat64UnilinearMap CreateBasisToVectorRotationMap(this int basisVectorIndex, IReadOnlyDictionary<int, double> unitVector, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = unitVector.GetVectorTermScalar(basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (v1.IsOne())
            return vSpaceDimensions.CreateIdentityLinUnilinearMap();

        // Special case: unitVector == -e_{basisVectorIndex}
        if (v1.IsMinusOne())
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            new LinFloat64UnilinearMapComposer();

        foreach (var (index, scalar) in unitVector)
        {
            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] =
                -scalar;

            map[index, index] = 1d - scalar * scalar / v1;

            foreach (var (index2, scalar2) in unitVector)
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] = -(scalar * scalar2) / v1;
            }
        }

        return map.GetMap();
    }

    public static LinFloat64UnilinearMap CreateVectorToBasisRotationMap(this IReadOnlyDictionary<int, double> unitVector, int basisVectorIndex, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 2)
            throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

        var v1 = unitVector.GetVectorTermScalar(basisVectorIndex);

        // Special case: unitVector == e_{basisVectorIndex}
        if (v1.IsOne())
            return vSpaceDimensions.CreateIdentityLinUnilinearMap();

        // Special case: unitVector == -e_{basisVectorIndex}
        if (v1.IsMinusOne())
        {
            //TODO: Handle this case
            throw new InvalidOperationException();
        }

        var map =
            new LinFloat64UnilinearMapComposer();

        foreach (var (index, scalar) in unitVector)
        {
            // Fill row number basisVectorIndex
            map[basisVectorIndex, index] = scalar;

            if (index == basisVectorIndex)
                continue;

            // Fill column number basisVectorIndex
            map[index, basisVectorIndex] =
                -scalar;

            // Fill diagonal
            map[index, index] = 1d - scalar * scalar / v1;

            // Fill remaining items
            foreach (var (index2, scalar2) in unitVector)
            {
                if (index2 == basisVectorIndex || index2 == index)
                    continue;

                map[index, index2] = -(scalar * scalar2) / v1;
            }
        }

        return map.GetMap();
    }


}