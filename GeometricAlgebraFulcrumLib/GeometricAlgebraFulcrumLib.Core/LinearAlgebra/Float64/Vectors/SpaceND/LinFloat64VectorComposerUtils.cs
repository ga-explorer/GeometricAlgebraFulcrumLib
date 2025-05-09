using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.SpaceND;

public static class LinFloat64VectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<int, double> CreateLinVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<int, double>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(key, value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<int, double> CreateValidLinVectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = new Dictionary<int, double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            basisScalarDictionary.Add(index, scalar);

            index++;
        }

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateZeroLinVector()
    {
        return LinFloat64Vector.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
    {
        return new LinFloat64Vector(
            basisScalarDictionary.ToSimpleDictionary()
        );
    }

    public static LinFloat64Vector CreateUnitLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
    {
        var norm = basisScalarDictionary.Values.GetVectorNorm();

        if (norm.IsOne())
            return new LinFloat64Vector(
                basisScalarDictionary.ToSimpleDictionary()
            );

        var normInv = 1d / norm;

        return new LinFloat64Vector(
            basisScalarDictionary.ToDictionary(
                p => p.Key,
                p => p.Value * normInv
            ).ToSimpleDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(params double[] scalarArray)
    {
        var scalarDictionary = scalarArray.CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this IEnumerable<double> scalarList)
    {
        var scalarDictionary = scalarList.CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateUnitLinVector(this IReadOnlyList<double> scalarList)
    {
        var normInv = 1d / scalarList.GetVectorNorm();

        var scalarDictionary = scalarList.Select(s => s * normInv)
            .CreateValidLinVectorDictionary(
            );

        return new LinFloat64Vector(scalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this IEnumerable<LinFloat64VectorTerm> indexScalarList)
    {
        return LinFloat64VectorComposer.Create()
            .AddTerms(indexScalarList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, 1d);

        return new LinFloat64Vector(basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this int index, double scalar)
    {
        if (scalar.IsZero())
            return new LinFloat64Vector();

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, scalar);

        return new LinFloat64Vector(basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector CreateLinVector(this KeyValuePair<int, double> indexScalarPair)
    {
        return indexScalarPair.Key.CreateLinVector(indexScalarPair.Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinVector(this ILinSignedBasisVector term)
    {
        if (term.IsZero)
            return new LinFloat64Vector();

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(term.Index, term.IsPositive ? 1d : -1d);

        return new LinFloat64Vector(basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinVector(this LinFloat64VectorTerm term)
    {
        if (term.IsZero)
            return new LinFloat64Vector();

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(term.Index, term.ScalarValue);

        return new LinFloat64Vector(basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinVector(this XGaFloat64Vector mv)
    {
        var indexScalarDictionary = mv.ToDictionary(
            p => p.Key.FirstIndex,
            p => p.Value
        );

        return indexScalarDictionary.CreateLinVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector ToLinVector(this RGaFloat64Vector mv)
    {
        var indexScalarDictionary = mv.ToDictionary(
            p => p.Key.FirstOneBitPosition(),
            p => p.Value
        );

        return indexScalarDictionary.CreateLinVector();
    }


    public static LinFloat64Vector DiagonalToLinVector(this double[,] matrix)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = LinFloat64VectorComposer.Create();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static LinFloat64Vector RowToLinVector(this double[,] matrix, int row)
    {
        var columnCount = matrix.GetLength(1);
        var composer = LinFloat64VectorComposer.Create();

        for (var j = 0; j < columnCount; j++)
        {
            var scalar = matrix[row, j];

            composer.SetTerm(j, scalar);
        }

        return composer.GetVector();
    }

    public static LinFloat64Vector ColumnToLinVector(this double[,] matrix, int column)
    {
        var rowCount = matrix.GetLength(0);
        var composer = LinFloat64VectorComposer.Create();

        for (var i = 0; i < rowCount; i++)
        {
            var scalar = matrix[i, column];

            composer.SetTerm(i, scalar);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> RowsToLinVectors(this double[,] matrix)
    {
        return matrix.GetLength(0).GetRange().Select(
            matrix.RowToLinVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> ColumnsToLinVectors(this double[,] matrix)
    {
        return matrix.GetLength(1).GetRange().Select(
            matrix.ColumnToLinVector
        );
    }


    public static LinFloat64Vector DiagonalToLinVector(this Matrix matrix)
    {
        var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        var composer = LinFloat64VectorComposer.Create();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static LinFloat64Vector RowToLinVector(this Matrix matrix, int row)
    {
        var composer = LinFloat64VectorComposer.Create();

        for (var j = 0; j < matrix.ColumnCount; j++)
            composer.SetTerm(j, matrix[row, j]);

        return composer.GetVector();
    }

    public static LinFloat64Vector ColumnToLinVector(this Matrix matrix, int column)
    {
        var composer = LinFloat64VectorComposer.Create();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetTerm(i, matrix[i, column]);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> RowsToLinVectors(this Matrix matrix)
    {
        return matrix.RowCount.GetRange().Select(
            matrix.RowToLinVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<LinFloat64Vector> ColumnsToLinVectors(this Matrix matrix)
    {
        return matrix.ColumnCount.GetRange().Select(
            matrix.ColumnToLinVector
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer CreateLinVectorComposer()
    {
        return LinFloat64VectorComposer.Create();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer ToLinVectorComposer(this LinFloat64Vector mv)
    {
        return LinFloat64VectorComposer.Create().SetVector(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer NegativeToLinVectorComposer(this LinFloat64Vector mv)
    {
        return LinFloat64VectorComposer.Create().SetVectorNegative(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer ToLinVectorComposer(this LinFloat64Vector mv, double scalingFactor)
    {
        return LinFloat64VectorComposer.Create().SetVector(mv, scalingFactor);
    }


    public static Float64ScalarComposer AddESpTerms(this Float64ScalarComposer composer, IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
    {
        if (mv1.Count == 0 || mv2.Count == 0)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1)
            {
                if (!mv2.TryGetValue(id, out var scalar2))
                    continue;

                composer.AddScalarValue(scalar1 * scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2)
            {
                if (!mv1.TryGetValue(id, out var scalar1))
                    continue;

                composer.AddScalarValue(scalar1 * scalar2);
            }
        }

        return composer;
    }
    
    public static LinFloat64VectorComposer AddComponentTimesTerms(this LinFloat64VectorComposer composer, IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
    {
        if (mv1.Count == 0 || mv2.Count == 0)
            return composer;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1)
            {
                if (!mv2.TryGetValue(id, out var scalar2))
                    continue;

                composer.AddTerm(id, scalar1 * scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2)
            {
                if (!mv1.TryGetValue(id, out var scalar1))
                    continue;

                composer.AddTerm(id, scalar1 * scalar2);
            }
        }

        return composer;
    }

}