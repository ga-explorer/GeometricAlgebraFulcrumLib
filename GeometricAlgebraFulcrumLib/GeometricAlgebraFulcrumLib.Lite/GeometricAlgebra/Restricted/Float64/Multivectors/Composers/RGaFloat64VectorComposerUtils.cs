using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

public static class RGaFloat64VectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, double> CreateVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, double>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(1UL << key, value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, double> CreateValidVectorDictionary(IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = new Dictionary<ulong, double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                basisScalarDictionary.Add(1UL << index, scalar);

            index++;
        }

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateZeroVector(this RGaFloat64Processor processor)
    {
        return new RGaFloat64Vector(processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IReadOnlyDictionary<ulong, double> idScalarDictionary)
    {
        if (idScalarDictionary.Count == 0 && idScalarDictionary is not EmptyDictionary<ulong, double>)
            return processor.CreateZeroVector();

        if (idScalarDictionary.Count == 1 && idScalarDictionary is not SingleItemDictionary<ulong, double>)
            return processor.CreateTermVector(idScalarDictionary.First());

        return new RGaFloat64Vector(processor, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IReadOnlyDictionary<int, double> indexScalarDictionary)
    {
        if (indexScalarDictionary.Count == 0)
            return processor.CreateZeroVector();

        if (indexScalarDictionary.Count == 1)
            return processor.CreateTermVector(indexScalarDictionary.First());

        return new RGaFloat64Vector(
            processor, 
            indexScalarDictionary.ToDictionary(
                p => 1UL << p.Key, 
                p => p.Value
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, params double[] scalarArray)
    {
        var scalarDictionary = CreateValidVectorDictionary(scalarArray);

        return processor.CreateVector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IEnumerable<double> scalarList)
    {
        var scalarDictionary = CreateValidVectorDictionary(scalarList);

        return processor.CreateVector(
            scalarDictionary
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, int termsCount, Func<int, double> indexToScalarFunc)
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
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<ulong, double>(
                1UL << index,
                1d
            );

        return new RGaFloat64Vector(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, int index, double scalar)
    {
        if (scalar.IsZero())
            return new RGaFloat64Vector(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, double>(
                1UL << index,
                scalar
            );

        return new RGaFloat64Vector(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, KeyValuePair<int, double> indexScalarPair)
    {
        return processor.CreateTermVector(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, ulong basisVector)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<ulong, double>(basisVector, 1d);

        return new RGaFloat64Vector(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, ulong basisVector, double scalar)
    {
        if (scalar.IsZero())
            return new RGaFloat64Vector(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, double>(basisVector, scalar);

        return new RGaFloat64Vector(processor, basisScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateTermVector(this RGaFloat64Processor processor, KeyValuePair<ulong, double> idScalarPair)
    {
        return processor.CreateTermVector(idScalarPair.Key, idScalarPair.Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateSymmetricVector(this RGaFloat64Processor processor, int count)
    {
        return processor.CreateSymmetricVector(count, 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateSymmetricVector(this RGaFloat64Processor processor, int count, double scalarValue)
    {
        return count switch
        {
            < 0 => throw new InvalidOperationException(),

            0 => new RGaFloat64Vector(
                processor,
                new EmptyDictionary<ulong, double>()
            ),

            1 => new RGaFloat64Vector(
                processor,
                new SingleItemDictionary<ulong, double>(1UL, scalarValue)
            ),

            _ => new RGaFloat64Vector(
                processor,
                new RGaFloat64RepeatedScalarVectorDictionary(count, scalarValue)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateSymmetricUnitVector(this RGaFloat64Processor processor, int count)
    {
        return processor.CreateSymmetricVector(
            count,
            1d / Math.Sqrt(count)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateUnitRGaVector(this double angle, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var processor = RGaFloat64Processor.Euclidean;

        var scalar1 = Math.Cos(angle);
        var scalar2 = Math.Sin(angle);

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateRGaPhasor(this double angle, double magnitude, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var processor = RGaFloat64Processor.Euclidean;

        var scalar1 = magnitude * Math.Cos(angle);
        var scalar2 = magnitude * Math.Sin(angle);

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, Float64Vector vector)
    {
        return processor.CreateVector(
            vector.GetIndexScalarDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IFloat64Vector2D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IFloat64Vector3D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector CreateVector(this RGaFloat64Processor processor, IFloat64Vector4D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .SetTerm(8, vector.W)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this Float64Vector vector)
    {
        return RGaFloat64Processor.Euclidean.CreateVector(
            vector.GetIndexScalarDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this Float64Vector vector, RGaFloat64Processor processor)
    {
        return processor.CreateVector(
            vector.GetIndexScalarDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector2D vector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector2D vector, RGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector3D vector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector3D vector, RGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector4D vector)
    {
        return RGaFloat64Processor
            .Euclidean
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .SetTerm(8, vector.W)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector ToRGaFloat64Vector(this IFloat64Vector4D vector, RGaFloat64Processor processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .SetTerm(8, vector.W)
            .GetVector();
    }


    public static RGaFloat64Vector DiagonalToRGaFloat64Vector(this double[,] matrix, RGaFloat64Processor processor)
    {
        var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
        var composer = processor.CreateComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static RGaFloat64Vector RowToRGaFloat64Vector(this double[,] matrix, int row, RGaFloat64Processor processor)
    {
        var columnCount = matrix.GetLength(1);
        var composer = processor.CreateComposer();

        for (var i = 0; i < columnCount; i++)
        {
            var scalar = matrix[i, row];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static RGaFloat64Vector ColumnToRGaFloat64Vector(this double[,] matrix, int column, RGaFloat64Processor processor)
    {
        var rowCount = matrix.GetLength(0);
        var composer = processor.CreateComposer();

        for (var i = 0; i < rowCount; i++)
        {
            var scalar = matrix[column, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaFloat64Vector> RowsToRGaFloat64Vectors(this double[,] matrix, RGaFloat64Processor processor)
    {
        return matrix.GetLength(0).GetRange().Select(
            r => matrix.RowToRGaFloat64Vector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaFloat64Vector> ColumnsToRGaFloat64Vectors(this double[,] matrix, RGaFloat64Processor processor)
    {
        return matrix.GetLength(1).GetRange().Select(
            c => matrix.ColumnToRGaFloat64Vector(c, processor)
        );
    }


    public static RGaFloat64Vector DiagonalToRGaFloat64Vector(this Matrix matrix, RGaFloat64Processor processor)
    {
        var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
        var composer = processor.CreateComposer();

        for (var i = 0; i < count; i++)
        {
            var scalar = matrix[i, i];

            composer.SetVectorTerm(i, scalar);
        }

        return composer.GetVector();
    }

    public static RGaFloat64Vector RowToRGaFloat64Vector(this Matrix matrix, int row, RGaFloat64Processor processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.ColumnCount; i++)
            composer.SetVectorTerm(i, matrix[row, i]);

        return composer.GetVector();
    }

    public static RGaFloat64Vector ColumnToRGaFloat64Vector(this Matrix matrix, int column, RGaFloat64Processor processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetVectorTerm(i, matrix[i, column]);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaFloat64Vector> RowsToRGaFloat64Vectors(this Matrix matrix, RGaFloat64Processor processor)
    {
        return matrix.RowCount.GetRange().Select(
            r => matrix.RowToRGaFloat64Vector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaFloat64Vector> ColumnsToRGaFloat64Vectors(this Matrix matrix, RGaFloat64Processor processor)
    {
        return matrix.ColumnCount.GetRange().Select(
            c => matrix.ColumnToRGaFloat64Vector(c, processor)
        );
    }
}