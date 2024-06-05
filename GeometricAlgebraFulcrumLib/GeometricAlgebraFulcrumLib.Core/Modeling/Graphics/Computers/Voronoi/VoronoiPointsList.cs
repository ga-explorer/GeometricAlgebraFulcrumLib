using System.Collections;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Computers.Voronoi;

public sealed class VoronoiPointsList : IReadOnlyList<LinFloat64Vector2D>
{
    private readonly List<LinFloat64Vector2D> _pointsList
        = new List<LinFloat64Vector2D>();


    public int Count => _pointsList.Count;

    public LinFloat64Vector2D this[int index] => _pointsList[index.Mod(_pointsList.Count)];

    public int DataPointsCount => _pointsList.Count - 3;

    public IEnumerable<LinFloat64Vector2D> DataPoints => _pointsList.Take(_pointsList.Count - 3);

    public IEnumerable<LinFloat64Vector2D> BoundingTrianglePoints => _pointsList.Skip(_pointsList.Count - 3);

    public VoronoiTriangle2D BoundingTriangle { get; }

    public BoundingSphere2D BoundingSphere { get; }

    public IEnumerable<VoronoiEdge2D> BoundingTriangleEdges => BoundingTriangle.Edges;


    public VoronoiPointsList(IEnumerable<ILinFloat64Vector2D> pointsList)
    {
        _pointsList.AddRange(
            pointsList.Select(p => p.ToLinVector2D())
        );

        BoundingSphere = BoundingSphere2D.CreateFromPoints(
            _pointsList.Cast<ILinFloat64Vector2D>(), 
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

        var point1 = LinFloat64Vector2D.Create(centerX - halfSideLength,
            centerY - radius);

        var point2 = LinFloat64Vector2D.Create(centerX + halfSideLength,
            centerY - radius);

        var d = point1.GetDistanceToPoint(centerX, centerY);

        var point3 = LinFloat64Vector2D.Create(centerX, 
            centerY + d);

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


    public IEnumerator<LinFloat64Vector2D> GetEnumerator()
    {
        return _pointsList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _pointsList.GetEnumerator();
    }
}