using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public sealed record GaListGradedSingleGradeEmpty<T> :
        GaListGradedSingleGradeBase<T>
    {
        public override IGaListEven<T> EvenList 
            => GaListEvenEmpty<T>.EmptyList;

        
        internal GaListGradedSingleGradeEmpty(uint grade) 
            : base(grade)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKey gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKey> GetGradeKeyRecords()
        {
            return Enumerable.Empty<GaRecordGradeKey>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords()
        {
            return Enumerable.Empty<GaRecordGradeKeyValue<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListGradedSingleGradeEmpty<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaListGradedSingleGradeEmpty<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaListGradedSingleGradeEmpty<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter)
        {
            return this;
        }
    }
}