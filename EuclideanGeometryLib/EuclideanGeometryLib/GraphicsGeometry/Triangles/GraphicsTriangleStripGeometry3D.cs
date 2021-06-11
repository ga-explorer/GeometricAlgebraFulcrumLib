using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Triangles;
using EuclideanGeometryLib.BasicShapes.Triangles.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Triangles
{
    public sealed class GraphicsTriangleStripGeometry3D 
        : GraphicsTrianglesGeometryBase3D
    {
        public static GraphicsTriangleStripGeometry3D Create(IReadOnlyList<ITuple3D> pointsList)
        {
            if (pointsList.Count < 3)
                throw new ArgumentException();

            return new GraphicsTriangleStripGeometry3D(pointsList);
        }

        public static GraphicsTriangleStripGeometry3D Create(params ITuple3D[] pointsList)
        {
            if (pointsList.Length < 3)
                throw new ArgumentException();

            return new GraphicsTriangleStripGeometry3D(pointsList);
        }

        public static GraphicsTriangleStripGeometry3D Create(IEnumerable<ITuple3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            if (pointsArray.Length < 3)
                throw new ArgumentException();

            return new GraphicsTriangleStripGeometry3D(pointsArray);
        }
        

        public override GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.TriangleStrip;

        public override IReadOnlyList<int> VertexIndices
            => Enumerable.Range(0, VertexPoints.Count).ToArray();

        public override int Count
            => VertexPoints.Count - 2;

        public override ITriangle3D this[int index] 
            => Triangle3D.Create(
                VertexPoints[index],
                VertexPoints[index + 1],
                VertexPoints[index + 2]
            );

        public override IReadOnlyList<Triplet<ITuple3D>> TriangleVerticesPoints
        {
            get
            {
                var pointsList = new List<Triplet<ITuple3D>>(Count);

                for (var i = 0; i < VertexPoints.Count - 2; i++)
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

                for (var i = 0; i < VertexPoints.Count - 2; i++)
                    pointsList.Add(new Triplet<int>(i, i + 1, i + 2));

                return pointsList;
            }
        }


        private GraphicsTriangleStripGeometry3D(IReadOnlyList<ITuple3D> pointsList)
            : base(pointsList)
        {
        }


        public override IEnumerator<ITriangle3D> GetEnumerator()
        {
            for (var i = 0; i < VertexPoints.Count - 2; i++)
            {
                var point1 = VertexPoints[i];
                var point2 = VertexPoints[i + 1];
                var point3 = VertexPoints[i + 2];

                yield return Triangle3D.Create(point1, point2, point3);
            }
        }
    }
}