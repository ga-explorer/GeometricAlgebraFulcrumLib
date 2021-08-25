using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaScalarComposer<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public T Scalar { get; private set; }


        internal GaScalarComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.GetZeroScalar();
        }

        internal GaScalarComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] T scalar)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalar;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> Clear()
        {
            Scalar = ScalarProcessor.GetZeroScalar();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> SetScalar([NotNull] T scalar)
        {
            Scalar = scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> Negative()
        {
            Scalar = ScalarProcessor.Negative(Scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> AddScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Add(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> SubtractScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> AddScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> AddScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> SubtractScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaScalarComposer<T> SubtractScalars(params T[] scalarsArray)
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
        public void RemoveZeroTerms()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveZeroTerms(bool nearZeroFlag)
        {
            if (nearZeroFlag && ScalarProcessor.IsNearZero(Scalar))
                Scalar = ScalarProcessor.GetZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNearZeroTerms()
        {
            if (ScalarProcessor.IsNearZero(Scalar))
                Scalar = ScalarProcessor.GetZeroScalar();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageScalar<T> GetScalar()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }
    }
}