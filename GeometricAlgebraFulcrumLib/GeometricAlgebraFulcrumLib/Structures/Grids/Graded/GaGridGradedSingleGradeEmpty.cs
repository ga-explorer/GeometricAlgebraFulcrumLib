using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedSingleGradeEmpty<T> :
        GaGridGradedSingleGradeBase<T>
    {
        public override IGaGridEven<T> EvenGrid
            => GaGridEvenEmpty<T>.EmptyGrid;


        internal GaGridGradedSingleGradeEmpty(uint grade) 
            : base(grade)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, ulong key1, ulong key2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(uint grade, GaRecordKeyPair key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            return Enumerable.Empty<GaRecordGradeKeyPair>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            return Enumerable.Empty<GaRecordGradeKeyPairValue<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return new GaGridGradedSingleGrade<T2>(Grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }
    }
}