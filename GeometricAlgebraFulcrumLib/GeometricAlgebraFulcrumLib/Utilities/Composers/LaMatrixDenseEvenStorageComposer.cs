using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class LaMatrixDenseEvenStorageComposer<T> :
        LaMatrixEvenStorageComposerBase<T>
    {
        public T[,] ValuesArray { get; private set; }

        public override int Count 
            => Count1 * Count2;

        public int Count1 
            => ValuesArray.GetLength(0);

        public int Count2 
            => ValuesArray.GetLength(1);


        internal LaMatrixDenseEvenStorageComposer([NotNull] IScalarProcessor<T> scalarProcessor, int count1, int count2)
            : base(scalarProcessor)
        {
            ValuesArray = scalarProcessor.GetArrayZero2D(count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> Clear()
        {
            ValuesArray = ScalarProcessor.GetArrayZero2D(
                ValuesArray.GetLength(0),
                ValuesArray.GetLength(1)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> RemoveTerm(IndexPairRecord key)
        {
            ValuesArray[key.Index1, key.Index2] = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixEvenStorageComposerBase<T> Reset(int count1, int count2)
        {
            ValuesArray = ScalarProcessor.GetArrayZero2D(count1, count2);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> SetTerm(IndexPairRecord key, [NotNull] T value)
        {
            ValuesArray[key.Index1, key.Index2] = value;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> AddTerm(IndexPairRecord key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Add(
                ValuesArray[key1, key2], 
                value
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> SubtractTerm(IndexPairRecord key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Subtract(
                ValuesArray[key1, key2], 
                value
            );

            return this;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            for (var key1 = 0UL; key1 < (ulong) Count1; key1++)
            for (var key2 = 0UL; key2 < (ulong) Count1; key2++)
                ValuesArray[key1, key2] = valueMapping(
                    ValuesArray[key1, key2]
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaMatrixEvenStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping)
        {
            for (var key1 = 0UL; key1 < (ulong) Count1; key1++)
            for (var key2 = 0UL; key2 < (ulong) Count1; key2++)
                ValuesArray[key1, key2] = valueMapping(
                    key1, 
                    key2,
                    ValuesArray[key1, key2]
                );

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> CreateEvenGrid()
        {
            return CreateDenseEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixDenseEvenStorage<T> CreateDenseEvenGrid()
        {
            return (Count1 * Count2) switch
            {
                0 => LaMatrixEmptyStorage<T>.EmptyMatrix,
                1 => ValuesArray[0, 0].CreateLaMatrixZeroIndexStorage(),
                _ => ValuesArray.CreateEvenGridDenseArray()
            };
        }
    }
}