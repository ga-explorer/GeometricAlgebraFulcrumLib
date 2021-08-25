using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Utils;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public class GaListGradedComposer<T>
    {
        public Dictionary<uint, GaListEvenComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, GaListEvenComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GaRecordGradeKey> EvenKeyToGradedKeyMapping { get; set; }

        public Func<uint, GaListEvenComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public GaListEvenComposerBase<T> this[uint grade]
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


        internal GaListGradedComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;

            DefaultEvenComposerConstructor = 
                _ => new GaListEvenComposerSparse<T>(scalarProcessor);

            EvenKeyToGradedKeyMapping =
                GaBasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetDenseComposer(uint grade, int count)
        {
            var composer = new GaListEvenComposerDense<T>(ScalarProcessor, count);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new GaListEvenComposerSparse<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveTerm(uint grade, ulong key)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveTerms(uint grade, params ulong[] keys)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(keys);

            return RemoveEmptyComposers();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveTerms(uint grade, IEnumerable<ulong> keys)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(keys);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveTerm(ulong evenKey)
        {
            var (grade, key) = EvenKeyToGradedKeyMapping(evenKey);

            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveTerms(params ulong[] evenKeys)
        {
            foreach (var key in evenKeys)
                RemoveTerm(key);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveGrades(params uint[] gradesList)
        {
            foreach (var grade in gradesList)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> RemoveEmptyComposers()
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
        public GaListGradedComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> MapValues(Func<uint, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues((key, value) => gradeKeyValueMapping(grade, key, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> MapValues(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapValues(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerm(ulong evenKey, T value)
        {
            var (grade, key) = 
                EvenKeyToGradedKeyMapping(evenKey);

            this[grade].SetTerm(key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SetTerm(key++, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerm(uint grade, ulong key, T value)
        {
            this[grade].SetTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(uint grade, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SetTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(IEnumerable<GaRecordGradeKeyValue<T>> keyTermRecords)
        {
            foreach (var (grade, key, value) in keyTermRecords)
                SetTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetTerms(IGaListGraded<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeListRecords())
                this[grade].SetTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetScaledTerms(IEnumerable<GaRecordGradeEvenListValue<T>> scaledTermsList)
        {
            foreach (var (grade, keyValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SetScaledTerms(scalingFactor, keyValueList.GetKeyValueRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetScaledTerms(T scalingFactor, IGaListEven<T> keyValueList)
        {
            foreach (var (key, value) in keyValueList.GetKeyValueRecords()) 
                SetTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SetScaledTerms(IEnumerable<GaRecordEvenListValue<T>> scaledTermsList)
        {
            foreach (var (keyValueList, scalingFactor) in scaledTermsList) 
                SetScaledTerms(scalingFactor, keyValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerm(ulong evenKey, T value)
        {
            var (grade, key) = 
                EvenKeyToGradedKeyMapping(evenKey);

            this[grade].AddTerm(key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                AddTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerm(uint grade, ulong key, T value)
        {
            this[grade].AddTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(uint grade, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                AddTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(IEnumerable<GaRecordGradeKeyValue<T>> keyTermRecords)
        {
            foreach (var (grade, key, value) in keyTermRecords)
                AddTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddTerms(IGaListGraded<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeListRecords()) 
                this[grade].AddTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddScaledTerms(IEnumerable<GaRecordGradeEvenListValue<T>> scaledTermsList)
        {
            foreach (var (grade, keyValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.AddScaledTerms(scalingFactor, keyValueList.GetKeyValueRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddScaledTerms(T scalingFactor, IGaListEven<T> keyValueList)
        {
            foreach (var (key, value) in keyValueList.GetKeyValueRecords()) 
                AddTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> AddScaledTerms(IEnumerable<GaRecordEvenListValue<T>> scaledTermsList)
        {
            foreach (var (keyValueList, scalingFactor) in scaledTermsList) 
                AddScaledTerms(scalingFactor, keyValueList);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerm(ulong evenKey, T value)
        {
            var (grade, key) = 
                EvenKeyToGradedKeyMapping(evenKey);

            this[grade].SubtractTerm(key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(IEnumerable<T> valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(params T[] valuesList)
        {
            var key = 0UL;
            foreach (var value in valuesList)
                SubtractTerm(key++, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerm(uint grade, ulong key, T value)
        {
            this[grade].SubtractTerm(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(uint grade, IEnumerable<GaRecordKeyValue<T>> keyTermRecords)
        {
            foreach (var (key, value) in keyTermRecords)
                SubtractTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(IEnumerable<GaRecordGradeKeyValue<T>> keyTermRecords)
        {
            foreach (var (grade, key, value) in keyTermRecords)
                SubtractTerm(grade, key, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractTerms(IGaListGraded<T> gradedList)
        {
            foreach (var (grade, evenList) in gradedList.GetGradeListRecords())
                this[grade].SubtractTerms(evenList);

            return this;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractScaledTerms(IEnumerable<GaRecordGradeEvenListValue<T>> scaledTermsList)
        {
            foreach (var (grade, keyValueList, scalingFactor) in scaledTermsList)
            {
                var composer = this[grade];

                composer.SubtractScaledTerms(scalingFactor, keyValueList.GetKeyValueRecords());
            }

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractScaledTerms(T scalingFactor, IGaListEven<T> keyValueList)
        {
            foreach (var (key, value) in keyValueList.GetKeyValueRecords()) 
                SubtractTerm(key, ScalarProcessor.Times(scalingFactor, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListGradedComposer<T> SubtractScaledTerms(IEnumerable<GaRecordEvenListValue<T>> scaledTermsList)
        {
            foreach (var (keyValueList, scalingFactor) in scaledTermsList) 
                SubtractScaledTerms(scalingFactor, keyValueList);

            return this;
        }


        public IGaListGraded<T> CreateGradedList()
        {
            if (ComposersDictionary.Count == 0)
                return GaListGradedEmpty<T>.EmptyList;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateEvenList().CreateGradedListSingleGrade(grade);

                return new GaListGradedSingleGrade<T>(grade, composer.CreateEvenList());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateEvenList()
                )
                .CreateGradedListSparse();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> CreateEvenList()
        {
            return CreateGradedList().ToEvenList();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageVector<T> CreateStorageVector()
        {
            return ComposersDictionary.TryGetValue(1, out var composer) 
                ? composer.CreateStorageVector() 
                : GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageBivector<T> CreateStorageBivector()
        {
            return ComposersDictionary.TryGetValue(2, out var composer) 
                ? composer.CreateStorageBivector() 
                : GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageKVector<T> CreateStorageKVector(uint grade)
        {
            return ComposersDictionary.TryGetValue(grade, out var composer) 
                ? composer.CreateStorageKVector(grade) 
                : GaStorageKVector<T>.ZeroKVector(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> CreateStorageMultivector()
        {
            return CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorGraded<T> CreateStorageGradedMultivector()
        {
            return CreateGradedList().CreateStorageGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaStorageMultivectorSparse<T> CreateStorageSparseMultivector()
        {
            return CreateEvenList().CreateStorageSparseMultivector();
        }
    }
}