using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfSphere3D : ScalarDistanceFunction
    {
        public double Radius { get; set; } = 0.5d;


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var sdf = point.ENorm() - Radius;
            return SdfAlpha * sdf - SdfDelta;
        }
    }
}
