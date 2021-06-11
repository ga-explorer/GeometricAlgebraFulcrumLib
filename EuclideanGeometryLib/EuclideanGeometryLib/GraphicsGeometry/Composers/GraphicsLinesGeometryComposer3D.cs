using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using DataStructuresLib.Dictionary;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Composers
{
    /// <summary>
    /// This class can be used to incrementally build a graphics line geometry
    /// in 3D
    /// </summary>
    public class GraphicsLinesGeometryComposer3D
    {
        private readonly Dictionary3Keys<double, GraphicsVertex3D> _verticesTable
            = new Dictionary3Keys<double, GraphicsVertex3D>();

        private readonly List<GraphicsVertex3D> _verticesList
            = new List<GraphicsVertex3D>();

        private readonly List<int> _indicesList =
            new List<int>();


        public double DistanceEpsilon { get; set; } 
            = 1e-7d;

        public bool AllowDuplicatePoints { get; private set; }

        public bool AllowSmallLines { get; set; }

        public bool GenerateColors { get; set; }

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Lines;

        public IEnumerable<IGraphicsVertex3D> Vertices 
            => _verticesTable.Select(p => p.Value);

        public IEnumerable<ITuple3D> VertexPoints 
            => _verticesTable.Select(p => p.Value);

        public IEnumerable<int> LineVertexIndices
            => _indicesList;

        public IEnumerable<IGraphicsVertex3D> LineVertices
            => _indicesList.Select(i => _verticesList[i]);

        public IEnumerable<ITuple3D> LineVertexPoints
            => _indicesList.Select(i => _verticesList[i].Point);

        public int VerticesCount 
            => _verticesTable.Count;


        public GraphicsLinesGeometryComposer3D()
        {
        }


        private GraphicsVertex3D CreateVertexFromPoint(ITuple3D point)
        {
            return new GraphicsVertex3D(_verticesList.Count, point);
        }

        private GraphicsVertex3D CreateVertexFromVertex(IGraphicsVertex3D vertex)
        {
            return new GraphicsVertex3D(_verticesList.Count, vertex);
        }

        private bool StoreLine(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2)
        {
            //Make sure all side lengths are far enough from zero
            if (!AllowSmallLines)
            {
                if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
            }

            //Add triangle vertex indices to indices list
            _indicesList.Add(vertex1.Index);
            _indicesList.Add(vertex2.Index);

            return true;
        }


        public GraphicsLinesGeometryComposer3D Clear()
        {
            _verticesTable.Clear();
            _verticesList.Clear();
            _indicesList.Clear();

            return this;
        }


        public GraphicsLinesGeometryComposer3D BeginBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = false;

            return this;
        }

        public GraphicsLinesGeometryComposer3D EndBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = true;

            return this;
        }


        public IGraphicsVertex3D AddVertexFromPoint(double x, double y, double z)
        {
            GraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        public IGraphicsVertex3D AddVertexFromPoint(ITuple3D point)
        {
            GraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromPoint(point);

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            var x = point.X;
            var y = point.Y;
            var z = point.Z;

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromPoint(point);

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        public IGraphicsVertex3D AddVertexFromVertex(IGraphicsVertex3D vertex)
        {
            GraphicsVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = CreateVertexFromVertex(vertex);

                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            var x = vertex.X;
            var y = vertex.Y;
            var z = vertex.Z;

            if (_verticesTable.TryGetValue(x, y, z, out storedVertex))
                return storedVertex;

            storedVertex = CreateVertexFromVertex(vertex);

            _verticesTable.Add(x, y, z, storedVertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }


        public GraphicsLinesGeometryComposer3D AddVerticesFromPoints(params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddVerticesFromPoints(IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddVerticesFromVertices(IEnumerable<IGraphicsVertex3D> verticesList)
        {
            foreach (var point in verticesList)
                AddVertexFromVertex(point);

            return this;
        }


        public bool AddLineFromIndices(int vertexIndex1, int vertexIndex2)
        {
            if (vertexIndex1 == vertexIndex2)
                return false;

            return StoreLine(
                _verticesList[vertexIndex1], 
                _verticesList[vertexIndex2]
            );
        }

        public bool AddLineFromIndices(IPair<int> vertexIndices)
        {
            if (vertexIndices.Item1 == vertexIndices.Item2)
                return false;

            return StoreLine(
                _verticesList[vertexIndices.Item1], 
                _verticesList[vertexIndices.Item2]
            );
        }

        public bool AddLineFromPoints(ITuple3D point1, ITuple3D point2)
        {
            return StoreLine(
                AddVertexFromPoint(point1.X, point1.Y, point1.Z), 
                AddVertexFromPoint(point2.X, point2.Y, point2.Z)
            );
        }

        public bool AddLineFromPoints(IPair<ITuple3D> points)
        {
            return StoreLine(
                AddVertexFromPoint(points.Item1), 
                AddVertexFromPoint(points.Item2)
            );
        }

        public bool AddLineFromVertices(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2)
        {
            return StoreLine(
                AddVertexFromVertex(vertex1), 
                AddVertexFromVertex(vertex2)
            );
        }

        public bool AddLineFromVertices(IPair<IGraphicsVertex3D> vertices)
        {
            return StoreLine(
                AddVertexFromVertex(vertices.Item1), 
                AddVertexFromVertex(vertices.Item2)
            );
        }


        public GraphicsLinesGeometryComposer3D AddLines(IEnumerable<ILineSegment3D> linesList)
        {
            foreach (var lineSegment in linesList)
                AddLineFromPoints(
                    lineSegment.GetPoint1(), 
                    lineSegment.GetPoint2()
                );

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLineFromIndices(IEnumerable<IPair<int>> lineVertexIndicesList)
        {
            foreach (var pair in lineVertexIndicesList)
                AddLineFromIndices(pair.Item1, pair.Item2);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLineFromIndices(IEnumerable<int> linePointsIndicesList)
        {
            var firstPointFlag = true;
            var index1 = -1;

            foreach (var index2 in linePointsIndicesList)
            {
                if (firstPointFlag)
                {
                    index1 = index2;
                    firstPointFlag = false;
                }
                else
                {
                    firstPointFlag = true;

                    AddLineFromIndices(index1, index2);
                }
            }

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLinesFromPoints(IEnumerable<IPair<ITuple3D>> linesList)
        {
            foreach (var pair in linesList)
                AddLineFromPoints(pair.Item1, pair.Item2);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLinesFromPoints(IEnumerable<ITuple3D> linePointsList)
        {
            var firstPointFlag = true;
            ITuple3D point1 = null;

            foreach (var point in linePointsList)
            {
                if (firstPointFlag)
                {
                    point1 = point;
                    firstPointFlag = false;
                }
                else
                {
                    firstPointFlag = true;

                    AddLineFromPoints(point1, point);
                }
            }

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLinesFromVertices(IEnumerable<IPair<IGraphicsVertex3D>> lineVerticesList)
        {
            foreach (var pair in lineVerticesList)
                AddLineFromVertices(pair.Item1, pair.Item2);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddLinesFromVertices(IEnumerable<IGraphicsVertex3D> linePointsList)
        {
            var firstVertexFlag = true;
            IGraphicsVertex3D vertex1 = null;

            foreach (var vertex in linePointsList)
            {
                if (firstVertexFlag)
                {
                    vertex1 = vertex;
                    firstVertexFlag = false;
                }
                else
                {
                    firstVertexFlag = true;

                    AddLineFromVertices(vertex1, vertex);
                }
            }

            return this;
        }


        public GraphicsLinesGeometryComposer3D AddPolyline(IEnumerable<int> indicesList, bool closedPolyline)
        {
            var firstIndex = -1;
            var index1 = -1;

            foreach (var index2 in indicesList)
            {
                if (index1 < 0)
                {
                    firstIndex = index2;
                    index1 = index2;
                    continue;
                }

                AddLineFromIndices(index1, index2);

                index1 = index2;
            }

            if (closedPolyline)
                AddLineFromIndices(index1, firstIndex);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddPolyline(IEnumerable<ITuple3D> pointsList, bool closedPolyline)
        {
            ITuple3D firstPoint = null;
            ITuple3D point1 = null;

            foreach (var point2 in pointsList)
            {
                if (ReferenceEquals(point1, null))
                {
                    firstPoint = point2;
                    point1 = point2;
                    continue;
                }

                AddLineFromPoints(point1, point2);

                point1 = point2;
            }

            if (closedPolyline)
                AddLineFromPoints(point1, firstPoint);

            return this;
        }

        public GraphicsLinesGeometryComposer3D AddPolyline(IEnumerable<IGraphicsVertex3D> pointsList, bool closedPolyline)
        {
            IGraphicsVertex3D firstPoint = null;
            IGraphicsVertex3D point1 = null;

            foreach (var point2 in pointsList)
            {
                if (ReferenceEquals(point1, null))
                {
                    firstPoint = point2;
                    point1 = point2;
                    continue;
                }

                AddLineFromVertices(point1, point2);

                point1 = point2;
            }

            if (closedPolyline)
                AddLineFromVertices(point1, firstPoint);

            return this;
        }

        
        public GraphicsLinesGeometry3D GenerateGeometry()
        {
            var geometry = 
                GraphicsLinesGeometry3D.Create(VertexPoints, _indicesList);

            return geometry;
        }
    }
}
