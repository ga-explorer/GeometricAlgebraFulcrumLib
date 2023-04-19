using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
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

                basisScalarDictionary.Add(1UL << index, scalar);

                index++;
            }

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateZeroVector(this RGaFloat64Processor metric)
        {
            return new RGaFloat64Vector(metric);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<ulong, double>)
                return metric.CreateZeroVector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<ulong, double>)
                return metric.CreateVector(basisScalarDictionary.First());

            return new RGaFloat64Vector(metric, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IReadOnlyDictionary<int, double> basisScalarDictionary)
        {
            return new RGaFloat64Vector(metric, basisScalarDictionary.CreateVectorDictionary());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, params double[] scalarArray)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarArray);

            return new RGaFloat64Vector(metric, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IEnumerable<double> scalarList)
        {
            var scalarDictionary = CreateValidVectorDictionary(scalarList);

            return new RGaFloat64Vector(metric, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<ulong, double>(
                    1UL << index,
                    1d
                );

            return new RGaFloat64Vector(metric, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, int index, double scalar)
        {
            if (scalar.IsZero())
                return new RGaFloat64Vector(metric);

            var basisScalarDictionary =
                new SingleItemDictionary<ulong, double>(
                    1UL << index,
                    scalar
                );

            return new RGaFloat64Vector(metric, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, KeyValuePair<int, double> indexScalarPair)
        {
            return metric.CreateVector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, KeyValuePair<ulong, double> indexScalarPair)
        {
            return metric.CreateVector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, ulong basisVector)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<ulong, double>(basisVector, 1d);

            return new RGaFloat64Vector(metric, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, ulong basisVector, double scalar)
        {
            if (scalar.IsZero())
                return new RGaFloat64Vector(metric);

            var basisScalarDictionary =
                new SingleItemDictionary<ulong, double>(basisVector, scalar);

            return new RGaFloat64Vector(metric, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, int termsCount, Func<int, double> indexToScalarFunc)
        {
            var composer = metric.CreateComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetVectorTerm(index, scalar);
            }

            return composer.GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateSymmetricVector(this RGaFloat64Processor metric, int count)
        {
            return metric.CreateSymmetricVector(count, 1d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateSymmetricVector(this RGaFloat64Processor metric, int count, double scalarValue)
        {
            return count switch
            {
                < 0 => throw new InvalidOperationException(),

                0 => new RGaFloat64Vector(
                        metric,
                        new EmptyDictionary<ulong, double>()
                    ),

                1 => new RGaFloat64Vector(
                        metric,
                        new SingleItemDictionary<ulong, double>(1UL, scalarValue)
                    ),

                _ => new RGaFloat64Vector(
                        metric,
                        new RGaFloat64RepeatedScalarVectorDictionary(count, scalarValue)
                    )
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateSymmetricUnitVector(this RGaFloat64Processor metric, int count)
        {
            return metric.CreateSymmetricVector(
                count,
                1d / Math.Sqrt(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateUnitRGaVector(this double angle, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var metric = RGaFloat64Processor.Euclidean;

            var scalar1 = Math.Cos(angle);
            var scalar2 = Math.Sin(angle);

            return metric
                .CreateComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateRGaPhasor(this double angle, double magnitude, int index1, int index2)
        {
            Debug.Assert(index2 > index1);

            var metric = RGaFloat64Processor.Euclidean;

            var scalar1 = magnitude * Math.Cos(angle);
            var scalar2 = magnitude * Math.Sin(angle);

            return metric
                .CreateComposer()
                .SetVectorTerm(index1, scalar1)
                .SetVectorTerm(index2, scalar2)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, LinFloat64Vector vector)
        {
            return metric.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IFloat64Tuple2D vector)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IFloat64Tuple3D vector)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector CreateVector(this RGaFloat64Processor metric, IFloat64Tuple4D vector)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .SetTerm(8, vector.W)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this LinFloat64Vector vector)
        {
            return RGaFloat64Processor.Euclidean.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this LinFloat64Vector vector, RGaFloat64Processor metric)
        {
            return metric.CreateVector(
                vector.GetIndexScalarDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple2D vector)
        {
            return RGaFloat64Processor
                .Euclidean
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple2D vector, RGaFloat64Processor metric)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple3D vector)
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
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple3D vector, RGaFloat64Processor metric)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple4D vector)
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
        public static RGaFloat64Vector ToRGaVector(this IFloat64Tuple4D vector, RGaFloat64Processor metric)
        {
            return metric
                .CreateComposer()
                .SetTerm(1, vector.X)
                .SetTerm(2, vector.Y)
                .SetTerm(4, vector.Z)
                .SetTerm(8, vector.W)
                .GetVector();
        }


        public static RGaFloat64Vector DiagonalToRGaVector(this double[,] matrix, RGaFloat64Processor metric)
        {
            var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            var composer = metric.CreateComposer();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                composer.SetVectorTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static RGaFloat64Vector RowToRGaVector(this double[,] matrix, int row, RGaFloat64Processor metric)
        {
            var columnCount = matrix.GetLength(1);
            var composer = metric.CreateComposer();

            for (var i = 0; i < columnCount; i++)
            {
                var scalar = matrix[i, row];

                composer.SetVectorTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static RGaFloat64Vector ColumnToRGaVector(this double[,] matrix, int column, RGaFloat64Processor metric)
        {
            var rowCount = matrix.GetLength(0);
            var composer = metric.CreateComposer();

            for (var i = 0; i < rowCount; i++)
            {
                var scalar = matrix[column, i];

                composer.SetVectorTerm(i, scalar);
            }

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> RowsToRGaVectors(this double[,] matrix, RGaFloat64Processor metric)
        {
            return matrix.GetLength(0).GetRange().Select(
                r => matrix.RowToRGaVector(r, metric)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> ColumnsToRGaVectors(this double[,] matrix, RGaFloat64Processor metric)
        {
            return matrix.GetLength(1).GetRange().Select(
                c => matrix.ColumnToRGaVector(c, metric)
            );
        }


        public static RGaFloat64Vector DiagonalToRGaVector(this Matrix matrix, RGaFloat64Processor metric)
        {
            var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
            var composer = metric.CreateComposer();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                composer.SetVectorTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static RGaFloat64Vector RowToRGaVector(this Matrix matrix, int row, RGaFloat64Processor metric)
        {
            var composer = metric.CreateComposer();

            for (var i = 0; i < matrix.ColumnCount; i++)
                composer.SetVectorTerm(i, matrix[row, i]);

            return composer.GetVector();
        }

        public static RGaFloat64Vector ColumnToRGaVector(this Matrix matrix, int column, RGaFloat64Processor metric)
        {
            var composer = metric.CreateComposer();

            for (var i = 0; i < matrix.RowCount; i++)
                composer.SetVectorTerm(i, matrix[i, column]);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> RowsToRGaVectors(this Matrix matrix, RGaFloat64Processor metric)
        {
            return matrix.RowCount.GetRange().Select(
                r => matrix.RowToRGaVector(r, metric)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<RGaFloat64Vector> ColumnsToRGaVectors(this Matrix matrix, RGaFloat64Processor metric)
        {
            return matrix.ColumnCount.GetRange().Select(
                c => matrix.ColumnToRGaVector(c, metric)
            );
        }
    }
}