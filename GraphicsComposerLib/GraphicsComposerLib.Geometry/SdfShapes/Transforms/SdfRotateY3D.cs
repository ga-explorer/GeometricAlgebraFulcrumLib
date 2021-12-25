using System;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Transforms
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRotateY3D : SdfUnaryOperation
    {
        public double Angle { get; set; }
            = 0;
        
        public override double GetScalarDistance(ITuple3D point)
        {
            var cosAngle = Math.Cos(-Angle);
            var sinAngle = Math.Sin(-Angle);

            var q = new Tuple3D(
                point.X * cosAngle + point.Z * sinAngle,
                point.Y,
                point.Z * cosAngle - point.X * sinAngle
            );

            return Surface.GetScalarDistance(q);
        }
    }
}
