using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Adaptive;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces.Sampled;

public sealed class GrParametricSurfaceTree3D :
    IGraphicsParametricSurface3D
{
    private readonly HashSet<AdaptiveCurveTreeCornerPosition3D> _cornerPositionSet
        = new HashSet<AdaptiveCurveTreeCornerPosition3D>();

    private readonly Dictionary<Pair<AdaptiveCurveTreeCornerPosition3D>, int> _cornerDictionary
        = new Dictionary<Pair<AdaptiveCurveTreeCornerPosition3D>, int>();

    private readonly List<GrParametricSurfaceTreeCorner3D> _cornerList
        = new List<GrParametricSurfaceTreeCorner3D>();

    private readonly List<GrParametricSurfaceTreeLeaf3D> _leafNodeList
        = new List<GrParametricSurfaceTreeLeaf3D>();


    public IGraphicsParametricSurface3D Surface { get; }

    public GrParametricSurfaceTreeBranch3D RootNode { get; private set; }
        
    public int TreeLevelCount { get; internal set; }

    /// <summary>
    /// The number of segments per grid side for this tree
    /// </summary>
    public int GridSegmentCount 
        => 1 << TreeLevelCount;

    public int LeafNodeCount 
        => _leafNodeList.Count;
        
    public IEnumerable<GrParametricSurfaceTreeNode3D> Nodes
    {
        get
        {
            var stack = new Stack<GrParametricSurfaceTreeNode3D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                yield return node;

                if (node is not GrParametricSurfaceTreeBranch3D branchNode) 
                    continue;

                stack.Push(branchNode.Child11);
                stack.Push(branchNode.Child10);
                stack.Push(branchNode.Child01);
                stack.Push(branchNode.Child00);
            }
        }
    }

    public IEnumerable<GrParametricSurfaceTreeBranch3D> BranchNodes
    {
        get
        {
            var stack = new Stack<GrParametricSurfaceTreeBranch3D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var branchNode = stack.Pop();

                yield return branchNode;

                if (branchNode.Child11 is GrParametricSurfaceTreeBranch3D childBranchNode11)
                    stack.Push(childBranchNode11);

                if (branchNode.Child10 is GrParametricSurfaceTreeBranch3D childBranchNode10)
                    stack.Push(childBranchNode10);
                    
                if (branchNode.Child01 is GrParametricSurfaceTreeBranch3D childBranchNode01)
                    stack.Push(childBranchNode01);

                if (branchNode.Child00 is GrParametricSurfaceTreeBranch3D childBranchNode00)
                    stack.Push(childBranchNode00);
            }
        }
    }

    public IEnumerable<GrParametricSurfaceTreeLeaf3D> LeafNodes
    {
        get
        {
            var stack = new Stack<GrParametricSurfaceTreeNode3D>();

            stack.Push(RootNode);

            while (stack.Count > 0)
            {
                var node = stack.Pop();

                if (node is GrParametricSurfaceTreeLeaf3D leafNode)
                {
                    yield return leafNode;
                    continue;
                }

                var branchNode = (GrParametricSurfaceTreeBranch3D) node;
                    
                stack.Push(branchNode.Child11);
                stack.Push(branchNode.Child10);
                stack.Push(branchNode.Child01);
                stack.Push(branchNode.Child00);
            }
        }
    }

    public IReadOnlyList<GrParametricSurfaceTreeLeaf3D> LeafNodesList 
        => _leafNodeList;
        
    public IBoundingBox2D ParameterValueRange { get; }

    public double MinParameterValue1 
        => ParameterValueRange.MinX;

    public double MinParameterValue2 
        => ParameterValueRange.MinY;

    public double MaxParameterValue1 
        => ParameterValueRange.MaxX;

    public double MaxParameterValue2 
        => ParameterValueRange.MaxY;
        

    public int CornerCount 
        => _cornerList.Count;

    public IReadOnlyList<GrParametricSurfaceTreeCorner3D> CornerList 
        => _cornerList;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTree3D(IGraphicsParametricSurface3D surface, IBoundingBox2D parameterValueRange)
    {
        Surface = surface;
        ParameterValueRange = parameterValueRange;
        RootNode = null;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTree3D(IGraphicsParametricSurface3D surface)
    {
        Surface = surface;
        ParameterValueRange = BoundingBox2D.Create(0, 0, 1, 1);
        RootNode = null;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTree3D Clear()
    {
        RootNode = null;
        _cornerList.Clear();
        _leafNodeList.Clear();
        _cornerPositionSet.Clear();
        _cornerDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> GetParameterValue(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
    {
        var t1 = cornerPosition.Item1.GetInterpolationValue();
        var t2 = cornerPosition.Item2.GetInterpolationValue();

        var parameterValue1 = t1.Lerp(MinParameterValue1, MaxParameterValue1);
        var parameterValue2 = t2.Lerp(MinParameterValue2, MaxParameterValue2);

        return new Pair<double>(parameterValue1, parameterValue2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTree3D ClearLeafEdgeData()
    {
        foreach (var leafNode in LeafNodesList) 
            leafNode.ClearEdgeData();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTree3D GenerateLeafEdgeData()
    {
        foreach (var leafNode in LeafNodesList) 
            leafNode.GenerateEdgeData();

        return this;
    }

    public GrParametricSurfaceTree3D GenerateTree(GrParametricSurfaceTreeOptions3D options)
    {
        Clear();

        RootNode = new GrParametricSurfaceTreeBranch3D(this);

        RootNode.GenerateTree(options);
            
        if (options.ForceBalancedTree)
            BalanceTree();

        foreach (var corner in _cornerList)
            corner.Frame.Index = corner.Index;
            
        _cornerPositionSet.Clear();
        _cornerDictionary.Clear();

        return this;
    }

    private void BalanceTree()
    {
        var leafNodeSet = new HashSet<GrParametricSurfaceTreeLeaf3D>();

        foreach (var leafNode in _leafNodeList.Where(n => n.Level < TreeLevelCount - 1))
            leafNodeSet.Add(leafNode);

        while (leafNodeSet.Count > 0)
        {
            var leafNode = leafNodeSet.First();
            leafNodeSet.Remove(leafNode);

            var neighborLeafNodesList = leafNode.GetLeafNeighbors();
            var deepestLevel = neighborLeafNodesList.Max(n => n.Level);

            // The leaf node needs to be split
            if (deepestLevel > leafNode.Level + 1)
            {
                // Replace the leaf node by a new branch
                var branchNode = leafNode.Split();

                // Add neighbor leaf nodes that require splitting due to new level difference
                foreach (var neighborLeafNode in neighborLeafNodesList.Where(n => n.Level < leafNode.Level))
                    leafNodeSet.Add(neighborLeafNode);
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCorner(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
    {
        return _cornerDictionary.ContainsKey(cornerPosition);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTreeCorner3D GetCorner(int cornerIndex)
    {
        return _cornerList[cornerIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceLocalFrame3D GetFrame(int cornerIndex)
    {
        return _cornerList[cornerIndex].Frame;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceTreeCorner3D GetCorner(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
    {
        var cornerIndex = _cornerDictionary[cornerPosition];

        return _cornerList[cornerIndex];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCornerIndex(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition)
    {
        return _cornerDictionary[cornerPosition];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCorner(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition, out GrParametricSurfaceTreeCorner3D corner)
    {
        if (_cornerDictionary.TryGetValue(cornerPosition, out var cornerIndex))
        {
            corner = _cornerList[cornerIndex];
            return true;
        }

        corner = default;
        return false;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCornerIndex(Pair<AdaptiveCurveTreeCornerPosition3D> cornerPosition, out int cornerIndex)
    {
        return _cornerDictionary.TryGetValue(cornerPosition, out cornerIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal AdaptiveCurveTreeCornerPosition3D GetOrAddCornerPosition(int level, int segmentCount)
    {
        var newPosition = new AdaptiveCurveTreeCornerPosition3D(level, segmentCount);

        if (_cornerPositionSet.TryGetValue(newPosition, out var position))
            return position;

        _cornerPositionSet.Add(newPosition);

        return newPosition;
    }

    internal GrParametricSurfaceTreeCorner3D GetOrAddCorner(AdaptiveCurveTreeCornerPosition3D position1, AdaptiveCurveTreeCornerPosition3D position2)
    {
        var cornerPosition = 
            new Pair<AdaptiveCurveTreeCornerPosition3D>(position1, position2);

        if (_cornerDictionary.TryGetValue(cornerPosition, out var index))
            return _cornerList[index];

        var (parameterValue1, parameterValue2) = 
            GetParameterValue(cornerPosition);

        index = _cornerList.Count;
        var frame = Surface.GetFrame(parameterValue1, parameterValue2);
        var corner = new GrParametricSurfaceTreeCorner3D(this, index, frame, cornerPosition);

        _cornerList.Add(corner);
        _cornerDictionary.Add(cornerPosition, index);

        return corner;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal GrParametricSurfaceTreeLeaf3D AddLeafNode(GrParametricSurfaceTreeLeaf3D leafNode)
    {
        _leafNodeList.Add(leafNode);
            
        return leafNode;
    }

    public GrParametricSurfaceTreeSample3D GetSample(double parameterValue1, double parameterValue2)
    {
        if (!RootNode.Contains(parameterValue1, parameterValue2))
            throw new ArgumentOutOfRangeException();

        var branchNode = RootNode;

        while (true)
        {
            var childNode = 
                branchNode.GetChildContaining(parameterValue1, parameterValue2);

            if (childNode is GrParametricSurfaceTreeLeaf3D leafNode)
                return new GrParametricSurfaceTreeSample3D(
                    leafNode, 
                    parameterValue1, 
                    parameterValue2
                );

            branchNode = (GrParametricSurfaceTreeBranch3D) childNode;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<int> GetGridIndex(GrParametricSurfaceTreeCorner3D cornerPosition)
    {
        return cornerPosition.GridIndex;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrVertexListTriangleGeometry3D GenerateGeometry()
    {
        var geometry =
            new GrVertexListTriangleGeometry3D(
                CornerList.Select(c => c.Frame).ToArray()
            );

        var triangleIndexTriplets = 
            LeafNodesList.SelectMany(n => n.GetTriangleIndexTriplets());
            
        geometry.AddTriangles(triangleIndexTriplets);
            
        //var triangleGridIndexTriplets = 
        //    surfaceTree.LeafNodes.SelectMany(n => n.GetTriangleGridIndexTriplets());

        //foreach (var (i1, i2, i3) in triangleGridIndexTriplets)
        //{
        //    var s1 = $"({i1.Item1}, {i1.Item2})";
        //    var s2 = $"({i2.Item1}, {i2.Item2})";
        //    var s3 = $"({i3.Item1}, {i3.Item2})";

        //    Console.WriteLine($"Triangle: <{s1}, {s2}, {s3}>");
        //}

        return geometry;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return RootNode is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetPoint(double parameterValue1, double parameterValue2)
    {
        return GetSample(parameterValue1, parameterValue2).GetPoint();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetNormal(double parameterValue1, double parameterValue2)
    {
        return GetSample(parameterValue1, parameterValue2).GetNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2)
    {
        return GetSample(parameterValue1, parameterValue2).GetNormal().GetUnitNormal();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2)
    {
        return GetSample(parameterValue1, parameterValue2).GetFrame();
    }
}