using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded
{
    public class LinVectorGradedStorageComposer<T> :
        ILinVectorStorageComposer<T>
    {
        public Dictionary<uint, LinVectorStorageComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, LinVectorStorageComposerBase<T>>();


        public int GradesCount
            => ComposersDictionary.Count;

        public IScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, RGaGradeKvIndexRecord> EvenIndexToGradedIndexMapping { get; set; }

        public Func<uint, LinVectorStorageComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public LinVectorStorageComposerBase<T> this[uint grade]
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


        internal LinVectorGradedStorageComposer(IScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            DefaultEvenComposerConstructor =
                _ => new LinVectorSparseStorageComposer<T>(scalarProcessor);

            EvenIndexToGradedIndexMapping =
                BasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
        {
            foreach (var (grade, composer) in ComposersDictionary)
                foreach (var (index, scalar) in composer.GetIndexScalarRecords())
                    yield return new RGaKvIndexScalarRecord<T>(
                        BasisBladeUtils.BasisBladeGradeIndexToId(grade, index),
                        scalar
                    );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetDenseComposer(uint grade, int count)
        {
            var composer = new LinVectorDenseStorageComposer<T>(ScalarProcessor, count);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new LinVectorSparseStorageComposer<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveTerm(uint grade, ulong index)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveTerms(uint grade, params ulong[] indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveTerms(uint grade, IEnumerable<ulong> indexs)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(indexs);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveTerm(ulong evenKey)
        {
            var (grade, index) = EvenIndexToGradedIndexMapping(evenKey);

            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(index);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveTerms(params ulong[] evenKeys)
        {
            foreach (var index in evenKeys)
                RemoveTerm(index);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveGrades(params uint[] gradesList)
        {
            foreach (var grade in gradesList)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> RemoveEmptyComposers()
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
        public LinVectorGradedStorageComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> MapScalars(Func<uint, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars((index, value) => gradeKeyValueMapping(grade, index, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> MapScalars(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapScalars(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> MapScalars(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapScalars(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> Negative()
        {
            return MapScalars(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> Times(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> Divide(T scalingFactor)
        {
            return MapScalars(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerm(ulong evenKey, T value)
        {
            var (grade, index) =
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(IEnumerable<RGaKvIndexScalarRecord<T>> idTermRecords)
        {
            foreach (var (id, value) in idTermRecords)
                SetTerm(id, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SetTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerm(uint grade, ulong index, T value)
        {
            this[grade].SetTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(IEnumerable<RGaGradeKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SetTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords())
                this[grade].SetTerms(vectorStorage);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<GaGradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SetScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords())
                SetTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SetScaledTerms(IEnumerable<GaLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList)
                SetScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerm(ulong evenKey, T value)
        {
            var (grade, index) =
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(IEnumerable<RGaKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                AddTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerm(uint grade, ulong index, T value)
        {
            this[grade].AddTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(IEnumerable<RGaGradeKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                AddTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords())
                this[grade].AddTerms(vectorStorage);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<GaGradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.AddScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddScaledTerms(T scalingFactor, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarRecords)
        {
            foreach (var (index, scalar) in indexScalarRecords)
                AddTerm(index, ScalarProcessor.Times(scalingFactor, scalar));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords())
                AddTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> AddScaledTerms(IEnumerable<GaLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (indexValueList, scalingFactor) in scaledTermsList)
                AddScaledTerms(scalingFactor, indexValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerm(ulong evenKey, T value)
        {
            var (grade, index) =
                EvenIndexToGradedIndexMapping(evenKey);

            this[grade].SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<RGaKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(params T[] valuesList)
        {
            var index = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(index++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerm(uint grade, ulong index, T value)
        {
            this[grade].SubtractTerm(index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(IEnumerable<RGaGradeKvIndexScalarRecord<T>> indexTermRecords)
        {
            foreach (var (grade, index, value) in indexTermRecords)
                SubtractTerm(grade, index, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractTerms(ILinVectorGradedStorage<T> vectorGradedStorage)
        {
            foreach (var (grade, vectorStorage) in vectorGradedStorage.GetGradeStorageRecords())
                this[grade].SubtractTerms(vectorStorage);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<GaGradeLinVectorStorageScalarRecord<T>> scaledTermsList)
        {
            foreach (var (grade, indexValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SubtractScaledTerms(scalingFactor, indexValueList.GetIndexScalarRecords());
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractScaledTerms(T scalingFactor, ILinVectorStorage<T> indexValueList)
        {
            foreach (var (index, value) in indexValueList.GetIndexScalarRecords())
                SubtractTerm(index, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorGradedStorageComposer<T> SubtractScaledTerms(IEnumerable<GaLinVectorStorageScalarRecord<T>> scaledTermsList)
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
            return CreateLinVectorGradedStorage().CreateMultivectorStorageGraded();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> CreateMultivectorStorageSparse()
        {
            return CreateLinVectorStorage().CreateMultivectorStorageSparse();
        }
    }
}