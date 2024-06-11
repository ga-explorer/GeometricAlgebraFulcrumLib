using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public static class RGaVectorComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateVectorDictionary<T>(this IReadOnlyDictionary<int, T> inputDictionary)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (key, value) in inputDictionary)
            basisScalarDictionary.Add(1UL << key, value);

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateValidVectorDictionary<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarList)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!processor.ScalarProcessor.IsValid(scalar))
                throw new InvalidOperationException();

            if (!processor.ScalarProcessor.IsZero(scalar))
                basisScalarDictionary.Add(1UL << index, scalar);

            index++;
        }

        return basisScalarDictionary;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateValidVectorDictionary<T>(this RGaProcessor<T> processor, IEnumerable<Scalar<T>> scalarList)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                basisScalarDictionary.Add(1UL << index, scalar.ScalarValue);

            index++;
        }

        return basisScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Dictionary<ulong, T> CreateValidVectorDictionary<T>(this RGaProcessor<T> processor, IEnumerable<IScalar<T>> scalarList)
    {
        var basisScalarDictionary = new Dictionary<ulong, T>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if (!scalar.IsZero())
                basisScalarDictionary.Add(1UL << index, scalar.ScalarValue);

            index++;
        }

        return basisScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> basisScalarDictionary)
    {
        if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, T>)
            return processor.VectorZero;

        if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, T>)
            return processor.VectorTerm(basisScalarDictionary.First());

        return new RGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, IReadOnlyDictionary<int, T> basisScalarDictionary)
    {
        return new RGaVector<T>(processor, basisScalarDictionary.CreateVectorDictionary());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params int[] scalarArray)
    {
        var scalarDictionary = processor.CreateValidVectorDictionary(
            scalarArray.Select(processor.ScalarProcessor.ValueFromNumber)
        );

        return processor.Vector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params double[] scalarArray)
    {
        var scalarDictionary = processor.CreateValidVectorDictionary(
            scalarArray.Select(processor.ScalarProcessor.ValueFromNumber)
        );

        return processor.Vector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params string[] scalarArray)
    {
        var scalarDictionary = processor.CreateValidVectorDictionary(
            scalarArray.Select(
                text => processor.ScalarProcessor.ScalarFromText(text)
            )
        );

        return processor.Vector(
            scalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params T[] scalarArray)
    {
        return processor.Vector(
            processor.CreateValidVectorDictionary(scalarArray)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params Scalar<T>[] scalarArray)
    {
        return processor.Vector(
            processor.CreateValidVectorDictionary(scalarArray)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, params IScalar<T>[] scalarArray)
    {
        return processor.Vector(
            processor.CreateValidVectorDictionary(scalarArray)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarList)
    {
        return processor.Vector(
            processor.CreateValidVectorDictionary(scalarList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this IEnumerable<T> scalarList, RGaProcessor<T> processor)
    {
        return processor.Vector(
            processor.CreateValidVectorDictionary(scalarList)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> CreateRGaVector<T>(this IEnumerable<T> scalarList, RGaProcessor<T> processor)
    {
        var scalarDictionary = CreateValidVectorDictionary(processor, scalarList);

        return new RGaVector<T>(processor, scalarDictionary);
    }
        
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, int termsCount, Func<int, double> indexToScalarFunc)
    {
        var scalarProcessor = processor.ScalarProcessor;
        var composer = processor.CreateComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetVectorTerm(index, scalarProcessor.ValueFromNumber(scalar));
        }

        return composer.GetVector();
    }

    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, int termsCount, Func<int, string> indexToScalarFunc)
    {
        var scalarProcessor = processor.ScalarProcessor;
        var composer = processor.CreateComposer();

        for (var index = 0; index < termsCount; index++)
        {
            var scalar = indexToScalarFunc(index);

            composer.SetVectorTerm(index, scalarProcessor.ValueFromText(scalar));
        }

        return composer.GetVector();
    }

    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, int termsCount, Func<int, T> indexToScalarFunc)
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
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(
                1UL << index,
                processor.ScalarProcessor.OneValue
            );

        return new RGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, int index, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(
                1UL << index,
                scalar
            );

        return new RGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<int, T> indexScalarPair)
    {
        return processor.VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, int index, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new RGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(
                1UL << index,
                scalar.ScalarValue
            );

        return new RGaVector<T>(processor, basisScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, ulong basisVector)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(basisVector, processor.ScalarProcessor.OneValue);

        return new RGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, ulong basisVector, T scalar)
    {
        if (processor.ScalarProcessor.IsZero(scalar))
            return new RGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(basisVector, scalar);

        return new RGaVector<T>(processor, basisScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, ulong basisVector, Scalar<T> scalar)
    {
        if (scalar.IsZero())
            return new RGaVector<T>(processor);

        var basisScalarDictionary =
            new SingleItemDictionary<ulong, T>(
                basisVector,
                scalar.ScalarValue
            );

        return new RGaVector<T>(processor, basisScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorTerm<T>(this RGaProcessor<T> processor, KeyValuePair<ulong, T> indexScalarPair)
    {
        return processor.VectorTerm(indexScalarPair.Key, indexScalarPair.Value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorSymmetric<T>(this RGaProcessor<T> processor, int count)
    {
        return processor.VectorSymmetric(

            count,
            processor.ScalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorSymmetric<T>(this RGaProcessor<T> processor, int count, T scalarValue)
    {
        return count switch
        {
            < 0 => throw new InvalidOperationException(),

            0 => new RGaVector<T>(
                processor,

                new EmptyDictionary<ulong, T>()
            ),

            1 => new RGaVector<T>(
                processor,

                new SingleItemDictionary<ulong, T>(1UL, scalarValue)
            ),

            _ => new RGaVector<T>(
                processor,

                new RGaRepeatedScalarVectorDictionary<T>(count, scalarValue)
            )
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorSymmetric<T>(this RGaProcessor<T> processor, int count, IScalar<T> scalarValue)
    {
        return VectorSymmetric(processor, count, scalarValue.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorSymmetricUnit<T>(this RGaProcessor<T> processor, int count)
    {
        return processor.VectorSymmetric(
            count,
            processor.ScalarProcessor.Sqrt(count).Inverse().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> CreateUnitRGaVector<T>(this LinAngle<T> angle, int index1, int index2, RGaProcessor<T> processor)
    {
        Debug.Assert(index2 > index1);

        var (scalar1, scalar2) = angle;

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorUnit<T>(this RGaProcessor<T> processor, LinAngle<T> angle, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var scalar1 = angle.Cos().ScalarValue;
        var scalar2 = angle.Sin().ScalarValue;

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static RGaVector<T> VectorPhasor<T>(this RGaProcessor<T> processor, LinAngle<T> angle, T magnitude, int index1, int index2)
    //{
    //    Debug.Assert(index2 > index1);

    //    var scalar1 = processor.ScalarProcessor.Times(magnitude, processor.ScalarProcessor.Cos(angle));
    //    var scalar2 = processor.ScalarProcessor.Times(magnitude, processor.ScalarProcessor.Sin(angle));

    //    return processor
    //        .CreateComposer()
    //        .SetVectorTerm(index1, scalar1)
    //        .SetVectorTerm(index2, scalar2)
    //        .GetVector();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> VectorPhasor<T>(this RGaProcessor<T> processor, LinAngle<T> angle, T magnitude, int index1, int index2)
    {
        Debug.Assert(index2 > index1);

        var scalar1 = processor.ScalarProcessor.Times(magnitude, angle.Cos().ScalarValue);
        var scalar2 = processor.ScalarProcessor.Times(magnitude, angle.Sin().ScalarValue);

        return processor
            .CreateComposer()
            .SetVectorTerm(index1, scalar1)
            .SetVectorTerm(index2, scalar2)
            .GetVector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, LinVector<T> vector)
    {
        if (vector.VSpaceDimensions > 64)
            throw new InvalidOperationException();

        var idScalarDictionary =
            vector.GetIndexScalarDictionary().ToDictionary(
                p => 1UL << p.Key,
                p => p.Value
            );

        return processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, ILinFloat64Vector2D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, ILinFloat64Vector3D vector)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> Vector<T>(this RGaProcessor<T> processor, ILinFloat64Vector4D vector)
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
    public static RGaVector<T> ToRGaVector<T>(this LinVector<T> vector)
    {
        if (vector.VSpaceDimensions > 64)
            throw new InvalidOperationException();

        var idScalarDictionary =
            vector.GetIndexScalarDictionary().ToDictionary(
                p => 1UL << p.Key,
                p => p.Value
            );

        return RGaProcessor<T>.CreateEuclidean(vector.ScalarProcessor).Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ToRGaVector<T>(this LinVector<T> vector, RGaProcessor<T> processor)
    {
        if (vector.VSpaceDimensions > 64)
            throw new InvalidOperationException();

        var idScalarDictionary =
            vector.GetIndexScalarDictionary().ToDictionary(
                p => 1UL << p.Key,
                p => p.Value
            );

        return processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ToRGaVector<T>(this ILinFloat64Vector2D vector, RGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ToRGaVector<T>(this ILinFloat64Vector3D vector, RGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> ToRGaVector<T>(this ILinFloat64Vector4D vector, RGaProcessor<T> processor)
    {
        return processor
            .CreateComposer()
            .SetTerm(1, vector.X)
            .SetTerm(2, vector.Y)
            .SetTerm(4, vector.Z)
            .SetTerm(8, vector.W)
            .GetVector();
    }


    public static RGaVector<T> DiagonalToRGaVector<T>(this T[,] matrix, RGaProcessor<T> processor)
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

    public static RGaVector<T> RowToRGaVector<T>(this T[,] matrix, int row, RGaProcessor<T> processor)
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

    public static RGaVector<T> ColumnToRGaVector<T>(this T[,] matrix, int column, RGaProcessor<T> processor)
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
    public static IEnumerable<RGaVector<T>> RowsToRGaVectors<T>(this T[,] matrix, RGaProcessor<T> processor)
    {
        return matrix.GetLength(0).GetRange().Select(
            r => matrix.RowToRGaVector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaVector<T>> ColumnsToRGaVectors<T>(this T[,] matrix, RGaProcessor<T> processor)
    {
        return matrix.GetLength(1).GetRange().Select(
            c => matrix.ColumnToRGaVector(c, processor)
        );
    }


    public static RGaVector<T> DiagonalToRGaVector<T>(this Matrix matrix, RGaProcessor<T> processor)
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

    public static RGaVector<T> RowToRGaVector<T>(this Matrix matrix, int row, RGaProcessor<T> processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.ColumnCount; i++)
            composer.SetVectorTerm(i, matrix[row, i]);

        return composer.GetVector();
    }

    public static RGaVector<T> ColumnToRGaVector<T>(this Matrix matrix, int column, RGaProcessor<T> processor)
    {
        var composer = processor.CreateComposer();

        for (var i = 0; i < matrix.RowCount; i++)
            composer.SetVectorTerm(i, matrix[i, column]);

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaVector<T>> RowsToRGaVectors<T>(this Matrix matrix, RGaProcessor<T> processor)
    {
        return matrix.RowCount.GetRange().Select(
            r => matrix.RowToRGaVector(r, processor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaVector<T>> ColumnsToRGaVectors<T>(this Matrix matrix, RGaProcessor<T> processor)
    {
        return matrix.ColumnCount.GetRange().Select(
            c => matrix.ColumnToRGaVector(c, processor)
        );
    }
}