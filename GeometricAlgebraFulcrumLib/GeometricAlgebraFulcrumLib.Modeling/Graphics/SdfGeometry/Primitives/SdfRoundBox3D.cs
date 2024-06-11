using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfRoundBox3D : ScalarDistanceFunction
{
    public LinFloat64Vector3D SideHalfLengths { get; set; }
        = LinFloat64Vector3D.Create(0.5d, 0.5d, 0.5d);
        
    public double Radius { get; set; }
        = 0.1d;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var d = point.ComponentsAbs() - SideHalfLengths;

        var v1 = d.ComponentsMax(0.0d).VectorENorm() - Radius;
            
        // remove v2 for an only partially signed sdf 
        var v2 = Math.Min(Math.Max(d.X, Math.Max(d.Y, d.Z)), 0.0d);

        return v1 + v2;
    }
}