using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    /// <summary>
    /// This class contains an internal read-only list of type T and a binary tree
    /// index with fixed number of levels. The structure of this tree is fixed but the values
    /// in the leaf nodes can be updated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class GaListEvenTree<T> : 
        IGaListEvenSparse<T>
    {
        private readonly List<Pair<int>> _internalNodesList;

        private readonly IReadOnlyList<ulong> _leafNodeIDsList;

        private T[] _leafNodeValuesArray;


        public int TreeDepth { get; }

        public T DefaultValue { get; set; }
            = default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _leafNodeIDsList.Count;
        }
        
        public T this[int id] 
        {
            get => TryGetLeafNodeIndex((ulong) id, out var index)
                ? _leafNodeValuesArray[index] : DefaultValue;
            set
            {
                if (TryGetLeafNodeIndex((ulong) id, out var index))
                    _leafNodeValuesArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        public T this[ulong id] 
        {
            get => TryGetLeafNodeIndex(id, out var index)
                ? _leafNodeValuesArray[index] : DefaultValue;
            set
            {
                if (TryGetLeafNodeIndex(id, out var index))
                    _leafNodeValuesArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong id)
        {
            return TryGetLeafNodeIndex(id, out var index)
                ? _leafNodeValuesArray[index]
                : DefaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            return _leafNodeIDsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _leafNodeValuesArray;
        }

        public int RootNodeIndex
            => 0;

        public Pair<int> RootNode 
            => _internalNodesList[0];


        private GaListEvenTree(int treeDepth, List<Pair<int>> internalNodesList, IReadOnlyList<ulong> leafNodeIDsList, T[] leafNodeValuesArray)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _internalNodesList = internalNodesList;

            _leafNodeValuesArray = new T[_leafNodeIDsList.Count];

            leafNodeValuesArray.CopyTo(_leafNodeValuesArray, 0);

            ConstructIndexTree();
        }

        internal GaListEvenTree(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeValuesArray = new T[_leafNodeIDsList.Count];
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal GaListEvenTree(int treeDepth, IReadOnlyDictionary<ulong, T> leafNodes)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodes.Keys.ToArray();
            _leafNodeValuesArray = leafNodes.Values.ToArray();
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal GaListEvenTree(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<T> leafNodeValuesList)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            if (leafNodeIDsList.Count != leafNodeValuesList.Count)
                throw new InvalidOperationException();

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeValuesArray = leafNodeValuesList.ToArray();
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }
        
        internal GaListEvenTree(int treeDepth, GaListEvenTree<T> binaryTree)
        {
            Debug.Assert(treeDepth >= binaryTree.TreeDepth);

            TreeDepth = treeDepth;

            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeValuesArray = new T[binaryTree.GetSparseCount()];
            binaryTree._leafNodeValuesArray.CopyTo(_leafNodeValuesArray, 0);

            if (treeDepth == binaryTree.TreeDepth)
            {
                _internalNodesList = binaryTree._internalNodesList;

                return;
            }

            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal GaListEvenTree(GaListEvenTree<T> binaryTree)
        {
            TreeDepth = binaryTree.TreeDepth;
            _internalNodesList = binaryTree._internalNodesList;
            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeValuesArray = new T[binaryTree.GetSparseCount()];
            binaryTree._leafNodeValuesArray.CopyTo(_leafNodeValuesArray, 0);
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
        public GaListEvenTree<T> ClearValues()
        {
            _leafNodeValuesArray = new T[GetSparseCount()];
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetInternalNodeByIndex(int index)
        {
            return _internalNodesList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyValue<T> GetLeafNodeByIndex(int index)
        {
            return new(
                _leafNodeIDsList[index],
                _leafNodeValuesArray[index]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetLeafNodeIdByIndex(int index)
        {
            return _leafNodeIDsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetLeafNodeValueByIndex(int index)
        {
            return _leafNodeValuesArray[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenTree<T> SetLeafNodeValueByIndex(int index, T value)
        {
            _leafNodeValuesArray[index] = value;

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
        public bool ContainsKey(ulong key)
        {
            return ContainsId(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong id, out T value)
        {
            if (TryGetLeafNodeIndex(id, out var index))
            {
                value = _leafNodeValuesArray[index];
                return true;
            }

            value = DefaultValue;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetLeafNodeIndexValue(ulong id, out int index, out T value)
        {
            if (TryGetLeafNodeIndex(id, out index))
            {
                value = _leafNodeValuesArray[index];
                return true;
            }

            value = DefaultValue;
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
        
        public IEnumerable<KeyValuePair<ulong, T>> GetLeafNodes()
        {
            for (var i = 0; i < GetSparseCount(); i++)
                yield return new KeyValuePair<ulong, T>(
                    _leafNodeIDsList[i], 
                    _leafNodeValuesArray[i]
                );
        }


        public GaListEvenTree<T> UpdateValuesById(Func<ulong, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeValuesArray[i] = mappingFunc(id);
            }

            return this;
        }

        public GaListEvenTree<T> UpdateValuesByIdValue(Func<ulong, T, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeValuesArray[i] = mappingFunc(id, _leafNodeValuesArray[i]);
            }

            return this;
        }

        public GaListEvenTree<T> UpdateValuesByIndex(Func<int, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
                _leafNodeValuesArray[i] = mappingFunc(i);

            return this;
        }

        public GaListEvenTree<T> UpdateValuesByIndexValue(Func<int, T, T> mappingFunc)
        {
            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
                _leafNodeValuesArray[i] = mappingFunc(i, _leafNodeValuesArray[i]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenTree<T> UpdateValues(Func<T, T> mappingFunc)
        {
            _leafNodeValuesArray = _leafNodeValuesArray.Select(mappingFunc).ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetLeafNodeValuesArrayCopy()
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            _leafNodeValuesArray.CopyTo(leafNodeValuesArray, 0);

            return leafNodeValuesArray;
        }

        public T[] GetLeafNodeValuesArrayCopyById(Func<ulong, T> mappingFunc)
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeValuesArray[i] = mappingFunc(id);
            }

            return leafNodeValuesArray;
        }

        public T[] GetLeafNodeValuesArrayCopyByIdValue(Func<ulong, T, T> mappingFunc)
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeValuesArray[i] = mappingFunc(id, _leafNodeValuesArray[i]);
            }

            return leafNodeValuesArray;
        }

        public T[] GetLeafNodeValuesArrayCopyByIndex(Func<int, T> mappingFunc)
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
                leafNodeValuesArray[i] = mappingFunc(i);

            return leafNodeValuesArray;
        }

        public T[] GetLeafNodeValuesArrayCopyByIndexValue(Func<int, T, T> mappingFunc)
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
                leafNodeValuesArray[i] = mappingFunc(i, _leafNodeValuesArray[i]);

            return leafNodeValuesArray;
        }

        public T[] GetLeafNodeValuesArrayCopyByIndexValue(Func<T, T> mappingFunc)
        {
            var leafNodeValuesArray = new T[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
                leafNodeValuesArray[i] = mappingFunc(_leafNodeValuesArray[i]);

            return leafNodeValuesArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _leafNodeIDsList.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey()
        {
            return _leafNodeIDsList.Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey()
        {
            return _leafNodeIDsList.Max();
        }

        public IEnumerable<ulong> GetEmptyKeys(ulong maxKey)
        {
            var keysList = maxKey.GetRange().ToList();

            foreach (var key in _leafNodeIDsList)
                keysList.Remove(key);

            return keysList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetCopy()
        {
            return GetTreeCopy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            return new GaListEvenTree<T>(
                TreeDepth,
                _leafNodeIDsList.Select(keyMapping).ToArray(),
                _leafNodeValuesArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListEvenTree<T2>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeValuesArray.Select(valueMapping).ToArray()
            );
        }

        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var leafNodeValuesArray = new T2[_leafNodeValuesArray.Length];

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                leafNodeValuesArray[i] = keyValueMapping(
                    _leafNodeIDsList[i],
                    _leafNodeValuesArray[i]
                );
            }

            return new GaListEvenTree<T2>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeValuesArray
            );
        }

        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeValuesList = new List<T>();

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                if (!keyFilter(id)) continue;

                leafNodeIDsList.Add(id);
                leafNodeValuesList.Add(_leafNodeValuesArray[i]);
            }

            return new GaListEvenTree<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeValuesList
            );
        }

        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeValuesList = new List<T>();

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];
                var value = _leafNodeValuesArray[i];

                if (!keyValueFilter(id, value)) continue;

                leafNodeIDsList.Add(id);
                leafNodeValuesList.Add(value);
            }

            return new GaListEvenTree<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeValuesList
            );
        }

        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeValuesList = new List<T>();

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var value = _leafNodeValuesArray[i];

                if (!valueFilter(value)) continue;

                leafNodeIDsList.Add(_leafNodeIDsList[i]);
                leafNodeValuesList.Add(value);
            }

            return new GaListEvenTree<T>(
                TreeDepth,
                leafNodeIDsList,
                leafNodeValuesList
            );
        }

        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var i = 0; i < _leafNodeValuesArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];
                var value = _leafNodeValuesArray[i];

                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = value;
                else
                    keyValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            if (_leafNodeIDsList.Count == 0)
            {
                evenList = GaListEvenEmpty<T>.EmptyList;
                return true;
            }

            if (_leafNodeIDsList.Count == 1)
            {
                var key = _leafNodeIDsList[0];
                var value = _leafNodeValuesArray[0];

                evenList = key == 0UL
                    ? new GaListEvenSingleKeyZero<T>(value)
                    : new GaListEvenSingleKey<T>(key, value);

                return true;
            }

            evenList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenTree<T> GetTreeCopy()
        {
            return new GaListEvenTree<T>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeValuesArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenTree<T> GetTreeCopy(T[] leafNodeValuesArray)
        {
            if (leafNodeValuesArray.Length != _leafNodeValuesArray.Length)
                throw new InvalidOperationException();

            return new GaListEvenTree<T>(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeValuesArray
            );
        }


        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            for (var i = 0; i < GetSparseCount(); i++)
                yield return new GaRecordKeyValue<T>(
                    _leafNodeIDsList[i], 
                    _leafNodeValuesArray[i]
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
                    var leafNodeValue = _leafNodeValuesArray[index];

                    composer
                        .AppendLine($"{prefixText}Leaf <Index = {index}, ID = {leafNodeId}, Value = {leafNodeValue}>");

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
