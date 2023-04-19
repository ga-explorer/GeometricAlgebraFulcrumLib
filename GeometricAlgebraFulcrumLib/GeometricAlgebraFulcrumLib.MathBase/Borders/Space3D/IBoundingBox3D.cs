namespace GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D
{
    public interface IBoundingBox3D : IBorderSurface3D
    {
        double MinX { get; }

        double MinY { get; }

        double MinZ { get; }


        double MaxX { get; }

        double MaxY { get; }

        double MaxZ { get; }
        

        double MidX { get; }

        double MidY { get; }

        double MidZ { get; }
    }
}