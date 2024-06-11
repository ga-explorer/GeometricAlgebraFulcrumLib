using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.SdfShapes.RayMarching;

public sealed class RayMarchingMaterial
{
    public LinFloat64Vector3D AmbientColor { get; set; }
        = LinFloat64Vector3D.Create(0.2, 0.2, 0.2);

    public LinFloat64Vector3D DiffuseColor { get; set; }
        = LinFloat64Vector3D.Create(0.7, 0.2, 0.2);

    public LinFloat64Vector3D SpecularColor { get; set; }
        = LinFloat64Vector3D.Create(1.0, 1.0, 1.0);

    public double SpecularShininess { get; set; }
        = 10.0d;


}