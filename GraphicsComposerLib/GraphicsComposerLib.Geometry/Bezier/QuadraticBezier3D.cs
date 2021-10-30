using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Bezier
{
    public sealed class QuadraticBezier3D
    {
        public Tuple3D P0 { get; }

        public Tuple3D P1 { get; }

        public Tuple3D P2 { get; }

        public Tuple3D this[double t]
            => t.DeCasteljau(P0, P1, P2);


        public QuadraticBezier3D(Tuple3D p0, Tuple3D p1, Tuple3D p2)
        {
            P0 = new Tuple3D(p0);
            P1 = new Tuple3D(p1);
            P2 = new Tuple3D(p2);
        }


        public Tuple3D GetTangentVector(double t)
        {
            return new Tuple3D(
                2.0d * (P2.X - P0.X),
                2.0d * (P2.Y - P0.Y),
                2.0d * (P2.Z - P0.Z)
                );
        }

        public Tuple3D GetOrthogonalBivector(double t)
        {
            //TODO: Generate this with GMac
            return new Tuple3D(
                2.0d * (P2.X - P0.X),
                2.0d * (P2.Y - P0.Y),
                2.0d * (P2.Z - P0.Z)
            );
        }


        public LinearBezier3D FirstDerivative()
        {
            return new LinearBezier3D(
                2.0d * (P1 - P0),
                2.0d * (P2 - P1)
            );
        }

    }
}
