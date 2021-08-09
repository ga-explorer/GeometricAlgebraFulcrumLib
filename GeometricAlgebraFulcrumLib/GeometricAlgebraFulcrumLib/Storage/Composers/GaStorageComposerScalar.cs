using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaStorageComposerScalar<T>
        : IGaStorageComposerKVector<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public T Scalar { get; private set; }


        internal GaStorageComposerScalar([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.ZeroScalar;
        }

        internal GaStorageComposerScalar([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] T scalar)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalar;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> Clear()
        {
            Scalar = ScalarProcessor.ZeroScalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> SetScalar([NotNull] T scalar)
        {
            Scalar = scalar;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> SetScalarToNegative()
        {
            Scalar = ScalarProcessor.Negative(Scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> AddScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Add(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> SubtractScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, scalar);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> AddScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> AddScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> SubtractScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageComposerScalar<T> SubtractScalars(params T[] scalarsArray)
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
                Scalar = ScalarProcessor.ZeroScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveNearZeroTerms()
        {
            if (ScalarProcessor.IsNearZero(Scalar))
                Scalar = ScalarProcessor.ZeroScalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageScalar<T> GetScalar()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> GetVector()
        {
            return GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> GetVector(bool copyFlag)
        {
            return GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> GetBivector()
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> GetBivector(bool copyFlag)
        {
            return GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector(uint grade)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        public IGaStorageKVector<T> GetKVector(uint grade, bool copyFlag)
        {
            if (grade != 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> GetKVector(bool copyFlag)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> GetMultivector()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> GetMultivector(bool copyFlag)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivectorGraded<T> GetGradedMultivector()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivectorGraded<T> GetGradedMultivector(bool copyFlag)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageScalar<T>.ZeroScalar
                : GaStorageScalar<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetSparseMultivector()
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageMultivectorSparse<T>.ZeroMultivector
                : GaStorageMultivectorSparse<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetSparseMultivector(bool copyFlag)
        {
            return ScalarProcessor.IsZero(Scalar)
                ? GaStorageMultivectorSparse<T>.ZeroMultivector
                : GaStorageMultivectorSparse<T>.Create(Scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(bool copyFlag)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> GetTreeMultivector(int treeDepth, bool copyFlag)
        {
            throw new NotImplementedException();
        }

    }
}