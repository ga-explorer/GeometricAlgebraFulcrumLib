using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Planes.Space3D.Float64;

public interface IFloat64Plane3D :
    IAlgebraicElement
{
    double OriginX { get; }

    double OriginY { get; }

    double OriginZ { get; }


    double Direction1X { get; }

    double Direction1Y { get; }

    double Direction1Z { get; }


    double Direction2X { get; }

    double Direction2Y { get; }

    double Direction2Z { get; }


    Float64Plane3D ToPlane();
}