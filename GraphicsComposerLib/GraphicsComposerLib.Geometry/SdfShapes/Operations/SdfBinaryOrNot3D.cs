using System;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBinaryOrNot3D : SdfBinaryOperation
    {
        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return Math.Min(
                Surface1.GetScalarDistance(point),
                -Surface2.GetScalarDistance(point)
            );
        }
    }
}
