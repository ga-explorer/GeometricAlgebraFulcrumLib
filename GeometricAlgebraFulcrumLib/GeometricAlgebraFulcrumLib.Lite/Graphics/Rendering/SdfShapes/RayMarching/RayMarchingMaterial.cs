using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.SdfShapes.RayMarching
{
    public sealed class RayMarchingMaterial
    {
        public Float64Vector3D AmbientColor { get; set; }
            = Float64Vector3D.Create(0.2, 0.2, 0.2);

        public Float64Vector3D DiffuseColor { get; set; }
            = Float64Vector3D.Create(0.7, 0.2, 0.2);

        public Float64Vector3D SpecularColor { get; set; }
            = Float64Vector3D.Create(1.0, 1.0, 1.0);

        public double SpecularShininess { get; set; }
            = 10.0d;


    }
}
