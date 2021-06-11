using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Composers
{
    public class GraphicsLinesListGeometryComposer3D
    {
        private readonly List<IGraphicsVertex3D> _verticesList 
            = new List<IGraphicsVertex3D>();

        
        public double DistanceEpsilon { get; set; }
            = 1e-7d;

        public bool AllowSmallLines { get; set; }

        public IEnumerable<IGraphicsVertex3D> Vertices
            => _verticesList;

        public IEnumerable<ITuple3D> VertexPoints
            => _verticesList
                .Select(p => p.Point);

        public IEnumerable<int> LineVertexIndices
            => Enumerable
                .Range(0, _verticesList.Count);

        public IEnumerable<IGraphicsVertex3D> LineVertices
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i]);

        public IEnumerable<ITuple3D> LineVertexPoints
            => Enumerable
                .Range(0, _verticesList.Count)
                .Select(i => _verticesList[i].Point);

        public int VerticesCount
            => _verticesList.Count;

        
        public GraphicsLinesListGeometryComposer3D()
        {
        }


        public GraphicsLinesListGeometryComposer3D Clear()
        {
            _verticesList.Clear();

            return this;
        }


        private IGraphicsVertex3D CreateVertexFromPoint(ITuple3D point)
        {
            return new GraphicsVertex3D(_verticesList.Count, point);
        }

        private IGraphicsVertex3D CreateVertexFromVertex(IGraphicsVertex3D vertex)
        {
            return new GraphicsVertex3D(_verticesList.Count, vertex);
        }

        private IGraphicsVertex3D AddVertexFromPoint(ITuple3D point)
        {
            var storedVertex = CreateVertexFromPoint(point);

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertexFromPoint(double x, double y, double z)
        {
            var storedVertex = CreateVertexFromPoint(new Tuple3D(x, y, z));

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private IGraphicsVertex3D AddVertexFromVertex(IGraphicsVertex3D vertex)
        {
            var storedVertex = CreateVertexFromVertex(vertex);

            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        private bool StoreLine(IGraphicsVertex3D vertex1, IGraphicsVertex3D vertex2)
        {
            //Make sure all side lengths are far enough from zero
            if (!AllowSmallLines)
            {
                if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
            }

            return true;
        }
        

        public bool AddLine(ILineSegment3D lineSegment)
        {
            return StoreLine(
                AddVertexFromPoint(lineSegment.GetPoint1()), 
                AddVertexFromPoint(lineSegment.GetPoint2())
            );
        }

        public bool AddLineFromPoints(ITuple3D point1, ITuple3D point2)
        {
            return StoreLine(
                AddVertexFromPoint(point1),
                AddVertexFromPoint(point2)
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


        public GraphicsLinesListGeometryComposer3D AddLines(IEnumerable<ILineSegment3D> linesList)
        {
            foreach (var line in linesList)
            {
                var vertex1 = AddVertexFromPoint(line.GetPoint1());
                var vertex2 = AddVertexFromPoint(line.GetPoint2());

                StoreLine(vertex1, vertex2);
            }

            return this;
        }

        public GraphicsLinesListGeometryComposer3D AddLinesFromPoints(IEnumerable<IPair<ITuple3D>> linePointsList)
        {
            foreach (var points in linePointsList)
            {
                var vertex1 = AddVertexFromPoint(points.Item1);
                var vertex2 = AddVertexFromPoint(points.Item2);

                StoreLine(vertex1, vertex2);
            }

            return this;
        }

        public GraphicsLinesListGeometryComposer3D AddLinesFromPoints(IReadOnlyList<ITuple3D> pointsList)
        {
            if (pointsList.Count % 2 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < pointsList.Count; i += 2)
            {
                var vertex1 = AddVertexFromPoint(pointsList[i]);
                var vertex2 = AddVertexFromPoint(pointsList[i + 1]);

                StoreLine(vertex1, vertex2);
            }

            return this;
        }

        public GraphicsLinesListGeometryComposer3D AddLinesFromVertices(IEnumerable<IPair<IGraphicsVertex3D>> lineVerticesList)
        {
            foreach (var vertices in lineVerticesList)
            {
                var vertex1 = AddVertexFromVertex(vertices.Item1);
                var vertex2 = AddVertexFromVertex(vertices.Item2);

                StoreLine(vertex1, vertex2);
            }

            return this;
        }

        public GraphicsLinesListGeometryComposer3D AddLinesFromVertices(IReadOnlyList<IGraphicsVertex3D> verticesList)
        {
            if (verticesList.Count % 2 != 0)
                throw new InvalidOperationException();

            for (var i = 0; i < verticesList.Count; i += 2)
            {
                var vertex1 = AddVertexFromVertex(verticesList[i]);
                var vertex2 = AddVertexFromVertex(verticesList[i + 1]);

                StoreLine(vertex1, vertex2);
            }

            return this;
        }


        public GraphicsLinesListGeometry3D GenerateGeometry()
        {
            var geometry = 
                GraphicsLinesListGeometry3D.Create(LineVertexPoints);

            return geometry;
        }
    }
}