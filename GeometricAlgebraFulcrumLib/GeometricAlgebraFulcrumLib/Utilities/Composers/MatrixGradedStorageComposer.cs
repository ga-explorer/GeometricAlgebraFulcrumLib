using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class MatrixGradedStorageComposer<T>
    {
        public Dictionary<uint, MatrixStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, MatrixStorageComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GradeIndexRecord> EvenIndexToGradedIndexMapping { get; set; }
        
        public Func<uint, MatrixStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public MatrixStorageComposerBase<T> this[uint grade]
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


        internal MatrixGradedStorageComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            
            DefaultEvenComposerConstructor = 
                _ => new MatrixSparseStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                BasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetDenseComposer(uint grade, int count1, int count2)
        {
            var composer = new MatrixDenseStorageComposer<T>(ScalarProcessor, count1, count2);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new MatrixSparseStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveTerm(uint grade, IndexPairRecord evenKey)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(evenKey);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveTerm(uint grade, ulong index1, ulong index2)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index1, index2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveTerms(uint grade, params IndexPairRecord[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<IndexPairRecord> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveGrades(params uint[] gradesMatrixStorage)
        {
            foreach (var grade in gradesMatrixStorage)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveEmptyComposers()
        {
            var gradesMatrixStorage =
                ComposersDictionary
                    .Where(pair => pair.Value.IsEmpty())
                    .Select(pair => pair.Key)
                    .ToArray();

            return gradesMatrixStorage.Length > 0
                ? RemoveGrades(gradesMatrixStorage)
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SetTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> AddTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.AddTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SubtractTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SubtractTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> MapValues(Func<uint, ulong, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(
                    (index1, index2, value) => 
                        gradeKeyValueMapping(grade, index1, index2, value)
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> MapValues(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapValues(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].SetTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SetTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SetTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> AddTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].AddTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> AddTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].AddTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> AddTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SubtractTerm(uint grade, IndexPairRecord evenKey, T value)
        {
            this[grade].SubtractTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SubtractTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SubtractTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<IndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SubtractTerm(grade, index1, index2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MatrixGradedStorageComposer<T> SubtractTerms(IEnumerable<GradeIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                SubtractTerm(grade, index1, index2, value);

            return this;
        }


        public ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage()
        {
            if (ComposersDictionary.Count == 0)
                return LinMatrixEmptyGradedStorage<T>.EmptyStorage;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateLinMatrixStorage().CreateLinMatrixSingleGradeStorage(grade);

                return new LinMatrixSingleGradeStorage<T>(grade, composer.CreateLinMatrixStorage());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateLinMatrixStorage()
                )
                .CreateLinMatrixSparseGradedStorage();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinMatrixStorage<T> CreateLinMatrixEvenStorage()
        {
            return CreateLinMatrixGradedStorage().ToMatrixStorage(BasisBladeUtils.BasisBladeGradeIndexToId);
        }
    }
}