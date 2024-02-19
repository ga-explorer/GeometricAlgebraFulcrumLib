using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.SdfShapes.RayMarching;

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

    public Float64Vector3D EyePoint 
        => Float64Vector3D.Create(0, 0, EyeDistance);


    public Float64Vector3D GetRayDirection(int pixelX, int pixelY)
    {
        var x = pixelX - ResolutionX / 2.0d;
        var y = pixelY - ResolutionY / 2.0d;
        var z = ResolutionY / Math.Tan(VerticalFieldOfViewAngle / 2.0d);

        var length = Math.Sqrt(x * x + y * y + z * z);

        return Float64Vector3D.Create(x / length, 
            y / length, 
            -z / length);
    }
}