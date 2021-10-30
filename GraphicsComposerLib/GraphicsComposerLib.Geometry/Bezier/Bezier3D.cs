using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.Bezier
{
    public sealed class Bezier3D
    {
        public List<Tuple3D> ControlPoints { get; } = new List<Tuple3D>();

        public int Degree => ControlPoints.Count - 1;


        public Tuple3D this[double t]
            => t.DeCasteljau(ControlPoints.ToArray());


        public Bezier3D FirstDerivative()
        {
            var result = new Bezier3D();

            for (var n = 0; n < Degree; n++)
                result.ControlPoints.Add(Degree * (ControlPoints[n + 1] - ControlPoints[n]));

            return result;
        }

    }
}
