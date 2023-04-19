using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public abstract class LinVectorStorageComposerBase<T> :
        ILinVectorStorageComposer<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }


        protected LinVectorStorageComposerBase(IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract LinVectorStorageComposerBase<T> Clear();

        public abstract LinVectorStorageComposerBase<T> RemoveTerm(ulong index);

        public abstract LinVectorStorageComposerBase<T> RemoveZeroTerms();

        public abstract LinVectorStorageComposerBase<T> SetTerm(ulong index, T value);

        public abstract LinVectorStorageComposerBase<T> AddTerm(ulong index, T value);

        public abstract LinVectorStorageComposerBase<T> SubtractTerm(ulong index, T value);

        public abstract LinVectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping);

        public abstract LinVectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping);



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public abstract IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveBivectorTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveBivectorTerm(RGaKvIndexPairRecord basisVectorIndexPair)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveBivectorTerms(IEnumerable<RGaKvIndexPairRecord> indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
                RemoveBivectorTerm(basisVectorIndex1, basisVectorIndex2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveTerms(params ulong[] keysList)
        {
            foreach (var key in keysList)
                RemoveTerm(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> RemoveTerms(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                RemoveTerm(key);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            SetTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            SetTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetBivectorTerm(RGaKvIndexPairRecord basisVectorIndexPair, T scalar)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            SetTerm(
                index,
                basisVectorIndexPair.IsOrderedAscending()
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetBivectorTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            AddTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            AddTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddBivectorTerm(RGaKvIndexPairRecord basisVectorIndexPair, T scalar)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            AddTerm(
                index,
                basisVectorIndexPair.IsOrderedAscending()
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddBivectorTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            SubtractTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1,
                    basisVectorIndex2
                );

            SubtractTerm(
                index,
                basisVectorIndex1 < basisVectorIndex2
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractBivectorTerm(RGaKvIndexPairRecord basisVectorIndexPair, T scalar)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            SubtractTerm(
                index,
                basisVectorIndexPair.IsOrderedAscending()
                    ? scalar
                    : ScalarProcessor.Negative(scalar)
            );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractBivectorTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<RGaKvIndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorStorageComposerBase<T> Divide(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        public abstract ILinVectorStorage<T> CreateLinVectorStorage();

        public abstract ILinVectorDenseStorage<T> CreateLinVectorDenseStorage();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> CreateLinVectorGradedStorage()
        {
            return CreateLinVectorStorage().ToVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> CreateVectorStorage()
        {
            return CreateLinVectorStorage().CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> CreateBivectorStorage()
        {
            return CreateLinVectorStorage().CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> CreateKVectorStorage(uint grade)
        {
            return CreateLinVectorStorage().CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> CreateMultivectorStorage()
        {
            return CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> CreateMultivectorStorageSparse()
        {
            return CreateLinVectorStorage().CreateMultivectorStorageSparse();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> CreateMultivectorGradedStorage()
        {
            return CreateLinVectorGradedStorage().CreateMultivectorStorageGraded();
        }
    }
}