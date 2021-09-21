using EuclideanGeometryLib.BasicMath.Matrices;
using GraphicsComposerLib.Xeogl.Geometry;
using GraphicsComposerLib.Xeogl.Materials;
using GraphicsComposerLib.Xeogl.Transforms;

namespace GraphicsComposerLib.Xeogl.Objects
{
    public static class XeoglObjectsUtils
    {
        public static XeoglMesh ToXeoglMesh(this XeoglGeometry geometry)
            => new XeoglMesh() {Geometry = geometry};

        public static XeoglMesh ToXeoglMesh(this XeoglGeometry geometry, XeoglMaterial material)
            => new XeoglMesh()
            {
                Geometry = geometry,
                Material = material
            };

        public static XeoglMesh ToXeoglMesh(this XeoglGeometry geometry, Matrix4X4 transformMatrix, XeoglMaterial material)
            => new XeoglMesh()
            {
                Geometry = geometry,
                Transform = new XeoglMatrixTransform(transformMatrix),
                Material = material
            };

        public static XeoglMesh ToXeoglMesh(this XeoglGeometry geometry, IXeoglTransform transform, XeoglMaterial material)
            => new XeoglMesh()
            {
                Geometry = geometry,
                Transform = transform,
                Material = material
            };
    }
}
