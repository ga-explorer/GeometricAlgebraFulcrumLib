using System;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.SdfGeometry;

namespace GraphicsComposerLib.RayMarching
{
    public sealed class RayMarchingPointLight3D
    {
        public Tuple3D Position { get; set; }
            = new Tuple3D(0, 2, 4);

        public Tuple3D AmbientColor { get; set; }
            = new Tuple3D(0.5, 0.5, 0.5);

        public Tuple3D DiffuseColor { get; set; }
            = new Tuple3D(0.4, 0.4, 0.4);

        public Tuple3D SpecularColor { get; set; }
            = new Tuple3D(0.4, 0.4, 0.4);

        public Tuple3D EyePoint { get; set; }


        public Tuple3D GetColor(Tuple3D point, Tuple3D unitNormal, RayMarchingMaterial material)
        {
            var color = AmbientColor.ElementsProduct(material.AmbientColor);

            var vectorL = (Position - point).ToUnitVector();
            var vectorV = (EyePoint - point).ToUnitVector();
            var vectorR = (-vectorL).ReflectVectorOnUnitVector(unitNormal);
            
            var dotLN = vectorL.DotProduct(unitNormal);
            var dotRV = vectorR.DotProduct(vectorV);
            
            if (dotLN < 0.0) {
                // Light not visible from this point on the surface
                // apply only ambient component
                return color;
            } 

            var diffuseFactor = 
                dotLN * material.DiffuseColor;

            color += DiffuseColor.ElementsProduct(diffuseFactor);

            if (dotRV < 0.0) {
                // Light reflection in opposite direction as viewer, 
                // apply only ambient and diffuse components
                return color;
            }

            var specularFactor = 
                Math.Pow(dotRV, material.SpecularShininess) * 
                material.SpecularColor;

            color += SpecularColor.ElementsProduct(specularFactor);

            return color;
        }
    }
}
