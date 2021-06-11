using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.BasicShapes.Lines.Immutable;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Lines
{
    public sealed class GraphicsLinesListGeometry3D
        : IGraphicsLinesGeometry3D
    {
        public static GraphicsLinesListGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            if ((pointsList.Count & 1) == 1)
                throw new ArgumentException();

            return new GraphicsLinesListGeometry3D(pointsList);
        }

        public static GraphicsLinesListGeometry3D Create(params ITuple3D[] pointsList)
        {
            if ((pointsList.Length & 1) == 1)
                throw new ArgumentException();

            return new GraphicsLinesListGeometry3D(pointsList);
        }

        public static GraphicsLinesListGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            if ((pointsArray.Length & 1) == 1)
                throw new ArgumentException();

            return new GraphicsLinesListGeometry3D(pointsArray);
        }

        public static GraphicsLinesListGeometry3D Create(params ILineSegment3D[] lineSegmentsList)
        {
            var pointsList = new List<ITuple3D>(lineSegmentsList.Length * 2);

            foreach (var lineSegment in lineSegmentsList)
            {
                pointsList.Add(lineSegment.GetPoint1());
                pointsList.Add(lineSegment.GetPoint2());
            }

            return new GraphicsLinesListGeometry3D(pointsList);
        }

        public static GraphicsLinesListGeometry3D Create(IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            var pointsList = new List<ITuple3D>();

            foreach (var lineSegment in lineSegmentsList)
            {
                pointsList.Add(lineSegment.GetPoint1());
                pointsList.Add(lineSegment.GetPoint2());
            }

            return new GraphicsLinesListGeometry3D(pointsList);
        }

        
        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.Lines;

        public IEnumerable<IGraphicsVertex3D> Vertices
            => VertexPoints.Select((p, i) => new GraphicsVertex3D(i, p));

        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();

        public int Count
            => VertexPoints.Count >> 1;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                VertexPoints[2 * index],
                VertexPoints[2 * index + 1]
            );

        public IReadOnlyList<Pair<ITuple3D>> LineVerticesPoints
        {
            get
            {
                var pointsList = new List<Pair<ITuple3D>>(Count);

                for (var i = 0; i < VertexPoints.Count; i += 2)
                    pointsList.Add(new Pair<ITuple3D>(
                        VertexPoints[i],
                        VertexPoints[i + 1]
                    ));

                return pointsList;
            }
        }

        public IReadOnlyList<Pair<int>> LineVerticesIndices
        {
            get
            {
                var pointsList = new List<Pair<int>>(Count);

                for (var i = 0; i < VertexPoints.Count; i += 2)
                    pointsList.Add(new Pair<int>(i, i + 1));

                return pointsList;
            }
        }


        private GraphicsLinesListGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }

        
        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < VertexPoints.Count; i += 2)
            {
                var point1 = VertexPoints[i];
                var point2 = VertexPoints[i + 1];

                yield return LineSegment3D.Create(point1, point2);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}