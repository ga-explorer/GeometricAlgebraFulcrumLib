using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Structures.Data;

public sealed record TriangleData3D<T> :
    ITriplet<int>
{
    public int Item1 { get; }

    public int Item2 { get; }

    public int Item3 { get; }

    public int TriangleIndex { get; }

    public T DataValue { get; }

    public int MinIndex
        => Item1;

    public int MaxIndex
        => Item2 > Item3 ? Item2 : Item3;


    public TriangleData3D(int triangleIndex, int item1, int item2, int item3, [NotNull] T dataValue)
    {
        var (index1, index2, index3) = 
            GraphicsPrimitiveGeometryUtils.GetValidTriangleIndexTriplet(item1, item2, item3);

        Item1 = index1;
        Item2 = index2;
        Item3 = index3;
        TriangleIndex = triangleIndex;
        DataValue = dataValue;
    }

    public TriangleData3D(int triangleIndex, ITriplet<int> triplet, [NotNull] T dataValue)
    {
        var (index1, index2, index3) = 
            triplet.GetValidTriangleIndexTriplet();

        Item1 = index1;
        Item2 = index2;
        Item3 = index3;
        TriangleIndex = triangleIndex;
        DataValue = dataValue;
    }
}