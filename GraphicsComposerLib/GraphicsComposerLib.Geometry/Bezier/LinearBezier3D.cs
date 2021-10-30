using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Bezier
{
    public sealed class LinearBezier3D
    {
        public Tuple3D P0 { get; }

        public Tuple3D P1 { get; }

        public Tuple3D this[double t]
            => t.Lerp(P0, P1);


        public LinearBezier3D(Tuple3D p0, Tuple3D p1)
        {
            P0 = new Tuple3D(p0);
            P1 = new Tuple3D(p1);
        }


        public Tuple3D GetTangentVector(double t)
        {
            return P1 - P0;
        }


    }
}