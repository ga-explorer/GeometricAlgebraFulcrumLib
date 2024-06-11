using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;

public interface ILineTriplet3D : IAlgebraicElement
{
    double Origin1X { get; }

    double Origin1Y { get; }

    double Origin1Z { get; }


    double Origin2X { get; }

    double Origin2Y { get; }

    double Origin2Z { get; }


    double Origin3X { get; }

    double Origin3Y { get; }

    double Origin3Z { get; }


    double Direction1X { get; }

    double Direction1Y { get; }

    double Direction1Z { get; }


    double Direction2X { get; }

    double Direction2Y { get; }

    double Direction2Z { get; }


    double Direction3X { get; }

    double Direction3Y { get; }

    double Direction3Z { get; }
}