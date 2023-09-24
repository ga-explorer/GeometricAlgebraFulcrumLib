using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines
{
    public interface ILine3D : IGeometricElement
    {
        double OriginX { get; }

        double OriginY { get; }

        double OriginZ { get; }


        double DirectionX { get; }

        double DirectionY { get; }

        double DirectionZ { get; }


        Line3D ToLine();
    }
}