using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Composers
{
    public class GaGridGradedComposer<T>
    {
        public Dictionary<uint, GaGridEvenComposerBase<T>> ComposersDictionary { get; }
            = new Dictionary<uint, GaGridEvenComposerBase<T>>();


        public int GradesCount 
            => ComposersDictionary.Count;

        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public Func<ulong, GaRecordGradeKey> EvenKeyToGradedKeyMapping { get; set; }
        
        public Func<uint, GaGridEvenComposerBase<T>> DefaultEvenComposerConstructor { get; set; }

        public GaGridEvenComposerBase<T> this[uint grade]
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


        internal GaGridGradedComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
            
            DefaultEvenComposerConstructor = 
                _ => new GaGridEvenComposerSparse<T>(scalarProcessor);

            EvenKeyToGradedKeyMapping =
                GaBasisBladeUtils.BasisBladeIdToGradeIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return ComposersDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> Clear()
        {
            ComposersDictionary.Clear();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetDenseComposer(uint grade, int count1, int count2)
        {
            var composer = new GaGridEvenComposerDense<T>(ScalarProcessor, count1, count2);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetSparseComposer(uint grade)
        {
            var composer = new GaGridEvenComposerSparse<T>(ScalarProcessor);

            if (ComposersDictionary.ContainsKey(grade))
                ComposersDictionary[grade] = composer;
            else
                ComposersDictionary.Add(grade, composer);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveTerm(uint grade, GaRecordKeyPair evenKey)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(evenKey);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveTerm(uint grade, ulong key1, ulong key2)
        {
            if (ComposersDictionary.TryGetValue(grade, out var composer))
                composer.RemoveTerm(key1, key2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveTerms(uint grade, params GaRecordKeyPair[] keys)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(keys);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveTerms(uint grade, IEnumerable<GaRecordKeyPair> keys)
        {
            if (!ComposersDictionary.TryGetValue(grade, out var composer))
                return RemoveEmptyComposers();

            composer.RemoveTerms(keys);

            return RemoveEmptyComposers();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveGrade(uint grade)
        {
            ComposersDictionary.Remove(grade);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveGrades(params uint[] gradesGrid)
        {
            foreach (var grade in gradesGrid)
                ComposersDictionary.Remove(grade);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> RemoveEmptyComposers()
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
        public GaGridGradedComposer<T> RemoveZeroTerms()
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.RemoveZeroTerms();

            return RemoveEmptyComposers();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetTerms(IGaGridGraded<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeGridRecords())
            {
                var composer = this[grade];

                composer.SetTerms(evenGrid.GetKeyValueRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> AddTerms(IGaGridGraded<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeGridRecords())
            {
                var composer = this[grade];

                composer.AddTerms(evenGrid.GetKeyValueRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SubtractTerms(IGaGridGraded<T> gradedGrid)
        {
            foreach (var (grade, evenGrid) in gradedGrid.GetGradeGridRecords())
            {
                var composer = this[grade];

                composer.SubtractTerms(evenGrid.GetKeyValueRecords());
            }

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> MapValues(Func<uint, ulong, ulong, T, T> gradeKeyValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(
                    (key1, key2, value) => 
                        gradeKeyValueMapping(grade, key1, key2, value)
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> MapValues(Func<uint, T, T> gradeValueMapping)
        {
            foreach (var (grade, composer) in ComposersDictionary)
                composer.MapValues(value => gradeValueMapping(grade, value));

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> MapValues(Func<T, T> valueMapping)
        {
            foreach (var composer in ComposersDictionary.Values)
                composer.MapValues(valueMapping);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> Negative()
        {
            return MapValues(ScalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> Times(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> Divide(T scalingFactor)
        {
            return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetTerm(uint grade, GaRecordKeyPair evenKey, T value)
        {
            this[grade].SetTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetTerm(uint grade, ulong key1, ulong key2, T value)
        {
            this[grade].SetTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetTerms(uint grade, IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                SetTerm(grade, key1, key2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SetTerms(IEnumerable<GaRecordGradeKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (grade, key1, key2, value) in keyTermRecords)
                SetTerm(grade, key1, key2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> AddTerm(uint grade, GaRecordKeyPair evenKey, T value)
        {
            this[grade].AddTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> AddTerm(uint grade, ulong key1, ulong key2, T value)
        {
            this[grade].AddTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> AddTerms(uint grade, IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                AddTerm(grade, key1, key2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> AddTerms(IEnumerable<GaRecordGradeKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (grade, key1, key2, value) in keyTermRecords)
                AddTerm(grade, key1, key2, value);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SubtractTerm(uint grade, GaRecordKeyPair evenKey, T value)
        {
            this[grade].SubtractTerm(evenKey, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SubtractTerm(uint grade, ulong key1, ulong key2, T value)
        {
            this[grade].SubtractTerm(key1, key2, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SubtractTerms(uint grade, IEnumerable<GaRecordKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (key1, key2, value) in keyTermRecords)
                SubtractTerm(grade, key1, key2, value);

            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedComposer<T> SubtractTerms(IEnumerable<GaRecordGradeKeyPairValue<T>> keyTermRecords)
        {
            foreach (var (grade, key1, key2, value) in keyTermRecords)
                SubtractTerm(grade, key1, key2, value);

            return this;
        }


        public IGaGridGraded<T> CreateGradedGrid()
        {
            if (ComposersDictionary.Count == 0)
                return GaGridGradedEmpty<T>.EmptyGrid;

            if (ComposersDictionary.Count == 1)
            {
                var (grade, composer) = 
                    ComposersDictionary.First();

                composer.CreateEvenGrid().CreateGradedGridSingleGrade(grade);

                return new GaGridGradedSingleGrade<T>(grade, composer.CreateEvenGrid());
            }

            return ComposersDictionary
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value.CreateEvenGrid()
                )
                .CreateGradedGridSparse();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> CreateEvenGrid()
        {
            return CreateGradedGrid().ToEvenGrid(GaBasisBladeUtils.BasisBladeGradeIndexToId);
        }
    }
}