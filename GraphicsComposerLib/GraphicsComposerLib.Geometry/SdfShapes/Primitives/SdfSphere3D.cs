using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfSphere3D : ScalarDistanceFunction
    {
        public double Radius { get; set; } = 0.5d;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var sdf = point.GetVectorNorm() - Radius;
            return SdfAlpha * sdf - SdfDelta;
        }
    }
}
