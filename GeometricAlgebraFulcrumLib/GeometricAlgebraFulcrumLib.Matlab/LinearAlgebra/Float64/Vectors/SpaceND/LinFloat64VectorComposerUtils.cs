using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

public static class LinFloat64VectorComposerUtils
{
    
    internal static Dictionary<int, double> CreateLinVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<int, double>();

        foreach (var (key, value) in inputDictionary.ToTuples())
            basisScalarDictionary.Add(key, value);

        return basisScalarDictionary;
    }

    
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
    

    
    public static LinFloat64Vector CreateZeroLinVector()
    {
        return LinFloat64Vector.Zero;
    }

    
    public static LinFloat64Vector CreateLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
    {
        return LinFloat64Vector.Create(
            basisScalarDictionary.ToSimpleDictionary()
        );
    }

    public static LinFloat64Vector CreateUnitLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
    {
        var norm = basisScalarDictionary.Values.GetVectorNorm();

        if (norm.IsOne())
            return LinFloat64Vector.Create(
                basisScalarDictionary.ToSimpleDictionary()
            );

        var normInv = 1d / norm;

        return LinFloat64Vector.Create(
            basisScalarDictionary.ToDictionary(
                p => p.Key,
                p => p.Value * normInv
            ).ToSimpleDictionary()
        );
    }

    
    public static LinFloat64Vector CreateLinVector(params double[] scalarArray)
    {
        var scalarDictionary = scalarArray.CreateValidLinVectorDictionary();

        return LinFloat64Vector.Create(scalarDictionary);
    }

    
    public static LinFloat64Vector CreateLinVector(this IEnumerable<double> scalarList)
    {
        var scalarDictionary = scalarList.CreateValidLinVectorDictionary();

        return LinFloat64Vector.Create(scalarDictionary);
    }

    
    public static LinFloat64Vector CreateUnitLinVector(this IReadOnlyList<double> scalarList)
    {
        var normInv = 1d / scalarList.GetVectorNorm();

        var scalarDictionary = scalarList.Select(s => s * normInv)
            .CreateValidLinVectorDictionary(
            );

        return LinFloat64Vector.Create(scalarDictionary);
    }

    
    public static LinFloat64Vector CreateLinVector(this IEnumerable<LinFloat64VectorTerm> indexScalarList)
    {
        return LinFloat64VectorComposer.Create()
            .AddTerms(indexScalarList)
            .GetVector();
    }

    
    public static LinFloat64Vector CreateLinVector(this int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, 1d);

        return LinFloat64Vector.Create(basisScalarDictionary);
    }

    
    public static LinFloat64Vector CreateLinVector(this int index, double scalar)
    {
        if (scalar.IsZero())
            return LinFloat64Vector.Create();

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, scalar);

        return LinFloat64Vector.Create(basisScalarDictionary);
    }

    
    public static LinFloat64Vector CreateLinVector(this KeyValuePair<int, double> indexScalarPair)
    {
        return indexScalarPair.Key.CreateLinVector(indexScalarPair.Value);
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

    
    public static IEnumerable<LinFloat64Vector> RowsToLinVectors(this double[,] matrix)
    {
        return matrix.GetLength(0).GetRange().Select(
            matrix.RowToLinVector
        );
    }

    
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

    
    public static IEnumerable<LinFloat64Vector> RowsToLinVectors(this Matrix matrix)
    {
        return matrix.RowCount.GetRange().Select(
            matrix.RowToLinVector
        );
    }

    
    public static IEnumerable<LinFloat64Vector> ColumnsToLinVectors(this Matrix matrix)
    {
        return matrix.ColumnCount.GetRange().Select(
            matrix.ColumnToLinVector
        );
    }


    
    public static LinFloat64VectorComposer CreateLinVectorComposer()
    {
        return LinFloat64VectorComposer.Create();
    }



}