namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles
{
    public interface ITriangle3D : IFiniteGeometricShape3D, IIntersectable
    {
        double Point1X { get; }

        double Point1Y { get; }

        double Point1Z { get; }


        double Point2X { get; }

        double Point2Y { get; }

        double Point2Z { get; }


        double Point3X { get; }

        double Point3Y { get; }

        double Point3Z { get; }
    }
}