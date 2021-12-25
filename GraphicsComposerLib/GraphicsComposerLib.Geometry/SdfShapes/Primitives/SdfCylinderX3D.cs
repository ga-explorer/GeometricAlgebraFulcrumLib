using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderX3D : ScalarDistanceFunction
    {
        public Tuple2D CenterYz { get; set; }
            = new Tuple2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(ITuple3D point)
        {
            return (point.YzToTuple2D() - CenterYz).GetLength() - Radius;
        }
    }
}
