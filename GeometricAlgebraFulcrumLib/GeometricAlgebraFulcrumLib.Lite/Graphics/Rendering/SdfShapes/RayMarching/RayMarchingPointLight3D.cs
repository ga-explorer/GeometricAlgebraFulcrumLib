using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.SdfShapes.RayMarching
{
    public sealed class RayMarchingPointLight3D
    {
        public Float64Vector3D Position { get; set; }
            = Float64Vector3D.Create(0, 2, 4);

        public Float64Vector3D AmbientColor { get; set; }
            = Float64Vector3D.Create(0.5, 0.5, 0.5);

        public Float64Vector3D DiffuseColor { get; set; }
            = Float64Vector3D.Create(0.4, 0.4, 0.4);

        public Float64Vector3D SpecularColor { get; set; }
            = Float64Vector3D.Create(0.4, 0.4, 0.4);

        public Float64Vector3D EyePoint { get; set; }


        public Float64Vector3D GetColor(Float64Vector3D point, Float64Vector3D unitNormal, RayMarchingMaterial material)
        {
            var color = AmbientColor.ComponentsProduct(material.AmbientColor);

            var vectorL = (Position - point).ToUnitVector();
            var vectorV = (EyePoint - point).ToUnitVector();
            var vectorR = unitNormal.ReflectVectorOnUnitVector(-vectorL);
            
            var dotLN = vectorL.ESp(unitNormal);
            var dotRV = vectorR.ESp(vectorV);
            
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
