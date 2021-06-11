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
    public sealed class GraphicsLineStripGeometry3D
        : IGraphicsLinesGeometry3D
    {
        public static GraphicsLineStripGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            return new GraphicsLineStripGeometry3D(pointsList);
        }

        public static GraphicsLineStripGeometry3D Create(params ITuple3D[] pointsList)
        {
            return new GraphicsLineStripGeometry3D(pointsList);
        }

        public static GraphicsLineStripGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            return new GraphicsLineStripGeometry3D(pointsList.ToArray());
        }


        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.LineStrip;

        public IEnumerable<IGraphicsVertex3D> Vertices
            => VertexPoints.Select((p, i) => new GraphicsVertex3D(i, p));

        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();

        public int Count
            => VertexPoints.Count - 1;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                VertexPoints[index],
                VertexPoints[index + 1]
            );

        public IReadOnlyList<Pair<ITuple3D>> LineVerticesPoints
        {
            get
            {
                var pointsList = new List<Pair<ITuple3D>>(Count);

                for (var i = 0; i < VertexPoints.Count - 1; i++)
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

                for (var i = 0; i < VertexPoints.Count - 1; i++)
                    pointsList.Add(new Pair<int>(i, i + 1));

                return pointsList;
            }
        }


        private GraphicsLineStripGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }
        

        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < VertexPoints.Count - 1; i++)
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