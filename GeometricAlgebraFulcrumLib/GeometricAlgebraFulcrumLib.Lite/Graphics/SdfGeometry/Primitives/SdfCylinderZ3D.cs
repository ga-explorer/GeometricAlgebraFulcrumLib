using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderZ3D : ScalarDistanceFunction
    {
        public Float64Vector2D CenterXy { get; set; }
            = Float64Vector2D.Create((Float64Scalar)0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            return (point.XyToVector2D() - CenterXy).ENorm() - Radius;
        }
    }
}
