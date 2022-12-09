using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfLineSegment3D : ScalarDistanceFunction
    {
        public Float64Tuple3D Point1 { get; set; }
            = new Float64Tuple3D(0, 0, 0);
            
        public Float64Tuple3D Point2 { get; set; }
            = new Float64Tuple3D(1, 0, 0);

        public double Radius { get; set; }
            = 0.1d;

        public Float64Tuple3D Direction => Point2 - Point1;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var pa = point - Point1; 
            var ba = Direction;

            var h = (pa.VectorDot(ba) / ba.GetVectorNormSquared()).ClampToUnit();

            return (pa - ba * h).GetVectorNorm() - Radius;
        }
    }
}
