using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public sealed record GaListGradedSingleGradeKey<T> :
        GaListGradedSingleGradeBase<T>
    {
        public ulong Key 
            => SingleKeyList.Key;

        public T Value 
            => SingleKeyList.Value;

        public IGaListEvenSingleKey<T> SingleKeyList { get; }

        public override IGaListEven<T> EvenList 
            => SingleKeyList;


        internal GaListGradedSingleGradeKey(uint grade, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyList = new GaListEvenSingleKeyZero<T>(value);
        }
        
        internal GaListGradedSingleGradeKey(uint grade, ulong key, [NotNull] T value) 
            : base(grade)
        {
            SingleKeyList = key > 0
                ? new GaListEvenSingleKey<T>(key, value)
                : new GaListEvenSingleKeyZero<T>(value);
        }
        
        internal GaListGradedSingleGradeKey(uint grade, [NotNull] IGaListEvenSingleKey<T> singleKeyList) 
            : base(grade)
        {
            SingleKeyList = singleKeyList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key)
        {
            return grade == Grade && key == Key
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKey gradeKey)
        {
            var (grade, key) = gradeKey;

            return grade == Grade && key == Key
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key)
        {
            return grade == Grade && key == Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key, out T value)
        {
            if (grade == Grade && key == Key)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKey> GetGradeKeyRecords()
        {
            yield return new GaRecordGradeKey(Grade, Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords()
        {
            yield return new GaRecordGradeKeyValue<T>(Grade, Key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> GetCopy()
        {
            return new GaListGradedSingleGradeKey<T>(Grade, Key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListGradedSingleGradeKey<T2>(
                Grade, 
                Key, 
                valueMapping(Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaListGradedSingleGradeKey<T2>(
                Grade, 
                Key, 
                keyValueMapping(Key, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaListGradedSingleGradeKey<T2>(
                Grade, 
                Key, 
                gradeKeyValueMapping(Grade, Key, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return keyFilter(Key)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return gradeKeyFilter(Grade, Key)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return gradeValueFilter(Grade, Value)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(Key, Value)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return gradeKeyValueFilter(Grade, Key, Value)
                ? this
                : new GaListGradedSingleGradeEmpty<T>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var key = gradeKeyToEvenKeyMapping(Grade, Key);
                
            return key == 0
                ? new GaListEvenSingleKeyZero<T>(Value)
                : new GaListEvenSingleKey<T>(key, Value);
        }
    }
}