using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegmentZ3D : ScalarDistanceFunction
    {
        public double Length { get; set; }
            = 1.0d;

        public double Radius { get; set; }
            = 0.1d;


        public override double GetScalarDistance(ITuple3D point)
        {
            var v = new Tuple3D(
                point.X,
                point.Y,
                point.Z - point.Z.ClampTo(Length)
            ).GetLength();

            return v - Radius;
        }
    }
}
