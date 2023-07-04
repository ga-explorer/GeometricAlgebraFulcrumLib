using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfGeometry.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotateZ3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;
        
        public override double GetScalarDistance(IFloat64Vector3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);

            var q = Float64Vector3D.Create(point.X * cosAngle - point.Y * sinAngle,
                point.Y * cosAngle + point.X * sinAngle,
                point.Z);

            return Surface.GetScalarDistance(q);
        }
    }
}
