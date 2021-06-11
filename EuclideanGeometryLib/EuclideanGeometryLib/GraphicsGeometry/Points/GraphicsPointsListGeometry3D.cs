using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;

namespace EuclideanGeometryLib.GraphicsGeometry.Points
{
    public sealed class GraphicsPointsListGeometry3D
        : IGraphicsPointsGeometry3D
    {
        public static GraphicsPointsListGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            return new GraphicsPointsListGeometry3D(pointsList);
        }

        public static GraphicsPointsListGeometry3D Create(params ITuple3D[] pointsList)
        {
            return new GraphicsPointsListGeometry3D(pointsList);
        }

        public static GraphicsPointsListGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            return new GraphicsPointsListGeometry3D(pointsList.ToArray());
        }


        public IEnumerable<IGraphicsVertex3D> Vertices
            => VertexPoints.Select((p, i) => new GraphicsVertex3D(i, p));

        public IReadOnlyList<ITuple3D> VertexPoints { get; }

        public GraphicsPrimitiveType3D PrimitiveType
            => GraphicsPrimitiveType3D.Points;

        public int Count 
            => VertexPoints.Count;

        public ITuple3D this[int index] 
            => VertexPoints[index];

        public IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();


        private GraphicsPointsListGeometry3D(IReadOnlyList<ITuple3D> pointsList)
        {
            VertexPoints = pointsList;
        }


        public IEnumerator<ITuple3D> GetEnumerator()
        {
            return VertexPoints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}