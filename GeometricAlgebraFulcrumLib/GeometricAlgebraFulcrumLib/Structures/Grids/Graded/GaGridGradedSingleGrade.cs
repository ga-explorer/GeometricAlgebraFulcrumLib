using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedSingleGrade<T> :
        GaGridGradedSingleGradeBase<T>
    {
        
        public override IGaGridEven<T> EvenGrid { get; }


        internal GaGridGradedSingleGrade(uint grade)
            : base(grade)
        {
            EvenGrid = GaGridEvenEmpty<T>.EmptyGrid;
        }

        internal GaGridGradedSingleGrade(uint grade, [NotNull] IGaGridEven<T> evenGrid)
            : base(grade)
        {
            EvenGrid = evenGrid.IsNullOrEmpty() 
                ? GaGridEvenEmpty<T>.EmptyGrid
                : evenGrid;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key1, ulong key2)
        {
            return grade == Grade
                ? EvenGrid.GetValue(key1, key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            var (grade, key1, key2) = gradeKey;

            return grade == Grade
                ? EvenGrid.GetValue(key1, key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, GaRecordKeyPair key)
        {
            return grade == Grade
                ? EvenGrid.GetValue(key)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return grade == Grade
                ? EvenGrid.ContainsKey(key1, key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return grade == Grade
                ? EvenGrid.ContainsKey(key)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            if (grade == Grade)
                return EvenGrid.TryGetValue(key, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            if (grade == Grade)
                return EvenGrid.TryGetValue(key1, key2, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            return EvenGrid.GetKeys().Select(
                key => new GaRecordGradeKeyPair(Grade, key.Key1, key.Key2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            return EvenGrid.GetKeyValueRecords().Select(
                keyValueRecord =>
                {
                    var (key1, key2, value) = keyValueRecord;

                    return new GaRecordGradeKeyPairValue<T>(Grade, key1, key2, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> GetCopy()
        {
            return new GaGridGradedSingleGrade<T>(
                Grade,
                EvenGrid.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(
                Grade,
                EvenGrid.MapValues(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(
                Grade,
                EvenGrid.MapValues(keyValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(
                Grade,
                EvenGrid.MapValues((key1, key2, value) => 
                    gradeKeyValueMapping(Grade, key1, key2, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            var evenGrid = EvenGrid.FilterByKey(keyFilter);

            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var evenGrid = EvenGrid.FilterByKey(
                (key1, key2) => 
                    gradeKeyFilter(Grade, key1, key2)
            );
            
            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var evenGrid = EvenGrid.FilterByValue(valueFilter);
            
            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);

            
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var evenGrid = EvenGrid.FilterByValue(
                value => gradeValueFilter(Grade, value)
            );
            
            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            
            var evenGrid = EvenGrid.FilterByKeyValue(keyValueFilter);
            
            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var evenGrid = EvenGrid.FilterByKeyValue(
                (key1,key2, value) => 
                    gradeKeyValueFilter(Grade, key1, key2, value)
            );
            
            return evenGrid.IsEmpty()
                ? GaGridGradedEmpty<T>.EmptyGrid
                : new GaGridGradedSingleGrade<T>(Grade, evenGrid);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return EvenGrid.MapKeys(
                (key1, key2) => 
                    new GaRecordKeyPair(
                        gradeKeyToEvenKeyMapping(Grade, key1),
                        gradeKeyToEvenKeyMapping(Grade, key2)
                    )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return EvenGrid.MapKeys(
                (key1, key2) => 
                    gradeKeyToEvenKeyMapping(Grade, key1, key2)
            );
        }
    }
}