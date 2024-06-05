namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;

public interface ILineSegment3D : 
    IFiniteGeometricShape3D
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point1Z { get; }


    double Point2X { get; }

    double Point2Y { get; }

    double Point2Z { get; }
}