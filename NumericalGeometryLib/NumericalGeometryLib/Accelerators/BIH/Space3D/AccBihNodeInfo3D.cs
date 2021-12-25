using System.Collections.Generic;
using DataStructuresLib;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space3D.Immutable;

namespace NumericalGeometryLib.Accelerators.BIH.Space3D
{
    /// <summary>
    /// This class can be used for collecting information about a BIH node
    /// and its child nodes for debugging and statistics
    /// </summary>
    public sealed class AccBihNodeInfo3D
    {
        public static AccBihNodeInfo3D Create<T>(IAccBih3D<T> bih)
            where T : IFiniteGeometricShape3D
        {
            return new AccBihNodeInfo3D(
                bih.RootNode, 
                BoundingBox3D.CreateInfinite()
            );
        }


        public IAccBihNode3D Node { get; }

        public string NodeId
        {
            get { return Node.NodeId; }
        }

        public int TreeDepth
        {
            get { return Node.NodeId.Length; }
        }

        public int NodeDepth
        {
            get { return Node.NodeDepth; }
        }

        public BoundingBox3D BoundingBox { get; }

        public bool IsLeaf
        {
            get { return Node.IsLeaf; }
        }

        public bool IsInternal
        {
            get { return Node.IsInternal; }
        }

        public bool IsSingleInternal
        {
            get { return Node.IsSingleInternal; }
        }

        public int FirstObjectIndex
        {
            get { return Node.FirstObjectIndex; }
        }

        public int LastObjectIndex
        {
            get { return Node.LastObjectIndex; }
        }

        public bool HasLeftChild
        {
            get { return Node.HasLeftChild; }
        }

        public bool HasRightChild
        {
            get { return Node.HasRightChild; }
        }

        public bool HasBoundingBox
        {
            get { return !ReferenceEquals(BoundingBox, null); }
        }

        //public bool IntersectionTestsEnabled => Node.IntersectionTestsEnabled;

        public int SplitAxisIndex
        {
            get { return Node.SplitAxisIndex; }
        }

        public string SplitAxisName
        {
            get { return Node.SplitAxisName; }
        }

        public double ClipValue0
        {
            get { return Node.ClipValue0; }
        }

        public double ClipValue1
        {
            get { return Node.ClipValue1; }
        }


        internal AccBihNodeInfo3D(IAccBihNode3D node, BoundingBox3D boundingBox)
        {
            Node = node;
            BoundingBox = boundingBox;
        }


        public BoundingBox3D GetBoundingBox()
        {
            return BoundingBox3D.Create(
                (IEnumerable<IFiniteGeometricShape3D>)Node
            );
        }

        public AccBihNodeInfo3D GetLeftChildInfo()
        {
            if (!Node.HasLeftChild)
                return null;

            return new AccBihNodeInfo3D(Node.LeftChild, Node.GetLeftChildBoundingBox(BoundingBox));
            //{
            //    //NodeId = NodeId.SetCharacterAt(TreeDepth - 1, '0'),
            //    //NodeDepth = NodeDepth + 1,
            //    //TreeDepth = TreeDepth - 1,
            //    BoundingBox = Node.GetLeftChildBoundingBox(BoundingBox)
            //};
        }

        public AccBihNodeInfo3D GetRightChildInfo()
        {
            if (!Node.HasRightChild)
                return null;

            return new AccBihNodeInfo3D(Node.RightChild, Node.GetRightChildBoundingBox(BoundingBox));
            //{
            //    //NodeId = NodeId.SetCharacterAt(TreeDepth - 1, '1'),
            //    //NodeDepth = NodeDepth + 1,
            //    //TreeDepth = TreeDepth - 1,
            //    BoundingBox = Node.GetRightChildBoundingBox(BoundingBox)
            //};
        }

        public IEnumerable<AccBihNodeInfo3D> GetNodesInfo(bool depthFirst = false)
        {
            var nodesInfoList = new QueueStack<AccBihNodeInfo3D>(depthFirst) { this };

            while (nodesInfoList.Count > 0)
            {
                var nodeInfo = nodesInfoList.Remove();

                yield return nodeInfo;

                if (nodeInfo.IsLeaf)
                    continue;

                if (nodeInfo.HasLeftChild)
                    nodesInfoList.Add(nodeInfo.GetLeftChildInfo());

                if (nodeInfo.HasRightChild)
                    nodesInfoList.Add(nodeInfo.GetRightChildInfo());
            }
        }

        public IEnumerable<AccBihNodeInfo3D> GetInternalNodesInfo(bool depthFirst = false)
        {
            var nodesInfoList = new QueueStack<AccBihNodeInfo3D>(depthFirst) { this };

            while (nodesInfoList.Count > 0)
            {
                var nodeInfo = nodesInfoList.Remove();

                if (nodeInfo.IsLeaf)
                    continue;

                yield return nodeInfo;

                if (nodeInfo.HasLeftChild)
                    nodesInfoList.Add(nodeInfo.GetLeftChildInfo());

                if (nodeInfo.HasRightChild)
                    nodesInfoList.Add(nodeInfo.GetRightChildInfo());
            }
        }

        public IEnumerable<AccBihNodeInfo3D> GetLeafNodesInfo(bool depthFirst = false)
        {
            var nodesInfoList = new QueueStack<AccBihNodeInfo3D>(depthFirst) { this };

            while (nodesInfoList.Count > 0)
            {
                var nodeInfo = nodesInfoList.Remove();

                if (nodeInfo.IsLeaf)
                {
                    yield return nodeInfo;
                    continue;
                }

                if (nodeInfo.HasLeftChild)
                    nodesInfoList.Add(nodeInfo.GetLeftChildInfo());

                if (nodeInfo.HasRightChild)
                    nodesInfoList.Add(nodeInfo.GetRightChildInfo());
            }
        }
    }
}