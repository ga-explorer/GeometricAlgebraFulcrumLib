using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers
{
    public static class XGaFloat64VectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<IIndexSet, double> CreateVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key.IndexToIndexSet(), value);

            return basisScalarDictionary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<IIndexSet, double> CreateValidVectorDictionary(IEnumerable<double> scalarList)
        {
            var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

            var index = 0;
            foreach (var scalar in scalarList)
            {
                if (!scalar.IsValid())
                    throw new InvalidOperationException();

                if(!scalar.IsZero())
                    basisScalarDictionary.Add(index.IndexToIndexSet(), scalar);

                index++;
            }

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateZeroVector(this XGaFloat64Processor processor)
        {
            return new XGaFloat64Vector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<IIndexSet, double>)
                return processor.CreateZeroVector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<IIndexSet, double>)
                return processor.CreateVector(basisScalarDictionary.First());

            return new XGaFloat64Vector(processor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IReadOnlyDictionary<int, double> basisScalarDictionary)
        {
            return new XGaFloat64Vector(processor, basisScalarDictionary.CreateVectorDictionary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, params double[] scalarArray)
        {
            return processor.CreateVector(
                CreateValidVectorDictionary(scalarArray)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IEnumerable<double> scalarList)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarList);

            return new XGaFloat64Vector(processor, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateXGaVector(this IEnumerable<double> scalarList, XGaFloat64Processor processor)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarList);

            return new XGaFloat64Vector(processor, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(
                    index.IndexToIndexSet(),
                    1d
                );

            return new XGaFloat64Vector(processor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, int index, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Vector(processor);

            var basisScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(
                    index.IndexToIndexSet(),
                    scalar
                );

            return new XGaFloat64Vector(processor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, KeyValuePair<int, double> indexScalarPair)
        {
            return processor.CreateVector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> indexScalarPair)
        {
            return processor.CreateVector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IIndexSet basisVector)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(basisVector, 1d);

            return new XGaFloat64Vector(processor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IIndexSet basisVector, double scalar)
        {
            if (scalar.IsZero())
                return new XGaFloat64Vector(processor);

            var basisScalarDictionary =
                new SingleItemDictionary<IIndexSet, double>(basisVector, scalar);

            return new XGaFloat64Vector(processor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, int termsCount, Func<int, double> indexToScalarFunc)
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
        public static XGaFloat64Vector CreateSymmetricVector(this XGaFloat64Processor processor, int count)
        {
            return processor.CreateSymmetricVector(count, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateSymmetricVector(this XGaFloat64Processor processor, int count, double scalarValue)
        {
            return count switch
            {
                < 0 => throw new InvalidOperationException(),

                0 => new XGaFloat64Vector(
                    processor,
                    new EmptyDictionary<IIndexSet, double>()
                ),

                1 => new XGaFloat64Vector(
                    processor,
                    new SingleItemDictionary<IIndexSet, double>(0.IndexToUInt64IndexSet(), scalarValue)
                ),

                _ => new XGaFloat64Vector(
                    processor,
                    new XGaFloat64RepeatedScalarVectorDictionary(count, scalarValue)
                )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateSymmetricUnitVector(this XGaFloat64Processor processor, int count)
        {
            return processor.CreateSymmetricVector(
                count,
                1d / Math.Sqrt(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateUnitXGaFloat64Vector(this double angle, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var processor = XGaFloat64Processor.Euclidean;

            var scalar1 = Math.Cos(angle);
            var scalar2 = Math.Sin(angle);

            return processor
                .CreateComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateXGaPhasor(this double angle, double magnitude, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var processor = XGaFloat64Processor.Euclidean;

            var scalar1 = magnitude * Math.Cos(angle);
            var scalar2 = magnitude * Math.Sin(angle);

            return processor
                .CreateComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, Float64Vector vector)
        {
            return processor.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IFloat64Vector2D vector)
        {
            return processor
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IFloat64Vector3D vector)
        {
            return processor
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector CreateVector(this XGaFloat64Processor processor, IFloat64Vector4D vector)
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
        public static XGaFloat64Vector ToXGaFloat64Vector(this Float64Vector vector)
        {
            return XGaFloat64Processor.Euclidean.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this Float64Vector vector, XGaFloat64Processor processor)
        {
            return processor.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector2D vector)
        {
            return XGaFloat64Processor
                .Euclidean
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector2D vector, XGaFloat64Processor processor)
        {
            return processor
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector3D vector)
        {
            return XGaFloat64Processor
                .Euclidean
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector3D vector, XGaFloat64Processor processor)
        {
            return processor
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector4D vector)
        {
            return XGaFloat64Processor
                .Euclidean
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .SetTerm(8, vector.W)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Vector ToXGaFloat64Vector(this IFloat64Vector4D vector, XGaFloat64Processor processor)
        {
            return processor
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .SetTerm(8, vector.W)
                .GetVector();
        }


        public static XGaFloat64Vector DiagonalToXGaFloat64Vector(this double[,] matrix, XGaFloat64Processor processor)
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

        public static XGaFloat64Vector RowToXGaFloat64Vector(this double[,] matrix, int row, XGaFloat64Processor processor)
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

        public static XGaFloat64Vector ColumnToXGaFloat64Vector(this double[,] matrix, int column, XGaFloat64Processor processor)
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
        public static IEnumerable<XGaFloat64Vector> RowsToXGaFloat64Vectors(this double[,] matrix, XGaFloat64Processor processor)
        {
            return matrix.GetLength(0).GetRange().Select(
                r => matrix.RowToXGaFloat64Vector(r, processor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaFloat64Vector> ColumnsToXGaFloat64Vectors(this double[,] matrix, XGaFloat64Processor processor)
        {
            return matrix.GetLength(1).GetRange().Select(
                c => matrix.ColumnToXGaFloat64Vector(c, processor)
            );
        }


        public static XGaFloat64Vector DiagonalToXGaFloat64Vector(this Matrix matrix, XGaFloat64Processor processor)
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

        public static XGaFloat64Vector RowToXGaFloat64Vector(this Matrix matrix, int row, XGaFloat64Processor processor)
        {
            var composer = processor.CreateComposer();

            for (var i = 0; i < matrix.ColumnCount; i++)
                composer.SetVectorTerm(i, matrix[row, i]);

            return composer.GetVector();
        }

        public static XGaFloat64Vector ColumnToXGaFloat64Vector(this Matrix matrix, int column, XGaFloat64Processor processor)
        {
            var composer = processor.CreateComposer();

            for (var i = 0; i < matrix.RowCount; i++)
                composer.SetVectorTerm(i, matrix[i, column]);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaFloat64Vector> RowsToXGaFloat64Vectors(this Matrix matrix, XGaFloat64Processor processor)
        {
            return matrix.RowCount.GetRange().Select(
                r => matrix.RowToXGaFloat64Vector(r, processor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<XGaFloat64Vector> ColumnsToXGaFloat64Vectors(this Matrix matrix, XGaFloat64Processor processor)
        {
            return matrix.ColumnCount.GetRange().Select(
                c => matrix.ColumnToXGaFloat64Vector(c, processor)
            );
        }
    }
}