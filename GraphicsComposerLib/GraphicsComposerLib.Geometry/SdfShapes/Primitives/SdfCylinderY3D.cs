using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderY3D : ScalarDistanceFunction
    {
        public Tuple2D CenterXz { get; set; }
            = new Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(ITuple3D point)
        {
            return (point.XzToTuple2D() - CenterXz).GetLength() - Radius;
        }
    }
}
