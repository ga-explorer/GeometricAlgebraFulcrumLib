namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Circles
{
    public interface ICircleSegment2D : IFiniteGeometricShape2D
    {
        double CenterX { get; }

        double CenterY { get; }

        double OriginX { get; }

        double OriginY { get; }

        double TurnsValue { get; }
    }
}