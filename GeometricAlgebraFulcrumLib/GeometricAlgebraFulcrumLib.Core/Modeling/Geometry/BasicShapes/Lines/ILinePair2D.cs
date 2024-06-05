using GeometricAlgebraFulcrumLib.Core.Algebra;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;

public interface ILinePair2D : IAlgebraicElement
{
    double Origin1X { get; }

    double Origin1Y { get; }


    double Origin2X { get; }

    double Origin2Y { get; }


    double Direction1X { get; }

    double Direction1Y { get; }


    double Direction2X { get; }

    double Direction2Y { get; }
}