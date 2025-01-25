using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

public class AccBih2D<T> : IAccBih2D<T> 
    where T : IFloat64FiniteGeometricShape2D
{
    public bool IsValid()
    {
        return true;
    }

    public int BihDepth { get; }

    public Float64BoundingBox2D BoundingBox { get; }

    public int Count => RootNode.Count;

    public T this[int index] => RootNode[index];

    public bool IntersectionTestsEnabled { get; set; } = true;

    public IAccBihNode2D<T> RootNode { get; }


    public AccBih2D(IReadOnlyList<T> geometricObjectsList, int depthLimit, int singleDepthLimit, int leafObjectsLimit)
    {
        var result = geometricObjectsList.CreateBihRootNode2D(
            depthLimit,
            singleDepthLimit,
            leafObjectsLimit
        );

        RootNode = result.Item1;
        BoundingBox = result.Item2;
        BihDepth = RootNode.GetChildHierarchyDepth();

        foreach (var node in RootNode.GetNodes())
            node.NodeId = node.NodeId.PadRight(BihDepth, '-');
    }


    public AccBihNodeInfo2D GetRootNodeInfo()
    {
        return AccBihNodeInfo2D.Create(this);
    }

    public Float64BoundingBox2D GetBoundingBox()
    {
        return Float64BoundingBox2D.Create(
            (IEnumerable<IFloat64FiniteGeometricShape2D>) RootNode
        );
    }

    public Float64BoundingBoxComposer2D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer2D.Create(
            (IEnumerable<IFloat64FiniteGeometricShape2D>) RootNode
        );
    }

    public IEnumerator<T> GetEnumerator()
    {
        return RootNode.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return RootNode.GetEnumerator();
    }
}