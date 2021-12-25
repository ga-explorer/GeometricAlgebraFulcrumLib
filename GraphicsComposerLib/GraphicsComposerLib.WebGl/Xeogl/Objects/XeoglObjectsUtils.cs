using NumericalGeometryLib.BasicMath.Matrices;
using GraphicsComposerLib.WebGl.Xeogl.Geometry;
using GraphicsComposerLib.WebGl.Xeogl.Materials;
using GraphicsComposerLib.WebGl.Xeogl.Transforms;

namespace GraphicsComposerLib.WebGl.Xeogl.Objects
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

        public static XeoglMesh ToXeoglMesh(this XeoglGeometry geometry, SquareMatrix4 transformMatrix, XeoglMaterial material)
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
