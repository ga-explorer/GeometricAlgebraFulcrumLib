using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public sealed class ScalarComposer<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public T Scalar { get; private set; }


        internal ScalarComposer([NotNull] IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.ScalarZero;
        }

        internal ScalarComposer([NotNull] IScalarProcessor<T> scalarProcessor, [NotNull] T scalar)
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
        public GaScalarStorage<T> CreateGaScalarStorage()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaScalarStorage<T>.ZeroScalar
                : GaScalarStorage<T>.Create(Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVectorStorage<T> CreateGaVectorStorage(ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaVectorStorage<T>.ZeroVector
                : GaVectorStorage<T>.Create(index, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorStorage<T> CreateGaBivectorStorage(ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaBivectorStorage<T>.ZeroBivector
                : GaBivectorStorage<T>.Create(index, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorStorage<T> CreateGaBivectorStorage(ulong index1, ulong index2)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaBivectorStorage<T>.ZeroBivector
                : ScalarProcessor.CreateBivectorStorage(index1, index2, Scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaKVectorStorage<T> CreateGaKVectorStorage(uint grade, ulong index)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaKVectorStorage<T>.ZeroKVector(grade)
                : ScalarProcessor.CreateKVectorStorage(grade, index, Scalar);
        }
    }
}