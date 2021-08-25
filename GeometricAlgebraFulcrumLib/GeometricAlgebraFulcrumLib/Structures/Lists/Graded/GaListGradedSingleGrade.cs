using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public sealed record GaListGradedSingleGrade<T> :
        GaListGradedSingleGradeBase<T>
    {
        public override IGaListEven<T> EvenList { get; }


        internal GaListGradedSingleGrade(uint grade)
            : base(grade)
        {
            EvenList = GaListEvenEmpty<T>.EmptyList;
        }

        internal GaListGradedSingleGrade(uint grade, [NotNull] IGaListEven<T> evenList)
            : base(grade)
        {
            EvenList = evenList.IsNullOrEmpty() 
                ? GaListEvenEmpty<T>.EmptyList
                : evenList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return EvenList.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key)
        {
            return grade == Grade
                ? EvenList.GetValue(key)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKey gradeKey)
        {
            return gradeKey.Grade == Grade
                ? EvenList.GetValue(gradeKey.Key)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key)
        {
            return grade == Grade
                ? EvenList.ContainsKey(key)
                : throw new KeyNotFoundException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key, out T value)
        {
            if (grade == Grade)
                return EvenList.TryGetValue(key, out value);

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKey> GetGradeKeyRecords()
        {
            return EvenList.GetKeys().Select(
                key => new GaRecordGradeKey(Grade, key)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords()
        {
            return EvenList.GetKeyValueRecords().Select(
                keyValueRecord =>
                {
                    var (key, value) = keyValueRecord;

                    return new GaRecordGradeKeyValue<T>(Grade, key, value);
                }
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> GetCopy()
        {
            return new GaListGradedSingleGrade<T>(
                Grade,
                EvenList.GetCopy()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListGradedSingleGrade<T2>(
                Grade,
                EvenList.MapValues(valueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaListGradedSingleGrade<T2>(
                Grade,
                EvenList.MapValues(keyValueMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaListGradedSingleGrade<T2>(
                Grade,
                EvenList.MapValues((key, value) => 
                    gradeKeyValueMapping(Grade, key, value)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var evenList = EvenList.FilterByKey(keyFilter);

            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter)
        {
            var evenList = EvenList.FilterByKey(
                key => gradeKeyFilter(Grade, key)
            );
            
            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var evenList = EvenList.FilterByValue(
                value => gradeValueFilter(Grade, value)
            );
            
            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var evenList = EvenList.FilterByKeyValue(keyValueFilter);
            
            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            var evenList = EvenList.FilterByKeyValue(
                (key, value) => gradeKeyValueFilter(Grade, key, value)
            );
            
            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var evenList = EvenList.FilterByValue(valueFilter);
            
            return evenList.IsEmpty()
                ? GaListGradedEmpty<T>.EmptyList
                : new GaListGradedSingleGrade<T>(Grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return EvenList.MapKeys(index => 
                gradeKeyToEvenKeyMapping(Grade, index)
            );
        }
    }
}