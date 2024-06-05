using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Points;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry.Primitives;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Geometry;

public static class XeoglGeometryUtils
{
    public static XeoglPointsGeometry ToXeoglGeometry(this IGraphicsPointGeometry3D graphicsGeometry)
        => new XeoglPointsGeometry(graphicsGeometry);
        
    public static XeoglLinesGeometry ToXeoglGeometry(this IGraphicsLineGeometry3D graphicsGeometry)
        => new XeoglLinesGeometry(graphicsGeometry);
        
    public static XeoglTrianglesGeometry ToXeoglGeometry(this IGraphicsTriangleGeometry3D graphicsGeometry)
        => new XeoglTrianglesGeometry(graphicsGeometry);

        
    public static XeoglLinesGeometry GetNormalLinesGeometry(this XeoglTrianglesGeometry geometry, double t2)
    {
        return new XeoglLinesGeometry(
            geometry
                .GraphicsTrianglesGeometry
                .GetNormalLines(t2)
                .ToGraphicsLinesListGeometry()
        );
    }

    public static XeoglLinesGeometry GetNormalLinesGeometry(this XeoglTrianglesGeometry geometry, double t1, double t2)
    {
        return new XeoglLinesGeometry(
            geometry
                .GraphicsTrianglesGeometry
                .GetNormalLines(t1, t2)
                .ToGraphicsLinesListGeometry()
        );
    }
}