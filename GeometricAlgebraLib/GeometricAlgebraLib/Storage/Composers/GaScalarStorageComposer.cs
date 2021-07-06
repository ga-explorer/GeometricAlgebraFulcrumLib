using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Storage.Composers
{
    public sealed class GaScalarStorageComposer<TScalar>
        : IGaScalarStorageComposer<TScalar>
    {
        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public TScalar Scalar { get; private set; }


        public GaScalarStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.ZeroScalar;
        }

        public GaScalarStorageComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, [NotNull] TScalar scalar)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalar;
        }


        public bool IsEmpty()
        {
            return false;
        }

        public GaScalarStorageComposer<TScalar> Clear()
        {
            Scalar = ScalarProcessor.ZeroScalar;

            return this;
        }


        public GaScalarStorageComposer<TScalar> SetScalar([NotNull] TScalar scalar)
        {
            Scalar = scalar;

            return this;
        }

        public GaScalarStorageComposer<TScalar> SetScalarToNegative()
        {
            Scalar = ScalarProcessor.Negative(Scalar);

            return this;
        }

        public GaScalarStorageComposer<TScalar> AddScalar([NotNull] TScalar scalar)
        {
            Scalar = ScalarProcessor.Add(Scalar, scalar);

            return this;
        }

        public GaScalarStorageComposer<TScalar> SubtractScalar([NotNull] TScalar scalar)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, scalar);

            return this;
        }

        public GaScalarStorageComposer<TScalar> AddScalars(IEnumerable<TScalar> scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<TScalar> AddScalars(params TScalar[] scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<TScalar> SubtractScalars(IEnumerable<TScalar> scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<TScalar> SubtractScalars(params TScalar[] scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }


        public IGaMultivectorStorage<TScalar> GetCompactStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                Scalar
            );
        }

        public IGaMultivectorGradedStorage<TScalar> GetCompactGradedStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                Scalar
            );
        }


        public IGaMultivectorStorage<TScalar> GetStorageCopy()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                Scalar
            );
        }

        public IGaMultivectorStorage<TScalar> GetStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                scalarMapping(Scalar)
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetMultivectorGradedStorageCopy()
        {
            return GaMultivectorGradedStorage<TScalar>.CreateScalar(
                ScalarProcessor, 
                Scalar
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetMultivectorTermsStorageCopy()
        {
            return GaMultivectorTermsStorage<TScalar>.CreateScalar(
                ScalarProcessor, 
                Scalar
            );
        }

        public GaMultivectorTreeStorage<TScalar> GetMultivectorTreeStorageCopy()
        {
            return GaMultivectorTreeStorage<TScalar>.CreateScalar(
                ScalarProcessor, 
                Scalar
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor, 
                Scalar
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorageCopy(Func<TScalar, TScalar> scalarMapping)
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor, 
                scalarMapping(Scalar)
            );
        }

        public IGaScalarStorage<TScalar> GetScalarStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor, 
                Scalar
            );
        }

        public IGaKVectorStorage<TScalar> GetKVectorStorage()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor, 
                Scalar
            );
        }
    }
}