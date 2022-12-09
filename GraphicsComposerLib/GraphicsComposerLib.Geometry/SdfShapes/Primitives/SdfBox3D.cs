using System;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.Primitives
{

    /// <summary>
    /// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
    /// </summary>
    public sealed class SdfBox3D : ScalarDistanceFunction
    {
        public Float64Tuple3D SideHalfLengths { get; set; }
            = new Float64Tuple3D(1, 1, 1);
            

        public override double GetScalarDistance(IFloat64Tuple3D point)
        {
            var d = point.ComponentsAbs() - SideHalfLengths;

            var v1 = d.ComponentsMax(0.0d).GetVectorNorm();
            
            // remove v2 for an only partially signed sdf 
            var v2 = Math.Min(Math.Max(d.X, Math.Max(d.Y, d.Z)), 0.0d);

            return v1 + v2;
        }
    }
}
