using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedSingleGradeKey<T> :
        GaGridGradedSingleGradeBase<T>
    {
        public ulong Key1 
            => SingleKeyGrid.Key1;

        public ulong Key2 
            => SingleKeyGrid.Key2;

        public T Value 
            => SingleKeyGrid.Value;

        public IGaGridEvenSingleKey<T> SingleKeyGrid { get; }

        public override IGaGridEven<T> EvenGrid 
            => SingleKeyGrid;


        internal GaGridGradedSingleGradeKey(uint grade, GaRecordKeyPair key, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyGrid = new GaGridEvenSingleKey<T>(key, value);
        }

        internal GaGridGradedSingleGradeKey(uint grade, ulong key1, ulong key2, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyGrid = new GaGridEvenSingleKey<T>(key1, key2, value);
        }
        
        internal GaGridGradedSingleGradeKey(uint grade, [NotNull] IGaGridEvenSingleKey<T> singleKeyGrid) 
            : base(grade)
        {
            SingleKeyGrid = singleKeyGrid;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key1, ulong key2)
        {
            return grade == Grade && key1 == Key1 && key2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            var (grade, key1, key2) = gradeKey;

            return grade == Grade && key1 == Key1 && key2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, GaRecordKeyPair key)
        {
            var (key1, key2) = key;

            return grade == Grade && key1 == Key1 && key2 == Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return grade == Grade && key1 == Key1 && key2 == Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return grade == Grade && key.Key1 == Key1 && key.Key2 == Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            if (grade == Grade && key.Key1 == Key1 && key.Key2 == Key2)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            if (grade == Grade && key1 == Key1 && key2 == Key2)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            yield return new GaRecordGradeKeyPair(Grade, Key1, Key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            yield return new GaRecordGradeKeyPairValue<T>(Grade, Key1, Key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> GetCopy()
        {
            return new GaGridGradedSingleGradeKey<T>(Grade, Key1, Key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridGradedSingleGradeKey<T2>(
                Grade, 
                Key1, 
                Key2,
                valueMapping(Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridGradedSingleGradeKey<T2>(
                Grade, 
                Key1, 
                Key2,
                keyValueMapping(Key1, Key2, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaGridGradedSingleGradeKey<T2>(
                Grade, 
                Key1, 
                Key2,
                gradeKeyValueMapping(Grade, Key1, Key2, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return keyFilter(Key1, Key2)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Key1, Key2)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, Value)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(Key1, Key2, Value)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Key1, Key2, Value)
                ? this
                : new GaGridGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var key1 = gradeKeyToEvenKeyMapping(Grade, Key1);
            var key2 = gradeKeyToEvenKeyMapping(Grade, Key2);
                
            return key1 == 0 && key2 == 0
                ? new GaGridEvenSingleKeyZero<T>(Value)
                : new GaGridEvenSingleKey<T>(key1, key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            var (key1, key2) = gradeKeyToEvenKeyMapping(Grade, Key1, Key2);
                
            return key1 == 0 && key2 == 0
                ? new GaGridEvenSingleKeyZero<T>(Value)
                : new GaGridEvenSingleKey<T>(key1, key2, Value);
        }
    }
}