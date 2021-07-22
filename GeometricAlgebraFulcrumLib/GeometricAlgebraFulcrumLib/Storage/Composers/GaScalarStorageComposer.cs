using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.Composers
{
    public sealed class GaScalarStorageComposer<T>
        : IGaScalarStorageComposer<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public T Scalar { get; private set; }


        public GaScalarStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalarProcessor.ZeroScalar;
        }

        public GaScalarStorageComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, [NotNull] T scalar)
        {
            ScalarProcessor = scalarProcessor;

            Scalar = scalar;
        }


        public bool IsEmpty()
        {
            return false;
        }

        public GaScalarStorageComposer<T> Clear()
        {
            Scalar = ScalarProcessor.ZeroScalar;

            return this;
        }


        public GaScalarStorageComposer<T> SetScalar([NotNull] T scalar)
        {
            Scalar = scalar;

            return this;
        }

        public GaScalarStorageComposer<T> SetScalarToNegative()
        {
            Scalar = ScalarProcessor.Negative(Scalar);

            return this;
        }

        public GaScalarStorageComposer<T> AddScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Add(Scalar, scalar);

            return this;
        }

        public GaScalarStorageComposer<T> SubtractScalar([NotNull] T scalar)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, scalar);

            return this;
        }

        public GaScalarStorageComposer<T> AddScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<T> AddScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Add(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<T> SubtractScalars(IEnumerable<T> scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }

        public GaScalarStorageComposer<T> SubtractScalars(params T[] scalarsArray)
        {
            Scalar = ScalarProcessor.Subtract(Scalar, ScalarProcessor.Add(scalarsArray));

            return this;
        }


        public IGasMultivector<T> GetCompactMultivector()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }

        public IGasGradedMultivector<T> GetCompactGradedMultivector()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }


        public IGasMultivector<T> GetMultivectorCopy()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }

        public IGasMultivector<T> GetMultivectorCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateScalar(scalarMapping(Scalar));
        }

        public IGasGradedMultivector<T> GetGradedMultivectorCopy()
        {
            var gradeIndexScalarDictionary = new Dictionary<uint, Dictionary<ulong, T>>()
            {
                {0, new Dictionary<ulong, T>() {{0, Scalar}}}
            };

            return new GasGradedMultivector<T>(
                ScalarProcessor, 
                gradeIndexScalarDictionary,
                0UL
            );
        }

        public IGasTermsMultivector<T> GetTermsMultivectorCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, T>() {{0, Scalar}};

            return new GasTermsMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                0UL
            );
        }

        public GasTreeMultivector<T> GetTreeMultivectorCopy()
        {
            var idScalarDictionary = new Dictionary<ulong, T>() {{0, Scalar}};

            return new GasTreeMultivector<T>(
                ScalarProcessor, 
                idScalarDictionary,
                0UL
            );
        }

        public IGasKVector<T> GetKVectorStorageCopy()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }

        public IGasKVector<T> GetKVectorStorageCopy(Func<T, T> scalarMapping)
        {
            return ScalarProcessor.CreateScalar(
                scalarMapping(Scalar)
            );
        }

        public IGasScalar<T> GetScalarStorage()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }

        public IGasKVector<T> GetKVectorStorage()
        {
            return ScalarProcessor.CreateScalar(Scalar);
        }
    }
}