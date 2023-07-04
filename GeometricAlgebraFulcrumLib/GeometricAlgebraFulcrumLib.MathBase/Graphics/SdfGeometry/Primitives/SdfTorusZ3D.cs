using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfTorusZ3D : ScalarDistanceFunction
    {
        public double InnerRadius { get; set; }
            = 0.5d;
        
        public double OuterRadius { get; set; }
            = 1.0d;


        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            var q = Float64Vector2D.Create((Float64Scalar)(point.LengthXy() - InnerRadius), point.Z);

            return q.ENorm() - OuterRadius;
        }
    }
}
