using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.BasicShapes.Lines.Immutable
{
    public sealed class LineTriplet3D
    {
        public double Origin1X { get; }

        public double Origin1Y { get; }

        public double Origin1Z { get; }


        public double Origin2X { get; }

        public double Origin2Y { get; }

        public double Origin2Z { get; }


        public double Origin3X { get; }

        public double Origin3Y { get; }

        public double Origin3Z { get; }


        public double Direction1X { get; }

        public double Direction1Y { get; }

        public double Direction1Z { get; }


        public double Direction2X { get; }

        public double Direction2Y { get; }

        public double Direction2Z { get; }


        public double Direction3X { get; }

        public double Direction3Y { get; }

        public double Direction3Z { get; }


        public Tuple3D Origin1
            => new Tuple3D(Origin1X, Origin1Y, Origin1Z);

        public Tuple3D Origin2
            => new Tuple3D(Origin2X, Origin2Y, Origin2Z);

        public Tuple3D Origin3
            => new Tuple3D(Origin3X, Origin3Y, Origin3Z);


        public Tuple3D Direction1
            => new Tuple3D(Direction1X, Direction1Y, Direction1Z);

        public Tuple3D Direction2
            => new Tuple3D(Direction2X, Direction2Y, Direction2Z);

        public Tuple3D Direction3
            => new Tuple3D(Direction3X, Direction3Y, Direction3Z);


        public Line3D Line1
            => new Line3D(
                Origin1X, Origin1Y, Origin1Z,
                Direction1X, Direction1Y, Direction1Z
            );

        public Line3D Line2
            => new Line3D(
                Origin2X, Origin2Y, Origin2Z,
                Direction2X, Direction2Y, Direction2Z
            );

        public Line3D Line3
            => new Line3D(
                Origin3X, Origin3Y, Origin3Z,
                Direction3X, Direction3Y, Direction3Z
            );


        internal LineTriplet3D()
        {

        }
    }
}
