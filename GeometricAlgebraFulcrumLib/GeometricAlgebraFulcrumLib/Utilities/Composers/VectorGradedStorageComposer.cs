using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class VectorGradedStorageComposer<T> :
        IVectorStorageComposer<T>
    {
        public Dictionary<uint, VectorStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, VectorStorageComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GradeIndexRecord> EvenIndexToGradedIndexMapping { get; set; }

        public Func<uint, VectorStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public VectorStorageComposerBase<T> this[uint grade]
        {
            get
            {
                if (ComposersDictionary.TryGetValue(grade, out var composer)) 
                    return composer;

                composer = DefaultEvenComposerConstructor(grade);
                ComposersDictionary.Add(grade, composer);

                return composer;
            }
        }


        internal VectorGradedStorageComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            DefaultEvenComposerConstructor = 
                _ => new VectorSparseStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                BasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetDenseComposer(uint grade, int count)
        {
            var composer = new VectorDenseStorageComposer<T>(ScalarProcessor, count);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new VectorSparseStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveTerm(uint grade, ulong index)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveTerms(uint grade, params ulong[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<ulong> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveTerm(ulong evenKey)
        {
            var (grade, index) = EvenIndexToGradedIndexMapping(evenKey);

            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveTerms(params ulong[] evenKeys)
        {
            foreach (var index in evenKeys)
                RemoveTerm(index);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveGrades(params uint[] gradesList)
        {
            foreach (var grade in gradesList)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveEmptyComposers()
        {
            var gradesList =
                ComposersDictionary
                    .Where(pair => pair.Value.IsEmpty())
                    .Select(pair => pair.Key)
                    .ToArray();

            return gradesList.Length > 0
                ? RemoveGrades(gradesList)
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> MapScalars(Func<uint, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars((index, value) => gradeKeyValueMapping(grade, index, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> MapScalars(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapScalars(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SetTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerm(uint grade, ulong index, T value)
        {
            this[grade].SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords())
                this[grade].SetTerms(vectorStorage);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<GradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SetScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                SetTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<LinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                SetScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].AddTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerm(uint grade, ulong index, T value)
        {
            this[grade].AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords()) 
                this[grade].AddTerms(vectorStorage);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<GradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.AddScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddScaledTerms(T scalingFactor, IEnumerable<IndexScalarRecord<T>> indexScalarRecords)
        {
            foreach (var (index, scalar) in indexScalarRecords) 
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                AddTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<LinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                AddScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SubtractTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerm(uint grade, ulong index, T value)
        {
            this[grade].SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords())
                this[grade].SubtractTerms(vectorStorage);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<GradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SubtractScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<LinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                SubtractScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        public ILinVectorGradedStorage<T> CreateLinVectorGradedStorage()
        {
            if (ComposersDictionary.Count == 0)
                return LinVectorEmptyGradedStorage<T>.EmptyStorage;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateLinVectorStorage().CreateLinVectorSingleGradeStorage(grade);

                return new LinVectorSingleGradeStorage<T>(grade, composer.CreateLinVectorStorage());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateLinVectorStorage()
                )
                .CreateLinVectorSparseGradedStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> CreateLinVectorStorage()
        {
            return CreateLinVectorGradedStorage().ToLinVectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> CreateVectorStorage()
        {
            return ComposersDictionary.TryGetValue(1, out var composer) 
                ? composer.CreateVectorStorage() 
                : VectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> CreateBivectorStorage()
        {
            return ComposersDictionary.TryGetValue(2, out var composer) 
                ? composer.CreateBivectorStorage() 
                : BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> CreateKVectorStorage(uint grade)
        {
            return ComposersDictionary.TryGetValue(grade, out var composer) 
                ? composer.CreateKVectorStorage(grade) 
                : KVectorStorage<T>.CreateKVectorZero(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> CreateMultivectorStorage()
        {
            return CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorGradedStorage<T> CreateMultivectorGradedStorage()
        {
            return CreateLinVectorGradedStorage().CreateMultivectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> CreateMultivectorSparseStorage()
        {
            return CreateLinVectorStorage().CreateMultivectorSparseStorage();
        }
    }
}