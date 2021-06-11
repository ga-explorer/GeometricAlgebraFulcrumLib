using System.Linq;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.SdfGeometry.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfOr3D : SdfAggregation
    {
        public override double ComputeSdf(Tuple3D point)
        {
            return Surfaces.Min(s => s.ComputeSdf(point));
        }
    }
}
