using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Points;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using GraphicsComposerLib.Xeogl.Geometry.Primitives;

namespace GraphicsComposerLib.Xeogl.Geometry
{
    public static class XeoglGeometryUtils
    {
        public static XeoglPointsGeometry ToXeoglGeometry(this IGraphicsPointsGeometry3D graphicsGeometry)
            => new XeoglPointsGeometry(graphicsGeometry);
        
        public static XeoglLinesGeometry ToXeoglGeometry(this IGraphicsLinesGeometry3D graphicsGeometry)
            => new XeoglLinesGeometry(graphicsGeometry);
        
        public static XeoglTrianglesGeometry ToXeoglGeometry(this IGraphicsTrianglesGeometry3D graphicsGeometry)
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
}
