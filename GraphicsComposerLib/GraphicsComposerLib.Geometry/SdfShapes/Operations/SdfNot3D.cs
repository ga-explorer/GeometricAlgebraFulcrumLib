using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfNot3D : SdfUnaryOperation
    {
        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            return -Surface.GetScalarDistance(point);
        }
    }
}
