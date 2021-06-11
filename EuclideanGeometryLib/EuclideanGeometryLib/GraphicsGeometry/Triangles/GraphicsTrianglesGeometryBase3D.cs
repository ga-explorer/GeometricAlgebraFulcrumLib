using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Triangles
{
    public abstract class GraphicsTrianglesGeometryBase3D 
        : IGraphicsTrianglesGeometry3D
    {
        public abstract GraphicsPrimitiveType3D PrimitiveType { get; }

        public IEnumerable<IGraphicsVertex3D> Vertices
        {
            get
            {
                if (ContainsVertexNormals)
                {
                    if (ContainsVertexUVs)
                    {
                        return VertexPoints.Select(
                            (p, i) => new GraphicsNormalTexturedVertex3D(i, p, _vertexUVs[i], _vertexNormals[i])
                        );
                    }
                    else
                    {
                        return VertexPoints.Select(
                            (p, i) => new GraphicsNormalVertex3D(i, p, _vertexNormals[i])
                        );
                    }
                }
                else
                {
                    if (ContainsVertexUVs)
                    {
                        return VertexPoints.Select(
                            (p, i) => new GraphicsTexturedVertex3D(i, p, _vertexUVs[i])
                        );
                    }
                    else
                    {
                        return VertexPoints.Select(
                            (p, i) => new GraphicsVertex3D(i, p)
                        );
                    }
                }
            }
        }
            

        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public abstract IReadOnlyList<int> VertexIndices { get; }

        public abstract int Count { get; }

        public abstract ITriangle3D this[int index] { get; }

        public abstract IReadOnlyList<Triplet<ITuple3D>> TriangleVerticesPoints { get; }

        public abstract IReadOnlyList<Triplet<int>> TriangleVerticesIndices { get; }

        public GraphicsVertexNormalComputationMethod NormalComputationMethod { get; set; }
            = GraphicsVertexNormalComputationMethod.AverageNormals;

        private IGraphicsNormal3D[] _vertexNormals;
        public IReadOnlyList<IGraphicsNormal3D> VertexNormals 
            => _vertexNormals;

        private ITuple2D[] _vertexUVs;
        public IReadOnlyList<ITuple2D> VertexUVs
            => _vertexUVs;

        private Color[] _vertexColors;
        public IReadOnlyList<Color> VertexColors 
            => _vertexColors;

        public bool ContainsVertexNormals
            => !ReferenceEquals(VertexNormals, null);

        public bool ContainsVertexUVs
            => !ReferenceEquals(_vertexUVs, null);

        public bool ContainsVertexColors
            => !ReferenceEquals(_vertexColors, null);

        public bool AutoVertexNormals { get; set; }


        protected GraphicsTrianglesGeometryBase3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }

        
        public void ClearVertexNormals()
        {
            _vertexNormals = null;
        }

        public void ClearVertexUVs()
        {
            _vertexUVs = null;
        }

        public void ClearVertexColors()
        {
            _vertexColors = null;
        }

        public void ClearVertexData()
        {
            _vertexNormals = null;
            _vertexUVs = null;
            _vertexColors = null;
        }

        public void SetVertexNormals(IGraphicsNormal3D[] vertexNormals)
        {
            if (vertexNormals.Length != VertexPoints.Count)
                throw new InvalidOperationException();

            _vertexNormals = vertexNormals;
        }

        public void SetVertexUVs(ITuple2D[] vertexUVs)
        {
            if (vertexUVs.Length != VertexPoints.Count)
                throw new InvalidOperationException();

            _vertexUVs = vertexUVs;
        }

        public void SetVertexColors(Color[] vertexColors)
        {
            if (vertexColors.Length != VertexPoints.Count)
                throw new InvalidOperationException();

            _vertexColors = vertexColors;
        }

        public void ReverseNormals()
        {
            if (ReferenceEquals(_vertexNormals, null))
                return;

            foreach (var normal in _vertexNormals)
                normal.MakeNegative();
        }

        
        private void UpdateVertexNormals(int vertexIndex1, int vertexIndex2, int vertexIndex3)
        {
            var vertex1 = VertexPoints[vertexIndex1];
            var vertex2 = VertexPoints[vertexIndex2];
            var vertex3 = VertexPoints[vertexIndex3];

            var normal1 = VertexNormals[vertexIndex1];
            var normal2 = VertexNormals[vertexIndex2];
            var normal3 = VertexNormals[vertexIndex3];

            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.WeightedNormals)
            {
                //Find inner angles of triangle
                var angle1 = vertex1.GetPointsAngle(vertex3, vertex2);
                var angle2 = vertex2.GetPointsAngle(vertex1, vertex3);
                var angle3 = vertex3.GetPointsAngle(vertex2, vertex1);

                //Find triangle normal, not unit but full normal vector
                var normal = 
                    VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(
                    !double.IsNaN(angle1) &&
                    !double.IsNaN(angle2) &&
                    !double.IsNaN(angle3) &&
                    !normal.IsInvalid
                );

                //Update normals of triangle vertices. See here for more information:
                //http://www.bytehazard.com/articles/vertnorm.html
                //normal.MakeUnitVector();
                normal1.Update(angle1 * normal);
                normal2.Update(angle2 * normal);
                normal3.Update(angle3 * normal);

                return;
            }
            
            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.AverageUnitNormals)
            {
                //Find triangle unit normal
                var normal = 
                    VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);
                
                //For debugging only
                Debug.Assert(!normal.IsInvalid);

                normal1.Update(normal);
                normal2.Update(normal);
                normal3.Update(normal);

                return;
            }
            
            if (NormalComputationMethod == GraphicsVertexNormalComputationMethod.AverageNormals)
            {
                //Find triangle normal, not unit but full normal vector
                var normal = 
                    VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(!normal.IsInvalid);

                normal1.Update(normal);
                normal2.Update(normal);
                normal3.Update(normal);

                return;
            }
        }

        public void ComputeVertexNormals(bool reverseNormals)
        {
            _vertexNormals = new GraphicsNormal3D[VertexPoints.Count];

            foreach (var triangleIndices in TriangleVerticesIndices)
                UpdateVertexNormals(
                    triangleIndices.Item1, 
                    triangleIndices.Item2, 
                    triangleIndices.Item3
                );

            if (reverseNormals)
            {
                foreach (var normal in _vertexNormals)
                    normal.MakeNegativeUnit();
            }
            else
            {
                foreach (var normal in _vertexNormals)
                    normal.MakeUnit();
            }
        }

        public abstract IEnumerator<ITriangle3D> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}