using System.Drawing;
using System.Linq;
using Aspose.ThreeD.Entities;
using Aspose.ThreeD.Utilities;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.LatticeShapes.Surfaces;

namespace GraphicsComposerLib.IO
{
    public static class Aspose3DUtils
    {
        public static Line GenerateAspose3DLinesMesh(this GrLatticeSurfaceList3D dataSet)
        {
            var linesMesh = new Line();

            linesMesh.Color = new Vector3(Color.CadetBlue);

            foreach (var vertex in dataSet.VertexList)
            {
                var v1 = new Vector4(vertex.X, vertex.Y, vertex.Z, 1d);
                var p2 = vertex.Point + 0.2 * new Tuple3D(vertex.Normal);
                var v2 = new Vector4(p2.X, p2.Y, p2.Z, 1d);

                linesMesh.ControlPoints.Add(v1);
                linesMesh.ControlPoints.Add(v2);
            }

            //linesMesh.ControlPoints.AddRange(
            //    dataSet.VertexList.Select(
            //        p => new Vector4(p.Point.X, p.Point.Y, p.Point.Z, 1d)
            //    )
            //);

            return linesMesh;
        }

        public static Mesh GenerateAspose3DTrianglesMesh(this GrLatticeSurfaceList3D dataSet)
        {
            var trianglesMesh = new Mesh();
            
            trianglesMesh.ControlPoints.AddRange(
                dataSet.VertexList.Select(
                    p => new Vector4(p.Point.X, p.Point.Y, p.Point.Z, 1d)
                )
            );

            foreach (var (i1, i2, i3) in dataSet.TriangleVertexIndices) 
                trianglesMesh.CreatePolygon(i1, i2, i3);

            //if (dataSet.GenerateNormals)
            {
                var elementNormal = trianglesMesh.CreateElement(
                    VertexElementType.Normal,
                    MappingMode.ControlPoint,
                    ReferenceMode.Direct
                ) as VertexElementNormal;

                elementNormal?.Data.AddRange(
                    dataSet.VertexNormals.Select(
                        n => new Vector4(-n.X, -n.Y, -n.Z, 0)
                    )
                );
            }

            //if (dataSet.GenerateTextureUVs)
            {
                var elementTextureUv = trianglesMesh.CreateElementUV(
                    TextureMapping.Diffuse,
                    MappingMode.ControlPoint,
                    ReferenceMode.Direct
                );

                elementTextureUv.Data.AddRange(
                    dataSet.VertexTextureUVs.Select(
                        n => new Vector4(n.Item1, n.Item2, 0, 1)
                    )
                );
            }

            ////if (dataSet.GenerateColors)
            //{
            //    var elementColor = mesh.CreateElement(
            //        VertexElementType.VertexColor,
            //        MappingMode.ControlPoint,
            //        ReferenceMode.Direct
            //    ) as VertexElementVertexColor;

            //    elementColor?.Data.AddRange(
            //        dataSet.VertexColors.Select(
            //            n => new Vector4(n)
            //        )
            //    );
            //}

            return trianglesMesh;
        }
    }
}
