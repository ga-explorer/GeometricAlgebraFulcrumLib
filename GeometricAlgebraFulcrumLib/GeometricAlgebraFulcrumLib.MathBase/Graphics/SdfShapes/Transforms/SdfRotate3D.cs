using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotate3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;

        public Float64Vector3D AxisOrigin { get; set; }
            = Float64Vector3D.Create(0, 0, 0);

        public Float64Vector3D AxisUnitDirection { get; set; }
            = Float64Vector3D.Create(1, 0, 0);

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);
            var v = point - AxisOrigin;
            var q = 
                AxisOrigin + 
                cosAngle * v +
                (1 - cosAngle) * AxisUnitDirection.ESp(v) * AxisUnitDirection +
                sinAngle * AxisUnitDirection.VectorCross(v);

            return Surface.GetScalarDistance(q);
        }
    }
}
