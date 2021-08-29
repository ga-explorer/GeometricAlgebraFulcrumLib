using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public abstract class LaVectorEvenStorageComposerBase<T>:
        ILaVectorStorageComposer<T>
    {
        public IScalarProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected LaVectorEvenStorageComposerBase([NotNull] IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract LaVectorEvenStorageComposerBase<T> Clear();

        public abstract LaVectorEvenStorageComposerBase<T> RemoveTerm(ulong index);

        public abstract LaVectorEvenStorageComposerBase<T> RemoveZeroTerms();
        
        public abstract LaVectorEvenStorageComposerBase<T> SetTerm(ulong index, [NotNull] T value);
        
        public abstract LaVectorEvenStorageComposerBase<T> AddTerm(ulong index, [NotNull] T value);

        public abstract LaVectorEvenStorageComposerBase<T> SubtractTerm(ulong index, [NotNull] T value);

        public abstract LaVectorEvenStorageComposerBase<T> MapScalars(Func<T, T> valueMapping);

        public abstract LaVectorEvenStorageComposerBase<T> MapScalars(Func<ulong, T, T> valueMapping);

        public abstract ILaVectorEvenStorage<T> CreateLaVectorEvenStorage();

        public abstract ILaVectorDenseEvenStorage<T> CreateLaVectorDenseStorage();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveBivectorTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveBivectorTerm(IndexPairRecord basisVectorIndexPair)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveBivectorTerms(IEnumerable<IndexPairRecord> indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
                RemoveBivectorTerm(basisVectorIndex1, basisVectorIndex2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveTerms(params ulong[] keysList)
        {
            foreach (var key in keysList)
                RemoveTerm(key);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> RemoveTerms(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                RemoveTerm(key);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> SetBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> SetBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public LaVectorEvenStorageComposerBase<T> SetBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetTerms(ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetTerms(ILaVectorEvenStorage<T> evenList)
        {
            foreach (var (key, value) in evenList.GetIndexScalarRecords())
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(ILaVectorDenseEvenStorage<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(T scalingFactor, ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> AddBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> AddBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public LaVectorEvenStorageComposerBase<T> AddBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddTerms(ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddTerms(ILaVectorEvenStorage<T> evenList)
        {
            foreach (var (key, value) in evenList.GetIndexScalarRecords())
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(ILaVectorDenseEvenStorage<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(T scalingFactor, ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> SubtractBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
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
        public LaVectorEvenStorageComposerBase<T> SubtractBivectorTerm(IndexPairRecord basisVectorIndexPair, T scalar)
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
        public LaVectorEvenStorageComposerBase<T> SubtractBivectorTerms(IEnumerable<IndexPairScalarRecord<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractTerms(ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractTerms(ILaVectorEvenStorage<T> evenList)
        {
            foreach (var (key, value) in evenList.GetIndexScalarRecords())
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(ILaVectorDenseEvenStorage<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, ILaVectorDenseEvenStorage<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetScalars())
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(IEnumerable<IndexScalarRecord<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorEvenStorageComposerBase<T> Divide(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorGradedStorage<T> CreateLaVectorGradedStorage()
        {
            return CreateLaVectorEvenStorage().ToGradedList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVectorStorage<T> CreateGaVectorStorage()
        {
            return CreateLaVectorEvenStorage().CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorStorage<T> CreateGaBivectorStorage()
        {
            return CreateLaVectorEvenStorage().CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaKVectorStorage<T> CreateGaKVectorStorage(uint grade)
        {
            return CreateLaVectorEvenStorage().CreateKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> CreateGaMultivectorStorage()
        {
            return CreateGaMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorSparseStorage<T> CreateGaMultivectorSparseStorage()
        {
            return CreateLaVectorEvenStorage().CreateStorageSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorGradedStorage<T> CreateGaMultivectorGradedStorage()
        {
            return CreateLaVectorGradedStorage().CreateStorageGradedMultivector();
        }
    }
}