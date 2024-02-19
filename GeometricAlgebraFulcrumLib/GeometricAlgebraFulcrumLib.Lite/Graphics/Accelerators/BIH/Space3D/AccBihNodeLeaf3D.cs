using System.Collections;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.BIH.Space3D;

public sealed class AccBihNodeLeaf3D<T> : IAccBihNode3D<T>
    where T : IFiniteGeometricShape3D
{
    private readonly T[] _geometricObjectsArray;


    public bool IntersectionTestsEnabled { get; set; } = true;

    public bool IsValid()
    {
        return true;
    }

    public int Count => LastObjectIndex - FirstObjectIndex + 1;

    public T this[int index] => _geometricObjectsArray[FirstObjectIndex + index];

    public string NodeId { get; set; }

    public bool IsRoot => NodeDepth == 0;

    public bool IsLeaf => true;

    public bool IsInternal => false;

    public bool IsSingleInternal => false;

    public int NodeDepth { get; }

    public int BihDepth => NodeId.Length;

    public int FirstObjectIndex { get; }

    public int LastObjectIndex { get; }

    public IEnumerable<IFiniteGeometricShape3D> Contents
    {
        get
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                yield return _geometricObjectsArray[i];
        }
    }

    public int SplitAxisIndex => -1;

    public string SplitAxisName => "none";

    public double ClipValue0 => double.PositiveInfinity;

    public double ClipValue1 => double.PositiveInfinity;

    public IAccBihNode3D LeftChild => null;

    public IAccBihNode3D RightChild => null;

    public IAccBihNode3D<T> LeftChildNode => null;

    public IAccBihNode3D<T> RightChildNode => null;

    public bool HasLeftChild => false;

    public bool HasRightChild => false;

    public bool HasNoChildren => true;


    internal AccBihNodeLeaf3D(string nodeId, int nodeDepth, T[] geometricObjectsArray, int firstArrayIndex, int lastArrayIndex)
    {
        NodeId = nodeId;
        NodeDepth = nodeDepth;

        _geometricObjectsArray = geometricObjectsArray;
        FirstObjectIndex = firstArrayIndex;
        LastObjectIndex = lastArrayIndex;
    }


    public IAccBihNode3D<T> GetChild(int index)
    {
        return null;
    }

    public bool Contains(IFiniteGeometricShape3D shape)
    {
        for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
            if (ReferenceEquals(_geometricObjectsArray[i], shape))
                return true;

        return false;
    }

    public BoundingBox3D GetBoundingBox()
    {
        return BoundingBox3D.Create((IEnumerable<T>)this);
    }

    public MutableBoundingBox3D GetMutableBoundingBox()
    {
        return MutableBoundingBox3D.Create((IEnumerable<T>)this);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
            yield return _geometricObjectsArray[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
            yield return _geometricObjectsArray[i];
    }
}