namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

public interface IFloat64BoundingBox3D :
    IFloat64BorderSurface3D
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