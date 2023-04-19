using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Planes.Immutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Planes
{
    public interface IPlane3D : IGeometricElement
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


        Plane3D ToPlane();
    }
}