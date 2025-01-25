using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;

public sealed class GrLineSoupGeometry3D
    : IGraphicsLineGeometry3D
{
    public static GrLineSoupGeometry3D Create(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if ((pointsList.Count & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(params ILinFloat64Vector3D[] pointsList)
    {
        if ((pointsList.Length & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        var pointsArray = pointsList.ToArray();

        if ((pointsArray.Length & 1) == 1)
            throw new ArgumentException();

        return new GrLineSoupGeometry3D(pointsArray);
    }

    public static GrLineSoupGeometry3D Create(params IFloat64LineSegment3D[] lineSegmentsList)
    {
        var pointsList = new List<ILinFloat64Vector3D>(lineSegmentsList.Length * 2);

        foreach (var lineSegment in lineSegmentsList)
        {
            pointsList.Add(lineSegment.GetPoint1());
            pointsList.Add(lineSegment.GetPoint2());
        }

        return new GrLineSoupGeometry3D(pointsList);
    }

    public static GrLineSoupGeometry3D Create(IEnumerable<IFloat64LineSegment3D> lineSegmentsList)
    {
        var pointsList = new List<ILinFloat64Vector3D>();

        foreach (var lineSegment in lineSegmentsList)
        {
            pointsList.Add(lineSegment.GetPoint1());
            pointsList.Add(lineSegment.GetPoint2());
        }

        return new GrLineSoupGeometry3D(pointsList);
    }

        
    public GraphicsPrimitiveType3D PrimitiveType
        => GraphicsPrimitiveType3D.LineList;

    public int VertexCount 
        => _vertexPoints.Count;

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
        => GeometryPoints.Select((p, i) => new GrVertex3D(i, p));

    private readonly IReadOnlyList<ILinFloat64Vector3D> _vertexPoints;
    public IEnumerable<ILinFloat64Vector3D> GeometryPoints 
        => _vertexPoints;

    public IEnumerable<int> GeometryIndices
        => Enumerable.Range(0, _vertexPoints.Count).ToArray();

    public int Count
        => _vertexPoints.Count >> 1;

    public IFloat64LineSegment3D this[int index] 
        => Float64LineSegment3D.Create(
            _vertexPoints[2 * index],
            _vertexPoints[2 * index + 1]
        );

    public IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints
    {
        get
        {
            for (var i = 0; i < _vertexPoints.Count; i += 2)
                yield return new Pair<ILinFloat64Vector3D>(
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


    private GrLineSoupGeometry3D(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if (pointsList.Count < 2 || pointsList.Count % 2 != 0)
            throw new InvalidOperationException();

        _vertexPoints = pointsList;
    }

        
    public ILinFloat64Vector3D GetGeometryPoint(int index)
    {
        return _vertexPoints[index];
    }

    public IEnumerator<IFloat64LineSegment3D> GetEnumerator()
    {
        for (var i = 0; i < _vertexPoints.Count; i += 2)
            yield return Float64LineSegment3D.Create(
                _vertexPoints[i], 
                _vertexPoints[i + 1]
            );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}