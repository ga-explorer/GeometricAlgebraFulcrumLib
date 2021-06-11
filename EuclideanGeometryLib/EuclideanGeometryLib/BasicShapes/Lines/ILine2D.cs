using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;

namespace EuclideanGeometryLib.BasicShapes.Lines
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