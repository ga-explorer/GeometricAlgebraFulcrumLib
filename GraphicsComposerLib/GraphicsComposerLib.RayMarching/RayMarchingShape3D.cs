using EuclideanGeometryLib.SdfGeometry;

namespace GraphicsComposerLib.RayMarching
{
    public sealed class RayMarchingShape3D
    {
        public ISdfGeometry3D Surface { get; }

        public RayMarchingMaterial Material { get; }
            = new RayMarchingMaterial();


        public RayMarchingShape3D(ISdfGeometry3D surface)
        {
            Surface = surface;
        }
    }
}
