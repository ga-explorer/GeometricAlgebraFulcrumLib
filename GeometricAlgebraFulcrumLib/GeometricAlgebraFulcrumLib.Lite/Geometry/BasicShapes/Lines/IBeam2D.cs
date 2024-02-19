namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;

public interface IBeam2D : IGeometricElement
{
    double OriginX { get; }

    double OriginY { get; }


    double Direction1X { get; }

    double Direction1Y { get; }


    double Direction2X { get; }

    double Direction2Y { get; }
}