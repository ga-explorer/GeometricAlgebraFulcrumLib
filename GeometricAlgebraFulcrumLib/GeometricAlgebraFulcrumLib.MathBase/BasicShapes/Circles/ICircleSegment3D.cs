namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Circles
{
    public interface ICircleSegment3D : IFiniteGeometricShape3D
    {
        double Direction1X { get; }

        double Direction1Y { get; }

        double Direction1Z { get; }


        double Direction2X { get; }

        double Direction2Y { get; }

        double Direction2Z { get; }


        double CenterX { get; }

        double CenterY { get; }

        double CenterZ { get; }


        double OriginX { get; }

        double OriginY { get; }

        double OriginZ { get; }


        double TurnsValue { get; }
    }
}