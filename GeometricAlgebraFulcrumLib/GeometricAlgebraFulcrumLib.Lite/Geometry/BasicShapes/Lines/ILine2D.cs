using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines
{
    public interface ILine2D : IGeometricElement
    {
        double OriginX { get; }

        double OriginY { get; }


        double DirectionX { get; }

        double DirectionY { get; }


        Line2D ToLine();
    }
}