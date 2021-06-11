using System;
using System.Collections.Generic;

namespace DataStructuresLib.SimpleTree
{
    /// <summary>
    /// A simple tree is a node with possibly several child nodes of the same base class type.
    /// In a simple tree only a parent can have reference to the child node.
    /// Several kinds of nodes exist derived from this base class
    /// </summary>
    /// <typeparam name="TLeaf"></typeparam>
    [Serializable]
    public abstract class SimpleTreeNode<TLeaf>
    {
        public abstract IEnumerable<SimpleTreeNode<TLeaf>> ChildNodes { get; }

        public abstract IEnumerable<SimpleTreeBranch<TLeaf>> ChildBranches { get; }


        public bool IsLeaf 
            => this is SimpleTreeLeaf<TLeaf>;

        public bool IsBranchDictionaryByName 
            => this is SimpleTreeBranchDictionaryByName<TLeaf>;

        public bool IsBranchDictionaryByIndex 
            => this is SimpleTreeBranchDictionaryByIndex<TLeaf>;

        public bool IsBranchList 
            => this is SimpleTreeBranchList<TLeaf>;

        public bool IsNodeDictionaryByName 
            => this is SimpleTreeNodeDictionaryByName<TLeaf>;

        public bool IsNodeDictionaryByIndex 
            => this is SimpleTreeNodeDictionaryByIndex<TLeaf>;

        public bool IsNodeList 
            => this is SimpleTreeNodeList<TLeaf>;


        public SimpleTreeLeaf<TLeaf> AsLeaf 
            => this as SimpleTreeLeaf<TLeaf>;

        public SimpleTreeBranchDictionaryByName<TLeaf> AsBranchDictionaryByName 
            => this as SimpleTreeBranchDictionaryByName<TLeaf>;

        public SimpleTreeBranchDictionaryByIndex<TLeaf> AsBranchDictionaryByIndex 
            => this as SimpleTreeBranchDictionaryByIndex<TLeaf>;

        public SimpleTreeBranchList<TLeaf> AsBranchList 
            => this as SimpleTreeBranchList<TLeaf>;

        public SimpleTreeNodeDictionaryByName<TLeaf> AsNodeDictionaryByName 
            => this as SimpleTreeNodeDictionaryByName<TLeaf>;

        public SimpleTreeNodeDictionaryByIndex<TLeaf> AsNodeDictionaryByIndex 
            => this as SimpleTreeNodeDictionaryByIndex<TLeaf>;

        public SimpleTreeNodeList<TLeaf> AsNodeList 
            => this as SimpleTreeNodeList<TLeaf>;


        //public abstract void ToString(LinearTextComposer textBuilder);

        //public override string ToString()
        //{
        //    var textBuilder = new LinearTextComposer();

        //    ToString(textBuilder);

        //    return textBuilder.ToString();
        //}
    }
}
