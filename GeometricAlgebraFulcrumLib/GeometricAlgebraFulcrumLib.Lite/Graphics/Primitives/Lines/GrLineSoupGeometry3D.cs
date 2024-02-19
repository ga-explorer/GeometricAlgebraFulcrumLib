using System.Collections;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Lines;

public sealed class GrLineSoupGeometry3D
    : IGraphicsLineGeometry3D
{
    public static GrLineSoupGeometry3D Create(IReadOnlyList<IFloat64Vector3D> pointsList)
    {
        if ((pointsList.Count & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(params IFloat64Vector3D[] pointsList)
    {
        if ((pointsList.Length & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(IEnumerable<IFloat64Vector3D> pointsList)
    {
        var pointsArray = pointsList.ToArray();

        if ((pointsArray.Length & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsArray);
    }

    public static GrLineSoupGeometry3D Create(params ILineSegment3D[] lineSegmentsList)
    {
        var pointsList = new List<IFloat64Vector3D>(lineSegmentsList.Length * 2);

        foreach (var lineSegment in lineSegmentsList)
        {
            pointsList.Add(lineSegment.GetPoint1());
            pointsList.Add(lineSegment.GetPoint2());
        }

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(IEnumerable<ILineSegment3D> lineSegmentsList)
    {
        var pointsList = new List<IFloat64Vector3D>();

        foreach (var lineSegment in lineSegmentsList)
        {
            pointsList.Add(lineSegment.GetPoint1());
            pointsList.Add(lineSegment.GetPoint2());
        }

        return new GrLineSoupGeometry3D(pointsList);
    }

        
    public GraphicsPrimitiveType3D PrimitiveType
        => GraphicsPrimitiveType3D.Lines;

    public int VertexCount 
        => _vertexPoints.Count;

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
        => GeometryPoints.Select((p, i) => new GrVertex3D(i, p));

    private readonly IReadOnlyList<IFloat64Vector3D> _vertexPoints;
    public IEnumerable<IFloat64Vector3D> GeometryPoints 
        => _vertexPoints;

    public IEnumerable<int> GeometryIndices
        => Enumerable.Range(0, _vertexPoints.Count).ToArray();

    public int Count
        => _vertexPoints.Count >> 1;

    public ILineSegment3D this[int index] 
        => LineSegment3D.Create(
            _vertexPoints[2 * index],
            _vertexPoints[2 * index + 1]
        );

    public IEnumerable<Pair<IFloat64Vector3D>> LineVertexPoints
    {
        get
        {
            for (var i = 0; i < _vertexPoints.Count; i += 2)
                yield return new Pair<IFloat64Vector3D>(
                    _vertexPoints[i],
                    _vertexPoints[i + 1]
                );
        }
    }

    public IEnumerable<Pair<int>> LineVertexIndices
    {
        get
        {
            for (var i = 0; i < _vertexPoints.Count; i += 2)
                yield return new Pair<int>(i, i + 1);
        }
    }


    private GrLineSoupGeometry3D(IReadOnlyList<IFloat64Vector3D> pointsList)
    {
        if (pointsList.Count < 2 || pointsList.Count % 2 != 0)
            throw new InvalidOperationException();

        _vertexPoints = pointsList;
    }

        
    public IFloat64Vector3D GetGeometryPoint(int index)
    {
        return _vertexPoints[index];
    }

    public IEnumerator<ILineSegment3D> GetEnumerator()
    {
        for (var i = 0; i < _vertexPoints.Count; i += 2)
            yield return LineSegment3D.Create(
                _vertexPoints[i], 
                _vertexPoints[i + 1]
            );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}