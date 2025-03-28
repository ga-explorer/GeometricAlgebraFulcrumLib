﻿using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

public interface IAccBihNode2D
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

    IAccBihNode2D LeftChild { get; }

    IAccBihNode2D RightChild { get; }

    bool HasLeftChild { get; }

    bool HasRightChild { get; }

    bool HasNoChildren { get; }

    int SplitAxisIndex { get; }

    string SplitAxisName { get; }

    double ClipValue0 { get; }

    double ClipValue1 { get; }

    IEnumerable<IFloat64FiniteGeometricShape2D> Contents { get; }

    bool Contains(IFloat64FiniteGeometricShape2D shape);
}

public interface IAccBihNode2D<out T> 
    : IAccBihNode2D, IFloat64GeometricObjectsContainer2D<T> 
    where T : IFloat64FiniteGeometricShape2D
{
    IAccBihNode2D<T> LeftChildNode { get; }

    IAccBihNode2D<T> RightChildNode { get; }

    IAccBihNode2D<T> GetChild(int index);
}