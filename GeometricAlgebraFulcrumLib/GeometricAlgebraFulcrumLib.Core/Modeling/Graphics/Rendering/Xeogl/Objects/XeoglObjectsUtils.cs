using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Materials;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Transforms;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Objects;

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