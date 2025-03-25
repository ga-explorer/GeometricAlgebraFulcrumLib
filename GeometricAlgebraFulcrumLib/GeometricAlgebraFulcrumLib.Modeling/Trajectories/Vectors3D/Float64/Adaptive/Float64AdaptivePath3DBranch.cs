using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;

public sealed class Float64AdaptivePath3DBranch :
    Float64AdaptivePath3DNode
{
    public override int Count => 2;

    public Float64AdaptivePath3DNode Child0 { get; private set; }

    public Float64AdaptivePath3DNode Child1 { get; private set; }


    /// <summary>
    /// Constructor of the root node of the tree
    /// </summary>
    /// <param name="parentTree"></param>
    internal Float64AdaptivePath3DBranch(Float64AdaptivePath3D parentTree)
        : base(parentTree)
    {
    }

    internal Float64AdaptivePath3DBranch(Float64AdaptivePath3DBranch parentBranch, bool isRightChild)
        : base(parentBranch, isRightChild)
    {
    }


    private Float64AdaptivePath3DBranch CreateBranchChildren(Float64AdaptivePath3DSamplingOptions options)
    {
        Child0 = new Float64AdaptivePath3DBranch(this, false).GenerateTree(options);
        Child1 = new Float64AdaptivePath3DBranch(this, true).GenerateTree(options);

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

    internal Float64AdaptivePath3DNode GenerateTree(Float64AdaptivePath3DSamplingOptions options)
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

            // Continue subdivision if not at max level and frame normals are far from parallel
            Level < options.MaxLevelCount && !HasNearEdgeFrames(options);

        if (continueSubdivision)
            return CreateBranchChildren(options);

        // Stop subdivision and replace this branch with a leaf node
        return ParentTree.AddLeafNode(
            new Float64AdaptivePath3DLeaf(ParentBranch, IsRightChild)
        );
    }

    internal Float64AdaptivePath3DNode GetChildContaining(double parameterValue)
    {
        if (Child0.Contains(parameterValue))
            return Child0;

        if (Child1.Contains(parameterValue))
            return Child1;

        throw new InvalidOperationException();
    }

    internal Float64AdaptivePath3DNode GetChildContainingLength(double length)
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

    public override IEnumerator<Float64AdaptivePath3DNode> GetEnumerator()
    {
        yield return Child0;
        yield return Child1;
    }
}