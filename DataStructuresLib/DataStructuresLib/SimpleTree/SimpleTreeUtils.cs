using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataStructuresLib.SimpleTree
{
    public static class SimpleTreeUtils
    {
        /// <summary>
        /// Save a simple tree node to a file using serialization
        /// </summary>
        /// <typeparam name="TLeaf"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="filePath"></param>
        public static void SaveToFile<TLeaf>(this SimpleTreeNode<TLeaf> treeNode, string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Create, FileAccess.Write))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, treeNode);
            }
        }

        /// <summary>
        /// Load a simple tree node from a file using serialization
        /// </summary>
        /// <typeparam name="TLeaf"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static SimpleTreeNode<TLeaf> LoadFromFile<TLeaf>(string filePath)
        {
            SimpleTreeNode<TLeaf> treeNode;

            using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                var formatter = new BinaryFormatter();

                treeNode = (SimpleTreeNode<TLeaf>)formatter.Deserialize(stream);
            }

            return treeNode;
        }


        /// <summary>
        /// Create an exact copy of the given tree branch but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeBranch"></param>
        /// <returns></returns>
        public static SimpleTreeBranch<T2> Cast<T1, T2>(this SimpleTreeBranch<T1> treeBranch)
            where T1 : class
            where T2 : class
        {
            return new SimpleTreeBranch<T2>(
                treeBranch.BranchIndex,
                treeBranch.BranchName,
                treeBranch.BranchType,
                Cast<T1, T2>(treeBranch.BranchNode)
                );
        }

        /// <summary>
        /// Create an exact copy of the given tree branch but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeBranch"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeBranch<T2> Cast<T1, T2>(this SimpleTreeBranch<T1> treeBranch, Func<T1, T2> castFunc)
        {
            return new SimpleTreeBranch<T2>(
                treeBranch.BranchIndex,
                treeBranch.BranchName,
                treeBranch.BranchType,
                Cast(treeBranch.BranchNode, castFunc)
                );
        }

        /// <summary>
        /// Create an exact copy of the given tree branch but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeBranch"></param>
        /// <returns></returns>
        public static SimpleTreeBranch<string> ToStringTree<T1>(this SimpleTreeBranch<T1> treeBranch)
        {
            return new SimpleTreeBranch<string>(
                treeBranch.BranchIndex,
                treeBranch.BranchName,
                treeBranch.BranchType,
                ToStringTree(treeBranch.BranchNode)
                );
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace the leaf value of type T1 by a value
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeLeaf<T2> Cast<T1, T2>(this SimpleTreeLeaf<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            return new SimpleTreeLeaf<T2>(treeNode.Value as T2);
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace the leaf value of type T1 by a value
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeLeaf<T2> Cast<T1, T2>(this SimpleTreeLeaf<T1> treeNode, Func<T1, T2> castFunc)
        {
            return new SimpleTreeLeaf<T2>(castFunc(treeNode.Value));
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace the leaf value of type T1 by a value
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeLeaf<string> ToStringTree<T1>(this SimpleTreeLeaf<T1> treeNode)
        {
            return new SimpleTreeLeaf<string>(treeNode.Value.ToString());
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByIndex<T2> Cast<T1, T2>(this SimpleTreeBranchDictionaryByIndex<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByIndex<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast<T1, T2>(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByIndex<T2> Cast<T1, T2>(this SimpleTreeBranchDictionaryByIndex<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByIndex<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast(pair.Value, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByIndex<string> ToStringTree<T1>(this SimpleTreeBranchDictionaryByIndex<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByIndex<string>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, ToStringTree(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByName<T2> Cast<T1, T2>(this SimpleTreeBranchDictionaryByName<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByName<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast<T1, T2>(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByName<T2> Cast<T1, T2>(this SimpleTreeBranchDictionaryByName<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByName<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast(pair.Value, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchDictionaryByName<string> ToStringTree<T1>(this SimpleTreeBranchDictionaryByName<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeBranchDictionaryByName<string>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, ToStringTree(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchList<T2> Cast<T1, T2>(this SimpleTreeBranchList<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeBranchList<T2>();

            foreach (var item in treeNode)
                newTreeNode.Add(Cast<T1, T2>(item));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeBranchList<T2> Cast<T1, T2>(this SimpleTreeBranchList<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeBranchList<T2>();

            foreach (var item in treeNode)
                newTreeNode.Add(Cast(item, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeBranchList<string> ToStringTree<T1>(this SimpleTreeBranchList<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeBranchList<string>();

            foreach (var item in treeNode)
                newTreeNode.Add(ToStringTree(item));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByIndex<T2> Cast<T1, T2>(this SimpleTreeNodeDictionaryByIndex<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByIndex<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast<T1, T2>(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByIndex<T2> Cast<T1, T2>(this SimpleTreeNodeDictionaryByIndex<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByIndex<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast(pair.Value, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByIndex<string> ToStringTree<T1>(this SimpleTreeNodeDictionaryByIndex<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByIndex<string>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, ToStringTree(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByName<T2> Cast<T1, T2>(this SimpleTreeNodeDictionaryByName<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByName<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast<T1, T2>(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByName<T2> Cast<T1, T2>(this SimpleTreeNodeDictionaryByName<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByName<T2>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, Cast(pair.Value, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeDictionaryByName<string> ToStringTree<T1>(this SimpleTreeNodeDictionaryByName<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeNodeDictionaryByName<string>();

            foreach (var pair in treeNode)
                newTreeNode.Add(pair.Key, ToStringTree(pair.Value));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeList<T2> Cast<T1, T2>(this SimpleTreeNodeList<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var newTreeNode = new SimpleTreeNodeList<T2>();

            foreach (var item in treeNode)
                newTreeNode.Add(Cast<T1, T2>(item));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeNodeList<T2> Cast<T1, T2>(this SimpleTreeNodeList<T1> treeNode, Func<T1, T2> castFunc)
        {
            var newTreeNode = new SimpleTreeNodeList<T2>();

            foreach (var item in treeNode)
                newTreeNode.Add(Cast(item, castFunc));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNodeList<string> ToStringTree<T1>(this SimpleTreeNodeList<T1> treeNode)
        {
            var newTreeNode = new SimpleTreeNodeList<string>();

            foreach (var item in treeNode)
                newTreeNode.Add(ToStringTree(item));

            return newTreeNode;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the as operator
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNode<T2> Cast<T1, T2>(this SimpleTreeNode<T1> treeNode)
            where T1 : class
            where T2 : class
        {
            var node1 = treeNode.AsLeaf;

            if (ReferenceEquals(node1, null) == false)
                return Cast<T1, T2>(node1);

            var node2 = treeNode.AsBranchDictionaryByIndex;

            if (ReferenceEquals(node2, null) == false)
                return Cast<T1, T2>(node2);

            var node3 = treeNode.AsBranchDictionaryByName;

            if (ReferenceEquals(node3, null) == false)
                return Cast<T1, T2>(node3);

            var node4 = treeNode.AsBranchList;

            if (ReferenceEquals(node4, null) == false)
                return Cast<T1, T2>(node4);

            var node5 = treeNode.AsNodeDictionaryByIndex;

            if (ReferenceEquals(node5, null) == false)
                return Cast<T1, T2>(node5);

            var node6 = treeNode.AsNodeDictionaryByName;

            if (ReferenceEquals(node6, null) == false)
                return Cast<T1, T2>(node6);

            var node7 = treeNode.AsNodeList;

            if (ReferenceEquals(node7, null) == false)
                return Cast<T1, T2>(node7);

            return null;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type T2 using the given cast function
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="treeNode"></param>
        /// <param name="castFunc"></param>
        /// <returns></returns>
        public static SimpleTreeNode<T2> Cast<T1, T2>(this SimpleTreeNode<T1> treeNode, Func<T1, T2> castFunc)
        {
            var node1 = treeNode.AsLeaf;

            if (ReferenceEquals(node1, null) == false)
                return Cast(node1, castFunc);

            var node2 = treeNode.AsBranchDictionaryByIndex;

            if (ReferenceEquals(node2, null) == false)
                return Cast(node2, castFunc);

            var node3 = treeNode.AsBranchDictionaryByName;

            if (ReferenceEquals(node3, null) == false)
                return Cast(node3, castFunc);

            var node4 = treeNode.AsBranchList;

            if (ReferenceEquals(node4, null) == false)
                return Cast(node4, castFunc);

            var node5 = treeNode.AsNodeDictionaryByIndex;

            if (ReferenceEquals(node5, null) == false)
                return Cast(node5, castFunc);

            var node6 = treeNode.AsNodeDictionaryByName;

            if (ReferenceEquals(node6, null) == false)
                return Cast(node6, castFunc);

            var node7 = treeNode.AsNodeList;

            if (ReferenceEquals(node7, null) == false)
                return Cast(node7, castFunc);

            return null;
        }

        /// <summary>
        /// Create an exact copy of the given tree node but replace all leaf values of type T1 by values
        /// of type string using the ToString() method
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public static SimpleTreeNode<string> ToStringTree<T1>(this SimpleTreeNode<T1> treeNode)
        {
            var node1 = treeNode.AsLeaf;

            if (ReferenceEquals(node1, null) == false)
                return ToStringTree(node1);

            var node2 = treeNode.AsBranchDictionaryByIndex;

            if (ReferenceEquals(node2, null) == false)
                return ToStringTree(node2);

            var node3 = treeNode.AsBranchDictionaryByName;

            if (ReferenceEquals(node3, null) == false)
                return ToStringTree(node3);

            var node4 = treeNode.AsBranchList;

            if (ReferenceEquals(node4, null) == false)
                return ToStringTree(node4);

            var node5 = treeNode.AsNodeDictionaryByIndex;

            if (ReferenceEquals(node5, null) == false)
                return ToStringTree(node5);

            var node6 = treeNode.AsNodeDictionaryByName;

            if (ReferenceEquals(node6, null) == false)
                return ToStringTree(node6);

            var node7 = treeNode.AsNodeList;

            if (ReferenceEquals(node7, null) == false)
                return ToStringTree(node7);

            return null;
        }
    }
}
