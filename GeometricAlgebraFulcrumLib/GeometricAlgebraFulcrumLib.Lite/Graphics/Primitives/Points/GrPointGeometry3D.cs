﻿using System.Collections;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Points;

/// <summary>
/// This class represents a points geometry where the list of points are partially
/// used in rendering by selecting which indices to render
/// </summary>
public sealed class GrPointGeometry3D
    : IGraphicsPointGeometry3D
{
    public static GrPointGeometry3D Create(IReadOnlyList<IFloat64Vector3D> pointsList)
    {
        return new GrPointGeometry3D(pointsList);
    }

    public static GrPointGeometry3D Create(params IFloat64Vector3D[] pointsList)
    {
        return new GrPointGeometry3D(pointsList);
    }

    public static GrPointGeometry3D Create(IEnumerable<IFloat64Vector3D> pointsList)
    {
        return new GrPointGeometry3D(pointsList.ToArray());
    }


    private readonly IReadOnlyList<IFloat64Vector3D> _vertexPoints;
    public IEnumerable<IFloat64Vector3D> GeometryPoints 
        => _vertexPoints;

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
        => GeometryPoints.Select((p, i) => new GrVertex3D(i, p));

    public GraphicsPrimitiveType3D PrimitiveType
        => GraphicsPrimitiveType3D.Points;

    public int VertexCount 
        => _vertexPoints.Count;

    private readonly List<int> _vertexIndicesList = new List<int>();
    public IEnumerable<int> GeometryIndices
        => _vertexIndicesList;

    public int Count
        => _vertexIndicesList.Count;

    public IFloat64Vector3D this[int index] 
        => _vertexPoints[_vertexIndicesList[index]];


    private GrPointGeometry3D(IReadOnlyList<IFloat64Vector3D> pointsList)
    {
        _vertexPoints = pointsList;
    }

        
    public IFloat64Vector3D GetGeometryPoint(int index)
    {
        return _vertexPoints[index];
    }

    public GrPointGeometry3D ClearPoints()
    {
        _vertexIndicesList.Clear();

        return this;
    }

    public GrPointGeometry3D RemovePoint(int pointIndex)
    {
        if (pointIndex < 0 || pointIndex >= _vertexPoints.Count)
            return this;

        _vertexIndicesList.RemoveAt(pointIndex);

        return this;
    }


    public GrPointGeometry3D AppendPoint(int vertexIndex)
    {
        vertexIndex = vertexIndex.Mod(_vertexPoints.Count);

        _vertexIndicesList.Add(vertexIndex);

        return this;
    }

    public GrPointGeometry3D PrependPoint(int vertexIndex)
    {
        vertexIndex = vertexIndex.Mod(_vertexPoints.Count);

        _vertexIndicesList.Insert(0, vertexIndex);

        return this;
    }

    public GrPointGeometry3D InsertPoint(int pointIndex, int vertexIndex)
    {
        if (pointIndex < 0 || pointIndex >= _vertexPoints.Count)
            throw new IndexOutOfRangeException(nameof(pointIndex));

        vertexIndex = vertexIndex.Mod(_vertexPoints.Count);

        _vertexIndicesList.Insert(pointIndex, vertexIndex);

        return this;
    }


    public GrPointGeometry3D AppendPoints(IEnumerable<int> vertexIndicesList)
    {
        _vertexIndicesList.AddRange(
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }
        
    public GrPointGeometry3D AppendPoints(params int[] vertexIndicesList)
    {
        _vertexIndicesList.AddRange(
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }
        
    public GrPointGeometry3D PrependPoints(IEnumerable<int> vertexIndicesList)
    {
        _vertexIndicesList.InsertRange(
            0,
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }
        
    public GrPointGeometry3D PrependPoints(params int[] vertexIndicesList)
    {
        _vertexIndicesList.InsertRange(
            0,
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }
        
    public GrPointGeometry3D InsertPoints(int pointIndex, IEnumerable<int> vertexIndicesList)
    {
        if (pointIndex < 0 || pointIndex >= _vertexPoints.Count)
            throw new IndexOutOfRangeException(nameof(pointIndex));

        _vertexIndicesList.InsertRange(
            pointIndex,
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }
        
    public GrPointGeometry3D InsertPoints(int pointIndex, params int[] vertexIndicesList)
    {
        if (pointIndex < 0 || pointIndex >= _vertexPoints.Count)
            throw new IndexOutOfRangeException(nameof(pointIndex));

        _vertexIndicesList.InsertRange(
            pointIndex,
            vertexIndicesList.Select(i => i.Mod(_vertexPoints.Count))
        );

        return this;
    }

        
    public IEnumerator<IFloat64Vector3D> GetEnumerator()
    {
        return _vertexIndicesList.Select(
            i => _vertexPoints[i]
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}