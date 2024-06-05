using System.Runtime.CompilerServices;

// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

public sealed class GrParametricVolumeTreeLeaf3D :
    GrParametricVolumeTreeNode3D
{
    public override int Count 
        => 0;

    public GrParametricVolumeTreeLeafFace3D FaceXX0 { get; }

    public GrParametricVolumeTreeLeafFace3D FaceXX1 { get; }

    public GrParametricVolumeTreeLeafFace3D FaceX0X { get; }

    public GrParametricVolumeTreeLeafFace3D FaceX1X { get; }
        
    public GrParametricVolumeTreeLeafFace3D Face0XX { get; }

    public GrParametricVolumeTreeLeafFace3D Face1XX { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeLeaf3D(GrParametricVolumeTreeBranch3D parentBranch, bool isChild1XX, bool isChildX1X, bool isChildXX1)
        : base(parentBranch, isChild1XX, isChildX1X, isChildXX1)
    {
        FaceXX0 = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.SideXX0
        );
            
        FaceXX1 = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.SideXX1
        );
            
        FaceX0X = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.SideX0X
        );

        FaceX1X = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.SideX1X
        );
            
        Face0XX = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.Side0XX
        );

        Face1XX = new GrParametricVolumeTreeLeafFace3D(
            this, 
            GrParametricVolumeTreeNodeSide3D.Side1XX
        );
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeBranch3D Split()
    {
        var branchNode = new GrParametricVolumeTreeBranch3D(
            ParentBranch, 
            IsChild1XX, 
            IsChildX1X,
            IsChildXX1,
            true
        );

        ParentBranch.SetChild(this);

        return branchNode;
    }


    /// <summary>
    /// Find the lower-level leaf node neighbors of this node from the given side
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<GrParametricVolumeTreeLeaf3D> GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D side)
    {
        if (!TryGetNeighbor(side, false, out var neighborNode) || neighborNode.IsLeaf)
            return Enumerable.Empty<GrParametricVolumeTreeLeaf3D>();

        var neighborBranchNode = 
            (GrParametricVolumeTreeBranch3D) neighborNode;

        return side switch
        {
            GrParametricVolumeTreeNodeSide3D.Side0XX => 
                neighborBranchNode.LeafNodes1XX,

            GrParametricVolumeTreeNodeSide3D.Side1XX => 
                neighborBranchNode.LeafNodes0XX,

            GrParametricVolumeTreeNodeSide3D.SideX0X => 
                neighborBranchNode.LeafNodesX1X,

            GrParametricVolumeTreeNodeSide3D.SideX1X => 
                neighborBranchNode.LeafNodesX0X,

            GrParametricVolumeTreeNodeSide3D.SideXX0 => 
                neighborBranchNode.LeafNodesXX1,

            _ => 
                neighborBranchNode.LeafNodesXX0
        };
    }
        
    /// <summary>
    /// Find the all leaf node neighbors of this node from the given side
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<GrParametricVolumeTreeLeaf3D> GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D side)
    {
        if (!TryGetNeighbor(side, true, out var neighborNode))
            return Enumerable.Empty<GrParametricVolumeTreeLeaf3D>();

        if (neighborNode is GrParametricVolumeTreeLeaf3D neighborLeafNode)
            return new[] { neighborLeafNode };

        var neighborBranchNode = 
            (GrParametricVolumeTreeBranch3D) neighborNode;

        return side switch
        {
            GrParametricVolumeTreeNodeSide3D.Side0XX => 
                neighborBranchNode.LeafNodes1XX,

            GrParametricVolumeTreeNodeSide3D.Side1XX => 
                neighborBranchNode.LeafNodes0XX,

            GrParametricVolumeTreeNodeSide3D.SideX0X => 
                neighborBranchNode.LeafNodesX1X,

            GrParametricVolumeTreeNodeSide3D.SideX1X => 
                neighborBranchNode.LeafNodesX0X,

            GrParametricVolumeTreeNodeSide3D.SideXX0 => 
                neighborBranchNode.LeafNodesXX1,

            _ => 
                neighborBranchNode.LeafNodesXX0
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<GrParametricVolumeTreeLeaf3D> GetLeafNeighbors()
    {
        var leafNeighborsList = new List<GrParametricVolumeTreeLeaf3D>();

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideXX0)
        );

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideXX1)
        );

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideX0X)
        );

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideX1X)
        );

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.Side0XX)
        );

        leafNeighborsList.AddRange(
            GetLeafNeighbors(GrParametricVolumeTreeNodeSide3D.Side1XX)
        );

        return leafNeighborsList;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricVolumeTreeLeaf3D ClearFaceData()
    {
        FaceXX0.Clear();
        FaceXX1.Clear();
        FaceX0X.Clear();
        FaceX1X.Clear();
        Face0XX.Clear();
        Face1XX.Clear();

        return this;
    }

    public GrParametricVolumeTreeLeaf3D GenerateFaceData()
    {
        FaceXX0.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX0, this);
        FaceXX1.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX1, this);
        FaceX0X.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideX0X, this);
        FaceX1X.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideX1X, this);
        FaceXX0.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX0, this);
        FaceXX1.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX1, this);
        
        if (Level == ParentTree.TreeLevelCount)
            return this;

        var leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideXX0);

        foreach (var neighborsLeafNode in leafNeighbors)
            FaceXX0.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX1, neighborsLeafNode);
            
        leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideXX1);

        foreach (var neighborsLeafNode in leafNeighbors)
            FaceXX1.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideXX0, neighborsLeafNode);


        leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideX0X);

        foreach (var neighborsLeafNode in leafNeighbors)
            FaceX0X.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideX1X, neighborsLeafNode);
            
        leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.SideX1X);

        foreach (var neighborsLeafNode in leafNeighbors)
            FaceX1X.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.SideX0X, neighborsLeafNode);


        leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.Side0XX);

        foreach (var neighborsLeafNode in leafNeighbors)
            Face0XX.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.Side1XX, neighborsLeafNode);
            
        leafNeighbors =
            GetDeeperLeafNeighbors(GrParametricVolumeTreeNodeSide3D.Side1XX);

        foreach (var neighborsLeafNode in leafNeighbors)
            Face1XX.AddFramesFromSide(GrParametricVolumeTreeNodeSide3D.Side0XX, neighborsLeafNode);
        
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerator<GrParametricVolumeTreeNode3D> GetEnumerator()
    {
        return Enumerable.Empty<GrParametricVolumeTreeNode3D>().GetEnumerator();
    }
}