using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class LaMatrixGradedStorageComposer<T>
    {
        public Dictionary<uint, LaMatrixEvenStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, LaMatrixEvenStorageComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GradeIndexRecord> EvenIndexToGradedIndexMapping { get; set; }
        
        public Func<uint, LaMatrixEvenStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public LaMatrixEvenStorageComposerBase<T> this[uint grade]
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


        internal LaMatrixGradedStorageComposer([NotNull] IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            
            DefaultEvenComposerConstructor = 
                _ => new LaMatrixSparseEvenStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                GaBasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetDenseComposer(uint grade, int count1, int count2)
        {
            var composer = new LaMatrixDenseEvenStorageComposer<T>(ScalarProcessor, count1, count2);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new LaMatrixSparseEvenStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveTerm(uint grade, IndexPairRecord evenKey)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(evenKey);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveTerm(uint grade, ulong index1, ulong index2)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index1, index2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveTerms(uint grade, params IndexPairRecord[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<IndexPairRecord> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveGrades(params uint[] gradesGrid)
        {
            foreach (var grade in gradesGrid)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveEmptyComposers()
        {
            var gradesGrid =
                ComposersDictionary
                    .Where(pair => pair.Value.IsEmpty())
                    .Select(pair => pair.Key)
                    .ToArray();

            return gradesGrid.Length > 0
                ? RemoveGrades(gradesGrid)
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetTerms(ILaMatrixGradedStorage<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SetTerms(evenGrid.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> AddTerms(ILaMatrixGradedStorage<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.AddTerms(evenGrid.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SubtractTerms(ILaMatrixGradedStorage<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SubtractTerms(evenGrid.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> MapValues(Func<uint, ulong, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(
                    (index1, index2, value) => 
                        gradeKeyValueMapping(grade, index1, index2, value)
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> MapValues(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapValues(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].SetTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SetTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SetTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> AddTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].AddTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> AddTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].AddTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> AddTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SubtractTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].SubtractTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SubtractTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SubtractTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SubtractTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaMatrixGradedStorageComposer<T> SubtractTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                SubtractTerm(grade, index1, index2, value);

            return this;
        }


        public ILaMatrixGradedStorage<T> CreateLaMatrixGradedStorage()
        {
            if (ComposersDictionary.Count == 0)
                return LaMatrixEmptyGradedStorage<T>.EmptyGrid;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateEvenGrid().CreateLaMatrixSingleGradeStorage(grade);

                return new LaMatrixSingleGradeStorage<T>(grade, composer.CreateEvenGrid());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateEvenGrid()
                )
                .CreateLaMatrixSparseGradedStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILaMatrixEvenStorage<T> CreateLaMatrixEvenStorage()
        {
            return CreateLaMatrixGradedStorage().ToEvenStorage(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }
    }
}