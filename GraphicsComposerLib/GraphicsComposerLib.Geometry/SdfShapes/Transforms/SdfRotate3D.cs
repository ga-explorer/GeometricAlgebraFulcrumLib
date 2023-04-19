using System;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotate3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;

        public Float64Tuple3D AxisOrigin { get; set; }
            = new Float64Tuple3D(0, 0, 0);

        public Float64Tuple3D AxisUnitDirection { get; set; }
            = new Float64Tuple3D(1, 0, 0);

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);
            var v = point - AxisOrigin;
            var q = 
                AxisOrigin + 
                cosAngle * v +
                (1 - cosAngle) * AxisUnitDirection.VectorDot(v) * AxisUnitDirection +
                sinAngle * AxisUnitDirection.VectorCross(v);

            return Surface.GetScalarDistance(q);
        }
    }
}
