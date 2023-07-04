using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderY3D : ScalarDistanceFunction
    {
        public Float64Vector2D CenterXz { get; set; }
            = Float64Vector2D.Create((Float64Scalar)0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            return (point.XzToVector2D() - CenterXz).ENorm() - Radius;
        }
    }
}
