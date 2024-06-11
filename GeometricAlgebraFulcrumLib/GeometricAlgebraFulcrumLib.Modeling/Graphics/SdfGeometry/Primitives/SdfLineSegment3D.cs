using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfLineSegment3D : ScalarDistanceFunction
{
    public LinFloat64Vector3D Point1 { get; set; }
        = LinFloat64Vector3D.Create(0, 0, 0);
            
    public LinFloat64Vector3D Point2 { get; set; }
        = LinFloat64Vector3D.Create(1, 0, 0);

    public double Radius { get; set; }
        = 0.1d;

    public LinFloat64Vector3D Direction => Point2 - Point1;


    public override double GetScalarDistance(ILinFloat64Vector3D point)
    {
        var pa = point - Point1; 
        var ba = Direction;

        var h = (pa.VectorESp(ba) / ba.VectorENormSquared()).ClampToUnit();

        return (pa - ba * h).VectorENorm() - Radius;
    }
}