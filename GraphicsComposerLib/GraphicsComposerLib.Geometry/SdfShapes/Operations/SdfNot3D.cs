using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfNot3D : SdfUnaryOperation
    {
        public override double GetScalarDistance(ITuple3D point)
        {
            return -Surface.GetScalarDistance(point);
        }
    }
}
