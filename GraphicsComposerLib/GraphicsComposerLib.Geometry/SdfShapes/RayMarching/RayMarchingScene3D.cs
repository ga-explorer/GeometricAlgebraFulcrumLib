using System;
using System.Collections.Generic;
using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.SdfShapes.Primitives;

namespace GraphicsComposerLib.Geometry.SdfShapes.RayMarching
{
    public sealed class RayMarchingScene3D
    {
        public Tuple3D BackgroundColor { get; set; }
            = Color.Black.ToTuple3D();

        public RayMarchingCamera3D Camera { get; }
            = new RayMarchingCamera3D();

        public List<RayMarchingPointLight3D> LightsList { get; }
            = new List<RayMarchingPointLight3D>();

        public RayMarchingShape3D Shape { get; set; }
            = new RayMarchingShape3D(new SdfSphere3D());

        public SdfRayMarchingComputer3D Computer { get; }
            = new SdfRayMarchingComputer3D();


        public Bitmap Render()
        {
            var image = new Bitmap(
                Camera.ResolutionX, 
                Camera.ResolutionY
            );
            
            var rayOrigin = Camera.EyePoint;

            var light1 = new RayMarchingPointLight3D() 
            { 
                Position = new Tuple3D(0, 2, 4),
                AmbientColor = new Tuple3D(0.5, 0.5, 0.5),
                DiffuseColor = new Tuple3D(0.4, 0.4, 0.4),
                SpecularColor = new Tuple3D(0.4, 0.4, 0.4),
                EyePoint = Camera.EyePoint 
            };

            var light2 = new RayMarchingPointLight3D() 
            { 
                Position = new Tuple3D(2 * Math.Sin(0.37), 2 * Math.Cos(0.37), 2),
                AmbientColor = new Tuple3D(0.5, 0.5, 0.5),
                DiffuseColor = new Tuple3D(0.4, 0.4, 0.4),
                SpecularColor = new Tuple3D(0.4, 0.4, 0.4),
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

                    var colorVector = BackgroundColor;

                    if (intersectionInfo.Item1)
                    {
                        var surfacePoint = 
                            rayOrigin + intersectionInfo.Item2 * rayDirection;

                        var unitNormal = 
                            Shape.Surface.ComputeSdfNormal(surfacePoint);

                        colorVector = new Tuple3D(0, 0, 0);
                        foreach (var light in LightsList)
                            colorVector += light.GetColor(
                                surfacePoint, 
                                unitNormal, 
                                Shape.Material
                            ); 
                    }

                    image.SetPixel(pixelX, pixelY, colorVector.ToColor());
                }
            }

            Console.WriteLine("Computer Min Iterations: " + Computer.MinStepsCounter);
            Console.WriteLine("Computer Max Iterations: " + Computer.MaxStepsCounter);
            Console.WriteLine();

            return image;
        }
    }
}
