using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;

public interface IFloat64LineSegment3D :
    IFloat64BoundedShape3D,
    IPair<LinFloat64Vector3D>
{
    double Point1X { get; }

    double Point1Y { get; }

    double Point1Z { get; }


    double Point2X { get; }

    double Point2Y { get; }

    double Point2Z { get; }


    LinFloat64Vector3D Point1 { get; }

    LinFloat64Vector3D Point2 { get; }
}