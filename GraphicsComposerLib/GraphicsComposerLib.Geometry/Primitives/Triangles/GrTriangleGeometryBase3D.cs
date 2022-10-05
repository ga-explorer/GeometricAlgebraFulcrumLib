using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Triangles;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Triangles
{
    public abstract class GrTriangleGeometryBase3D 
        : IGraphicsTriangleGeometry3D
    {
        public abstract GraphicsPrimitiveType3D PrimitiveType { get; }

        public int VertexCount 
            => _vertexPoints.Count;

        public IEnumerable<IGraphicsVertex3D> GeometryVertices
        {
            get
            {
                if (VertexNormalsEnabled)
                {
                    if (VertexTextureUVsEnabled)
                    {
                        return GeometryPoints.Select(
                            (p, i) => new GrNormalTextureVertex3D(i, p, _vertexUVs[i], _vertexNormals[i])
                        );
                    }
                    else
                    {
                        return GeometryPoints.Select(
                            (p, i) => new GrNormalVertex3D(i, p, _vertexNormals[i])
                        );
                    }
                }
                else
                {
                    if (VertexTextureUVsEnabled)
                    {
                        return GeometryPoints.Select(
                            (p, i) => new GrTextureVertex3D(i, p, _vertexUVs[i])
                        );
                    }
                    else
                    {
                        return GeometryPoints.Select(
                            (p, i) => new GrVertex3D(i, p)
                        );
                    }
                }
            }
        }
            

        private readonly IReadOnlyList<ITuple3D> _vertexPoints;
        public IEnumerable<ITuple3D> GeometryPoints 
            => _vertexPoints;

        public abstract IEnumerable<int> GeometryIndices { get; }

        public abstract int Count { get; }

        public abstract ITriangle3D this[int index] { get; }

        public abstract IEnumerable<Triplet<ITuple3D>> TriangleVertexPoints { get; }

        public abstract IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

        public GrNormal3D GetVertexNormal(int index)
        {
            return _vertexNormals[index];
        }

        public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
            = GrVertexNormalComputationMethod.AverageNormals;

        private GrNormal3D[] _vertexNormals;
        public IEnumerable<ITuple3D> VertexNormals 
            => _vertexNormals;

        private ITuple2D[] _vertexUVs;
        public IEnumerable<ITuple2D> VertexTextureUVs
            => _vertexUVs;

        private Color[] _vertexColors;
        public IEnumerable<Color> VertexColors 
            => _vertexColors;

        public bool VertexNormalsEnabled
            => !ReferenceEquals(_vertexNormals, null);

        public bool VertexTextureUVsEnabled
            => !ReferenceEquals(_vertexUVs, null);

        public bool VertexColorsEnabled
            => !ReferenceEquals(_vertexColors, null);

        public bool AutoVertexNormals { get; set; }


        protected GrTriangleGeometryBase3D(IReadOnlyList<ITuple3D> pointsList)
        {
            _vertexPoints = pointsList;
        }

        
        public ITuple3D GetGeometryPoint(int index)
        {
            return _vertexPoints[index];
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

        public void SetVertexNormals(GrNormal3D[] vertexNormals)
        {
            if (vertexNormals.Length != _vertexPoints.Count)
                throw new InvalidOperationException();

            _vertexNormals = vertexNormals;
        }

        public void SetVertexUVs(ITuple2D[] vertexUVs)
        {
            if (vertexUVs.Length != _vertexPoints.Count)
                throw new InvalidOperationException();

            _vertexUVs = vertexUVs;
        }

        public void SetVertexColors(Color[] vertexColors)
        {
            if (vertexColors.Length != _vertexPoints.Count)
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
            var vertex1 = _vertexPoints[vertexIndex1];
            var vertex2 = _vertexPoints[vertexIndex2];
            var vertex3 = _vertexPoints[vertexIndex3];

            var normal1 = GetVertexNormal(vertexIndex1);
            var normal2 = GetVertexNormal(vertexIndex2);
            var normal3 = GetVertexNormal(vertexIndex3);

            if (NormalComputationMethod == GrVertexNormalComputationMethod.WeightedNormals)
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
                    normal.IsValid()
                );

                //Update normals of triangle vertices. See here for more information:
                //http://www.bytehazard.com/articles/vertnorm.html
                //normal.MakeUnitVector();
                normal1.Update(angle1 * normal);
                normal2.Update(angle2 * normal);
                normal3.Update(angle3 * normal);

                return;
            }
            
            if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageUnitNormals)
            {
                //Find triangle unit normal
                var normal = 
                    VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);
                
                //For debugging only
                Debug.Assert(normal.IsValid());

                normal1.Update(normal);
                normal2.Update(normal);
                normal3.Update(normal);

                return;
            }
            
            if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageNormals)
            {
                //Find triangle normal, not unit but full normal vector
                var normal = 
                    VectorAlgebraUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

                //For debugging only
                Debug.Assert(normal.IsValid());

                normal1.Update(normal);
                normal2.Update(normal);
                normal3.Update(normal);

                return;
            }
        }

        public void ComputeVertexNormals(bool reverseNormals)
        {
            _vertexNormals = new GrNormal3D[_vertexPoints.Count];

            foreach (var (index1, index2, index3) in TriangleVertexIndices)
                UpdateVertexNormals(index1, index2, index3);

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