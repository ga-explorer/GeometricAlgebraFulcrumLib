using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public abstract record GaListGradedSingleGradeBase<T> :
        IGaListGradedSingleGrade<T>
    {
        public uint Grade { get; }
        
        public abstract IGaListEven<T> EvenList { get; }
        
        public int GradesCount 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            yield return Grade;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaListEven<T>> GetLists()
        {
            yield return EvenList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return EvenList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return EvenList.GetValues();
        }


        protected GaListGradedSingleGradeBase(uint grade)
        {
            Grade = grade;
        }


        public abstract bool IsEmpty();

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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetList(uint grade)
        {
            return grade == Grade
                ? EvenList
                : GaListEvenEmpty<T>.EmptyList;
        }

        public abstract T GetValue(uint grade, ulong key);

        public abstract T GetValue(GaRecordGradeKey gradeKey);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade == Grade;
        }

        public abstract bool ContainsKey(uint grade, ulong key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetList(uint grade, out IGaListEven<T> evenList)
        {
            if (grade == Grade)
            {
                evenList = EvenList;
                return true;
            }

            evenList = null;
            return false;
        }

        public abstract bool TryGetValue(uint grade, ulong key, out T value);

        public abstract IEnumerable<GaRecordGradeKey> GetGradeKeyRecords();

        public abstract IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords();

        public abstract IGaListGraded<T> GetCopy();

        public abstract IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping);

        public abstract IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping);

        public abstract IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            return gradeFilter(Grade)
                ? this
                : GaListGradedEmpty<T>.EmptyList;
        }

        public abstract IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter);

        public abstract IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter);

        public abstract IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter);

        public abstract IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter);

        public abstract IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter);

        public abstract IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter);

        public abstract IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactList(out IGaListGraded<T> gradedList)
        {
            if (EvenList.TryGetCompactList(out var evenList))
            {
                gradedList = evenList.CreateGradedListSingleGrade(Grade);
                return true;
            }

            gradedList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenList<T>> GetGradeListRecords()
        {
            yield return new GaRecordGradeEvenList<T>(Grade, EvenList);
        }
    }
}