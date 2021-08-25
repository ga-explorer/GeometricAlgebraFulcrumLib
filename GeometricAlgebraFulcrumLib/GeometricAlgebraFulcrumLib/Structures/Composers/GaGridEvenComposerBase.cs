using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public abstract class GaGridEvenComposerBase<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected GaGridEvenComposerBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public abstract GaGridEvenComposerBase<T> Clear();

        public abstract GaGridEvenComposerBase<T> RemoveTerm(GaRecordKeyPair key);

        public abstract GaGridEvenComposerBase<T> SetTerm(GaRecordKeyPair key, [NotNull] T value);
        
        public abstract GaGridEvenComposerBase<T> AddTerm(GaRecordKeyPair key, [NotNull] T value);

        public abstract GaGridEvenComposerBase<T> SubtractTerm(GaRecordKeyPair key, [NotNull] T value);

        public abstract GaGridEvenComposerBase<T> MapValues(Func<T, T> valueMapping);

        public abstract GaGridEvenComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping);

        public abstract IGaGridEven<T> CreateEvenGrid();

        public abstract IGaGridEvenDense<T> CreateDenseEvenGrid();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> RemoveTerm(ulong key1, ulong key2)
        {
            return RemoveTerm(new GaRecordKeyPair(key1, key2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> RemoveTerms(params GaRecordKeyPair[] keysList)
        {
            foreach (var key in keysList)
                RemoveTerm(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> RemoveTerms(IEnumerable<GaRecordKeyPair> keysList)
        {
            foreach (var key in keysList.ToArray())
                RemoveTerm(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract GaGridEvenComposerBase<T> RemoveZeroTerms();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SetTerm(ulong key1, ulong key2, [NotNull] T value)
        {
            return SetTerm(new GaRecordKeyPair(key1, key2), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SetTerms(IGaGridEvenDense<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var key1 = 0UL; key1 < count1; key1++)
            for (var key2 = 0UL; key2 < count2; key2++)
                SetTerm(key1, key2, evenGrid.GetValue(key1, key2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SetTerms(IGaGridEven<T> evenGrid)
        {
            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                SetTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SetTerms(IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                SetTerm(new GaRecordKeyPair(key1, key2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> AddTerm(ulong key1, ulong key2, [NotNull] T value)
        {
            return AddTerm(new GaRecordKeyPair(key1, key2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> AddTerms(IGaGridEvenDense<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var key1 = 0UL; key1 < count1; key1++)
            for (var key2 = 0UL; key2 < count2; key2++)
                AddTerm(key1, key2, evenGrid.GetValue(key1, key2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> AddTerms(IGaGridEven<T> evenGrid)
        {
            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                AddTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> AddTerms(IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                AddTerm(new GaRecordKeyPair(key1, key2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SubtractTerm(ulong key1, ulong key2, [NotNull] T value)
        {
            return SubtractTerm(new GaRecordKeyPair(key1, key2), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SubtractTerms(IGaGridEvenDense<T> evenGrid)
        {
            var count1 = (ulong) evenGrid.Count1;
            var count2 = (ulong) evenGrid.Count2;

            for (var key1 = 0UL; key1 < count1; key1++)
            for (var key2 = 0UL; key2 < count2; key2++)
                SubtractTerm(key1, key2, evenGrid.GetValue(key1, key2));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SubtractTerms(IGaGridEven<T> evenGrid)
        {
            foreach (var (key1, key2, value) in evenGrid.GetKeyValueRecords())
                SubtractTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> SubtractTerms(IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                SubtractTerm(new GaRecordKeyPair(key1, key2), value);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridEvenComposerBase<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }
    }
}