using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable
{
    public sealed class LinePair3D : ILinePair3D
    {
        public static LinePair3D Create(ILine3D line1, ILine3D line2)
        {
            return new LinePair3D(
                line1.OriginX, 
                line1.OriginY,
                line1.OriginZ,
                line1.DirectionX, 
                line1.DirectionY,
                line1.DirectionZ,
                line2.OriginX, 
                line2.OriginY,
                line2.OriginZ,
                line2.DirectionX, 
                line2.DirectionY,
                line2.DirectionZ
            );
        }

        public static LinePair3D Create(IFloat64Vector3D origin1, IFloat64Vector3D direction1, IFloat64Vector3D origin2, IFloat64Vector3D direction2)
        {
            return new LinePair3D(
                origin1.X, 
                origin1.Y,
                origin1.Z,
                direction1.X, 
                direction1.Y,
                direction1.Z,
                origin2.X, 
                origin2.Y,
                origin2.Z,
                direction2.X, 
                direction2.Y,
                direction2.Z
            );
        }


        public double Origin1X { get; }

        public double Origin1Y { get; }

        public double Origin1Z { get; }


        public double Origin2X { get; }

        public double Origin2Y { get; }

        public double Origin2Z { get; }


        public double Direction1X { get; }

        public double Direction1Y { get; }

        public double Direction1Z { get; }


        public double Direction2X { get; }

        public double Direction2Y { get; }

        public double Direction2Z { get; }

        public bool IsValid()
        {
            return !double.IsNaN(Origin1X) &&
                   !double.IsNaN(Origin1Y) &&
                   !double.IsNaN(Direction1X) &&
                   !double.IsNaN(Direction1Y) &&
                   !double.IsNaN(Origin2X) &&
                   !double.IsNaN(Origin2Y) &&
                   !double.IsNaN(Direction2X) &&
                   !double.IsNaN(Direction2Y);
        }


        internal LinePair3D(double o1X, double o1Y, double o1Z, double d1X, double d1Y, double d1Z, double o2X, double o2Y, double o2Z, double d2X, double d2Y, double d2Z)
        {
            Origin1X = o1X;
            Origin1Y = o1Y;
            Origin1Z = o1Z;

            Origin2X = o2X;
            Origin2Y = o2Y;
            Origin2Z = o2Z;

            Direction1X = d1X;
            Direction1Y = d1Y;
            Direction1Z = d1Z;

            Direction2X = d2X;
            Direction2Y = d2Y;
            Direction2Z = d2Z;
        }


        
    }
}
