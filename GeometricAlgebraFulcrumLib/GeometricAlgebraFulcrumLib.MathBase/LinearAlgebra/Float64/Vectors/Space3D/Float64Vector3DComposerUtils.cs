using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public static class Float64Vector3DComposerUtils
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Dictionary<int, double> CreateLinVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
        //{
        //    var basisScalarDictionary = new Dictionary<int, double>();

        //    foreach (var (key, value) in inputDictionary)
        //        basisScalarDictionary.Add(key, value);

        //    return basisScalarDictionary;
        //}
    
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //internal static Dictionary<int, double> CreateValidLinVectorDictionary(this IEnumerable<double> scalarList)
        //{
        //    var basisScalarDictionary = new Dictionary<int, double>();

        //    var index = 0;
        //    foreach (var scalar in scalarList)
        //    {
        //        if (!scalar.IsValid())
        //            throw new InvalidOperationException();

        //        basisScalarDictionary.Add(index, scalar);

        //        index++;
        //    }

        //    return basisScalarDictionary;
        //}


        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateZeroLinVector()
        //{
        //    return ITriplet<double>.ZeroVector;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
        //{
        //    return new ITriplet<double>(
        //        basisScalarDictionary.ToSimpleDictionary()
        //    );
        //}
    
        //public static ITriplet<double> CreateUnitLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
        //{
        //    var norm = basisScalarDictionary.Values.GetVectorNorm();

        //    if (norm.IsOne())
        //        return new ITriplet<double>(
        //            basisScalarDictionary.ToSimpleDictionary()
        //        );

        //    var normInv = 1d / norm;

        //    return new ITriplet<double>(
        //        basisScalarDictionary.ToDictionary(
        //            p => p.Key, 
        //            p => p.Value * normInv
        //        ).ToSimpleDictionary()
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(params double[] scalarArray)
        //{
        //    var scalarDictionary = CreateValidLinVectorDictionary(scalarArray);

        //    return new ITriplet<double>(scalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this IEnumerable<double> scalarList)
        //{
        //    var scalarDictionary = CreateValidLinVectorDictionary(scalarList);

        //    return new ITriplet<double>(scalarDictionary);
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateUnitLinVector(this IReadOnlyList<double> scalarList)
        //{
        //    var normInv = 1d / scalarList.GetVectorNorm();

        //    var scalarDictionary = CreateValidLinVectorDictionary(
        //        scalarList.Select(s => s * normInv)
        //    );

        //    return new ITriplet<double>(scalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this IEnumerable<LinFloat64VectorTerm> indexScalarList)
        //{
        //    return new Float64Tuple3DComposer()
        //        .AddTerms(indexScalarList)
        //        .GetVector();
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this int index)
        //{
        //    var basisScalarDictionary =
        //        new SingleItemDictionary<int, double>(index, 1d);

        //    return new ITriplet<double>(basisScalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this int index, double scalar)
        //{
        //    if (scalar.IsZero())
        //        return new ITriplet<double>();

        //    var basisScalarDictionary =
        //        new SingleItemDictionary<int, double>(index, scalar);

        //    return new ITriplet<double>(basisScalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> CreateLinVector(this KeyValuePair<int, double> indexScalarPair)
        //{
        //    return indexScalarPair.Key.CreateLinVector(indexScalarPair.Value);
        //}
    
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> ToVector(this ILinSignedBasisVector term)
        //{
        //    if (term.IsZero)
        //        return new ITriplet<double>();

        //    var basisScalarDictionary =
        //        new SingleItemDictionary<int, double>(term.Index, term.IsPositive ? 1d : -1d);

        //    return new ITriplet<double>(basisScalarDictionary);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> ToVector(this LinFloat64VectorTerm term)
        //{
        //    if (term.IsZero)
        //        return new ITriplet<double>();

        //    var basisScalarDictionary =
        //        new SingleItemDictionary<int, double>(term.Index, term.ScalarValue);

        //    return new ITriplet<double>(basisScalarDictionary);
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> VectorToLinVector(this IReadOnlyDictionary<IIndexSet, double> mv)
        //{
        //    var indexScalarDictionary = mv.ToDictionary(
        //        p => p.Key.FirstIndex,
        //        p => p.Value
        //    );

        //    return indexScalarDictionary.CreateLinVector();
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static ITriplet<double> VectorToLinVector(this IReadOnlyDictionary<ulong, double> mv)
        //{
        //    var indexScalarDictionary = mv.ToDictionary(
        //        p => p.Key.FirstOneBitPosition(),
        //        p => p.Value
        //    );

        //    return indexScalarDictionary.CreateLinVector();
        //}
        
        
        //public static ITriplet<double> DiagonalToLinVector(this double[,] matrix)
        //{
        //    var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < count; i++)
        //    {
        //        var scalar = matrix[i, i];

        //        composer.SetTerm(i, scalar);
        //    }

        //    return composer.GetVector();
        //}

        //public static ITriplet<double> RowToLinVector(this double[,] matrix, int row)
        //{
        //    var columnCount = matrix.GetLength(1);
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < columnCount; i++)
        //    {
        //        var scalar = matrix[i, row];

        //        composer.SetTerm(i, scalar);
        //    }

        //    return composer.GetVector();
        //}
        
        //public static ITriplet<double> ColumnToLinVector(this double[,] matrix, int column)
        //{
        //    var rowCount = matrix.GetLength(0);
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < rowCount; i++)
        //    {
        //        var scalar = matrix[column, i];

        //        composer.SetTerm(i, scalar);
        //    }

        //    return composer.GetVector();
        //}
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<ITriplet<double>> RowsToLinVectors(this double[,] matrix)
        //{
        //    return matrix.GetLength(0).GetRange().Select(
        //        matrix.RowToLinVector
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<ITriplet<double>> ColumnsToLinVectors(this double[,] matrix)
        //{
        //    return matrix.GetLength(1).GetRange().Select(
        //        matrix.ColumnToLinVector
        //    );
        //}

        
        //public static ITriplet<double> DiagonalToLinVector(this Matrix matrix)
        //{
        //    var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < count; i++)
        //    {
        //        var scalar = matrix[i, i];

        //        composer.SetTerm(i, scalar);
        //    }

        //    return composer.GetVector();
        //}

        //public static ITriplet<double> RowToLinVector(this Matrix matrix, int row)
        //{
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < matrix.ColumnCount; i++)
        //        composer.SetTerm(i, matrix[row, i]);

        //    return composer.GetVector();
        //}
        
        //public static ITriplet<double> ColumnToLinVector(this Matrix matrix, int column)
        //{
        //    var composer = new Float64Tuple3DComposer();

        //    for (var i = 0; i < matrix.RowCount; i++)
        //        composer.SetTerm(i, matrix[i, column]);

        //    return composer.GetVector();
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<ITriplet<double>> RowsToLinVectors(this Matrix matrix)
        //{
        //    return matrix.RowCount.GetRange().Select(
        //        matrix.RowToLinVector
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<ITriplet<double>> ColumnsToLinVectors(this Matrix matrix)
        //{
        //    return matrix.ColumnCount.GetRange().Select(
        //        matrix.ColumnToLinVector
        //    );
        //}
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer ToComposer(this IFloat64Tuple3D mv)
        {
            return Float64Vector3DComposer.Create().SetVector(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer NegativeToComposer(this IFloat64Tuple3D mv)
        {
            return Float64Vector3DComposer.Create().SetVectorNegative(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer ToComposer(this IFloat64Tuple3D mv, double scalingFactor)
        {
            return Float64Vector3DComposer.Create().SetVector(mv, scalingFactor);
        }

    
        //public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
        //{
        //    if (mv1.Count == 0 || mv2.Count == 0)
        //        return composer;
        
        //    if (mv1.Count <= mv2.Count)
        //    {
        //        foreach (var (id, scalar1) in mv1)
        //        {
        //            if (!mv2.TryGetValue(id, out var scalar2))
        //                continue;

        //            composer.AddScalarValue(scalar1 * scalar2);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var (id, scalar2) in mv2)
        //        {
        //            if (!mv1.TryGetValue(id, out var scalar1))
        //                continue;

        //            composer.AddScalarValue(scalar1 * scalar2);
        //        }
        //    }

        //    return composer;
        //}

    }
}