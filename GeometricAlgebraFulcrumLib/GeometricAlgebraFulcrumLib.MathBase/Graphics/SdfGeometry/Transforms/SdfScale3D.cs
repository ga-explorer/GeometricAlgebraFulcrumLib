using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfScale3D : SdfUnaryOperation
    {
        public double Factor { get; set; }
            = 1.0d;

        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            var s = 1d / Factor;
            var p = Float64Vector3D.Create(point.X * s,
                point.Y * s,
                point.Z * s);

            return Factor * Surface.GetScalarDistance(p);
        }
    }
}
