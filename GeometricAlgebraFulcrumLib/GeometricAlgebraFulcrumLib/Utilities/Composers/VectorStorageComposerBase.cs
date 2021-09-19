using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public abstract class VectorStorageComposerBase<T>:
        IVectorStorageComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected VectorStorageComposerBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract VectorStorageComposerBase<T> Clear();

        public abstract VectorStorageComposerBase<T> RemoveTerm(ulong index);

        public abstract VectorStorageComposerBase<T> RemoveZeroTerms();
        
        public abstract VectorStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value);
        
        public abstract VectorStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value);

        public abstract VectorStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value);

        public abstract VectorStorageComposerBase<T> MapScalars(Func<T, T> valueMapping);

        public abstract VectorStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping);



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        public abstract IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveBivectorTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index =
                BasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveBivectorTerm(IndexPairRecord basisVectorIndexPair)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveBivectorTerms(IEnumerable<IndexPairRecord> indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
                RemoveBivectorTerm(basisVectorIndex1, basisVectorIndex2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveTerms(params ulong[] keysList)
        {
            foreach (var key in keysList)
                RemoveTerm(key);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> RemoveTerms(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                RemoveTerm(key);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> SetBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> SetBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public VectorStorageComposerBase<T> SetBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> AddBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> AddBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public VectorStorageComposerBase<T> AddBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> SubtractBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public VectorStorageComposerBase<T> SubtractBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public VectorStorageComposerBase<T> SubtractBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractTerms(ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractTerms(ILinVectorStorage<T> vectorStorage)
        {
            foreach (var (key, value) in vectorStorage.GetIndexScalarRecords())
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(ILinVectorDenseStorage<T> vectorStorage, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, ILinVectorDenseStorage<T> vectorStorage)
        {
            var key = 0UL;
            foreach (var value in vectorStorage.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorageComposerBase<T> Divide(T scalingFactor)
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
            return CreateMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> CreateMultivectorSparseStorage()
        {
            return CreateLinVectorStorage().CreateMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> CreateMultivectorGradedStorage()
        {
            return CreateLinVectorGradedStorage().CreateMultivectorGradedStorage();
        }
    }
}