using System.Diagnostics;
using System.Runtime.CompilerServices;

// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.ParametricShapes.Volumes.Sampled;

public sealed class GrParametricVolumeTreeBranch3D :
    GrParametricVolumeTreeNode3D
{
    public override int Count 
        => 8;

    public GrParametricVolumeTreeNode3D Child000 { get; private set; }

    public GrParametricVolumeTreeNode3D Child001 { get; private set; }

    public GrParametricVolumeTreeNode3D Child010 { get; private set; }

    public GrParametricVolumeTreeNode3D Child011 { get; private set; }
        
    public GrParametricVolumeTreeNode3D Child100 { get; private set; }

    public GrParametricVolumeTreeNode3D Child101 { get; private set; }

    public GrParametricVolumeTreeNode3D Child110 { get; private set; }

    public GrParametricVolumeTreeNode3D Child111 { get; private set; }

    //public int MidFrameIndexX0 
    //    => Child000.FrameIndex10;

    //public int MidFrameIndexX1 
    //    => Child001.FrameIndex11;

    //public int MidFrameIndex0X 
    //    => Child000.FrameIndex01;

    //public int MidFrameIndex1X
    //    => Child010.FrameIndex11;
        
    //public int MidFrameIndex 
    //    => Child000.FrameIndex11;


    //public GrParametricVolumeTreeCornerIndex MidCornerIndexX0 
    //    => Child000.CornerIndex10;

    //public GrParametricVolumeTreeCornerIndex MidCornerIndexX1 
    //    => Child001.CornerIndex11;

    //public GrParametricVolumeTreeCornerIndex MidCornerIndex0X 
    //    => Child000.CornerIndex01;

    //public GrParametricVolumeTreeCornerIndex MidCornerIndex1X
    //    => Child010.CornerIndex11;
        
    //public GrParametricVolumeTreeCornerIndex MidCornerIndex 
    //    => Child000.CornerIndex11;
        

    //public Pair<int> MidGridIndexX0 
    //    => Child000.GridIndex10;

    //public Pair<int> MidGridIndexX1 
    //    => Child001.GridIndex11;

    //public Pair<int> MidGridIndex0X 
    //    => Child000.GridIndex01;

    //public Pair<int> MidGridIndex1X
    //    => Child010.GridIndex11;
        
    //public Pair<int> MidGridIndex 
    //    => Child000.GridIndex11;


    //public GrParametricVolumeLocalFrame3D MidFrameX0 
    //    => Child000.Frame10;

    //public GrParametricVolumeLocalFrame3D MidFrameX1 
    //    => Child001.Frame11;

    //public GrParametricVolumeLocalFrame3D MidFrame0X 
    //    => Child000.Frame01;

    //public GrParametricVolumeLocalFrame3D MidFrame1X
    //    => Child010.Frame11;
        
    //public GrParametricVolumeLocalFrame3D MidFrame 
    //    => Child000.Frame11;



    /// <summary>
    /// Constructor of the root node of the tree
    /// </summary>
    /// <param name="parentTree"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeBranch3D(GrParametricVolumeTree3D parentTree) 
        : base(parentTree)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeBranch3D(GrParametricVolumeTreeBranch3D parentBranch, bool isChild1XX, bool isChildX1X, bool isChildXX1, bool createLeafChildren = false)
        : base(parentBranch, isChild1XX, isChildX1X, isChildXX1)
    {
        if (createLeafChildren)
        {
            Child000 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, false, false, false)
            );

            Child001 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, false, false, true)
            );

            Child010 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, false, true, false)
            );

            Child011 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, false, true, true)
            );

            Child100 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, true, false, false)
            );

            Child101 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, true, false, true)
            );

            Child110 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, true, true, false)
            );

            Child111 = ParentTree.AddLeafNode(
                new GrParametricVolumeTreeLeaf3D(this, true, true, true)
            );
        }
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetChild(GrParametricVolumeTreeNode3D node)
    {
        Debug.Assert(ReferenceEquals(node.ParentBranch, this) && node.Level == Level + 1);

        if (node.IsChild000)
            Child000 = node;

        else if (node.IsChild001)
            Child001 = node;

        else if (node.IsChild010)
            Child010 = node;

        else if (node.IsChild011)
            Child011 = node;

        else if (node.IsChild100)
            Child100 = node;
            
        else if (node.IsChild101)
            Child101 = node;

        else if (node.IsChild110)
            Child110 = node;

        else
            Child111 = node;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GrParametricVolumeTreeBranch3D CreateBranchChildren(GrParametricVolumeTreeOptions3D options)
    {
        Child000 = new GrParametricVolumeTreeBranch3D(this, false, false, false).GenerateTree(options);
        Child001 = new GrParametricVolumeTreeBranch3D(this, false, false, true).GenerateTree(options);
        Child010 = new GrParametricVolumeTreeBranch3D(this, false, true, false).GenerateTree(options);
        Child011 = new GrParametricVolumeTreeBranch3D(this, false, true, true).GenerateTree(options);
        Child100 = new GrParametricVolumeTreeBranch3D(this, true, false, false).GenerateTree(options);
        Child101 = new GrParametricVolumeTreeBranch3D(this, true, false, true).GenerateTree(options);
        Child110 = new GrParametricVolumeTreeBranch3D(this, true, true, false).GenerateTree(options);
        Child111 = new GrParametricVolumeTreeBranch3D(this, true, true, true).GenerateTree(options);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeNode3D GenerateTree(GrParametricVolumeTreeOptions3D options)
    {
        var continueSubdivision =
            // Always subdivide the root node
            IsRoot ||
                
            // Continue subdivision for the required initial number of levels
            Level < options.MinLevelCount ||

            // Continue subdivision if not at at max level and frame normals are far from parallel
            (Level < options.MaxLevelCount && !HasNearEdgeFrames(options));

        if (continueSubdivision)
            return CreateBranchChildren(options);

        // Stop subdivision and replace this branch with a leaf node
        return ParentTree.AddLeafNode(
            new GrParametricVolumeTreeLeaf3D(ParentBranch, IsChild1XX, IsChildX1X, IsChildXX1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricVolumeTreeNode3D GetChildContaining(double parameterValue1, double parameterValue2, double parameterValue3)
    {
        if (Child000.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child000;

        if (Child001.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child001;

        if (Child010.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child010;

        if (Child011.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child011;

        if (Child000.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child100;

        if (Child001.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child101;

        if (Child010.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child110;

        if (Child011.Contains(parameterValue1, parameterValue2, parameterValue3))
            return Child111;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<GrParametricVolumeTreeNode3D> GetChildrenOnSide(GrParametricVolumeTreeNodeSide3D side)
    {
        switch (side)
        {
            case GrParametricVolumeTreeNodeSide3D.Side0XX:
                yield return Child000;
                yield return Child001;
                yield return Child010;
                yield return Child011;
                break;

            case GrParametricVolumeTreeNodeSide3D.Side1XX:
                yield return Child100;
                yield return Child101;
                yield return Child110;
                yield return Child111;
                break;

            case GrParametricVolumeTreeNodeSide3D.SideX0X:
                yield return Child000;
                yield return Child001;
                yield return Child100;
                yield return Child101;
                break;

            case GrParametricVolumeTreeNodeSide3D.SideX1X:
                yield return Child010;
                yield return Child011;
                yield return Child110;
                yield return Child111;
                break;

            case GrParametricVolumeTreeNodeSide3D.SideXX0:
                yield return Child000;
                yield return Child010;
                yield return Child100;
                yield return Child110;
                break;

            default:
                yield return Child001;
                yield return Child011;
                yield return Child101;
                yield return Child111;
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerator<GrParametricVolumeTreeNode3D> GetEnumerator()
    {
        yield return Child000;
        yield return Child001;
        yield return Child010;
        yield return Child011;
        yield return Child100;
        yield return Child101;
        yield return Child110;
        yield return Child111;
    }
}