using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public interface IFloat64LinePair3D :
    IAlgebraicElement
{
    double Origin1X { get; }

    double Origin1Y { get; }

    double Origin1Z { get; }


    double Origin2X { get; }

    double Origin2Y { get; }

    double Origin2Z { get; }


    double Direction1X { get; }

    double Direction1Y { get; }

    double Direction1Z { get; }


    double Direction2X { get; }

    double Direction2Y { get; }

    double Direction2Z { get; }
}