using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Colors;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.SdfGeometry.Primitives;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class RayMarchingScene3D
{
    public Color BackgroundColor { get; set; }
        = Color.Transparent;

    public RayMarchingCamera3D Camera { get; }
        = new RayMarchingCamera3D();

    public List<RayMarchingPointLight3D> LightsList { get; }
        = new List<RayMarchingPointLight3D>();

    public RayMarchingShape3D Shape { get; set; }
        = new RayMarchingShape3D(new SdfSphere3D());

    public SdfRayMarchingComputer3D Computer { get; }
        = new SdfRayMarchingComputer3D();


    public Image Render()
    {
        var image = new Image<Rgba32>(
            Camera.ResolutionX, 
            Camera.ResolutionY
        );
            
        var rayOrigin = Camera.EyePoint;

        var light1 = new RayMarchingPointLight3D() 
        { 
            Position = LinFloat64Vector3D.Create(0, 2, 4),
            AmbientColor = LinFloat64Vector3D.Create(0.5, 0.5, 0.5),
            DiffuseColor = LinFloat64Vector3D.Create(0.4, 0.4, 0.4),
            SpecularColor = LinFloat64Vector3D.Create(0.4, 0.4, 0.4),
            EyePoint = Camera.EyePoint 
        };

        var light2 = new RayMarchingPointLight3D() 
        { 
            Position = LinFloat64Vector3D.Create(2 * Math.Sin(0.37), 2 * Math.Cos(0.37), 2),
            AmbientColor = LinFloat64Vector3D.Create(0.5, 0.5, 0.5),
            DiffuseColor = LinFloat64Vector3D.Create(0.4, 0.4, 0.4),
            SpecularColor = LinFloat64Vector3D.Create(0.4, 0.4, 0.4),
            EyePoint = Camera.EyePoint 
        };
            
        LightsList.Add(light1);
        LightsList.Add(light2);

        Computer.ResetCounters();

        for (var pixelX = 0; pixelX < image.Width; pixelX++)
        {
            for (var pixelY = 0; pixelY < image.Height; pixelY++)
            {
                var rayDirection = Camera.GetRayDirection(pixelX, pixelY);
                var ray = new Line3D(rayOrigin, rayDirection);
                    
                var intersectionInfo = 
                    Computer.ComputeIntersection(Shape.Surface, ray);

                var colorVector = BackgroundColor.RgbToVector3D();

                if (intersectionInfo.Item1)
                {
                    var surfacePoint = 
                        rayOrigin + intersectionInfo.Item2 * rayDirection;

                    var unitNormal = 
                        Shape.Surface.ComputeSdfNormal(surfacePoint);

                    colorVector = LinFloat64Vector3D.Create(0, 0, 0);
                    foreach (var light in LightsList)
                        colorVector += light.GetColor(
                            surfacePoint, 
                            unitNormal, 
                            Shape.Material
                        ); 
                }

                image[pixelX, pixelY] = colorVector.ToRgbColor();
            }
        }

        Console.WriteLine("Computer Min Iterations: " + Computer.MinStepsCounter);
        Console.WriteLine("Computer Max Iterations: " + Computer.MaxStepsCounter);
        Console.WriteLine();

        return image;
    }
}