using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.SdfShapes.Operations
{
    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfRounding3D : SdfUnaryOperation
    {
        public double Radius { get; set; }
            = 0.1d;


        public override double GetScalarDistance(ITuple3D point)
        {
            return Surface.GetScalarDistance(point) - Radius;
        }
    }
}
