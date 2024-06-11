using System.Collections;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

public sealed class AccBihNodeLeaf2D<T> 
    : IAccBihNode2D<T> where T : IFiniteGeometricShape2D
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

    public IAccBihNode2D LeftChild => null;

    public IAccBihNode2D RightChild => null;

    public IAccBihNode2D<T> LeftChildNode => null;

    public IAccBihNode2D<T> RightChildNode => null;

    public bool HasLeftChild => false;

    public bool HasRightChild => false;

    public bool HasNoChildren => true;

    public int SplitAxisIndex => -1;

    public string SplitAxisName => "none";

    public double ClipValue0 => double.PositiveInfinity;

    public double ClipValue1 => double.PositiveInfinity;

    public IEnumerable<IFiniteGeometricShape2D> Contents
    {
        get
        {
            for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
                yield return _geometricObjectsArray[i];
        }
    }


    internal AccBihNodeLeaf2D(string nodeId, int nodeDepth, T[] geometricObjectsArray, int firstArrayIndex, int lastArrayIndex)
    {
        NodeId = nodeId;
        NodeDepth = nodeDepth;

        _geometricObjectsArray = geometricObjectsArray;
        FirstObjectIndex = firstArrayIndex;
        LastObjectIndex = lastArrayIndex;
    }


    public IAccBihNode2D<T> GetChild(int index)
    {
        return null;
    }

    public bool Contains(IFiniteGeometricShape2D shape)
    {
        for (var i = FirstObjectIndex; i <= LastObjectIndex; i++)
            if (ReferenceEquals(_geometricObjectsArray[i], shape))
                return true;

        return false;
    }

    public BoundingBox2D GetBoundingBox()
    {
        return BoundingBox2D.Create((IEnumerable<T>)this);
    }

    public MutableBoundingBox2D GetMutableBoundingBox()
    {
        return MutableBoundingBox2D.Create((IEnumerable<T>)this);
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