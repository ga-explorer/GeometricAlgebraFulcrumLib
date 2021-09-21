using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.RayMarching
{
    public sealed class RayMarchingMaterial
    {
        public Tuple3D AmbientColor { get; set; }
            = new Tuple3D(0.2, 0.2, 0.2);

        public Tuple3D DiffuseColor { get; set; }
            = new Tuple3D(0.7, 0.2, 0.2);

        public Tuple3D SpecularColor { get; set; }
            = new Tuple3D(1.0, 1.0, 1.0);

        public double SpecularShininess { get; set; }
            = 10.0d;


    }
}
