using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded
{
    public class LinMatrixGradedStorageComposer<T>
    {
        public Dictionary<uint, LinMatrixStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, LinMatrixStorageComposerBase<T>>();


        public int GradesCount
            => ComposersDictionary.Count;

        public IScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, RGaGradeKvIndexRecord> EvenIndexToGradedIndexMapping { get; set; }

        public Func<uint, LinMatrixStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public LinMatrixStorageComposerBase<T> this[uint grade]
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


        internal LinMatrixGradedStorageComposer(IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            DefaultEvenComposerConstructor =
                _ => new LinMatrixSparseStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                BasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetDenseComposer(uint grade, int count1, int count2)
        {
            var composer = new LinMatrixDenseStorageComposer<T>(ScalarProcessor, count1, count2);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new LinMatrixSparseStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveTerm(uint grade, RGaKvIndexPairRecord evenKey)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(evenKey);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveTerm(uint grade, ulong index1, ulong index2)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index1, index2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveTerms(uint grade, params RGaKvIndexPairRecord[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<RGaKvIndexPairRecord> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveGrades(params uint[] gradesMatrixStorage)
        {
            foreach (var grade in gradesMatrixStorage)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> RemoveEmptyComposers()
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
        public LinMatrixGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SetTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> AddTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.AddTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SubtractTerms(ILinMatrixGradedStorage<T> matrixGradedStorage)
        {
            foreach (var (grade, matrixStorage) in matrixGradedStorage.GetGradeStorageRecords())
            {
                var composer = this[grade];

                composer.SubtractTerms(matrixStorage.GetIndexScalarRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> MapValues(Func<uint, ulong, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(
                    (index1, index2, value) =>
                        gradeKeyValueMapping(grade, index1, index2, value)
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> MapValues(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapValues(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetTerm(uint grade, RGaKvIndexPairRecord evenKey, T value)
        {
            this[grade].SetTerm(evenKey, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SetTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SetTerms(IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                SetTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> AddTerm(uint grade, RGaKvIndexPairRecord evenKey, T value)
        {
            this[grade].AddTerm(evenKey, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> AddTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].AddTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> AddTerms(IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index1, index2, value) in indexTermRecords)
                AddTerm(grade, index1, index2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SubtractTerm(uint grade, RGaKvIndexPairRecord evenKey, T value)
        {
            this[grade].SubtractTerm(evenKey, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SubtractTerm(uint grade, ulong index1, ulong index2, T value)
        {
            this[grade].SubtractTerm(index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index1, index2, value) in indexTermRecords)
                SubtractTerm(grade, index1, index2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinMatrixGradedStorageComposer<T> SubtractTerms(IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> indexTermRecords)
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