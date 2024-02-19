using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.SdfGeometry.Primitives;

/// <summary>
/// http://iquilezles.org/www/articles/distfunctions/distfunctions.htm
/// </summary>
public sealed class SdfLineSegment3D : ScalarDistanceFunction
{
    public Float64Vector3D Point1 { get; set; }
        = Float64Vector3D.Create(0, 0, 0);
            
    public Float64Vector3D Point2 { get; set; }
        = Float64Vector3D.Create(1, 0, 0);

    public double Radius { get; set; }
        = 0.1d;

    public Float64Vector3D Direction => Point2 - Point1;


    public override double GetScalarDistance(IFloat64Vector3D point)
    {
        var pa = point - Point1; 
        var ba = Direction;

        var h = (pa.ESp(ba) / ba.ENormSquared()).ClampToUnit();

        return (pa - ba * h).ENorm() - Radius;
    }
}