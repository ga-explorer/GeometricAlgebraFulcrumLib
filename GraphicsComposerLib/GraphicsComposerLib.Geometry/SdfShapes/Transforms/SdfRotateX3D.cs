using System;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotateX3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;
        
        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);

            var q = new Float64Tuple3D(
                point.X,
                point.Y * cosAngle - point.Z * sinAngle,
                point.Z * cosAngle + point.Y * sinAngle
            );

            return Surface.GetScalarDistance(q);
        }
    }
}
