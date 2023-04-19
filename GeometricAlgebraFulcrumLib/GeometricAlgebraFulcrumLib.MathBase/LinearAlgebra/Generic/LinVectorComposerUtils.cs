using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic
{
    public static class LinVectorComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<int, T> CreateLinVectorDictionary<T>(this IReadOnlyDictionary<int, T> inputDictionary)
        {
            var basisScalarDictionary = new Dictionary<int, T>();

            foreach (var (key, value) in inputDictionary)
                basisScalarDictionary.Add(key, value);

            return basisScalarDictionary;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Dictionary<int, T> CreateValidLinVectorDictionary<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            var basisScalarDictionary = new Dictionary<int, T>();

            var index = 0;
            foreach (var scalar in scalarList)
            {
                if (!scalarProcessor.IsValid(scalar))
                    throw new InvalidOperationException();

                basisScalarDictionary.Add(index, scalar);

                index++;
            }

            return basisScalarDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateZeroLinVector<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LinVector<T>(scalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> basisScalarDictionary)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<int, T>)
                return scalarProcessor.CreateZeroLinVector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<int, T>)
                return scalarProcessor.CreateLinVector(basisScalarDictionary.First());

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IReadOnlyDictionary<int, T> basisScalarDictionary, IScalarProcessor<T> scalarProcessor)
        {
            if (basisScalarDictionary.Count == 0 && basisScalarDictionary is not EmptyDictionary<int, T>)
                return scalarProcessor.CreateZeroLinVector();

            if (basisScalarDictionary.Count == 1 && basisScalarDictionary is not SingleItemDictionary<int, T>)
                return scalarProcessor.CreateLinVector(basisScalarDictionary.First());

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarArray)
        {
            var scalarDictionary = CreateValidLinVectorDictionary(scalarProcessor, scalarArray);

            return new LinVector<T>(scalarProcessor, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
        {
            var scalarDictionary = CreateValidLinVectorDictionary(scalarProcessor, scalarList);

            return new LinVector<T>(scalarProcessor, scalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<LinVectorTerm<T>> indexScalarList)
        {
            return scalarProcessor
                .CreateLinVectorComposer()
                .AddTerms(indexScalarList)
                .GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int index)
        {
            var basisScalarDictionary =
                new SingleItemDictionary<int, T>(index, scalarProcessor.ScalarOne);

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int index, T scalar)
        {
            if (scalarProcessor.IsZero(scalar))
                return new LinVector<T>(scalarProcessor);

            var basisScalarDictionary =
                new SingleItemDictionary<int, T>(index, scalar);

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, KeyValuePair<int, T> indexScalarPair)
        {
            return scalarProcessor.CreateLinVector(indexScalarPair.Key, indexScalarPair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this int index, Scalar<T> scalar)
        {
            var scalarProcessor = scalar.ScalarProcessor;
            if (scalar.IsZero())
                return new LinVector<T>(scalarProcessor);

            var basisScalarDictionary =
                new SingleItemDictionary<int, T>(index, scalar.ScalarValue);

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, double> indexToScalarFunc)
        {
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetTerm(index, scalarProcessor.GetScalarFromNumber(scalar));
            }

            return composer.GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, string> indexToScalarFunc)
        {
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetTerm(index, scalarProcessor.GetScalarFromText(scalar));
            }

            return composer.GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> CreateLinVector<T>(this IScalarProcessor<T> scalarProcessor, int termsCount, Func<int, T> indexToScalarFunc)
        {
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var index = 0; index < termsCount; index++)
            {
                var scalar = indexToScalarFunc(index);

                composer.SetTerm(index, scalar);
            }

            return composer.GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ToVector<T>(this LinVectorTerm<T> term)
        {
            var scalarProcessor = term.ScalarProcessor;

            if (term.IsZero)
                return new LinVector<T>(scalarProcessor);

            var basisScalarDictionary =
                new SingleItemDictionary<int, T>(term.Index, term.ScalarValue);

            return new LinVector<T>(scalarProcessor, basisScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ToLinVector<T>(this XGaVector<T> mv)
        {
            var indexScalarDictionary = mv.IdScalarPairs.ToDictionary(
                p => p.Key.FirstIndex,
                p => p.Value
            );

            return mv.ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ToLinVector<T>(this RGaVector<T> mv)
        {
            var indexScalarDictionary = mv.IdScalarPairs.ToDictionary(
                p => p.Key.FirstOneBitPosition(),
                p => p.Value
            );

            return mv.ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }

        
        public static LinVector<T> DiagonalToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
        {
            var count = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                if (scalar is not null)
                    composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }

        public static LinVector<T> RowToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor, int row)
        {
            var columnCount = matrix.GetLength(1);
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < columnCount; i++)
            {
                var scalar = matrix[i, row];

                if (scalar is not null)
                    composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }
        
        public static LinVector<T> ColumnToLinVector<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor, int column)
        {
            var rowCount = matrix.GetLength(0);
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < rowCount; i++)
            {
                var scalar = matrix[column, i];

                if (scalar is not null)
                    composer.SetTerm(i, scalar);
            }

            return composer.GetVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinVector<T>> RowsToLinVectors<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
        {
            return matrix.GetLength(0).GetRange().Select(
                r => matrix.RowToLinVector(scalarProcessor, r)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinVector<T>> ColumnsToLinVectors<T>(this T[,] matrix, IScalarProcessor<T> scalarProcessor)
        {
            return matrix.GetLength(1).GetRange().Select(
                c => matrix.ColumnToLinVector(scalarProcessor, c)
            );
        }

        
        public static LinVector<T> DiagonalToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
        {
            var count = Math.Min(matrix.RowCount, matrix.ColumnCount);
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < count; i++)
            {
                var scalar = matrix[i, i];

                composer.SetTerm(i, scalarProcessor.GetScalarFromNumber(scalar));
            }

            return composer.GetVector();
        }

        public static LinVector<T> RowToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor, int row)
        {
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < matrix.ColumnCount; i++)
                composer.SetTerm(
                    i, 
                    scalarProcessor.GetScalarFromNumber(matrix[row, i])
                );

            return composer.GetVector();
        }
        
        public static LinVector<T> ColumnToLinVector<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor, int column)
        {
            var composer = scalarProcessor.CreateLinVectorComposer();

            for (var i = 0; i < matrix.RowCount; i++)
                composer.SetTerm(
                    i, 
                    scalarProcessor.GetScalarFromNumber(matrix[i, column])
                );

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinVector<T>> RowsToLinVectors<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
        {
            return matrix.RowCount.GetRange().Select(
                r => matrix.RowToLinVector(scalarProcessor, r)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<LinVector<T>> ColumnsToLinVectors<T>(this Matrix matrix, IScalarProcessor<T> scalarProcessor)
        {
            return matrix.ColumnCount.GetRange().Select(
                c => matrix.ColumnToLinVector(scalarProcessor, c)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorComposer<T> CreateLinVectorComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LinVectorComposer<T>(scalarProcessor);
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorComposer<T> ToComposer<T>(this LinVector<T> mv)
        {
            return new LinVectorComposer<T>(mv.ScalarProcessor).SetVector(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorComposer<T> NegativeToComposer<T>(this LinVector<T> mv)
        {
            return new LinVectorComposer<T>(mv.ScalarProcessor).SetVectorNegative(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVectorComposer<T> ToComposer<T>(this LinVector<T> mv, T scalingFactor)
        {
            return new LinVectorComposer<T>(mv.ScalarProcessor).SetVector(mv, scalingFactor);
        }

    
        public static ScalarComposer<T> AddESpTerms<T>(this ScalarComposer<T> composer, LinVector<T> mv1, LinVector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return composer;

            var scalarProcessor = composer.ScalarProcessor;

            if (mv1.Count <= mv2.Count)
            {
                foreach (var (id, scalar1) in mv1.IndexScalarPairs)
                {
                    if (!mv2.TryGetTermScalar(id, out var scalar2))
                        continue;

                    composer.AddScalarValue(scalarProcessor.Times(scalar1, scalar2));
                }
            }
            else
            {
                foreach (var (id, scalar2) in mv2.IndexScalarPairs)
                {
                    if (!mv1.TryGetTermScalar(id, out var scalar1))
                        continue;

                    composer.AddScalarValue(scalarProcessor.Times(scalar1, scalar2));
                }
            }

            return composer;
        }

    }
}