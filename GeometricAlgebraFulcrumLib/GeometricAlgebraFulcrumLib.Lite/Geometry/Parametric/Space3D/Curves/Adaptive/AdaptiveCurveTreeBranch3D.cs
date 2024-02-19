namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Adaptive;

public sealed class AdaptiveCurveTreeBranch3D :
    AdaptiveCurveTreeNode3D
{
    public override int Count => 2;

    public AdaptiveCurveTreeNode3D Child0 { get; private set; }

    public AdaptiveCurveTreeNode3D Child1 { get; private set; }


    /// <summary>
    /// Constructor of the root node of the tree
    /// </summary>
    /// <param name="parentTree"></param>
    internal AdaptiveCurveTreeBranch3D(AdaptiveCurve3D parentTree)
        : base(parentTree)
    {
    }

    internal AdaptiveCurveTreeBranch3D(AdaptiveCurveTreeBranch3D parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
    }


    private AdaptiveCurveTreeBranch3D CreateBranchChildren(AdaptiveCurveSamplingOptions3D options)
    {
        Child0 = new AdaptiveCurveTreeBranch3D(this, false).GenerateTree(options);
        Child1 = new AdaptiveCurveTreeBranch3D(this, true).GenerateTree(options);

        return this;
    }

    //private GrParametricCurveTreeBranch3D CreateLeafChildren()
    //{
    //    var child0 = new GrParametricCurveTreeLeaf3D(this, false);
    //    var child1 = new GrParametricCurveTreeLeaf3D(this, true);

    //    Child0 = ParentTree.AddLeafNode(child0);
    //    Child1 = ParentTree.AddLeafNode(child1);

    //    return this;
    //}

    internal AdaptiveCurveTreeNode3D GenerateTree(AdaptiveCurveSamplingOptions3D options)
    {
        // Update normals of second frame based on first frame
        if (ParentTree.FrameSamplingMethod == ParametricCurveLocalFrameSamplingMethod.MinimizedRotation)
            Frame1.SetMinimizedRotationNormals(Frame0);
        else
            Frame1.SetSimpleRotationNormals(Frame0);


        var continueSubdivision =
            // Always subdivide the root node
            IsRoot ||

            // Continue subdivision for the required initial number of levels
            Level < options.MinLevelCount ||

            // Continue subdivision if not at at max level and frame normals are far from parallel
            Level < options.MaxLevelCount && !HasNearEdgeFrames(options);

        if (continueSubdivision)
            return CreateBranchChildren(options);

        // Stop subdivision and replace this branch with a leaf node
        return ParentTree.AddLeafNode(
            new AdaptiveCurveTreeLeaf3D(ParentBranch, IsRightChild)
        );
    }

    internal AdaptiveCurveTreeNode3D GetChildContaining(double parameterValue)
    {
        if (Child0.Contains(parameterValue))
            return Child0;

        if (Child1.Contains(parameterValue))
            return Child1;

        throw new InvalidOperationException();
    }

    internal AdaptiveCurveTreeNode3D GetChildContainingLength(double length)
    {
        if (Child0.ContainsLength(length))
            return Child0;

        if (Child1.ContainsLength(length))
            return Child1;

        throw new InvalidOperationException();
    }

    internal override double UpdateLengthData(double length0)
    {
        Length0 = length0;

        length0 = Child0.UpdateLengthData(length0);
        Length1 = Child1.UpdateLengthData(length0);

        return Length1;
    }

    public override IEnumerator<AdaptiveCurveTreeNode3D> GetEnumerator()
    {
        yield return Child0;
        yield return Child1;
    }
}