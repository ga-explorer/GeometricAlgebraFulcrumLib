using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines
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