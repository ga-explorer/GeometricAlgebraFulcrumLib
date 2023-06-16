namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines
{
    public interface ILineSegment2D : IFiniteGeometricShape2D
    {
        double Point1X { get; }

        double Point1Y { get; }


        double Point2X { get; }

        double Point2Y { get; }
    }
}