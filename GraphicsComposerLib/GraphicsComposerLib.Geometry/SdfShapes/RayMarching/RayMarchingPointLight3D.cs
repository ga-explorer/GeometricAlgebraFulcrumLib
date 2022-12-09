using System;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.RayMarching
{
    public sealed class RayMarchingPointLight3D
    {
        public Float64Tuple3D Position { get; set; }
            = new Float64Tuple3D(0, 2, 4);

        public Float64Tuple3D AmbientColor { get; set; }
            = new Float64Tuple3D(0.5, 0.5, 0.5);

        public Float64Tuple3D DiffuseColor { get; set; }
            = new Float64Tuple3D(0.4, 0.4, 0.4);

        public Float64Tuple3D SpecularColor { get; set; }
            = new Float64Tuple3D(0.4, 0.4, 0.4);

        public Float64Tuple3D EyePoint { get; set; }


        public Float64Tuple3D GetColor(Float64Tuple3D point, Float64Tuple3D unitNormal, RayMarchingMaterial material)
        {
            var color = AmbientColor.ComponentsProduct(material.AmbientColor);

            var vectorL = (Position - point).ToUnitVector();
            var vectorV = (EyePoint - point).ToUnitVector();
            var vectorR = unitNormal.ReflectVectorOnUnitVector(-vectorL);
            
            var dotLN = vectorL.VectorDot(unitNormal);
            var dotRV = vectorR.VectorDot(vectorV);
            
            if (dotLN < 0.0) {
                // Light not visible from this point on the surface
                // apply only ambient component
                return color;
            } 

            var diffuseFactor = 
                dotLN * material.DiffuseColor;

            color += DiffuseColor.ComponentsProduct(diffuseFactor);

            if (dotRV < 0.0) {
                // Light reflection in opposite direction as viewer, 
                // apply only ambient and diffuse components
                return color;
            }

            var specularFactor = 
                Math.Pow(dotRV, material.SpecularShininess) * 
                material.SpecularColor;

            color += SpecularColor.ComponentsProduct(specularFactor);

            return color;
        }
    }
}
