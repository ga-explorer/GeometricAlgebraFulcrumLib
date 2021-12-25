using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicShapes.Lines;
using NumericalGeometryLib.BasicShapes.Lines.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Lines
{
    /// <summary>
    /// This class can be used to incrementally build a graphics line geometry
    /// in 3D
    /// </summary>
    public class GrLineGeometryComposer3D : 
        IGraphicsLineGeometry3D
    {
        private readonly Dictionary<Triplet<double>, GrVertex3D> _verticesTable
            = new Dictionary<Triplet<double>, GrVertex3D>();

        private readonly List<GrVertex3D> _verticesList
            = new List<GrVertex3D>();

        private readonly List<int> _indicesList =
            new List<int>();


        public double DistanceEpsilon { get; set; } 
            = 1e-7d;

        public bool AllowDuplicatePoints { get; private set; }

        public bool AllowSmallLines { get; set; }

        public bool GenerateColors { get; set; }

        public GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Lines;


        public int Count 
            => _indicesList.Count >> 1;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                _verticesList[_indicesList[2 * index]].Point,
                _verticesList[_indicesList[2 * index + 1]].Point
            );

        public int VertexCount 
            => _verticesList.Count;

        public IEnumerable<int> GeometryIndices 
            => _indicesList;

        public IEnumerable<IGraphicsVertex3D> GeometryVertices 
            => _verticesTable.Select(p => p.Value);

        public IEnumerable<ITuple3D> GeometryPoints 
            => _verticesTable.Select(p => p.Value);

        public IEnumerable<Pair<int>> LineVertexIndices
        {
            get
            {
                for (var i = 0; i < _indicesList.Count; i += 2)
                    yield return new Pair<int>(
                        _indicesList[i], 
                        _indicesList[i + 1]
                    );
            }
        }

        public IEnumerable<Pair<IGraphicsSurfaceLocalFrame3D>> LineVertices
        {
            get
            {
                for (var i = 0; i < _indicesList.Count; i += 2)
                    yield return new Pair<IGraphicsSurfaceLocalFrame3D>(
                        _verticesList[_indicesList[i]], 
                        _verticesList[_indicesList[i + 1]]
                    );
            }
        }

        public IEnumerable<Pair<ITuple3D>> LineVertexPoints
        {
            get
            {
                for (var i = 0; i < _indicesList.Count; i += 2)
                    yield return new Pair<ITuple3D>(
                        _verticesList[_indicesList[i]].Point, 
                        _verticesList[_indicesList[i + 1]].Point
                    );
            }
        }

        public int VerticesCount 
            => _verticesTable.Count;


        public GrLineGeometryComposer3D()
        {
        }

        
        public ITuple3D GetGeometryPoint(int index)
        {
            return _verticesList[index];
        }
        
        private bool StoreLine(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2)
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


        public GrLineGeometryComposer3D Clear()
        {
            _verticesTable.Clear();
            _verticesList.Clear();
            _indicesList.Clear();

            return this;
        }


        public GrLineGeometryComposer3D BeginBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = false;

            return this;
        }

        public GrLineGeometryComposer3D EndBatch()
        {
            _verticesTable.Clear();

            AllowDuplicatePoints = true;

            return this;
        }


        public IGraphicsSurfaceLocalFrame3D AddVertexFromPoint(double x, double y, double z)
        {
            var pointTriplet = new Triplet<double>(x, y, z);

            GrVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = new GrVertex3D(_verticesList.Count, x, y, z);
                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
                return storedVertex;

            storedVertex = new GrVertex3D(_verticesList.Count, x, y, z);
            _verticesList.Add(storedVertex);

            _verticesTable.Add(pointTriplet, storedVertex);

            return storedVertex;
        }

        public IGraphicsSurfaceLocalFrame3D AddVertexFromPoint(ITriplet<double> point)
        {
            var pointTriplet = point.ToTriplet();

            GrVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = new GrVertex3D(_verticesList.Count, point);
                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
                return storedVertex;

            storedVertex = new GrVertex3D(_verticesList.Count, point);
            _verticesList.Add(storedVertex);

            _verticesTable.Add(pointTriplet, storedVertex);

            return storedVertex;
        }

        public IGraphicsSurfaceLocalFrame3D AddVertexFromVertex(IGraphicsSurfaceLocalFrame3D vertex)
        {
            var pointTriplet = vertex.ToTriplet();

            GrVertex3D storedVertex;

            if (AllowDuplicatePoints)
            {
                storedVertex = new GrVertex3D(_verticesList.Count, vertex);
                _verticesList.Add(storedVertex);

                return storedVertex;
            }

            if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
                return storedVertex;

            storedVertex = new GrVertex3D(_verticesList.Count, vertex);
            _verticesList.Add(storedVertex);

            _verticesTable.Add(pointTriplet, storedVertex);

            return storedVertex;
        }


        public GrLineGeometryComposer3D AddVerticesFromPoints(params ITuple3D[] pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GrLineGeometryComposer3D AddVerticesFromPoints(IEnumerable<ITuple3D> pointsList)
        {
            foreach (var point in pointsList)
                AddVertexFromPoint(point);

            return this;
        }

        public GrLineGeometryComposer3D AddVerticesFromVertices(IEnumerable<IGraphicsSurfaceLocalFrame3D> verticesList)
        {
            foreach (var point in verticesList)
                AddVertexFromVertex(point);

            return this;
        }


        public bool AddLine(int vertexIndex1, int vertexIndex2)
        {
            if (vertexIndex1 == vertexIndex2)
                return false;

            return StoreLine(
                _verticesList[vertexIndex1], 
                _verticesList[vertexIndex2]
            );
        }

        public bool AddLine(IPair<int> vertexIndices)
        {
            if (vertexIndices.Item1 == vertexIndices.Item2)
                return false;

            return StoreLine(
                _verticesList[vertexIndices.Item1], 
                _verticesList[vertexIndices.Item2]
            );
        }

        public bool AddLine(ITuple3D point1, ITuple3D point2)
        {
            return StoreLine(
                AddVertexFromPoint(point1.X, point1.Y, point1.Z), 
                AddVertexFromPoint(point2.X, point2.Y, point2.Z)
            );
        }

        public bool AddLine(IPair<ITuple3D> points)
        {
            return StoreLine(
                AddVertexFromPoint(points.Item1), 
                AddVertexFromPoint(points.Item2)
            );
        }

        public bool AddLine(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2)
        {
            return StoreLine(
                AddVertexFromVertex(vertex1), 
                AddVertexFromVertex(vertex2)
            );
        }

        public bool AddLine(IPair<IGraphicsSurfaceLocalFrame3D> vertices)
        {
            return StoreLine(
                AddVertexFromVertex(vertices.Item1), 
                AddVertexFromVertex(vertices.Item2)
            );
        }


        public GrLineGeometryComposer3D AddLines(IEnumerable<ILineSegment3D> linesList)
        {
            foreach (var lineSegment in linesList)
                AddLine(
                    lineSegment.GetPoint1(), 
                    lineSegment.GetPoint2()
                );

            return this;
        }

        public GrLineGeometryComposer3D AddLine(IEnumerable<IPair<int>> lineVertexIndicesList)
        {
            foreach (var pair in lineVertexIndicesList)
                AddLine(pair.Item1, pair.Item2);

            return this;
        }

        public GrLineGeometryComposer3D AddLines(IEnumerable<int> linePointsIndicesList)
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
                    AddLine(index1, index2);
                    firstPointFlag = true;
                }
            }

            return this;
        }

        public GrLineGeometryComposer3D AddLines(IEnumerable<IPair<ITuple3D>> linesList)
        {
            foreach (var pair in linesList)
                AddLine(pair.Item1, pair.Item2);

            return this;
        }

        public GrLineGeometryComposer3D AddLines(IEnumerable<ITuple3D> linePointsList)
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
                    AddLine(point1, point);
                    firstPointFlag = true;
                }
            }

            return this;
        }

        public GrLineGeometryComposer3D AddLines(IEnumerable<IPair<IGraphicsSurfaceLocalFrame3D>> lineVerticesList)
        {
            foreach (var pair in lineVerticesList)
                AddLine(pair.Item1, pair.Item2);

            return this;
        }

        public GrLineGeometryComposer3D AddLines(IEnumerable<IGraphicsSurfaceLocalFrame3D> linePointsList)
        {
            var firstVertexFlag = true;
            IGraphicsSurfaceLocalFrame3D vertex1 = null;

            foreach (var vertex in linePointsList)
            {
                if (firstVertexFlag)
                {
                    vertex1 = vertex;
                    firstVertexFlag = false;
                }
                else
                {
                    AddLine(vertex1, vertex);
                    firstVertexFlag = true;
                }
            }

            return this;
        }


        public GrLineGeometryComposer3D AddPolyLine(IEnumerable<int> indicesList, bool closed)
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

                AddLine(index1, index2);

                index1 = index2;
            }

            if (closed)
                AddLine(index1, firstIndex);

            return this;
        }

        public GrLineGeometryComposer3D AddPolyLine(IEnumerable<ITuple3D> pointsList, bool closed)
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

                AddLine(point1, point2);

                point1 = point2;
            }

            if (closed)
                AddLine(point1, firstPoint);

            return this;
        }

        public GrLineGeometryComposer3D AddPolyLine(IEnumerable<IGraphicsSurfaceLocalFrame3D> pointsList, bool closed)
        {
            IGraphicsSurfaceLocalFrame3D firstPoint = null;
            IGraphicsSurfaceLocalFrame3D point1 = null;

            foreach (var point2 in pointsList)
            {
                if (ReferenceEquals(point1, null))
                {
                    firstPoint = point2;
                    point1 = point2;
                    continue;
                }

                AddLine(point1, point2);

                point1 = point2;
            }

            if (closed)
                AddLine(point1, firstPoint);

            return this;
        }

        
        public GrLineGeometry3D GenerateGeometry()
        {
            var geometry = 
                GrLineGeometry3D.Create(GeometryPoints, _indicesList);

            return geometry;
        }

        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < _indicesList.Count; i += 2)
                yield return LineSegment3D.Create(
                    _verticesList[_indicesList[2 * i]].Point,
                    _verticesList[_indicesList[2 * i + 1]].Point
                );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
