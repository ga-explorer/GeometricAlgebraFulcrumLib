using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps;

public static class LinFloat64UnilinearMapUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double[,] GetMapArray(this LinFloat64UnilinearMap map, int size)
    {
        return map.ToArray(size, size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<KeyValuePair<int, LinFloat64Vector>> GetColumns(this LinFloat64UnilinearMap map)
    {
        return map.GetMappedBasisVectors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector GetColumn(this LinFloat64UnilinearMap map, int colIndex)
    {
        return map.MapBasisVector(colIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector GetScaledColumn(this LinFloat64UnilinearMap map, int colIndex, double scalingFactor)
    {
        return map.MapBasisVector(colIndex).Times(scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector GetMappedColumn(this LinFloat64UnilinearMap map, int colIndex, Func<double, double> scalarMapping)
    {
        return map.MapBasisVector(colIndex).MapScalars(scalarMapping);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector GetMappedColumn(this LinFloat64UnilinearMap map, int colIndex, Func<int, double, double> indexScalarMapping)
    {
        return map.MapBasisVector(colIndex).MapScalars(indexScalarMapping);
    }


    public static LinFloat64Vector CombineColumns(this LinFloat64UnilinearMap map, IReadOnlyList<double> scalarList, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {
        var vector = LinFloat64Vector.ZeroVector;

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

    public static LinFloat64Vector CombineColumns(this LinFloat64UnilinearMap map, LinFloat64Vector scalingVector, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {
        
        var vector = LinFloat64Vector.ZeroVector;

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
    public static LinFloat64UnilinearMap CombineColumns(this LinFloat64UnilinearMap map1, LinFloat64UnilinearMap map2, Func<double, LinFloat64Vector, LinFloat64Vector> scalingFunc, Func<LinFloat64Vector, LinFloat64Vector, LinFloat64Vector> reducingFunc)
    {
        var vectorsDictionary = new Dictionary<int, LinFloat64Vector>();

        foreach (var (index, vector) in map2.GetColumns())
            vectorsDictionary.Add(
                index,
                map1.CombineColumns(vector, scalingFunc, reducingFunc)
            );

        return vectorsDictionary.ToLinUnilinearMap();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector MapVector(this double[,] matrix, LinFloat64Vector vector)
    {
        var composer = new LinFloat64VectorComposer();

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
        var composer = new LinFloat64VectorComposer();

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
        var composer = new LinFloat64VectorComposer();

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

    public static LinFloat64Vector MapVector(this LinFloat64UnilinearMap map, IReadOnlyList<double> vector)
    {
        var composer = new LinFloat64VectorComposer();

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

    public static LinFloat64Vector MapVector(this LinFloat64UnilinearMap map, IReadOnlyDictionary<int, double> vector)
    {
        var composer = new LinFloat64VectorComposer();

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
    public static LinFloat64UnilinearMap CreateVectorToVectorRotationMatrix(this IReadOnlyDictionary<int, double> sourceVector, IReadOnlyDictionary<int, double> targetVector, int basisVectorIndex, int vSpaceDimensions)
    {
        var map2 = CreateVectorToBasisRotationMap(sourceVector, basisVectorIndex, vSpaceDimensions);
        var map1 = CreateBasisToVectorRotationMap(basisVectorIndex, targetVector, vSpaceDimensions);

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
                -(scalar);

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
                -(scalar);

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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap AbsScalars(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Abs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Sqrt(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Sqrt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap SqrtOfAbs(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => s.SqrtOfAbs());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Exp(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Exp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap LogE(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => s.LogE());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Log2(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Log2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Log10(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Log10);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Log(this LinFloat64UnilinearMap v1, double scalar)
    {
        return v1.MapScalars(s => Math.Log(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Log(this double scalar, LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => Math.Log(scalar, s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Power(this LinFloat64UnilinearMap v1, double scalar)
    {
        return v1.MapScalars(s => Math.Pow(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Power(this double scalar, LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(s => Math.Log(scalar, s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Cos(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Cos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Sin(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Sin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Tan(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Tan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ArcCos(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Acos);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ArcSin(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Asin);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap ArcTan(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Atan);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Cosh(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Cosh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Sinh(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Sinh);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64UnilinearMap Tanh(this LinFloat64UnilinearMap v1)
    {
        return v1.MapScalars(Math.Tanh);
    }
}