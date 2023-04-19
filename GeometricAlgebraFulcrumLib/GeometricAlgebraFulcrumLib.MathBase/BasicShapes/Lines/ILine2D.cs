using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Lines
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