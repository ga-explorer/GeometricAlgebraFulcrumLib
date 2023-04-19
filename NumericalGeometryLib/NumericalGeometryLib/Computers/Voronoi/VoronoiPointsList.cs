using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Immutable;

namespace NumericalGeometryLib.Computers.Voronoi
{
    public sealed class VoronoiPointsList : IReadOnlyList<Float64Tuple2D>
    {
        private readonly List<Float64Tuple2D> _pointsList
            = new List<Float64Tuple2D>();


        public int Count
        {
            get { return _pointsList.Count; }
        }

        public Float64Tuple2D this[int index]
        {
            get { return _pointsList[index.Mod(_pointsList.Count)]; }
        }

        public int DataPointsCount
        {
            get { return _pointsList.Count - 3; }
        }

        public IEnumerable<Float64Tuple2D> DataPoints
        {
            get { return _pointsList.Take(_pointsList.Count - 3); }
        }

        public IEnumerable<Float64Tuple2D> BoundingTrianglePoints
        {
            get { return _pointsList.Skip(_pointsList.Count - 3); }
        }

        public VoronoiTriangle2D BoundingTriangle { get; }

        public BoundingSphere2D BoundingSphere { get; }

        public IEnumerable<VoronoiEdge2D> BoundingTriangleEdges
        {
            get { return BoundingTriangle.Edges; }
        }


        public VoronoiPointsList(IEnumerable<IFloat64Tuple2D> pointsList)
        {
            _pointsList.AddRange(
                pointsList.Select(p => p.ToTuple2D())
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

            var point1 = new Float64Tuple2D(
                centerX - halfSideLength,
                centerY - radius
            );

            var point2 = new Float64Tuple2D(
                centerX + halfSideLength,
                centerY - radius
            );

            var d = point1.GetDistanceToPoint(centerX, centerY);

            var point3 = new Float64Tuple2D(
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


        public IEnumerator<Float64Tuple2D> GetEnumerator()
        {
            return _pointsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _pointsList.GetEnumerator();
        }
    }
}