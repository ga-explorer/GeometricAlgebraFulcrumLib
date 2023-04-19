using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes.Triangles.Immutable;

namespace GraphicsComposerLib.Geometry.Primitives.Triangles
{
    public sealed class GrTriangleStripGeometry3D 
        : GrTriangleGeometryBase3D
    {
        public static GrTriangleStripGeometry3D Create(IReadOnlyList<IFloat64Tuple3D> pointsList)
        {
            if (pointsList.Count < 3)
                throw new ArgumentException();

            return new GrTriangleStripGeometry3D(pointsList);
        }

        public static GrTriangleStripGeometry3D Create(params IFloat64Tuple3D[] pointsList)
        {
            if (pointsList.Length < 3)
                throw new ArgumentException();

            return new GrTriangleStripGeometry3D(pointsList);
        }

        public static GrTriangleStripGeometry3D Create(IEnumerable<IFloat64Tuple3D> pointsList)
        {
            var pointsArray = pointsList.ToArray();

            if (pointsArray.Length < 3)
                throw new ArgumentException();

            return new GrTriangleStripGeometry3D(pointsArray);
        }
        

        public override GraphicsPrimitiveType3D PrimitiveType 
            => GraphicsPrimitiveType3D.TriangleStrip;

        public override IEnumerable<int> GeometryIndices
            => Enumerable.Range(0, VertexCount).ToArray();

        public override int Count
            => VertexCount - 2;

        public override ITriangle3D this[int index] 
            => Triangle3D.Create(
                GetGeometryPoint(index),
                GetGeometryPoint(index + 1),
                GetGeometryPoint(index + 2)
            );

        public override IEnumerable<Triplet<IFloat64Tuple3D>> TriangleVertexPoints
        {
            get
            {
                var pointsList = new List<Triplet<IFloat64Tuple3D>>(Count);

                for (var i = 0; i < VertexCount - 2; i++)
                    pointsList.Add(new Triplet<IFloat64Tuple3D>(
                        GetGeometryPoint(i),
                        GetGeometryPoint(i + 1),
                        GetGeometryPoint(i + 2)
                    ));

                return pointsList;
            }
        }

        public override IEnumerable<Triplet<int>> TriangleVertexIndices
        {
            get
            {
                var pointsList = new List<Triplet<int>>(Count);

                for (var i = 0; i < VertexCount - 2; i++)
                    pointsList.Add(new Triplet<int>(i, i + 1, i + 2));

                return pointsList;
            }
        }


        private GrTriangleStripGeometry3D(IReadOnlyList<IFloat64Tuple3D> pointsList)
            : base(pointsList)
        {
        }


        public override IEnumerator<ITriangle3D> GetEnumerator()
        {
            for (var i = 0; i < VertexCount - 2; i++)
            {
                var point1 = GetGeometryPoint(i);
                var point2 = GetGeometryPoint(i + 1);
                var point3 = GetGeometryPoint(i + 2);

                yield return Triangle3D.Create(point1, point2, point3);
            }
        }
    }
}