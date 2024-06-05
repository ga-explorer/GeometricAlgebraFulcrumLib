namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Adaptive;

public sealed class AdaptiveCurveTreeBranch2D :
    AdaptiveCurveTreeNode2D
{
    public override int Count => 2;

    public AdaptiveCurveTreeNode2D Child0 { get; private set; }

    public AdaptiveCurveTreeNode2D Child1 { get; private set; }


    /// <summary>
    /// Constructor of the root node of the tree
    /// </summary>
    /// <param name="parentTree"></param>
    internal AdaptiveCurveTreeBranch2D(AdaptiveCurve2D parentTree)
        : base(parentTree)
    {
    }

    internal AdaptiveCurveTreeBranch2D(AdaptiveCurveTreeBranch2D parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
    }


    private AdaptiveCurveTreeBranch2D CreateBranchChildren(AdaptiveCurveSamplingOptions2D options)
    {
        Child0 = new AdaptiveCurveTreeBranch2D(this, false).GenerateTree(options);
        Child1 = new AdaptiveCurveTreeBranch2D(this, true).GenerateTree(options);

        return this;
    }

    //private GrParametricCurveTreeBranch2D CreateLeafChildren()
    //{
    //    var child0 = new GrParametricCurveTreeLeaf2D(this, false);
    //    var child1 = new GrParametricCurveTreeLeaf2D(this, true);

    //    Child0 = ParentTree.AddLeafNode(child0);
    //    Child1 = ParentTree.AddLeafNode(child1);

    //    return this;
    //}

    internal AdaptiveCurveTreeNode2D GenerateTree(AdaptiveCurveSamplingOptions2D options)
    {
        //// Update normals of second frame based on first frame
        //if (ParentTree.FrameSamplingMethod == ParametricCurveLocalFrameSamplingMethod.MinimizedRotation)
        //    Frame1.SetMinimizedRotationNormals(Frame0);
        //else
        //    Frame1.SetSimpleRotationNormals(Frame0);


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
            new AdaptiveCurveTreeLeaf2D(ParentBranch, IsRightChild)
        );
    }

    internal AdaptiveCurveTreeNode2D GetChildContaining(double parameterValue)
    {
        if (Child0.Contains(parameterValue))
            return Child0;

        if (Child1.Contains(parameterValue))
            return Child1;

        throw new InvalidOperationException();
    }

    internal AdaptiveCurveTreeNode2D GetChildContainingLength(double length)
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

    public override IEnumerator<AdaptiveCurveTreeNode2D> GetEnumerator()
    {
        yield return Child0;
        yield return Child1;
    }
}