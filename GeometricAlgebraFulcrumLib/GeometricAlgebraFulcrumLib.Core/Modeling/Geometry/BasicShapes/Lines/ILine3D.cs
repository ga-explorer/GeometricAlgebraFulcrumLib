using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;

public interface ILine3D : IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }

    double OriginZ { get; }


    double DirectionX { get; }

    double DirectionY { get; }

    double DirectionZ { get; }


    Line3D ToLine();
}