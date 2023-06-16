namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D
{
    public interface IBoundingBox2D : IBorderCurve2D
    {
        double MinX { get; }

        double MinY { get; }


        double MaxX { get; }

        double MaxY { get; }
    }
}