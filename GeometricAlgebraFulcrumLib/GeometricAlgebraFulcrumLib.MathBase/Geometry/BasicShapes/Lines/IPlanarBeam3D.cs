using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines
{
    public interface IPlanarBeam3D : IGeometricElement
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
    }
}