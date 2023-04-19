using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Triangles
{
    public class GrTriangleSoupGeometryComposer3D :
        IGraphicsTriangleGeometry3D
    {
        private readonly List<IGraphicsVertex3D> _verticesList 
            = new List<IGraphicsVertex3D>();

        
        public GrVertexNormalComputationMethod NormalComputationMethod
            => VertexNormalsEnabled
                ? GrVertexNormalComputationMethod.AverageNormals
                : GrVertexNormalComputationMethod.None;

        public double DistanceEpsilon { get; set; }
            = 1e-7d;

        public bool AllowSmallTriangles { get; set; }
        
        public bool ReverseNormals { get; set; }

        public int Count 
            => _verticesList.Count / 3;

        public ITriangle3D this[int index] 
            => Triangle3D.Create(
                _verticesList[3 * index],
                _verticesList[3 * index + 1],
                _verticesList[3 * index + 2]
            );

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Triangles;

        public int VertexCount 
            => _verticesList.Count;

        public IEnumerable<int> GeometryIndices 
            => Enumerable.Range(0, _verticesList.Count);

        public IEnumerable<IGraphicsVertex3D> GeometryVertices
            => _verticesList;

        public IEnumerable<IFloat64Tuple3D> GeometryPoints
            => _verticesList;

        public IEnumerable<IFloat64Tuple2D> VertexTextureUVs
            => _verticesList
                .Select(p => p.ParameterValue.ToTuple2D());

        public IEnumerable<Color> VertexColors
            => _verticesList
                .Select(p => p.Color);

        public bool VertexNormalsEnabled { get; set; }
        
        public bool VertexTextureUVsEnabled { get; set; }
        
        public bool VertexColorsEnabled { get; set; }

        public IEnumerable<IFloat64Tuple3D> VertexNormals
            => _verticesList
                .Select(p => p.Normal);

        public IEnumerable<Triplet<int>> TriangleVertexIndices
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<int>(
                        i,
                        i + 1,
                        i + 2
                    );
            }
        }

        public IEnumerable<Triplet<IGraphicsSurfaceLocalFrame3D>> TriangleVertices
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<IGraphicsSurfaceLocalFrame3D>(
                        _verticesList[i],
                        _verticesList[i + 1],
                        _verticesList[i + 2]
                    );
            }
        }

        public IEnumerable<Triplet<IFloat64Tuple3D>> TriangleVertexPoints
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<IFloat64Tuple3D>(
                        _verticesList[i].Point,
                        _verticesList[i + 1].Point,
                        _verticesList[i + 2].Point
                    );
            }
        }

        public IEnumerable<Triplet<IFloat64Tuple2D>> TriangleVertexTextureUVs
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<IFloat64Tuple2D>(
                        _verticesList[i].ParameterValue.ToTuple2D(),
                        _verticesList[i + 1].ParameterValue.ToTuple2D(),
                        _verticesList[i + 2].ParameterValue.ToTuple2D()
                    );
            }
        }

        public IEnumerable<Triplet<Color>> TriangleVertexColors
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<Color>(
                        _verticesList[i].Color,
                        _verticesList[i + 1].Color,
                        _verticesList[i + 2].Color
                    );
            }
        }

        public IEnumerable<Triplet<Normal3D>> TriangleVertexNormals
        {
            get
            {
                for (var i = 0; i < _verticesList.Count; i += 3)
                    yield return new Triplet<Normal3D>(
                        _verticesList[i].Normal,
                        _verticesList[i + 1].Normal,
                        _verticesList[i + 2].Normal
                    );
            }
        }

        public int VerticesCount
            => _verticesList.Count;

        
        public GrTriangleSoupGeometryComposer3D()
        {
        }


        public GrTriangleSoupGeometryComposer3D Clear()
        {
            _verticesList.Clear();

            return this;
        }


        private IGraphicsVertex3D CreateVertex(IFloat64Tuple3D point)
        {
            var vertexIndex = _verticesList.Count;

            if (VertexNormalsEnabled)
            {
                if (VertexColorsEnabled)
                    return new GrNormalColorVertex3D(vertexIndex, point);

                else if (VertexTextureUVsEnabled)
                    return new GrNormalTextureVertex3D(vertexIndex, point);

                else
                    return new GrNormalVertex3D(vertexIndex, point);
            }
            else
            {
                if (VertexColorsEnabled)
                    return new GrColorVertex3D(vertexIndex, point);

                else if (VertexTextureUVsEnabled)
                    return new GrTextureVertex3D(vertexIndex, point);

                else
                    return new GrVertex3D(vertexIndex, point);
            }
        }

        private IGraphicsVertex3D CreateVertex(IGraphicsVertex3D vertex)
        {
            var vertexIndex = _verticesList.Count;

            if (VertexNormalsEnabled)
            {
                if (VertexColorsEnabled)
                    return new GrNormalColorVertex3D(vertexIndex, vertex);

                else if (VertexTextureUVsEnabled)
                    return new GrNormalTextureVertex3D(vertexIndex, vertex);

                else
                    return new GrNormalVertex3D(vertexIndex, vertex);
            }
            else
            {
                if (VertexColorsEnabled)
                    return new GrColorVertex3D(vertexIndex, vertex);

                else if (VertexTextureUVsEnabled)
                    return new GrTextureVertex3D(vertexIndex, vertex);

                else
                    return new GrVertex3D(vertexIndex, vertex);
            }
        }

        private IGraphicsVertex3D AddVertex(IFloat64Tuple3D point)
        {
            var storedVertex = CreateVertex(point);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertex(double x, double y, double z)
        {
            var storedVertex = CreateVertex(new Float64Tuple3D(x, y, z));

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertex(IGraphicsVertex3D vertex)
        {
            var storedVertex = CreateVertex(vertex);

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private bool StoreTriangle(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2, IGraphicsSurfaceLocalFrame3D vertex3)
        {
            //Make sure all side lengths are far enough from zero
            if (!AllowSmallTriangles)
            {
                if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
                if (vertex3.GetDistanceToPoint(vertex2) < DistanceEpsilon) return false;
                if (vertex1.GetDistanceToPoint(vertex3) < DistanceEpsilon) return false;
            }

            //Generate vertex normals
            if (VertexNormalsEnabled)
            {
                var normal = 
                    ReverseNormals
                        ? EuclideanFloat64TupleUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                        : EuclideanFloat64TupleUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);

                Debug.Assert(normal.IsValid());

                vertex1.Normal.Set(normal);
                vertex2.Normal.Set(normal);
                vertex3.Normal.Set(normal);
            }

            return true;
        }
        
        
        public IFloat64Tuple3D GetGeometryPoint(int index)
        {
            return _verticesList[index];
        }
        
        public Normal3D GetVertexNormal(int index)
        {
            return _verticesList[index].Normal;
        }

        public void ComputeVertexNormals(bool inverseNormals)
        {
            throw new NotImplementedException();
        }

        public bool AddTriangle(ITriangle3D triangle)
        {
            return StoreTriangle(
                AddVertex(triangle.GetPoint1()), 
                AddVertex(triangle.GetPoint2()), 
                AddVertex(triangle.GetPoint3())
            );
        }

        public bool AddTriangle(IFloat64Tuple3D point1, IFloat64Tuple3D point2, IFloat64Tuple3D point3)
        {
            return StoreTriangle(
                AddVertex(point1),
                AddVertex(point2),
                AddVertex(point3)
            );
        }

        public bool AddTriangle(ITriplet<IFloat64Tuple3D> points)
        {
            return StoreTriangle(
                AddVertex(points.Item1),
                AddVertex(points.Item2),
                AddVertex(points.Item3)
            );
        }

        public bool AddTriangle(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2, IGraphicsSurfaceLocalFrame3D vertex3)
        {
            return StoreTriangle(
                AddVertex(vertex1), 
                AddVertex(vertex2), 
                AddVertex(vertex3)
            );
        }

        public bool AddTriangle(ITriplet<IGraphicsSurfaceLocalFrame3D> vertices)
        {
            return StoreTriangle(
                AddVertex(vertices.Item1), 
                AddVertex(vertices.Item2), 
                AddVertex(vertices.Item3)
            );
        }


        public GrTriangleSoupGeometryComposer3D AddTriangles(IEnumerable<ITriangle3D> trianglesList)
        {
            foreach (var triangle in trianglesList)
            {
                var vertex1 = AddVertex(triangle.GetPoint1());
                var vertex2 = AddVertex(triangle.GetPoint2());
                var vertex3 = AddVertex(triangle.GetPoint3());

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GrTriangleSoupGeometryComposer3D AddTriangles(IEnumerable<ITriplet<IFloat64Tuple3D>> trianglePointsList)
        {
            foreach (var points in trianglePointsList)
            {
                var vertex1 = AddVertex(points.Item1);
                var vertex2 = AddVertex(points.Item2);
                var vertex3 = AddVertex(points.Item3);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GrTriangleSoupGeometryComposer3D AddTriangles(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            if (pointsList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < pointsList.Count; i += 3)
            {
                var vertex1 = AddVertex(pointsList[i]);
                var vertex2 = AddVertex(pointsList[i + 1]);
                var vertex3 = AddVertex(pointsList[i + 2]);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GrTriangleSoupGeometryComposer3D AddTriangles(IEnumerable<ITriplet<IGraphicsSurfaceLocalFrame3D>> triangleVerticesList)
        {
            foreach (var vertices in triangleVerticesList)
            {
                var vertex1 = AddVertex(vertices.Item1);
                var vertex2 = AddVertex(vertices.Item2);
                var vertex3 = AddVertex(vertices.Item3);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }

        public GrTriangleSoupGeometryComposer3D AddTriangles(IReadOnlyList<IGraphicsSurfaceLocalFrame3D> verticesList)
        {
            if (verticesList.Count % 3 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < verticesList.Count; i += 3)
            {
                var vertex1 = AddVertex(verticesList[i]);
                var vertex2 = AddVertex(verticesList[i + 1]);
                var vertex3 = AddVertex(verticesList[i + 2]);

                StoreTriangle(vertex1, vertex2, vertex3);
            }

            return this;
        }


        //public GrTriangleSoupGeometry3D GenerateGeometry()
        //{
        //    var geometry = 
        //        GrTriangleSoupGeometry3D.Create(TriangleVertexPoints);

        //    if (GenerateTextureUVs)
        //        geometry.SetVertexUVs(TriangleVertexTextureUVs.ToArray());

        //    if (GenerateColors)
        //        geometry.SetVertexColors(TriangleVertexColors.ToArray());

        //    if (GenerateNormals)
        //        geometry.SetVertexNormals(TriangleVertexNormals.ToArray());

        //    return geometry;
        //}

        public IEnumerator<ITriangle3D> GetEnumerator()
        {
            for (var i = 0; i < _verticesList.Count; i += 3)
                yield return Triangle3D.Create(
                    _verticesList[i],
                    _verticesList[i + 1],
                    _verticesList[i + 2]
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}