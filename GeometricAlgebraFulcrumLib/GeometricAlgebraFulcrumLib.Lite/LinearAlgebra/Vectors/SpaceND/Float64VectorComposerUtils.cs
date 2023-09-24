using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND
{
    public static class Float64VectorComposerUtils
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
        public static Float64Vector CreateZeroLinVector()
        {
            return Float64Vector.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
        {
            return new Float64Vector(
                basisScalarDictionary.ToSimpleDictionary()
            );
        }

        public static Float64Vector CreateUnitLinVector(this IReadOnlyDictionary<int, double> basisScalarDictionary)
        {
            var norm = basisScalarDictionary.Values.GetVectorNorm();

            if (norm.IsOne())
                return new Float64Vector(
                    basisScalarDictionary.ToSimpleDictionary()
                );

            var normInv = 1d / norm;

            return new Float64Vector(
                basisScalarDictionary.ToDictionary(
                    p => p.Key,
                    p => p.Value * normInv
                ).ToSimpleDictionary()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(params double[] scalarArray)
        {
            var scalarDictionary = scalarArray.CreateValidLinVectorDictionary();

            return new Float64Vector(scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this IEnumerable<double> scalarList)
        {
            var scalarDictionary = scalarList.CreateValidLinVectorDictionary();

            return new Float64Vector(scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateUnitLinVector(this IReadOnlyList<double> scalarList)
        {
            var normInv = 1d / scalarList.GetVectorNorm();

            var scalarDictionary = scalarList.Select(s => s * normInv)
.CreateValidLinVectorDictionary(
            );

            return new Float64Vector(scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this IEnumerable<Float64VectorTerm> indexScalarList)
        {
            return Float64VectorComposer.Create()
                .AddTerms(indexScalarList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<int, double>(index, 1d);

            return new Float64Vector(basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this int index, double scalar)
        {
            if (scalar.IsZero())
                return new Float64Vector();

            var basisScalarDictionary =
                new SingleItemDictionary<int, double>(index, scalar);

            return new Float64Vector(basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector CreateLinVector(this KeyValuePair<int, double> indexScalarPair)
        {
            return indexScalarPair.Key.CreateLinVector(indexScalarPair.Value);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToVector(this ILinSignedBasisVector term)
        {
            if (term.IsZero)
                return new Float64Vector();

            var basisScalarDictionary =
                new SingleItemDictionary<int, double>(term.Index, term.IsPositive ? 1d : -1d);

            return new Float64Vector(basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToVector(this Float64VectorTerm term)
        {
            if (term.IsZero)
                return new Float64Vector();

            var basisScalarDictionary =
                new SingleItemDictionary<int, double>(term.Index, term.ScalarValue);

            return new Float64Vector(basisScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToLinVector(this XGaFloat64Vector mv)
        {
            var indexScalarDictionary = mv.ToDictionary(
                p => p.Key.FirstIndex,
                p => p.Value
            );

            return indexScalarDictionary.CreateLinVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector ToLinVector(this RGaFloat64Vector mv)
        {
            var indexScalarDictionary = mv.ToDictionary(
                p => p.Key.FirstOneBitPosition(),
                p => p.Value
            );

            return indexScalarDictionary.CreateLinVector();
        }


        public static Float64Vector DiagonalToLinVector(this double[,] matrix)
        {
            var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            var composer = Float64VectorComposer.Create();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static Float64Vector RowToLinVector(this double[,] matrix, int row)
        {
            var columnCount = matrix.GetLength(1);
            var composer = Float64VectorComposer.Create();

            for (var j = 0; j < columnCount; j++)
            {
                var scalar = matrix[row, j];

                composer.SetTerm(j, scalar);
            }

            return composer.GetVector();
        }

        public static Float64Vector ColumnToLinVector(this double[,] matrix, int column)
        {
            var rowCount = matrix.GetLength(0);
            var composer = Float64VectorComposer.Create();

            for (var i = 0; i < rowCount; i++)
            {
                var scalar = matrix[i, column];

                composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector> RowsToLinVectors(this double[,] matrix)
        {
            return matrix.GetLength(0).GetRange().Select(
                matrix.RowToLinVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector> ColumnsToLinVectors(this double[,] matrix)
        {
            return matrix.GetLength(1).GetRange().Select(
                matrix.ColumnToLinVector
            );
        }


        public static Float64Vector DiagonalToLinVector(this Matrix matrix)
        {
            var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
            var composer = Float64VectorComposer.Create();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static Float64Vector RowToLinVector(this Matrix matrix, int row)
        {
            var composer = Float64VectorComposer.Create();

            for (var j = 0; j < matrix.ColumnCount; j++)
                composer.SetTerm(j, matrix[row, j]);

            return composer.GetVector();
        }

        public static Float64Vector ColumnToLinVector(this Matrix matrix, int column)
        {
            var composer = Float64VectorComposer.Create();

            for (var i = 0; i < matrix.RowCount; i++)
                composer.SetTerm(i, matrix[i, column]);

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector> RowsToLinVectors(this Matrix matrix)
        {
            return matrix.RowCount.GetRange().Select(
                matrix.RowToLinVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Float64Vector> ColumnsToLinVectors(this Matrix matrix)
        {
            return matrix.ColumnCount.GetRange().Select(
                matrix.ColumnToLinVector
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorComposer CreateLinVectorComposer()
        {
            return Float64VectorComposer.Create();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorComposer ToComposer(this Float64Vector mv)
        {
            return Float64VectorComposer.Create().SetVector(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorComposer NegativeToComposer(this Float64Vector mv)
        {
            return Float64VectorComposer.Create().SetVectorNegative(mv);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64VectorComposer ToComposer(this Float64Vector mv, double scalingFactor)
        {
            return Float64VectorComposer.Create().SetVector(mv, scalingFactor);
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

    }
}