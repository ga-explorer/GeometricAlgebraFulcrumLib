using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedEmpty<T> :
        IGaGridGraded<T>
    {
        public static GaGridGradedEmpty<T> EmptyGrid { get; }
            = new GaGridGradedEmpty<T>();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        public int GradesCount 
            => 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return Enumerable.Empty<uint>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaGridEven<T>> GetGrids()
        {
            return Enumerable.Empty<IGaGridEven<T>>();
        }


        private GaGridGradedEmpty()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetGrid(uint grade)
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, ulong key1, ulong key2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, GaRecordKeyPair key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetGrid(uint grade, out IGaGridEven<T> evenGrid)
        {
            evenGrid = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint key, out IGaGridEven<T> value)
        {
            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenGrid<T>> GetGradeGridRecords()
        {
            return Enumerable.Empty<GaRecordGradeEvenGrid<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            return Enumerable.Empty<GaRecordGradeKeyPair>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            return Enumerable.Empty<GaRecordGradeKeyPairValue<T>>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> GetCopy()
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return GaGridGradedEmpty<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return GaGridGradedEmpty<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            return GaGridGradedEmpty<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return GaGridGradedEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            return GaGridGradedEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return GaGridGradedEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return EmptyGrid;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            return GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactGrid(out IGaGridGraded<T> gradedGrid)
        {
            gradedGrid = this;
            return false;
        }
    }
}