using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class RayMarchingCamera3D
{
    public double VerticalFieldOfViewAngle{ get; set; }
        = Math.PI / 2;

    public double EyeDistance { get; set; }
        = 5.0d;

    public int ResolutionX { get; set; }
        = 640;

    public int ResolutionY { get; set; }
        = 640;

    public LinFloat64Vector3D EyePoint 
        => LinFloat64Vector3D.Create(0, 0, EyeDistance);


    public LinFloat64Vector3D GetRayDirection(int pixelX, int pixelY)
    {
        var x = pixelX - ResolutionX / 2.0d;
        var y = pixelY - ResolutionY / 2.0d;
        var z = ResolutionY / Math.Tan(VerticalFieldOfViewAngle / 2.0d);

        var length = Math.Sqrt(x * x + y * y + z * z);

        return LinFloat64Vector3D.Create(x / length, 
            y / length, 
            -z / length);
    }
}