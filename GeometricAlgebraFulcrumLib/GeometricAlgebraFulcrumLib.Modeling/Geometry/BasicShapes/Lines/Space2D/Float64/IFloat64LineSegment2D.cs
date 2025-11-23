using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;

public interface IFloat64LineSegment2D :
    IFloat64BoundedShape2D,
    IPair<LinFloat64Vector2D>
{
    double Point1X { get; }

    double Point1Y { get; }
    
    double Point2X { get; }

    double Point2Y { get; }

    LinFloat64Vector2D Point1 { get; }

    LinFloat64Vector2D Point2 { get; }
}