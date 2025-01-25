using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GeometricAlgebraFulcrumLib.MonoGame.Composers
{
    public class XnaGeometryComposer3D
    {
        public IGraphicsTriangleGeometry3D TriangleGeometry { get; }


        public XnaGeometryComposer3D(IGraphicsTriangleGeometry3D triangleGeometry)
        {
            TriangleGeometry = triangleGeometry;
        }


        public VertexPositionColorNormalTexture[] GeneratePositionColorNormalTexture(IGraphicsTriangleGeometry3D triangleGeometry)
        {
            var vertexList = 
                triangleGeometry.GeometryVertices.ToArray();

            var n = vertexList.Length;
            var vl = new VertexPositionColorNormalTexture[n];

            for (var i = 0; i < n; i++)
            {
                var vertex = vertexList[i];

                vl[i] = new VertexPositionColorNormalTexture(
                    vertex.Point.ToXnaVector3(),
                    vertex.Color.ToXnaColor(),
                    vertex.Normal.ToXnaVector3(),
                    vertex.ParameterValue.ToXnaVector2()
                );
            }

            return vl;
        }
    }
}
