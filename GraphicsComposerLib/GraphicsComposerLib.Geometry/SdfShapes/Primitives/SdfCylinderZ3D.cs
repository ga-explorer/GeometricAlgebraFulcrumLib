using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderZ3D : ScalarDistanceFunction
    {
        public Tuple2D CenterXy { get; set; }
            = new Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(ITuple3D point)
        {
            return (point.XyToTuple2D() - CenterXy).GetLength() - Radius;
        }
    }
}
