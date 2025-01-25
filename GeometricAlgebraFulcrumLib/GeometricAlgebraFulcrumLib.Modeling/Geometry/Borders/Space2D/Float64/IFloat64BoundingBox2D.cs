namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

public interface IFloat64BoundingBox2D :
    IFloat64BorderCurve2D
{
    double MinX { get; }

    double MinY { get; }


    double MaxX { get; }

    double MaxY { get; }
}