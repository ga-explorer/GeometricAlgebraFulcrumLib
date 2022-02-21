using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class ScalarComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public T Scalar { get; private set; }


        internal ScalarComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.ScalarZero;
        }

        internal ScalarComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, [NotNull] T scalar)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalar;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> Clear()
        {
            Scalar = ScalarProcessor.ScalarZero;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> SetScalar([NotNull] T scalar)
        {
            Scalar = scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> Negative()
        {
            Scalar = ScalarProcessor.Negative(Scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Add(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> SubtractScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> AddScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> SubtractScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> SubtractScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ScalarComposer<T> RemoveNearZeroTerms()
        {
            if (ScalarProcessor.IsNearZero(Scalar))
                Scalar = ScalarProcessor.ScalarZero;

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> CreateVectorStorage(ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? VectorStorage<T>.ZeroVector
                : VectorStorage<T>.CreateVector(index, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> CreateBivectorStorage(ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? BivectorStorage<T>.ZeroBivector
                : BivectorStorage<T>.Create(index, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> CreateBivectorStorage(ulong index1, ulong index2)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? BivectorStorage<T>.ZeroBivector
                : ScalarProcessor.CreateBivectorTermStorage(index1, index2, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> CreateKVectorStorage(uint grade, ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? KVectorStorage<T>.CreateKVectorZero(grade)
                : ScalarProcessor.CreateKVectorStorageTerm(grade, index, Scalar);
        }
    }
}