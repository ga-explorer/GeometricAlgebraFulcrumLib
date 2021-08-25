using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public abstract class GaListEvenComposerBase<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public abstract int Count { get; }
        

        protected GaListEvenComposerBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public abstract GaListEvenComposerBase<T> Clear();

        public abstract GaListEvenComposerBase<T> RemoveTerm(ulong key);

        public abstract GaListEvenComposerBase<T> RemoveZeroTerms();
        
        public abstract GaListEvenComposerBase<T> SetTerm(ulong key, [NotNull] T value);
        
        public abstract GaListEvenComposerBase<T> AddTerm(ulong key, [NotNull] T value);

        public abstract GaListEvenComposerBase<T> SubtractTerm(ulong key, [NotNull] T value);

        public abstract GaListEvenComposerBase<T> MapValues(Func<T, T> valueMapping);

        public abstract GaListEvenComposerBase<T> MapValues(Func<ulong, T, T> valueMapping);

        public abstract IGaListEven<T> CreateEvenList();

        public abstract IGaListEvenDense<T> CreateDenseEvenList();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count == 0;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveBivectorTerm(int basisVectorIndex1, int basisVectorIndex2)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2)
        {
            var index =
                GaBasisBivectorUtils.BasisVectorIndicesToBivectorIndex(
                    basisVectorIndex1, 
                    basisVectorIndex2
                );

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveBivectorTerm(GaRecordKeyPair basisVectorIndexPair)
        {
            var index =
                basisVectorIndexPair.BasisVectorIndicesToBivectorIndex();

            return RemoveTerm(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveBivectorTerms(IEnumerable<GaRecordKeyPair> indexList)
        {
            foreach (var (basisVectorIndex1, basisVectorIndex2) in indexList)
                RemoveBivectorTerm(basisVectorIndex1, basisVectorIndex2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveTerms(params ulong[] keysList)
        {
            foreach (var key in keysList)
                RemoveTerm(key);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> RemoveTerms(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                RemoveTerm(key);

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> SetBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> SetBivectorTerm(GaRecordKeyPair basisVectorIndexPair, T scalar)
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
        public GaListEvenComposerBase<T> SetBivectorTerms(IEnumerable<GaRecordKeyPairValue<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SetBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetTerms(IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetTerms(IGaListEven<T> evenList)
        {
            foreach (var (key, value) in evenList.GetKeyValueRecords())
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(IGaListEvenDense<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(T scalingFactor, IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SetScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> AddBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> AddBivectorTerm(GaRecordKeyPair basisVectorIndexPair, T scalar)
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
        public GaListEvenComposerBase<T> AddBivectorTerms(IEnumerable<GaRecordKeyPairValue<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                AddBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddTerms(IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddTerms(IGaListEven<T> evenList)
        {
            foreach (var (key, value) in evenList.GetKeyValueRecords())
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(IGaListEvenDense<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(T scalingFactor, IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> AddScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractBivectorTerm(int basisVectorIndex1, int basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> SubtractBivectorTerm(ulong basisVectorIndex1, ulong basisVectorIndex2, T scalar)
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
        public GaListEvenComposerBase<T> SubtractBivectorTerm(GaRecordKeyPair basisVectorIndexPair, T scalar)
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
        public GaListEvenComposerBase<T> SubtractBivectorTerms(IEnumerable<GaRecordKeyPairValue<T>> termsList)
        {
            foreach (var (index1, index2, scalar) in termsList)
                SubtractBivectorTerm(index1, index2, scalar);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractTerms(IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractTerms(IGaListEven<T> evenList)
        {
            foreach (var (key, value) in evenList.GetKeyValueRecords())
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(IGaListEvenDense<T> evenList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(T scalingFactor, IGaListEvenDense<T> evenList)
        {
            var key = 0UL;
            foreach (var value in evenList.GetValues())
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords, T scalingFactor)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(IEnumerable<T> valuesList, T scalingFactor)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(value, scalingFactor));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> SubtractScaledTerms(T scalingFactor, IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenComposerBase<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T> CreateGradedList()
        {
            return CreateEvenList().ToGradedList();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> CreateStorageVector()
        {
            return CreateEvenList().CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> CreateStorageBivector()
        {
            return CreateEvenList().CreateStorageBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> CreateStorageKVector(uint grade)
        {
            return CreateEvenList().CreateStorageKVector(grade);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> CreateStorageMultivector()
        {
            return CreateStorageSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> CreateStorageSparseMultivector()
        {
            return CreateEvenList().CreateStorageSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorGraded<T> CreateStorageGradedMultivector()
        {
            return CreateGradedList().CreateStorageGradedMultivector();
        }
    }
}