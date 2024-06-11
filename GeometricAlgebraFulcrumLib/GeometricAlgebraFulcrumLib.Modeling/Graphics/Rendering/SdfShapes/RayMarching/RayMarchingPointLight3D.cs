using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class RayMarchingPointLight3D
{
    public LinFloat64Vector3D Position { get; set; }
        = LinFloat64Vector3D.Create(0, 2, 4);

    public LinFloat64Vector3D AmbientColor { get; set; }
        = LinFloat64Vector3D.Create(0.5, 0.5, 0.5);

    public LinFloat64Vector3D DiffuseColor { get; set; }
        = LinFloat64Vector3D.Create(0.4, 0.4, 0.4);

    public LinFloat64Vector3D SpecularColor { get; set; }
        = LinFloat64Vector3D.Create(0.4, 0.4, 0.4);

    public LinFloat64Vector3D EyePoint { get; set; }


    public LinFloat64Vector3D GetColor(LinFloat64Vector3D point, LinFloat64Vector3D unitNormal, RayMarchingMaterial material)
    {
        var color = AmbientColor.ComponentsProduct(material.AmbientColor);

        var vectorL = (Position - point).ToUnitLinVector3D();
        var vectorV = (EyePoint - point).ToUnitLinVector3D();
        var vectorR = unitNormal.ReflectVectorOnUnitVector(-vectorL);
            
        var dotLN = vectorL.VectorESp(unitNormal);
        var dotRV = vectorR.VectorESp(vectorV);
            
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