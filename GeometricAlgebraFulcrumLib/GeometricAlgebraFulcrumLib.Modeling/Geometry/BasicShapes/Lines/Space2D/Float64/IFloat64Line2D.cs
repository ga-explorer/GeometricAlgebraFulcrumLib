using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public interface IFloat64Line2D :
    IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }


    double DirectionX { get; }

    double DirectionY { get; }


    Float64Line2D ToLine();
}