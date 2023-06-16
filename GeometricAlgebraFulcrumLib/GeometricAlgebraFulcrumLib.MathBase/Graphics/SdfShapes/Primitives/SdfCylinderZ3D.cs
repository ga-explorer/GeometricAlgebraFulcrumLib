using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes.Primitives
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfCylinderZ3D : ScalarDistanceFunction
    {
        public Float64Vector2D CenterXy { get; set; }
            = new Float64Vector2D(0, 0);

        public double Radius { get; set; }


        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return (point.XyToTuple2D() - CenterXy).ENorm() - Radius;
        }
    }
}
