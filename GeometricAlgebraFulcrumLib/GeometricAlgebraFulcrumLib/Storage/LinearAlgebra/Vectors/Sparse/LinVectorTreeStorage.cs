using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse
{
    /// <summary>
    /// This class contains an internal read-only list of type T and a binary tree
    /// index with fixed number of levels. The structure of this tree is fixed but the values
    /// in the leaf nodes can be updated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LinVectorTreeStorage<T> : 
        ILinVectorSparseStorage<T>
    {
        private readonly List<Pair<int>> _internalNodesList;

        private readonly IReadOnlyList<ulong> _leafNodeIDsList;

        private T[] _leafNodeScalarsArray;


        public int TreeDepth { get; }

        public T DefaultScalar { get; set; }
            = default;
        
        public T this[int id] 
        {
            get => TryGetLeafNodeIndex((ulong) id, out var index)
                ? _leafNodeScalarsArray[index] : DefaultScalar;
            set
            {
                if (TryGetLeafNodeIndex((ulong) id, out var index))
                    _leafNodeScalarsArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        public T this[ulong id] 
        {
            get => TryGetLeafNodeIndex(id, out var index)
                ? _leafNodeScalarsArray[index] : DefaultScalar;
            set
            {
                if (TryGetLeafNodeIndex(id, out var index))
                    _leafNodeScalarsArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        public int RootNodeIndex
            => 0;

        public Pair<int> RootNode 
            => _internalNodesList[0];


        private LinVectorTreeStorage(int treeDepth, List<Pair<int>> internalNodesList, IReadOnlyList<ulong> leafNodeIDsList, T[] leafNodeScalarsArray)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _internalNodesList = internalNodesList;

            _leafNodeScalarsArray = new T[_leafNodeIDsList.Count];

            leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);

            ConstructIndexTree();
        }

        internal LinVectorTreeStorage(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeScalarsArray = new T[_leafNodeIDsList.Count];
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal LinVectorTreeStorage(int treeDepth, IReadOnlyDictionary<ulong, T> leafNodes)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodes.Keys.ToArray();
            _leafNodeScalarsArray = leafNodes.Values.ToArray();
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal LinVectorTreeStorage(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeScalarsList)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            if (leafNodeIDsList.Count != leafNodeScalarsList.Count)
                throw new InvalidOperationException();

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeScalarsArray = leafNodeScalarsList.ToArray();
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }
        
        internal LinVectorTreeStorage(int treeDepth, LinVectorTreeStorage<T> binaryTree)
        {
            Debug.Assert(treeDepth >= binaryTree.TreeDepth);

            TreeDepth = treeDepth;

            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeScalarsArray = new T[binaryTree.GetSparseCount()];
            binaryTree._leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);

            if (treeDepth == binaryTree.TreeDepth)
            {
                _internalNodesList = binaryTree._internalNodesList;

                return;
            }

            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal LinVectorTreeStorage(LinVectorTreeStorage<T> binaryTree)
        {
            TreeDepth = binaryTree.TreeDepth;
            _internalNodesList = binaryTree._internalNodesList;
            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeScalarsArray = new T[binaryTree.GetSparseCount()];
            binaryTree._leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _leafNodeIDsList.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(ulong id)
        {
            return TryGetLeafNodeIndex(id, out var index)
                ? _leafNodeScalarsArray[index]
                : DefaultScalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetIndices()
        {
            return _leafNodeIDsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return _leafNodeScalarsArray;
        }

        private void ConstructIndexTree()
        {
            var emptyInternalNode = new Pair<int>(-1, -1);

            _internalNodesList.Clear();
            _internalNodesList.Add(new Pair<int>(-1, -1));

            for (var i = 0; i < GetSparseCount(); i++)
            {
                var id = _leafNodeIDsList[i];
                var bitMask = 1UL << (TreeDepth - 1);
                var index = 0;

                while (bitMask != 1)
                {
                    var (childIndex0, childIndex1) = _internalNodesList[index];

                    if ((bitMask & id) == 0)
                    {
                        if (childIndex0 < 0)
                        {
                            var newIndex = _internalNodesList.Count;

                            _internalNodesList[index] = new Pair<int>(newIndex, childIndex1);

                            _internalNodesList.Add(emptyInternalNode);

                            index = newIndex;
                        }
                        else
                            index = childIndex0;
                    }
                    else
                    {
                        if (childIndex1 < 0)
                        {
                            var newIndex = _internalNodesList.Count;

                            _internalNodesList[index] = new Pair<int>(childIndex0, newIndex);

                            _internalNodesList.Add(emptyInternalNode);

                            index = newIndex;
                        }
                        else
                            index = childIndex1;
                    }

                    bitMask >>= 1;
                }

                var (leafIndex0, leafIndex1) = _internalNodesList[index];

                if ((id & 1) == 0)
                {
                    if (leafIndex0 < 0)
                        _internalNodesList[index] = new Pair<int>(i, leafIndex1);
                    else
                        throw new InvalidOperationException();
                }
                else
                {
                    if (leafIndex1 < 0)
                        _internalNodesList[index] = new Pair<int>(leafIndex0, i);
                    else
                        throw new InvalidOperationException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> ClearScalars()
        {
            _leafNodeScalarsArray = new T[GetSparseCount()];
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetInternalNodeByIndex(int index)
        {
            return _internalNodesList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaKvIndexScalarRecord<T> GetLeafNodeByIndex(int index)
        {
            return new(
                _leafNodeIDsList[index],
                _leafNodeScalarsArray[index]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetLeafNodeIdByIndex(int index)
        {
            return _leafNodeIDsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetLeafNodeScalarByIndex(int index)
        {
            return _leafNodeScalarsArray[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> SetLeafNodeScalarByIndex(int index, T value)
        {
            _leafNodeScalarsArray[index] = value;

            return this;
        }
        
        public bool TryGetLeafNodeIndex(ulong id, out int index)
        {
            var bitMask = 1UL << (TreeDepth - 1);
            index = 0;

            while (bitMask != 1UL)
            {
                var (childIndex0, childIndex1) = _internalNodesList[index];

                if ((bitMask & id) == 0)
                {
                    if (childIndex0 < 0)
                    {
                        index = -1;
                        return false;
                    }
                    
                    index = childIndex0;
                }
                else
                {
                    if (childIndex1 < 0)
                    {
                        index = -1;
                        return false;
                    }
                    
                    index = childIndex1;
                }

                bitMask >>= 1;
            }

            var (leafIndex0, leafIndex1) = _internalNodesList[index];

            if ((id & 1) == 0)
            {
                if (leafIndex0 < 0)
                {
                    index = -1;
                    return false;
                }

                index = leafIndex0;
            }
            else
            {
                if (leafIndex1 < 0)
                {
                    index = -1;
                    return false;
                }

                index = leafIndex1;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(ulong index)
        {
            return ContainsId(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetScalar(ulong id, out T value)
        {
            if (TryGetLeafNodeIndex(id, out var index))
            {
                value = _leafNodeScalarsArray[index];
                return true;
            }

            value = DefaultScalar;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetLeafNodeIndexScalar(ulong id, out int index, out T value)
        {
            if (TryGetLeafNodeIndex(id, out index))
            {
                value = _leafNodeScalarsArray[index];
                return true;
            }

            value = DefaultScalar;
            return false;
        }
        
        public bool ContainsId(ulong id)
        {
            var bitMask = 1UL << (TreeDepth - 1);
            var index = 0;

            while (bitMask != 1)
            {
                var (childIndex0, childIndex1) = _internalNodesList[index];

                if ((bitMask & id) == 0)
                {
                    if (childIndex0 < 0)
                        return false;

                    index = childIndex0;
                }
                else
                {
                    if (childIndex1 < 0)
                        return false;

                    index = childIndex1;
                }

                bitMask >>= 1;
            }

            var (leafIndex0, leafIndex1) = _internalNodesList[index];

            if ((id & 1) == 0)
            {
                if (leafIndex0 < 0)
                    return false;
            }
            else
            {
                if (leafIndex1 < 0)
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetLeafNodeIDs()
        {
            return _leafNodeIDsList;
        }
        
        public IEnumerable<RGaKvIndexScalarRecord<T>> GetLeafNodes()
        {
            for (var i = 0; i < GetSparseCount(); i++)
                yield return new RGaKvIndexScalarRecord<T>(
                    _leafNodeIDsList[i], 
                    _leafNodeScalarsArray[i]
                );
        }


        public LinVectorTreeStorage<T> UpdateScalarsById(Func<ulong, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeScalarsArray[i] = mappingFunc(id);
            }

            return this;
        }

        public LinVectorTreeStorage<T> UpdateScalarsByIdScalar(Func<ulong, T, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeScalarsArray[i] = mappingFunc(id, _leafNodeScalarsArray[i]);
            }

            return this;
        }

        public LinVectorTreeStorage<T> UpdateScalarsByIndex(Func<int, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                _leafNodeScalarsArray[i] = mappingFunc(i);

            return this;
        }

        public LinVectorTreeStorage<T> UpdateScalarsByIndexScalar(Func<int, T, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                _leafNodeScalarsArray[i] = mappingFunc(i, _leafNodeScalarsArray[i]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> UpdateScalars(Func<T, T> mappingFunc)
        {
            _leafNodeScalarsArray = _leafNodeScalarsArray.Select(mappingFunc).ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetLeafNodeScalarsArrayCopy()
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            _leafNodeScalarsArray.CopyTo(leafNodeScalarsArray, 0);

            return leafNodeScalarsArray;
        }

        public T[] GetLeafNodeScalarsArrayCopyById(Func<ulong, T> mappingFunc)
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeScalarsArray[i] = mappingFunc(id);
            }

            return leafNodeScalarsArray;
        }

        public T[] GetLeafNodeScalarsArrayCopyByIdScalar(Func<ulong, T, T> mappingFunc)
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeScalarsArray[i] = mappingFunc(id, _leafNodeScalarsArray[i]);
            }

            return leafNodeScalarsArray;
        }

        public T[] GetLeafNodeScalarsArrayCopyByIndex(Func<int, T> mappingFunc)
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                leafNodeScalarsArray[i] = mappingFunc(i);

            return leafNodeScalarsArray;
        }

        public T[] GetLeafNodeScalarsArrayCopyByIndexScalar(Func<int, T, T> mappingFunc)
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                leafNodeScalarsArray[i] = mappingFunc(i, _leafNodeScalarsArray[i]);

            return leafNodeScalarsArray;
        }

        public T[] GetLeafNodeScalarsArrayCopyByIndexScalar(Func<T, T> mappingFunc)
        {
            var leafNodeScalarsArray = new T[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                leafNodeScalarsArray[i] = mappingFunc(_leafNodeScalarsArray[i]);

            return leafNodeScalarsArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _leafNodeIDsList.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinIndex()
        {
            return _leafNodeIDsList.Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxIndex()
        {
            return _leafNodeIDsList.Max();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
        {
            return maxCount.GetRange().Except(_leafNodeIDsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetCopy()
        {
            return GetTreeCopy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
        {
            return new LinVectorTreeStorage<T>(
                TreeDepth,
                _leafNodeIDsList.Select(indexMapping).ToArray(),
                _leafNodeScalarsArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
        {
            return new LinVectorTreeStorage<T2>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeScalarsArray.Select(valueMapping).ToArray()
            );
        }

        public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping)
        {
            var leafNodeScalarsArray = new T2[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                leafNodeScalarsArray[i] = indexScalarMapping(
                    _leafNodeIDsList[i],
                    _leafNodeScalarsArray[i]
                );
            }

            return new LinVectorTreeStorage<T2>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeScalarsArray
            );
        }

        public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<T>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                if (!indexFilter(id)) continue;

                leafNodeIDsList.Add(id);
                leafNodeScalarsList.Add(_leafNodeScalarsArray[i]);
            }

            return new LinVectorTreeStorage<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }

        public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<T>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];
                var value = _leafNodeScalarsArray[i];

                if (!indexScalarFilter(id, value)) continue;

                leafNodeIDsList.Add(id);
                leafNodeScalarsList.Add(value);
            }

            return new LinVectorTreeStorage<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }

        public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<T>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var value = _leafNodeScalarsArray[i];

                if (!valueFilter(value)) continue;

                leafNodeIDsList.Add(_leafNodeIDsList[i]);
                leafNodeScalarsList.Add(value);
            }

            return new LinVectorTreeStorage<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(
            Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
        {
            return GetIndexScalarRecords()
                .Select(record => record.MapRecord(indexToGradeIndexMapping))
                .CreateLinVectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, RGaGradeKvIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
        {
            return GetIndexScalarRecords()
                .Select(record => record.MapRecord(indexScalarToGradeIndexScalarMapping))
                .CreateLinVectorGradedStorage();
        }

        public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
        {
            if (_leafNodeIDsList.Count == 0)
            {
                vectorStorage = LinVectorEmptyStorage<T>.EmptyStorage;
                return true;
            }

            if (_leafNodeIDsList.Count == 1)
            {
                var index = _leafNodeIDsList[0];
                var value = _leafNodeScalarsArray[0];

                vectorStorage = index == 0UL
                    ? new LinVectorSingleScalarDenseStorage<T>(value)
                    : new LinVectorSingleScalarSparseStorage<T>(index, value);

                return true;
            }

            vectorStorage = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> GetTreeCopy()
        {
            return new LinVectorTreeStorage<T>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeScalarsArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTreeStorage<T> GetTreeCopy(T[] leafNodeScalarsArray)
        {
            if (leafNodeScalarsArray.Length != _leafNodeScalarsArray.Length)
                throw new InvalidOperationException();

            return new LinVectorTreeStorage<T>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeScalarsArray
            );
        }


        public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
        {
            for (var i = 0; i < GetSparseCount(); i++)
                yield return new RGaKvIndexScalarRecord<T>(
                    _leafNodeIDsList[i], 
                    _leafNodeScalarsArray[i]
                );
        }

        public override string ToString()
        {
            var composer = new StringBuilder();

            var stack = new Stack<Pair<int>>();
            stack.Push(new Pair<int>(TreeDepth, 0));

            while (stack.Count > 0)
            {
                var (treeDepth, index) = stack.Pop();
                var prefixText = "".PadLeft(2 * (TreeDepth - treeDepth));

                if (treeDepth == 0)
                {
                    var leafNodeId = _leafNodeIDsList[index];
                    var leafNodeScalar = _leafNodeScalarsArray[index];

                    composer
                        .AppendLine($"{prefixText}Leaf <Index = {index}, ID = {leafNodeId}, Scalar = {leafNodeScalar}>");

                    continue;
                }

                composer
                    .AppendLine($"{prefixText}Node <Index = {index}>");

                var (childNodeIndex0, childNodeIndex1) = _internalNodesList[index];

                if (childNodeIndex1 >= 0)
                    stack.Push(
                        new Pair<int>(treeDepth - 1, childNodeIndex1)
                    );

                if (childNodeIndex0 >= 0)
                    stack.Push(
                        new Pair<int>(treeDepth - 1, childNodeIndex0)
                    );
            }

            return composer.ToString();
        }
    }
}
