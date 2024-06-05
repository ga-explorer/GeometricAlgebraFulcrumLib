using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators.BIH.Space3D;

public interface IAccBihNode3D
{
    string NodeId { get; set; }

    bool IsRoot { get; }

    bool IsLeaf { get; }

    bool IsInternal { get; }

    bool IsSingleInternal { get; }

    int NodeDepth { get; }

    int BihDepth { get; }

    int FirstObjectIndex { get; }

    int LastObjectIndex { get; }

    IAccBihNode3D LeftChild { get; }

    IAccBihNode3D RightChild { get; }

    bool HasLeftChild { get; }

    bool HasRightChild { get; }

    bool HasNoChildren { get; }

    int SplitAxisIndex { get; }

    string SplitAxisName { get; }

    double ClipValue0 { get; }

    double ClipValue1 { get; }

    IEnumerable<IFiniteGeometricShape3D> Contents { get; }

    bool Contains(IFiniteGeometricShape3D shape);
}

public interface IAccBihNode3D<out T>
    : IAccBihNode3D, IGeometricObjectsContainer3D<T>
    where T : IFiniteGeometricShape3D
{
    IAccBihNode3D<T> LeftChildNode { get; }

    IAccBihNode3D<T> RightChildNode { get; }

    IAccBihNode3D<T> GetChild(int index);
}