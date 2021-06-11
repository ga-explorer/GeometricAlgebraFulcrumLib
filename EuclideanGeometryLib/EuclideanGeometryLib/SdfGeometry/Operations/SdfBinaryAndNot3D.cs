using System;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBinaryAndNot3D : SdfBinaryOperation
    {
        public override double ComputeSdf(Tuple3D point)
        {
            return Math.Max(
                Surface1.ComputeSdf(point),
                -Surface2.ComputeSdf(point)
            );
        }
    }
}
