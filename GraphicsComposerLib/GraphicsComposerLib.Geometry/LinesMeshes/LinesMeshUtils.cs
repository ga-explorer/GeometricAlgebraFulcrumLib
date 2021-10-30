using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.GraphicsGeometry.Composers;

namespace GraphicsComposerLib.Geometry.LinesMeshes
{
    public static class LinesMeshUtils
    {
        public static GraphicsLinesGeometryComposer3D ToLineMesh(this IEnumerable<ITuple3D> pointsList, bool closedFlag = false)
        {
            var lineMesh = new GraphicsLinesGeometryComposer3D();

            lineMesh.AddPolyline(pointsList, closedFlag);

            return lineMesh;
        }


    }
}
