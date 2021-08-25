using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public abstract record GaGridGradedSingleGradeBase<T> :
        IGaGridGradedSingleGrade<T>
    {
        public uint Grade { get; }

        public abstract IGaGridEven<T> EvenGrid { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return EvenGrid.GetSparseCount1();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return EvenGrid.GetSparseCount2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return EvenGrid.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return EvenGrid.GetValues();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaGridEven<T>> GetGrids()
        {
            yield return EvenGrid;
        }


        protected GaGridGradedSingleGradeBase(uint grade)
        {
            Grade = grade;
        }


        public abstract bool IsEmpty();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetGrid(uint grade)
        {
            return grade == Grade
                ? EvenGrid
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        public abstract T GetValue(uint grade, ulong key1, ulong key2);

        public abstract T GetValue(GaRecordGradeKeyPair gradeKey);

        public abstract T GetValue(uint grade, GaRecordKeyPair key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade == Grade;
        }

        public abstract bool ContainsKey(uint grade, ulong key1, ulong key2);

        public abstract bool ContainsKey(uint grade, GaRecordKeyPair key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetGrid(uint grade, out IGaGridEven<T> evenGrid)
        {
            if (grade == Grade)
            {
                evenGrid = EvenGrid;
                return true;
            }

            evenGrid = null;
            return false;
        }

        public abstract bool TryGetValue(uint grade, GaRecordKeyPair key, out T value);

        public abstract bool TryGetValue(uint grade, ulong key1, ulong key2, out T value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return Grade;
        }

        public abstract IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords();

        public abstract IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords();

        public abstract IGaGridGraded<T> GetCopy();

        public abstract IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping);

        public abstract IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping);

        public abstract IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : GaGridGradedEmpty<T>.EmptyGrid;
        }

        public abstract IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter);

        public abstract IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter);

        public abstract IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter);

        public abstract IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter);

        public abstract IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter);

        public abstract IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter);

        public abstract IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);

        public abstract IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactGrid(out IGaGridGraded<T> gradedGrid)
        {
            if (EvenGrid.TryGetCompactGrid(out var evenGrid))
            {
                gradedGrid = evenGrid.CreateGradedGridSingleGrade(Grade);
                return true;
            }

            gradedGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenGrid<T>> GetGradeGridRecords()
        {
            yield return new GaRecordGradeEvenGrid<T>(Grade, EvenGrid);
        }
    }
}