using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public sealed class GaGridEvenComposerSparse<T> :
        GaGridEvenComposerBase<T>
    {
        public Dictionary<GaRecordKeyPair, T> KeyValueDictionary { get; }
            = new Dictionary<GaRecordKeyPair, T>();

        public override int Count 
            => KeyValueDictionary.Count;
        

        internal GaGridEvenComposerSparse([NotNull] IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> Clear()
        {
            KeyValueDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> RemoveTerm(GaRecordKeyPair key)
        {
            KeyValueDictionary.Remove(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> RemoveZeroTerms()
        {
            var keysGrid = KeyValueDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(keysGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> SetTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            if (KeyValueDictionary.ContainsKey(key))
                KeyValueDictionary[key] = value;
            else
                KeyValueDictionary.Add(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> AddTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(key, out var value1))
                KeyValueDictionary[key] = ScalarProcessor.Add(value1, value1);
            else
                KeyValueDictionary.Add(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> SubtractTerm(GaRecordKeyPair key, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(key, out var value1))
                KeyValueDictionary[key] = ScalarProcessor.Subtract(value1, value1);
            else
                KeyValueDictionary.Add(key, ScalarProcessor.Negative(value));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var (key, value) in KeyValueDictionary)
                KeyValueDictionary[key] = valueMapping(value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaGridEvenComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping)
        {
            foreach (var (key, value) in KeyValueDictionary)
                KeyValueDictionary[key] = valueMapping(key.Key1, key.Key2, value);

            return this;
        }

        public override IGaGridEven<T> CreateEvenGrid()
        {
            if (KeyValueDictionary.Count == 0)
                return GaGridEvenEmpty<T>.EmptyGrid;

            if (KeyValueDictionary.Count == 1)
            {
                var ((key1, key2), value) = KeyValueDictionary.First();

                return value.CreateEvenGridSingleKey(key1, key2);
            }

            var denseCount1 =
                1UL + KeyValueDictionary.Keys.Select(key => key.Key1).Max();

            var denseCount2 =
                1UL + KeyValueDictionary.Keys.Select(key => key.Key2).Max();

            var denseCount = denseCount1 * denseCount2;

            if ((denseCount / 3) < (ulong) KeyValueDictionary.Count || denseCount1 > int.MaxValue || denseCount2 > int.MaxValue)
                return KeyValueDictionary.CreateEvenGrid();

            var array = ScalarProcessor.GetZeroScalarArray2D((int) denseCount1, (int) denseCount2);

            foreach (var ((key1, key2), scalar) in KeyValueDictionary)
                array[key1, key2] = scalar;

            return array.CreateEvenGridDenseArray();
        }
        
        public override IGaGridEvenDense<T> CreateDenseEvenGrid()
        {
            if (KeyValueDictionary.Count == 0)
                return GaGridEvenEmpty<T>.EmptyGrid;
            
            var denseCount1 =
                1UL + KeyValueDictionary.Keys.Select(key => key.Key1).Max();

            var denseCount2 =
                1UL + KeyValueDictionary.Keys.Select(key => key.Key2).Max();

            if (denseCount1 > int.MaxValue || denseCount2 > int.MaxValue)
                throw new InvalidOperationException();

            var array = ScalarProcessor.GetZeroScalarArray2D((int) denseCount1, (int) denseCount2);

            foreach (var ((key1, key2), scalar) in KeyValueDictionary)
                array[key1, key2] = scalar;

            return array.CreateEvenGridDenseArray();
        }
    }
}