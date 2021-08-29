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
    public class LaVectorGradedStorageComposer<T> :
        ILaVectorStorageComposer<T>
    {
        public Dictionary<uint, LaVectorEvenStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, LaVectorEvenStorageComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GradeIndexRecord> EvenIndexToGradedIndexMapping { get; set; }

        public Func<uint, LaVectorEvenStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public LaVectorEvenStorageComposerBase<T> this[uint grade]
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


        internal LaVectorGradedStorageComposer([NotNull] IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            DefaultEvenComposerConstructor = 
                _ => new LaVectorSparseEvenStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                GaBasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetDenseComposer(uint grade, int count)
        {
            var composer = new LaVectorDenseEvenStorageComposer<T>(ScalarProcessor, count);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new LaVectorSparseEvenStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveTerm(uint grade, ulong index)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveTerms(uint grade, params ulong[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<ulong> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveTerm(ulong evenKey)
        {
            var (grade, index) = EvenIndexToGradedIndexMapping(evenKey);

            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveTerms(params ulong[] evenKeys)
        {
            foreach (var index in evenKeys)
                RemoveTerm(index);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveGrades(params uint[] gradesList)
        {
            foreach (var grade in gradesList)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> RemoveEmptyComposers()
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
        public LaVectorGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> MapScalars(Func<uint, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars((index, value) => gradeKeyValueMapping(grade, index, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> MapScalars(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapScalars(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SetTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerm(uint grade, ulong index, T value)
        {
            this[grade].SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetTerms(ILaVectorGradedStorage<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeStorageRecords())
                this[grade].SetTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<GradeVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SetScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetScaledTerms(T scalingFactor, ILaVectorEvenStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                SetTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<VectorEvenStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                SetScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].AddTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerm(uint grade, ulong index, T value)
        {
            this[grade].AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddTerms(ILaVectorGradedStorage<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeStorageRecords()) 
                this[grade].AddTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<GradeVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.AddScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddScaledTerms(T scalingFactor, ILaVectorEvenStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                AddTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<VectorEvenStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                AddScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerm(ulong evenKey, T value)
        {
            var (grade, index) = 
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SubtractTerm(index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerm(uint grade, ulong index, T value)
        {
            this[grade].SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<IndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<GradeIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractTerms(ILaVectorGradedStorage<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeStorageRecords())
                this[grade].SubtractTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<GradeVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SubtractScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractScaledTerms(T scalingFactor, ILaVectorEvenStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords()) 
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<VectorEvenStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList) 
                SubtractScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        public ILaVectorGradedStorage<T> CreateLaVectorGradedStorage()
        {
            if (ComposersDictionary.Count == 0)
                return LaVectorEmptyGradedStorage<T>.EmptyList;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateLaVectorEvenStorage().CreateLaVectorSingleGradeStorage(grade);

                return new LaVectorSingleGradeStorage<T>(grade, composer.CreateLaVectorEvenStorage());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateLaVectorEvenStorage()
                )
                .CreateLaVectorSparseGradedStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaVectorEvenStorage<T> CreateLaVectorEvenStorage()
        {
            return CreateLaVectorGradedStorage().ToEvenList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVectorStorage<T> CreateGaVectorStorage()
        {
            return ComposersDictionary.TryGetValue(1, out var composer) 
                ? composer.CreateGaVectorStorage() 
                : GaVectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivectorStorage<T> CreateGaBivectorStorage()
        {
            return ComposersDictionary.TryGetValue(2, out var composer) 
                ? composer.CreateGaBivectorStorage() 
                : GaBivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaKVectorStorage<T> CreateGaKVectorStorage(uint grade)
        {
            return ComposersDictionary.TryGetValue(grade, out var composer) 
                ? composer.CreateGaKVectorStorage(grade) 
                : GaKVectorStorage<T>.ZeroKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> CreateGaMultivectorStorage()
        {
            return CreateGaMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorGradedStorage<T> CreateGaMultivectorGradedStorage()
        {
            return CreateLaVectorGradedStorage().CreateStorageGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorSparseStorage<T> CreateGaMultivectorSparseStorage()
        {
            return CreateLaVectorEvenStorage().CreateStorageSparseMultivector();
        }
    }
}