namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines
{
    public interface ILinePair2D : IGeometricElement
    {
        double Origin1X { get; }

        double Origin1Y { get; }


        double Origin2X { get; }

        double Origin2Y { get; }


        double Direction1X { get; }

        double Direction1Y { get; }


        double Direction2X { get; }

        double Direction2Y { get; }
    }
}