using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Bezier
{
    public sealed class CubicBezier3D
    {
        public Tuple3D P0 { get; }

        public Tuple3D P1 { get; }

        public Tuple3D P2 { get; }

        public Tuple3D P3 { get; }

        public Tuple3D this[double t]
            => t.DeCasteljau(P0, P1, P2, P3);


        public CubicBezier3D(Tuple3D p0, Tuple3D p1, Tuple3D p2, Tuple3D p3)
        {
            P0 = new Tuple3D(p0);
            P1 = new Tuple3D(p1);
            P2 = new Tuple3D(p2);
            P3 = new Tuple3D(p3);
        }

        public Tuple3D GetTangentVector(double t)
        {
            var s = 1.0d - t;

            var x0 = 3.0d * (P1.X - P0.X);
            var y0 = 3.0d * (P1.Y - P0.Y);
            var z0 = 3.0d * (P1.Z - P0.Z);

            var x1 = 3.0d * (P2.X - P1.X);
            var y1 = 3.0d * (P2.Y - P1.Y);
            var z1 = 3.0d * (P2.Z - P1.Z);

            var x2 = 3.0d * (P3.X - P2.X);
            var y2 = 3.0d * (P3.Y - P2.Y);
            var z2 = 3.0d * (P3.Z - P2.Z);

            x0 = s * x0 + t * x1;
            y0 = s * y0 + t * y1;
            z0 = s * z0 + t * z1;

            x1 = s * x1 + t * x2;
            y1 = s * y1 + t * y2;
            z1 = s * z1 + t * z2;

            return new Tuple3D(
                s * x0 + t * x1,
                s * y0 + t * y1,
                s * z0 + t * z1
            );
        }


        public QuadraticBezier3D FirstDerivative()
        {
            return new QuadraticBezier3D(
                3.0d * (P1 - P0),
                3.0d * (P2 - P1),
                3.0d * (P3 - P2)
                );
        }
    }
}