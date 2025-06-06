﻿using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Lines;

public class GrLineSoupGeometryComposer3D : 
    IGraphicsLineGeometry3D
{
    private readonly List<IGraphicsVertex3D> _verticesList 
        = new List<IGraphicsVertex3D>();

        
    public double DistanceEpsilon { get; set; }
        = 1e-7d;

    public bool AllowSmallLines { get; set; }

    public GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.LineList;

    public int Count 
        => _verticesList.Count >> 1;

    public IFloat64LineSegment3D this[int index] 
        => Float64LineSegment3D.Create(
            _verticesList[2 * index],
            _verticesList[2 * index + 1]
        );

    public int VertexCount 
        => _verticesList.Count;

    public IEnumerable<int> GeometryIndices 
        => Enumerable.Range(0, _verticesList.Count);

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
        => _verticesList;

    public IEnumerable<ILinFloat64Vector3D> GeometryPoints
        => _verticesList;

    public IEnumerable<Pair<int>> LineVertexIndices
    {
        get
        {
            for (var i = 0; i < _verticesList.Count; i += 2)
                yield return new Pair<int>(i, i + 1);
        }
    }

    public IEnumerable<Pair<IGraphicsSurfaceLocalFrame3D>> LineVertices
    {
        get
        {
            for (var i = 0; i < _verticesList.Count; i += 2)
                yield return new Pair<IGraphicsSurfaceLocalFrame3D>(
                    _verticesList[i], 
                    _verticesList[i + 1]
                );
        }
    }

    public IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints
    {
        get
        {
            for (var i = 0; i < _verticesList.Count; i += 2)
                yield return new Pair<ILinFloat64Vector3D>(
                    _verticesList[i].Point, 
                    _verticesList[i + 1].Point
                );
        }
    }

    public int VerticesCount
        => _verticesList.Count;

        
    public GrLineSoupGeometryComposer3D()
    {
    }


    public GrLineSoupGeometryComposer3D Clear()
    {
        _verticesList.Clear();

        return this;
    }


    private IGraphicsSurfaceLocalFrame3D AddVertex(ILinFloat64Vector3D point)
    {
        var storedVertex = new GrVertex3D(_verticesList.Count, point);
        _verticesList.Add(storedVertex);

        return storedVertex;
    }

    private IGraphicsSurfaceLocalFrame3D AddVertex(double x, double y, double z)
    {
        var storedVertex = new GrVertex3D(_verticesList.Count, x, y, z);
        _verticesList.Add(storedVertex);

        return storedVertex;
    }

    private IGraphicsSurfaceLocalFrame3D AddVertexFromVertex(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var storedVertex = new GrVertex3D(_verticesList.Count, vertex);
        _verticesList.Add(storedVertex);

        return storedVertex;
    }

    private bool StoreLine(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2)
    {
        //Make sure all side lengths are far enough from zero
        if (!AllowSmallLines)
        {
            if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
        }

        return true;
    }
        
        
    public ILinFloat64Vector3D GetGeometryPoint(int index)
    {
        return _verticesList[index];
    }

    public bool AddLine(IFloat64LineSegment3D lineSegment)
    {
        return StoreLine(
            AddVertex(lineSegment.GetPoint1()), 
            AddVertex(lineSegment.GetPoint2())
        );
    }

    public bool AddLine(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        return StoreLine(
            AddVertex(point1),
            AddVertex(point2)
        );
    }

    public bool AddLine(IPair<ILinFloat64Vector3D> points)
    {
        return StoreLine(
            AddVertex(points.Item1),
            AddVertex(points.Item2)
        );
    }

    public bool AddLineFromVertices(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2)
    {
        return StoreLine(
            AddVertexFromVertex(vertex1), 
            AddVertexFromVertex(vertex2)
        );
    }

    public bool AddLineFromVertices(IPair<IGraphicsSurfaceLocalFrame3D> vertices)
    {
        return StoreLine(
            AddVertexFromVertex(vertices.Item1), 
            AddVertexFromVertex(vertices.Item2)
        );
    }


    public GrLineSoupGeometryComposer3D AddLines(IEnumerable<IFloat64LineSegment3D> linesList)
    {
        foreach (var line in linesList)
        {
            var vertex1 = AddVertex(line.GetPoint1());
            var vertex2 = AddVertex(line.GetPoint2());

            StoreLine(vertex1, vertex2);
        }

        return this;
    }

    public GrLineSoupGeometryComposer3D AddLines(IEnumerable<IPair<ILinFloat64Vector3D>> linePointsList)
    {
        foreach (var points in linePointsList)
        {
            var vertex1 = AddVertex(points.Item1);
            var vertex2 = AddVertex(points.Item2);

            StoreLine(vertex1, vertex2);
        }

        return this;
    }

    public GrLineSoupGeometryComposer3D AddLines(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if (pointsList.Count % 2 != 0)
            throw new InvalidOperationException();

        for (var i = 0; i < pointsList.Count; i += 2)
        {
            var vertex1 = AddVertex(pointsList[i]);
            var vertex2 = AddVertex(pointsList[i + 1]);

            StoreLine(vertex1, vertex2);
        }

        return this;
    }

    public GrLineSoupGeometryComposer3D AddLines(IEnumerable<IPair<IGraphicsSurfaceLocalFrame3D>> lineVerticesList)
    {
        foreach (var vertices in lineVerticesList)
        {
            var vertex1 = AddVertexFromVertex(vertices.Item1);
            var vertex2 = AddVertexFromVertex(vertices.Item2);

            StoreLine(vertex1, vertex2);
        }

        return this;
    }

    public GrLineSoupGeometryComposer3D AddLines(IReadOnlyList<IGraphicsSurfaceLocalFrame3D> verticesList)
    {
        if (verticesList.Count % 2 != 0)
            throw new InvalidOperationException();

        for (var i = 0; i < verticesList.Count; i += 2)
        {
            var vertex1 = AddVertexFromVertex(verticesList[i]);
            var vertex2 = AddVertexFromVertex(verticesList[i + 1]);

            StoreLine(vertex1, vertex2);
        }

        return this;
    }


    //public GrLineSoupGeometry3D GenerateGeometry()
    //{
    //    var geometry = 
    //        GrLineSoupGeometry3D.Create(_verticesList);

    //    return geometry;
    //}

    public IEnumerator<IFloat64LineSegment3D> GetEnumerator()
    {
        for (var i = 0; i < _verticesList.Count; i += 2)
            yield return Float64LineSegment3D.Create(
                _verticesList[i],
                _verticesList[i + 1]
            );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}