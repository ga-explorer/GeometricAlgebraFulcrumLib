using System.Linq;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfOr3D : SdfAggregation
    {
        public override double GetScalarDistance(ITuple3D point)
        {
            return Surfaces.Min(s => s.GetScalarDistance(point));
        }
    }
}
