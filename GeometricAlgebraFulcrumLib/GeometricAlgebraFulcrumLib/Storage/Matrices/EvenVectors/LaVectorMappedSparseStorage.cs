using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorMappedSparseStorage<T> :
        ILaVectorSparseEvenStorage<T>
    {
        public ILaVectorSparseEvenStorage<T> SourceStorage { get; }
        
        public IReadOnlyList<ulong> IndexList { get; }

        public Func<ulong, ulong> IndexMapping { get; }

        public Func<ulong, T, T> IndexScalarMapping { get; }

        
        internal LaVectorMappedSparseStorage([NotNull] ILaVectorSparseEvenStorage<T> source, [NotNull] IReadOnlyList<ulong> indexList, [NotNull] Func<ulong, ulong> indexMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = indexMapping;
            IndexScalarMapping = (_, scalar) => scalar;
        }
        
        internal LaVectorMappedSparseStorage([NotNull] ILaVectorSparseEvenStorage<T> source, [NotNull] IReadOnlyList<ulong> indexList, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = index => index;
            IndexScalarMapping = indexScalarMapping;
        }
        
        internal LaVectorMappedSparseStorage([NotNull] ILaVectorSparseEvenStorage<T> source, [NotNull] IReadOnlyList<ulong> indexList, [NotNull] Func<ulong, ulong> indexMapping, [NotNull] Func<ulong, T, T> indexScalarMapping)
        {
            SourceStorage = source;
            IndexList = indexList;
            IndexMapping = indexMapping;
            IndexScalarMapping = indexScalarMapping;
        }


        public bool IsEmpty()
        {
            return IndexList.Count == 0;
        }

        public int GetSparseCount()
        {
            return IndexList.Count;
        }

        public IEnumerable<T> GetScalars()
        {
            return IndexList.Select(GetScalar);
        }

        public IEnumerable<ulong> GetIndices()
        {
            return IndexList;
        }

        public T GetScalar(ulong index)
        {
            index = IndexMapping(index);

            return IndexScalarMapping(index, SourceStorage.GetScalar(index));
        }

        public IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords()
        {
            return IndexList.Select(index => new IndexScalarRecord<T>(index, GetScalar(index)));
        }

        public bool ContainsIndex(ulong index)
        {
            return IndexList.Contains(index);
        }

        public bool TryGetScalar(ulong index, out T scalar)
        {
            index = IndexMapping(index);

            if (SourceStorage.TryGetScalar(index, out var value))
            {
                scalar = IndexScalarMapping(index, value);
                return true;
            }

            scalar = default;
            return false;
        }

        public ulong GetMinIndex()
        {
            return IndexList.Min();
        }

        public ulong GetMaxIndex()
        {
            return IndexList.Max();
        }

        public IEnumerable<ulong> GetEmptyIndices(ulong maxKey)
        {
            //var indexList = 
            //    IndexList
            //        .Where(index => index <= maxKey)
            //        .OrderBy(index => index);

            return (maxKey + 1).GetRange().Except(IndexList);
        }

        public ILaVectorEvenStorage<T> GetCopy()
        {
            return this;
        }

        public ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> keyMapping)
        {
            return new LaVectorMappedSparseStorage<T>(
                SourceStorage,
                IndexList.Select(keyMapping).ToArray(),
                IndexMapping,
                IndexScalarMapping
            );
        }

        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            throw new NotImplementedException();
        }

        public ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var valueDictionary = 
                IndexList
                    .Where(indexFilter)
                    .ToDictionary(index => index, GetScalar);

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            foreach (var index in IndexList)
            {
                var value = GetScalar(index);

                if (indexValueFilter(index, value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> scalarFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            foreach (var index in IndexList)
            {
                var value = GetScalar(index);

                if (scalarFilter(value))
                    valueDictionary.Add(index, value);
            }

            return valueDictionary.CreateLaVectorStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var id in IndexList)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                var scalar = GetScalar(id);

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var id in IndexList)
            {
                var (grade, index, scalar) = evenIndexScalarToGradeIndexScalarMapping(id, GetScalar(id));

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var indexValueDictionary))
                {
                    indexValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, indexValueDictionary);
                }

                if (indexValueDictionary.ContainsKey(index))
                    indexValueDictionary[index] = scalar;
                else
                    indexValueDictionary.Add(index, scalar);
            }

            return gradeKeyValueDictionary.CreateLaVectorGradedStorage();
        }

        public bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList)
        {
            if (IndexList.Count == 0)
            {
                evenList = LaVectorEmptyStorage<T>.ZeroStorage;
                return true;
            }

            if (IndexList.Count == 1)
            {
                var index = IndexList[0];
                evenList = GetScalar(index).CreateLaVectorSingleIndexEvenStorage(index);
                return true;
            }

            evenList = this;
            return false;
        }
    }
}