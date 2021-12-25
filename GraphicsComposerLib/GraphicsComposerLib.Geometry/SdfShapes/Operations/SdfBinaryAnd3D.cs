using System;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{

    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBinaryAnd3D : SdfBinaryOperation
    {
        public override double GetScalarDistance(ITuple3D point)
        {
            return Math.Max(
                Surface1.GetScalarDistance(point),
                Surface2.GetScalarDistance(point)
            );
        }
    }
}
