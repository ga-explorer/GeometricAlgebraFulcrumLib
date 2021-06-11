using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Triangles
{
    public sealed class GraphicsTrianglesListGeometry3D 
        : GraphicsTrianglesGeometryBase3D
    {
        public static GraphicsTrianglesListGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            if ((pointsList.Count % 3) != 0)
                throw new ArgumentException();

            return new GraphicsTrianglesListGeometry3D(pointsList);
        }

        public static GraphicsTrianglesListGeometry3D Create(params ITuple3D[] pointsList)
        {
            if ((pointsList.Length % 3) != 0)
                throw new ArgumentException();

            return new GraphicsTrianglesListGeometry3D(pointsList);
        }

        public static GraphicsTrianglesListGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            if ((pointsArray.Length % 3) != 0)
                throw new ArgumentException();

            return new GraphicsTrianglesListGeometry3D(pointsArray);
        }

        public static GraphicsTrianglesListGeometry3D Create(params ITriangle3D[] trianglesList)
        {
            var pointsList = new List<ITuple3D>(trianglesList.Length * 3);

            foreach (var triangle in trianglesList)
            {
                pointsList.Add(triangle.GetPoint1());
                pointsList.Add(triangle.GetPoint2());
                pointsList.Add(triangle.GetPoint3());
            }

            return new GraphicsTrianglesListGeometry3D(pointsList);
        }

        public static GraphicsTrianglesListGeometry3D Create(IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
        {
            var pointsList = new List<ITuple3D>();

            foreach (var triangle in trianglesList)
            {
                if (reversePoints)
                {
                    pointsList.Add(triangle.GetPoint3());
                    pointsList.Add(triangle.GetPoint2());
                    pointsList.Add(triangle.GetPoint1());
                }
                else                
                {
                    pointsList.Add(triangle.GetPoint1());
                    pointsList.Add(triangle.GetPoint2());
                    pointsList.Add(triangle.GetPoint3());
                }
            }

            return new GraphicsTrianglesListGeometry3D(pointsList);
        }

        
        public override GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.Triangles;

        public override IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();

        public override int Count
            => VertexPoints.Count / 3;

        public override ITriangle3D this[int index] 
            => Triangle3D.Create(
                VertexPoints[3 * index],
                VertexPoints[3 * index + 1],
                VertexPoints[3 * index + 2]
            );

        public override IReadOnlyList<Triplet<ITuple3D>> TriangleVerticesPoints
        {
            get
            {
                var pointsList = new List<Triplet<ITuple3D>>(Count);

                for (var i = 0; i < VertexPoints.Count; i += 3)
                    pointsList.Add(new Triplet<ITuple3D>(
                        VertexPoints[i],
                        VertexPoints[i + 1],
                        VertexPoints[i + 2]
                    ));

                return pointsList;
            }
        }

        public override IReadOnlyList<Triplet<int>> TriangleVerticesIndices
        {
            get
            {
                var pointsList = new List<Triplet<int>>(Count);

                for (var i = 0; i < VertexPoints.Count; i += 3)
                    pointsList.Add(new Triplet<int>(i, i + 1, i + 2));

                return pointsList;
            }
        }


        private GraphicsTrianglesListGeometry3D(IReadOnlyList<ITuple3D> pointsList)
            : base(pointsList)
        {
            if ((VertexPoints.Count % 3) != 0)
                throw new ArgumentException();
        }


        public override IEnumerator<ITriangle3D> GetEnumerator()
        {
            for (var i = 0; i < VertexPoints.Count; i += 3)
            {
                var point1 = VertexPoints[i];
                var point2 = VertexPoints[i + 1];
                var point3 = VertexPoints[i + 2];

                yield return Triangle3D.Create(point1, point2, point3);
            }
        }
    }
}