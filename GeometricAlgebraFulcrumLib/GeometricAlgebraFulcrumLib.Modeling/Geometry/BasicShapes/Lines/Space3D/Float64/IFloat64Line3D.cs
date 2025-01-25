using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public interface IFloat64Line3D :
    IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }

    double OriginZ { get; }


    double DirectionX { get; }

    double DirectionY { get; }

    double DirectionZ { get; }


    Float64Line3D ToLine();
}