using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space3D;

/// <summary>
/// This class can be used for collecting information about a Grid Cell
/// and its child geometric objects for debugging and statistics
/// </summary>
public sealed class AccGridCellInfo3D
{
    public int IndexX { get; }

    public int IndexY { get; }

    public int IndexZ { get; }

    public bool IsEmpty { get; }

    public IEnumerable<IFloat64BoundedShape3D> GeometricObjects { get; }

    public Float64BoundingBox3D BoundingBox { get; }


    internal AccGridCellInfo3D(int indexX, int indexY, int indexZ, Float64BoundingBox3D boundingBox)
    {
        IndexX = indexX;
        IndexY = indexY;
        IndexZ = indexZ;
        BoundingBox = boundingBox;
        IsEmpty = true;
        GeometricObjects = [];
    }

    internal AccGridCellInfo3D(int indexX, int indexY, int indexZ, Float64BoundingBox3D boundingBox, IEnumerable<IFloat64BoundedShape3D> geometricObjects)
    {
        IndexX = indexX;
        IndexY = indexY;
        IndexZ = indexZ;
        BoundingBox = boundingBox;
        IsEmpty = false;
        GeometricObjects = geometricObjects;
    }
}