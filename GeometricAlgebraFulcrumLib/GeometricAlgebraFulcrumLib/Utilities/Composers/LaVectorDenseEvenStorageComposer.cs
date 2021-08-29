using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class LaVectorDenseEvenStorageComposer<T> :
        LaVectorEvenStorageComposerBase<T>
    {
        public T[] TermsArray { get; private set; }

        public override int Count 
            => TermsArray.Length;


        internal LaVectorDenseEvenStorageComposer(IScalarProcessor<T> scalarProcessor, int count)
            : base(scalarProcessor)
        {
            TermsArray = scalarProcessor.GetArrayZero1D(count);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> Clear()
        {
            TermsArray = ScalarProcessor.GetArrayZero1D(TermsArray.Length);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorDenseEvenStorageComposer<T> Reset(int count)
        {
            TermsArray = ScalarProcessor.GetArrayZero1D(count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> RemoveTerm(ulong index)
        {
            TermsArray[index] = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> RemoveZeroTerms()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = value;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Add(TermsArray[index], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value)
        {
            TermsArray[index] = ScalarProcessor.Subtract(TermsArray[index], value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> MapScalars(Func<T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping(TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LaVectorEvenStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping)
        {
            for (var index = 0; index < Count; index++)
                TermsArray[index] = valueMapping((ulong) index, TermsArray[index]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> CreateLaVectorEvenStorage()
        {
            return CreateLaVectorDenseStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorDenseEvenStorage<T> CreateLaVectorDenseStorage()
        {
            return Count switch
            {
                0 => LaVectorEmptyStorage<T>.ZeroStorage,
                1 => TermsArray[0].CreateLaVectorZeroIndexStorage(),
                _ => TermsArray.LaVectorDenseStorage()
            };
        }
    }
}