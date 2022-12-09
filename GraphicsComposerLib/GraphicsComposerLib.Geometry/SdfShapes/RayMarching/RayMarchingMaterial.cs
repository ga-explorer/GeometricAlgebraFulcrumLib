using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.SdfShapes.RayMarching
{
    public sealed class RayMarchingMaterial
    {
        public Float64Tuple3D AmbientColor { get; set; }
            = new Float64Tuple3D(0.2, 0.2, 0.2);

        public Float64Tuple3D DiffuseColor { get; set; }
            = new Float64Tuple3D(0.7, 0.2, 0.2);

        public Float64Tuple3D SpecularColor { get; set; }
            = new Float64Tuple3D(1.0, 1.0, 1.0);

        public double SpecularShininess { get; set; }
            = 10.0d;


    }
}
