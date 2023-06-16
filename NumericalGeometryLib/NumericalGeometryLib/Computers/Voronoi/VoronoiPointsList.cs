using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace NumericalGeometryLib.Computers.Voronoi
{
    public sealed class VoronoiPointsList : IReadOnlyList<Float64Vector2D>
    {
        private readonly List<Float64Vector2D> _pointsList
            = new List<Float64Vector2D>();


        public int Count => _pointsList.Count;

        public Float64Vector2D this[int index] => _pointsList[index.Mod(_pointsList.Count)];

        public int DataPointsCount => _pointsList.Count - 3;

        public IEnumerable<Float64Vector2D> DataPoints => _pointsList.Take(_pointsList.Count - 3);

        public IEnumerable<Float64Vector2D> BoundingTrianglePoints => _pointsList.Skip(_pointsList.Count - 3);

        public VoronoiTriangle2D BoundingTriangle { get; }

        public BoundingSphere2D BoundingSphere { get; }

        public IEnumerable<VoronoiEdge2D> BoundingTriangleEdges => BoundingTriangle.Edges;


        public VoronoiPointsList(IEnumerable<IFloat64Tuple2D> pointsList)
        {
            _pointsList.AddRange(
                pointsList.Select(p => p.ToLinVector2D())
            );

            BoundingSphere = BoundingSphere2D.CreateFromPoints(
                _pointsList.Cast<IFloat64Tuple2D>(), 
                1
            );

            BoundingTriangle = CreateBoundingTriangle();
        }


        private VoronoiTriangle2D CreateBoundingTriangle()
        {
            var centerX = BoundingSphere.Center.X;
            var centerY = BoundingSphere.Center.Y;
            var radius = BoundingSphere.Radius;

            var halfSideLength = 
                radius / Math.Tan(Math.PI / 6);

            var point1 = new Float64Vector2D(
                centerX - halfSideLength,
                centerY - radius
            );

            var point2 = new Float64Vector2D(
                centerX + halfSideLength,
                centerY - radius
            );

            var d = point1.GetDistanceToPoint(centerX, centerY);

            var point3 = new Float64Vector2D(
                centerX, 
                centerY + d
            );

            var pointIndex1 = _pointsList.Count;
            var pointIndex2 = pointIndex1 + 1;
            var pointIndex3 = pointIndex1 + 2;

            _pointsList.Add(point1);
            _pointsList.Add(point2);
            _pointsList.Add(point3);

            return new VoronoiTriangle2D(
                this, 
                pointIndex1, 
                pointIndex2, 
                pointIndex3
            );
        }


        public IEnumerator<Float64Vector2D> GetEnumerator()
        {
            return _pointsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _pointsList.GetEnumerator();
        }
    }
}