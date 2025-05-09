﻿using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

/// <summary>
/// This class can be used for collecting information about a BIH node
/// and its child nodes for debugging and statistics
/// </summary>
public sealed class AccBihNodeInfo2D
{
    public static AccBihNodeInfo2D Create<T>(IAccBih2D<T> bih)
        where T : IFloat64FiniteGeometricShape2D
    {
        //var maxTreeDepth = bih.RootNode.GetChildHierarchyDepth();

        var rootNodeInfo =
            new AccBihNodeInfo2D(bih.RootNode, Float64BoundingBox2D.CreateInfinite());
        //{
        //    //NodeId = "".PadLeft(maxTreeDepth, '_'),
        //    //NodeDepth = 0,
        //    //TreeDepth = maxTreeDepth,
        //    BoundingBox = BoundingBox2D.CreateInfinite()
        //};

        //var nodesStack = new Stack<IAccBihNode2D>();
        //nodesStack.Push(rootNode);

        //var infoStack = new Stack<AccBihNodeInfo2D>();
        //infoStack.Push(rootNodeInfo);

        //while (nodesStack.Count > 0)
        //{
        //    var node = nodesStack.Pop();
        //    var nodeInfo = infoStack.Pop();

        //    var nodeId = nodeInfo.NodeId;

        //    nodeInfo.NodeId = nodeId.PadLeft(maxTreeDepth, '_');

        //    if (node.IsLeaf)
        //        continue;

        //    if (node.HasLeftChild)
        //    {
        //        nodeInfo.GetLeftChildInfo =
        //            new AccBihNodeInfo2D(node.LeftChild)
        //            {
        //                NodeId = "0" + nodeId,
        //                NodeDepth = nodeInfo.NodeDepth + 1,
        //                TreeDepth = nodeInfo.TreeDepth - 1,
        //                BoundingBox = GetLeftChildBoundingBox(node, nodeInfo.BoundingBox)
        //            };

        //        nodesStack.Push(node.LeftChild);
        //        infoStack.Push(nodeInfo.GetLeftChildInfo());
        //    }

        //    if (node.HasRightChild)
        //    {
        //        nodeInfo.GetRightChildInfo =
        //            new AccBihNodeInfo2D(node.RightChild)
        //            {
        //                NodeId = "1" + nodeId,
        //                NodeDepth = nodeInfo.NodeDepth + 1,
        //                TreeDepth = nodeInfo.TreeDepth - 1,
        //                BoundingBox = GetRightChildBoundingBox(node, nodeInfo.BoundingBox)
        //            };

        //        nodesStack.Push(node.RightChild);
        //        infoStack.Push(nodeInfo.GetRightChildInfo());
        //    }
        //}

        return rootNodeInfo;
    }


    public IAccBihNode2D Node { get; }

    public string NodeId => Node.NodeId;

    public int TreeDepth => Node.NodeId.Length;

    public int NodeDepth => Node.NodeDepth;

    public Float64BoundingBox2D BoundingBox { get; }

    public bool IsLeaf => Node.IsLeaf;

    public bool IsInternal => Node.IsInternal;

    public bool IsSingleInternal => Node.IsSingleInternal;

    public int FirstObjectIndex => Node.FirstObjectIndex;

    public int LastObjectIndex => Node.LastObjectIndex;

    public bool HasLeftChild => Node.HasLeftChild;

    public bool HasRightChild => Node.HasRightChild;

    public bool HasBoundingBox => !ReferenceEquals(BoundingBox, null);

    //public bool IntersectionTestsEnabled => Node.IntersectionTestsEnabled;

    public int SplitAxisIndex => Node.SplitAxisIndex;

    public string SplitAxisName => Node.SplitAxisName;

    public double ClipValue0 => Node.ClipValue0;

    public double ClipValue1 => Node.ClipValue1;


    internal AccBihNodeInfo2D(IAccBihNode2D node, Float64BoundingBox2D boundingBox)
    {
        Node = node;
        BoundingBox = boundingBox;
    }


    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Create(
            (IEnumerable<IFloat64FiniteGeometricShape2D>)Node
        );
    }

    public AccBihNodeInfo2D GetLeftChildInfo()
    {
        if (!Node.HasLeftChild)
            return null;

        return new AccBihNodeInfo2D(
            Node.LeftChild, 
            Node.GetLeftChildBoundingBox(BoundingBox)
        );

        //{
        //    //NodeId = NodeId.SetCharacterAt(TreeDepth - 1, '0'),
        //    //NodeDepth = NodeDepth + 1,
        //    //TreeDepth = TreeDepth - 1,
        //    BoundingBox = Node.GetLeftChildBoundingBox(BoundingBox)
        //};
    }

    public AccBihNodeInfo2D GetRightChildInfo()
    {
        if (!Node.HasRightChild)
            return null;

        return new AccBihNodeInfo2D(
            Node.RightChild, 
            Node.GetRightChildBoundingBox(BoundingBox)
        );

        //{
        //    //NodeId = NodeId.SetCharacterAt(TreeDepth - 1, '1'),
        //    //NodeDepth = NodeDepth + 1,
        //    //TreeDepth = TreeDepth - 1,
        //    BoundingBox = Node.GetRightChildBoundingBox(BoundingBox)
        //};
    }

    public IEnumerable<AccBihNodeInfo2D> GetNodesInfo(bool depthFirst = false)
    {
        var nodesInfoList = new QueueStack<AccBihNodeInfo2D>(depthFirst) {this};

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

    public IEnumerable<AccBihNodeInfo2D> GetInternalNodesInfo(bool depthFirst = false)
    {
        var nodesInfoList = new QueueStack<AccBihNodeInfo2D>(depthFirst) { this };

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

    public IEnumerable<AccBihNodeInfo2D> GetLeafNodesInfo(bool depthFirst = false)
    {
        var nodesInfoList = new QueueStack<AccBihNodeInfo2D>(depthFirst) { this };

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