using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresLib.SimpleTree
{
    /// <summary>
    /// A simple tree node that may have several child branches organized as a list
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    [Serializable]
    public sealed class SimpleTreeBranchList<TLeaf> : SimpleTreeNode<TLeaf>, IList<SimpleTreeBranch<TLeaf>>
    {
        private readonly List<SimpleTreeBranch<TLeaf>> _branches = new List<SimpleTreeBranch<TLeaf>>();


        public int IndexOf(SimpleTreeBranch<TLeaf> item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, SimpleTreeBranch<TLeaf> item)
        {
            _branches.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _branches.RemoveAt(index);
        }

        public SimpleTreeBranch<TLeaf> this[int index]
        {
            get => _branches[index];
            set => _branches[index] = value;
        }

        public void Add(string branchName, string branchType, TLeaf value)
        {
            var item = new SimpleTreeBranch<TLeaf>(_branches.Count, branchName, branchType, value);

            _branches.Add(item);
        }

        public void Add(string branchName, string branchType, SimpleTreeNode<TLeaf> branchNode)
        {
            var item = new SimpleTreeBranch<TLeaf>(_branches.Count, branchName, branchType, branchNode);

            _branches.Add(item);
        }

        public void Add(SimpleTreeBranch<TLeaf> item)
        {
            _branches.Add(item);
        }

        public void Clear()
        {
            _branches.Clear();
        }

        public bool Contains(SimpleTreeBranch<TLeaf> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(SimpleTreeBranch<TLeaf>[] array, int arrayIndex)
        {
            foreach (var item in _branches)
                array[arrayIndex++] = item;
        }

        public int Count => _branches.Count;

        public bool IsReadOnly => false;

        public bool Remove(SimpleTreeBranch<TLeaf> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<SimpleTreeBranch<TLeaf>> GetEnumerator()
        {
            return _branches.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _branches.GetEnumerator();
        }

        public override IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes
        {
            get { return _branches.Select(item => ReferenceEquals(item, null) ? null : item.BranchNode); }
        }

        public override IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches => Enumerable.Empty<SimpleTreeBranch<TLeaf>>();

        //public override void ToString(LinearTextComposer textBuilder)
        //{
        //    textBuilder.Append("{");
        //    textBuilder.IncreaseIndentation();

        //    foreach (var item in _branches)
        //        item.ToString(textBuilder);

        //    textBuilder.DecreaseIndentation();
        //    textBuilder.AppendAtNewLine("}");
        //}
    }
}
