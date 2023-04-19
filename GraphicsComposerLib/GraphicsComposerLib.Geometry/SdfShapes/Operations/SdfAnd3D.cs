using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{

    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfAnd3D : SdfAggregation
    {
        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return Surfaces.Max(s => s.GetScalarDistance(point));
        }
    }
}
