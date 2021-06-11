using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;

namespace EuclideanGeometryLib.BasicShapes.Lines
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