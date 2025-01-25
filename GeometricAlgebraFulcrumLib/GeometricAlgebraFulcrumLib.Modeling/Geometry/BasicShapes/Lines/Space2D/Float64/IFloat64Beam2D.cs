using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public interface IFloat64Beam2D :
    IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }


    double Direction1X { get; }

    double Direction1Y { get; }


    double Direction2X { get; }

    double Direction2Y { get; }
}