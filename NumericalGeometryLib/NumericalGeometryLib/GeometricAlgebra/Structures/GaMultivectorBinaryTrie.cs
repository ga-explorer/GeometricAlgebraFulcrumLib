using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;

namespace NumericalGeometryLib.GeometricAlgebra.Structures
{
    /// <summary>
    /// This class contains an internal read-only list of type double and a binary tree
    /// index with fixed number of levels. The structure of this tree is fixed but the values
    /// in the leaf nodes can be updated.
    /// </summary>
    public sealed class GaMultivectorBinaryTrie : 
        IReadOnlyCollection<double>
    {
        private readonly List<Pair<int>> _internalNodesList;

        private readonly IReadOnlyList<ulong> _leafNodeIDsList;

        private double[] _leafNodeScalarsArray;


        public int Count 
            => _leafNodeScalarsArray.Length;

        public int TreeDepth { get; }

        public uint VSpaceDimension 
            => (uint) TreeDepth;

        public ulong GaSpaceDimension 
            => 1UL << TreeDepth;

        public double DefaultScalar { get; set; }
            = default;
        
        public double this[int id] 
        {
            get => 
                TryGetLeafNodeIndex((ulong) id, out var index)
                    ? _leafNodeScalarsArray[index] 
                    : DefaultScalar;
            set
            {
                if (TryGetLeafNodeIndex((ulong) id, out var index))
                    _leafNodeScalarsArray[index] = value;

                throw new IndexOutOfRangeException();
            }
        }

        public double this[ulong id] 
        {
            get => 
                TryGetLeafNodeIndex(id, out var index)
                    ? _leafNodeScalarsArray[index] 
                    : DefaultScalar;
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


        private GaMultivectorBinaryTrie(int treeDepth, List<Pair<int>> internalNodesList, IReadOnlyList<ulong> leafNodeIDsList, double[] leafNodeScalarsArray)
        {
            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _internalNodesList = internalNodesList;

            _leafNodeScalarsArray = new double[_leafNodeIDsList.Count];

            leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);

            ConstructIndexTree();
        }

        internal GaMultivectorBinaryTrie(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodeIDsList;
            _leafNodeScalarsArray = new double[_leafNodeIDsList.Count];
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }
        
        internal GaMultivectorBinaryTrie(int treeDepth, IReadOnlyDictionary<ulong, double> leafNodes)
        {
            if (treeDepth < 1)
                throw new ArgumentOutOfRangeException(nameof(treeDepth));

            TreeDepth = treeDepth;
            _leafNodeIDsList = leafNodes.Keys.ToArray();
            _leafNodeScalarsArray = leafNodes.Values.ToArray();
            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal GaMultivectorBinaryTrie(int treeDepth, IReadOnlyList<ulong> leafNodeIDsList, IReadOnlyCollection<double> leafNodeScalarsList)
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
        
        internal GaMultivectorBinaryTrie(int treeDepth, GaMultivectorBinaryTrie binaryTree)
        {
            Debug.Assert(treeDepth >= binaryTree.TreeDepth);

            TreeDepth = treeDepth;

            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeScalarsArray = new double[binaryTree.Count];
            binaryTree._leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);

            if (treeDepth == binaryTree.TreeDepth)
            {
                _internalNodesList = binaryTree._internalNodesList;

                return;
            }

            _internalNodesList = new List<Pair<int>>(_leafNodeIDsList.Count);

            ConstructIndexTree();
        }

        internal GaMultivectorBinaryTrie(GaMultivectorBinaryTrie binaryTree)
        {
            TreeDepth = binaryTree.TreeDepth;
            _internalNodesList = binaryTree._internalNodesList;
            _leafNodeIDsList = binaryTree._leafNodeIDsList;
            _leafNodeScalarsArray = new double[binaryTree.Count];
            binaryTree._leafNodeScalarsArray.CopyTo(_leafNodeScalarsArray, 0);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetScalar(ulong id)
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
        public IEnumerable<double> GetScalars()
        {
            return _leafNodeScalarsArray;
        }

        private void ConstructIndexTree()
        {
            var emptyInternalNode = new Pair<int>(-1, -1);

            _internalNodesList.Clear();
            _internalNodesList.Add(new Pair<int>(-1, -1));

            for (var i = 0; i < Count; i++)
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
        public GaMultivectorBinaryTrie ClearScalars()
        {
            _leafNodeScalarsArray = new double[Count];
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<int> GetInternalNodeByIndex(int index)
        {
            return _internalNodesList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaIdScalarRecord GetLeafNodeByIndex(int index)
        {
            return new GaIdScalarRecord(
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
        public double GetLeafNodeScalarByIndex(int index)
        {
            return _leafNodeScalarsArray[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie SetLeafNodeScalarByIndex(int index, double value)
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
        public bool TryGetScalar(ulong id, out double value)
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
        public bool TryGetLeafNodeIndexScalar(ulong id, out int index, out double value)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaIdScalarRecord> GetIdScalarRecords()
        {
            for (var i = 0; i < Count; i++)
                yield return new GaIdScalarRecord(
                    _leafNodeIDsList[i], 
                    _leafNodeScalarsArray[i]
                );
        }


        public GaMultivectorBinaryTrie UpdateScalarsById(Func<ulong, double> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeScalarsArray[i] = mappingFunc(id);
            }

            return this;
        }

        public GaMultivectorBinaryTrie UpdateScalarsByIdScalar(Func<ulong, double, double> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                _leafNodeScalarsArray[i] = mappingFunc(id, _leafNodeScalarsArray[i]);
            }

            return this;
        }

        public GaMultivectorBinaryTrie UpdateScalarsByIndex(Func<int, double> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                _leafNodeScalarsArray[i] = mappingFunc(i);

            return this;
        }

        public GaMultivectorBinaryTrie UpdateScalarsByIndexScalar(Func<int, double, double> mappingFunc)
        {
            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                _leafNodeScalarsArray[i] = mappingFunc(i, _leafNodeScalarsArray[i]);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie UpdateScalars(Func<double, double> mappingFunc)
        {
            _leafNodeScalarsArray = _leafNodeScalarsArray.Select(mappingFunc).ToArray();

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetLeafNodeScalarsArrayCopy()
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            _leafNodeScalarsArray.CopyTo(leafNodeScalarsArray, 0);

            return leafNodeScalarsArray;
        }

        public double[] GetLeafNodeScalarsArrayCopyById(Func<ulong, double> mappingFunc)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeScalarsArray[i] = mappingFunc(id);
            }

            return leafNodeScalarsArray;
        }

        public double[] GetLeafNodeScalarsArrayCopyByIdScalar(Func<ulong, double, double> mappingFunc)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                leafNodeScalarsArray[i] = mappingFunc(id, _leafNodeScalarsArray[i]);
            }

            return leafNodeScalarsArray;
        }

        public double[] GetLeafNodeScalarsArrayCopyByIndex(Func<int, double> mappingFunc)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                leafNodeScalarsArray[i] = mappingFunc(i);

            return leafNodeScalarsArray;
        }

        public double[] GetLeafNodeScalarsArrayCopyByIndexScalar(Func<int, double, double> mappingFunc)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
                leafNodeScalarsArray[i] = mappingFunc(i, _leafNodeScalarsArray[i]);

            return leafNodeScalarsArray;
        }

        public double[] GetLeafNodeScalarsArrayCopyByIndexScalar(Func<double, double> mappingFunc)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

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
        public GaMultivectorBinaryTrie GetCopy()
        {
            return GetTreeCopy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie GetPermutation(Func<ulong, ulong> indexMapping)
        {
            return new GaMultivectorBinaryTrie(
                TreeDepth,
                _leafNodeIDsList.Select(indexMapping).ToArray(),
                _leafNodeScalarsArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie MapScalars(Func<double, double> valueMapping)
        {
            return new GaMultivectorBinaryTrie(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeScalarsArray.Select(valueMapping).ToArray()
            );
        }

        public GaMultivectorBinaryTrie MapScalars(Func<ulong, double, double> indexScalarMapping)
        {
            var leafNodeScalarsArray = new double[_leafNodeScalarsArray.Length];

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                leafNodeScalarsArray[i] = indexScalarMapping(
                    _leafNodeIDsList[i],
                    _leafNodeScalarsArray[i]
                );
            }

            return new GaMultivectorBinaryTrie(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeScalarsArray
            );
        }

        public GaMultivectorBinaryTrie FilterByIndex(Func<ulong, bool> indexFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<double>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];

                if (!indexFilter(id)) continue;

                leafNodeIDsList.Add(id);
                leafNodeScalarsList.Add(_leafNodeScalarsArray[i]);
            }

            return new GaMultivectorBinaryTrie(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }

        public GaMultivectorBinaryTrie FilterByIndexScalar(Func<ulong, double, bool> indexScalarFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<double>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var id = _leafNodeIDsList[i];
                var value = _leafNodeScalarsArray[i];

                if (!indexScalarFilter(id, value)) continue;

                leafNodeIDsList.Add(id);
                leafNodeScalarsList.Add(value);
            }

            return new GaMultivectorBinaryTrie(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }

        public GaMultivectorBinaryTrie FilterByScalar(Func<double, bool> valueFilter)
        {
            var leafNodeIDsList = new List<ulong>();
            var leafNodeScalarsList = new List<double>();

            for (var i = 0; i < _leafNodeScalarsArray.Length; i++)
            {
                var value = _leafNodeScalarsArray[i];

                if (!valueFilter(value)) continue;

                leafNodeIDsList.Add(_leafNodeIDsList[i]);
                leafNodeScalarsList.Add(value);
            }

            return new GaMultivectorBinaryTrie(
                TreeDepth,
                leafNodeIDsList,
                leafNodeScalarsList
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie GetTreeCopy()
        {
            return new GaMultivectorBinaryTrie(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                _leafNodeScalarsArray
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivectorBinaryTrie GetTreeCopy(double[] leafNodeScalarsArray)
        {
            if (leafNodeScalarsArray.Length != _leafNodeScalarsArray.Length)
                throw new InvalidOperationException();

            return new GaMultivectorBinaryTrie(
                TreeDepth,
                _internalNodesList,
                _leafNodeIDsList,
                leafNodeScalarsArray
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaIdScalarRecord> GetIndexScalarRecords()
        {
            for (var i = 0; i < Count; i++)
                yield return new GaIdScalarRecord(
                    _leafNodeIDsList[i], 
                    _leafNodeScalarsArray[i]
                );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<double> GetEnumerator()
        {
            for (var i = 0UL; i < GaSpaceDimension; i++)
                yield return this[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
