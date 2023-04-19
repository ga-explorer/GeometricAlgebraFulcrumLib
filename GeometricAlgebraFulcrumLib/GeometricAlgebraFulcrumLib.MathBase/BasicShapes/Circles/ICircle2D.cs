namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Circles
{
    public interface ICircle2D : IFiniteGeometricShape2D
    {
        double CenterX { get; }

        double CenterY { get; }

        double Radius { get; }

        double RadiusSquared { get; }
    }
}