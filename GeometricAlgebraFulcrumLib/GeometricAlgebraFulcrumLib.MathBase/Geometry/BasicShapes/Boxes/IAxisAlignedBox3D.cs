namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Boxes
{
    public interface IAxisAlignedBox3D : IFiniteGeometricShape3D
    {
        double Corner1X { get; }

        double Corner1Y { get; }

        double Corner1Z { get; }


        double Corner2X { get; }

        double Corner2Y { get; }

        double Corner2Z { get; }
    }
}