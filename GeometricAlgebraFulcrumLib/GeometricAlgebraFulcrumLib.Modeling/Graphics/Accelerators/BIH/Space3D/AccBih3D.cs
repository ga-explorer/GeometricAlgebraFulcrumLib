using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;

public class AccBih3D<T> : IAccBih3D<T> 
    where T : IFloat64FiniteGeometricShape3D
{
    public bool IsValid()
    {
        return true;
    }

    public int BihDepth { get; }

    public Float64BoundingBox3D BoundingBox { get; }

    public int Count => RootNode.Count;

    public T this[int index] => RootNode[index];

    public bool IntersectionTestsEnabled { get; set; } = true;

    public IAccBihNode3D<T> RootNode { get; }


    public AccBih3D(IReadOnlyList<T> geometricObjectsList, int depthLimit, int singleDepthLimit, int leafObjectsLimit)
    {
        var result = geometricObjectsList.CreateBihRootNode3D(
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


    public Float64BoundingBox3D GetBoundingBox()
    {
        return Float64BoundingBox3D.Create(
            (IEnumerable<IFloat64FiniteGeometricShape3D>) RootNode
        );
    }

    public Float64BoundingBoxComposer3D GetBoundingBoxComposer()
    {
        return Float64BoundingBoxComposer3D.Create(
            (IEnumerable<IFloat64FiniteGeometricShape3D>) RootNode
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