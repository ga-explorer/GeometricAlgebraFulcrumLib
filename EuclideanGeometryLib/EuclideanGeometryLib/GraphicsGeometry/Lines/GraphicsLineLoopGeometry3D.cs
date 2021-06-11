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
    public sealed class GraphicsLineLoopGeometry3D
        : IGraphicsLinesGeometry3D
    {
        public static GraphicsLineLoopGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            return new GraphicsLineLoopGeometry3D(pointsList);
        }
        
        public static GraphicsLineLoopGeometry3D Create(params ITuple3D[] pointsList)
        {
            return new GraphicsLineLoopGeometry3D(pointsList);
        }

        public static GraphicsLineLoopGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            return new GraphicsLineLoopGeometry3D(pointsList.ToArray());
        }


        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.LineLoop;

        public IEnumerable<IGraphicsVertex3D> Vertices
            => VertexPoints.Select((p, i) => new GraphicsVertex3D(i, p));

        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();

        public int Count
            => VertexPoints.Count;

        public ILineSegment3D this[int index] 
            => LineSegment3D.Create(
                VertexPoints[index],
                VertexPoints[(index + 1) % VertexPoints.Count]
            );

        public IReadOnlyList<Pair<ITuple3D>> LineVerticesPoints
        {
            get
            {
                var pointsList = new List<Pair<ITuple3D>>(Count);

                pointsList.AddRange(
                    VertexPoints.Select((t, i) => 
                        new Pair<ITuple3D>(t, VertexPoints[(i + 1) % VertexPoints.Count])
                    )
                );

                return pointsList;
            }
        }

        public IReadOnlyList<Pair<int>> LineVerticesIndices
        {
            get
            {
                var pointsList = new List<Pair<int>>(Count);

                pointsList.AddRange(
                    VertexPoints.Select((t, i) => 
                        new Pair<int>(i, (i + 1) % VertexPoints.Count)
                    )
                );

                return pointsList;
            }
        }


        private GraphicsLineLoopGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }


        public IEnumerator<ILineSegment3D> GetEnumerator()
        {
            for (var i = 0; i < VertexPoints.Count; i++)
            {
                var point1 = VertexPoints[i];
                var point2 = VertexPoints[(i + 1) % VertexPoints.Count];

                yield return LineSegment3D.Create(point1, point2);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}