using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;

public interface IBeam2D : IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }


    double Direction1X { get; }

    double Direction1Y { get; }


    double Direction2X { get; }

    double Direction2Y { get; }
}