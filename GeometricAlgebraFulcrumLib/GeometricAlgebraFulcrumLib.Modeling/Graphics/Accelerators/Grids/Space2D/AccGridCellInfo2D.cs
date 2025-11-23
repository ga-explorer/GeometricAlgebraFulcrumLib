using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;


namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space2D;

/// <summary>
/// This class can be used for collecting information about a Grid Cell
/// and its child geometric objects for debugging and statistics
/// </summary>
public sealed class AccGridCellInfo2D
{
    public int IndexX { get; }

    public int IndexY { get; }

    public bool IsEmpty { get; }

    public IEnumerable<IFloat64BoundedShape2D> GeometricObjects { get; }

    public Float64BoundingBox2D BoundingBox { get; }


    internal AccGridCellInfo2D(int indexX, int indexY, Float64BoundingBox2D boundingBox)
    {
        IndexX = indexX;
        IndexY = indexY;
        BoundingBox = boundingBox;
        IsEmpty = true;
        GeometricObjects = [];
    }

    internal AccGridCellInfo2D(int indexX, int indexY, Float64BoundingBox2D boundingBox, IEnumerable<IFloat64BoundedShape2D> geometricObjects)
    {
        IndexX = indexX;
        IndexY = indexY;
        BoundingBox = boundingBox;
        IsEmpty = false;
        GeometricObjects = geometricObjects;
    }
}