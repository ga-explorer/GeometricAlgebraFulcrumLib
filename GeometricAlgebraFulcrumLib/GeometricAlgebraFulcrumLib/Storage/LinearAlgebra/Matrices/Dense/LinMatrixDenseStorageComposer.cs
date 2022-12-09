using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixDenseStorageComposer<T> :
        LinMatrixStorageComposerBase<T>
    {
        public T[,] ValuesArray { get; private set; }

        public override int Count
            => Count1 * Count2;

        public int Count1
            => ValuesArray.GetLength(0);

        public int Count2
            => ValuesArray.GetLength(1);


        internal LinMatrixDenseStorageComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, int count1, int count2)
            : base(scalarProcessor)
        {
            ValuesArray = scalarProcessor.CreateArrayZero2D(count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> Clear()
        {
            ValuesArray = ScalarProcessor.CreateArrayZero2D(
                ValuesArray.GetLength(0),
                ValuesArray.GetLength(1)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> RemoveTerm(IndexPairRecord key)
        {
            ValuesArray[key.Index1, key.Index2] = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixStorageComposerBase<T> Reset(int count1, int count2)
        {
            ValuesArray = ScalarProcessor.CreateArrayZero2D(count1, count2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> SetTerm(IndexPairRecord key, [NotNull] T value)
        {
            ValuesArray[key.Index1, key.Index2] = value;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> AddTerm(IndexPairRecord key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Add(
                ValuesArray[key1, key2],
                value
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> SubtractTerm(IndexPairRecord key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Subtract(
                ValuesArray[key1, key2],
                value
            );

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            for (var key1 = 0UL; key1 < (ulong)Count1; key1++)
                for (var key2 = 0UL; key2 < (ulong)Count1; key2++)
                    ValuesArray[key1, key2] = valueMapping(
                        ValuesArray[key1, key2]
                    );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinMatrixStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping)
        {
            for (var key1 = 0UL; key1 < (ulong)Count1; key1++)
                for (var key2 = 0UL; key2 < (ulong)Count1; key2++)
                    ValuesArray[key1, key2] = valueMapping(
                        key1,
                        key2,
                        ValuesArray[key1, key2]
                    );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> CreateLinMatrixStorage()
        {
            return CreateLinMatrixDenseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage()
        {
            return (Count1 * Count2) switch
            {
                0 => LinMatrixEmptyStorage<T>.EmptyStorage,
                1 => ValuesArray[0, 0].CreateLinMatrixSingleScalarDenseStorage(),
                _ => ValuesArray.CreateLinMatrixDenseStorage()
            };
        }
    }
}