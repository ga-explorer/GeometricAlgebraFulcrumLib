using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public class GaListEvenComposerSparse<T> :
        GaListEvenComposerBase<T>
    {
        public Dictionary<ulong, T> KeyValueDictionary { get; }
            = new Dictionary<ulong, T>();

        public override int Count 
            => KeyValueDictionary.Count;


        internal GaListEvenComposerSparse([NotNull] IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> Clear()
        {
            KeyValueDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> RemoveTerm(ulong key)
        {
            KeyValueDictionary.Remove(key);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> RemoveZeroTerms()
        {
            var keysList = KeyValueDictionary
                .Where(pair => ScalarProcessor.IsZero(pair.Value))
                .Select(pair => pair.Key)
                .ToArray();

            return RemoveTerms(keysList);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> SetTerm(ulong key, [NotNull] T value)
        {
            if (KeyValueDictionary.ContainsKey(key))
                KeyValueDictionary[key] = value;
            else
                KeyValueDictionary.Add(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> AddTerm(ulong key, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(key, out var value1))
                KeyValueDictionary[key] = ScalarProcessor.Add(value1, value);
            else
                KeyValueDictionary.Add(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> SubtractTerm(ulong key, [NotNull] T value)
        {
            if (KeyValueDictionary.TryGetValue(key, out var value1))
                KeyValueDictionary[key] = ScalarProcessor.Subtract(value1, value);
            else
                KeyValueDictionary.Add(key, ScalarProcessor.Negative(value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var (key, value) in KeyValueDictionary)
                KeyValueDictionary[key] = valueMapping(value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> MapValues(Func<ulong, T, T> valueMapping)
        {
            foreach (var (key, value) in KeyValueDictionary)
                KeyValueDictionary[key] = valueMapping(key, value);

            return this;
        }
        
        public override IGaListEven<T> CreateEvenList()
        {
            if (KeyValueDictionary.Count == 0)
                return GaListEvenEmpty<T>.EmptyList;

            if (KeyValueDictionary.Count == 1)
            {
                var (key, value) = KeyValueDictionary.First();

                return value.CreateEvenListSingleKey(key);
            }

            var denseCount = 
                1UL + KeyValueDictionary.Keys.Max();

            if ((denseCount / 2) < (ulong) KeyValueDictionary.Count || denseCount > int.MaxValue)
                return KeyValueDictionary.CreateEvenList();

            var array = ScalarProcessor.GetZeroScalarArray1D((int) denseCount);

            foreach (var (key, scalar) in KeyValueDictionary)
                array[key] = scalar;

            return array.CreateEvenListDenseArray();
        }

        public override IGaListEvenDense<T> CreateDenseEvenList()
        {
            if (KeyValueDictionary.Count == 0)
                return GaListEvenEmpty<T>.EmptyList;

            var denseCount = 
                1UL + KeyValueDictionary.Keys.Max();

            if (denseCount > int.MaxValue)
                throw new InvalidOperationException();

            var array = ScalarProcessor.GetZeroScalarArray1D((int) denseCount);

            foreach (var (key, scalar) in KeyValueDictionary)
                array[key] = scalar;

            return array.CreateEvenListDenseArray();
        }
    }
}
