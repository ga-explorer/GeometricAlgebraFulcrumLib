using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public sealed class GaGridEvenComposerDense<T> :
        GaGridEvenComposerBase<T>
    {
        public T[,] ValuesArray { get; private set; }

        public override int Count 
            => Count1 * Count2;

        public int Count1 
            => ValuesArray.GetLength(0);

        public int Count2 
            => ValuesArray.GetLength(1);


        internal GaGridEvenComposerDense([NotNull] IGaScalarProcessor<T> scalarProcessor, int count1, int count2)
            : base(scalarProcessor)
        {
            ValuesArray = scalarProcessor.GetZeroScalarArray2D(count1, count2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> Clear()
        {
            ValuesArray = ScalarProcessor.GetZeroScalarArray2D(
                ValuesArray.GetLength(0),
                ValuesArray.GetLength(1)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> RemoveTerm(GaRecordKeyPair key)
        {
            ValuesArray[key.Key1, key.Key2] = ScalarProcessor.GetZeroScalar();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> Reset(int count1, int count2)
        {
            ValuesArray = ScalarProcessor.GetZeroScalarArray2D(count1, count2);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> SetTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            ValuesArray[key.Key1, key.Key2] = value;

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> AddTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Add(
                ValuesArray[key1, key2], 
                value
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> SubtractTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            var (key1, key2) = key;

            ValuesArray[key1, key2] = ScalarProcessor.Subtract(
                ValuesArray[key1, key2], 
                value
            );

            return this;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            for (var key1 = 0UL; key1 < (ulong) Count1; key1++)
            for (var key2 = 0UL; key2 < (ulong) Count1; key2++)
                ValuesArray[key1, key2] = valueMapping(
                    ValuesArray[key1, key2]
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping)
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
        public override IGaGridEven<T> CreateEvenGrid()
        {
            return CreateDenseEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEvenDense<T> CreateDenseEvenGrid()
        {
            return (Count1 * Count2) switch
            {
                0 => GaGridEvenEmpty<T>.EmptyGrid,
                1 => ValuesArray[0, 0].CreateEvenGridSingleZeroKey(),
                _ => ValuesArray.CreateEvenGridDenseArray()
            };
        }
    }
}