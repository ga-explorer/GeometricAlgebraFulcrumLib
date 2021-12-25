using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.SimpleTree
{
    /// <summary>
    /// A simple tree node that may have several child branches organized as a dictionary with integer keys
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    [Serializable]
    public sealed class SimpleTreeBranchDictionaryByIndex<TLeaf>
        : SimpleTreeNode<TLeaf>, IDictionary<int, SimpleTreeBranch<TLeaf>>
    {
        private readonly Dictionary<int, SimpleTreeBranch<TLeaf>> _branches =
            new Dictionary<int, SimpleTreeBranch<TLeaf>>();


        public void Add(int branchIndex, string branchName, string branchType, TLeaf branchValue)
        {
            var branch = new SimpleTreeBranch<TLeaf>(branchIndex, branchName, branchType, branchValue);

            _branches.Add(branchIndex, branch);
        }

        public void Add(int branchIndex, string branchName, string branchType, SimpleTreeNode<TLeaf> branchValue)
        {
            var branch = new SimpleTreeBranch<TLeaf>(branchIndex, branchName, branchType, branchValue);

            _branches.Add(branchIndex, branch);
        }

        public void Add(SimpleTreeBranch<TLeaf> branch)
        {
            _branches.Add(branch.BranchIndex, branch);
        }

        public void Add(int key, SimpleTreeBranch<TLeaf> value)
        {
            _branches.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return _branches.ContainsKey(key);
        }

        public ICollection<int> Keys => _branches.Keys;

        public bool Remove(int key)
        {
            return _branches.Remove(key);
        }

        public bool TryGetValue(int key, out SimpleTreeBranch<TLeaf> value)
        {
            return _branches.TryGetValue(key, out value);
        }

        public ICollection<SimpleTreeBranch<TLeaf>> Values => _branches.Values;

        public SimpleTreeBranch<TLeaf> this[int key]
        {
            get => _branches[key];
            set => _branches[key] = value;
        }

        public void Add(KeyValuePair<int, SimpleTreeBranch<TLeaf>> item)
        {
            _branches.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _branches.Clear();
        }

        public bool Contains(KeyValuePair<int, SimpleTreeBranch<TLeaf>> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<int, SimpleTreeBranch<TLeaf>>[] array, int arrayIndex)
        {
            foreach (var pair in _branches)
                array[arrayIndex++] = pair;
        }

        public int Count => _branches.Count;

        public bool IsReadOnly => false;

        public bool Remove(KeyValuePair<int, SimpleTreeBranch<TLeaf>> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<int, SimpleTreeBranch<TLeaf>>> GetEnumerator()
        {
            return _branches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _branches.GetEnumerator();
        }

        public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes
        {
            get { return _branches.Select(pair => ReferenceEquals(pair.Value, null) ? null : pair.Value.BranchNode); }
        }

        public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches
        {
            get { return _branches.Select(pair => pair.Value); }
        }


        //public override void ToString(LinearTextComposer textBuilder)
        //{
        //    textBuilder.AppendLine("{");
        //    textBuilder.IncreaseIndentation();

        //    var flag = false;
        //    foreach (var pair in _branches)
        //    {
        //        if (flag)
        //            textBuilder.AppendLine(",");
        //        else
        //            flag = true;

        //        pair.Value.ToString(textBuilder);
        //    }

        //    textBuilder.DecreaseIndentation();
        //    textBuilder.AppendAtNewLine("}");
        //}
    }
}
