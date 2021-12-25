using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;

namespace NumericalGeometryLib.BasicShapes.Lines
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