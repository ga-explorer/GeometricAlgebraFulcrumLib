using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public sealed class GaListEvenComposerDense<T> :
        GaListEvenComposerBase<T>
    {
        public T[] TermsArray { get; private set; }

        public override int Count 
            => TermsArray.Length;


        internal GaListEvenComposerDense(IGaScalarProcessor<T> scalarProcessor, int count)
            : base(scalarProcessor)
        {
            TermsArray = scalarProcessor.GetZeroScalarArray1D(count);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> Clear()
        {
            TermsArray = ScalarProcessor.GetZeroScalarArray1D(TermsArray.Length);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerDense<T> Reset(int count)
        {
            TermsArray = ScalarProcessor.GetZeroScalarArray1D(count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> RemoveTerm(ulong key)
        {
            TermsArray[key] = ScalarProcessor.GetZeroScalar();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> SetTerm(ulong key, [NotNull] T value)
        {
            TermsArray[key] = value;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> AddTerm(ulong key, [NotNull] T value)
        {
            TermsArray[key] = ScalarProcessor.Add(TermsArray[key], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> SubtractTerm(ulong key, [NotNull] T value)
        {
            TermsArray[key] = ScalarProcessor.Subtract(TermsArray[key], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> MapValues(Func<T, T> valueMapping)
        {
            for (var key = 0; key < Count; key++)
                TermsArray[key] = valueMapping(TermsArray[key]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaListEvenComposerBase<T> MapValues(Func<ulong, T, T> valueMapping)
        {
            for (var key = 0; key < Count; key++)
                TermsArray[key] = valueMapping((ulong) key, TermsArray[key]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> CreateEvenList()
        {
            return CreateDenseEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEvenDense<T> CreateDenseEvenList()
        {
            return Count switch
            {
                0 => GaListEvenEmpty<T>.EmptyList,
                1 => TermsArray[0].CreateEvenListSingleKeyZero(),
                _ => TermsArray.CreateEvenListDenseArray()
            };
        }
    }
}