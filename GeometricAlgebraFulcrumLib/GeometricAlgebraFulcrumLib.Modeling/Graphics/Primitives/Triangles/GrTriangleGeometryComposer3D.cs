﻿using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;

/// <summary>
/// This class can be used to incrementally build a graphics triangle geometry
/// in 3D
/// </summary>
public class  GrTriangleGeometryComposer3D :
    IGraphicsTriangleGeometry3D
{
    private readonly Dictionary<Triplet<Float64Scalar>, IGraphicsVertex3D> _verticesTable
        = new Dictionary<Triplet<Float64Scalar>, IGraphicsVertex3D>();

    private readonly List<IGraphicsVertex3D> _verticesList 
        = new List<IGraphicsVertex3D>();

    private readonly List<int> _indicesList
        = new List<int>();


    public double DistanceEpsilon { get; set; }
        = 1e-7d;

    public bool AllowDuplicatePoints { get; private set; }

    public bool AllowSmallTriangles { get; set; }

    public bool VertexTextureUVsEnabled { get; set; }

    public bool VertexColorsEnabled { get; set; }

    public bool VertexNormalsEnabled { get; set; }

    public int Count 
        => _indicesList.Count / 3;

    public IFloat64Triangle3D this[int index] 
        => Float64Triangle3D.Create(
            _verticesList[_indicesList[index]],
            _verticesList[_indicesList[index + 1]],
            _verticesList[_indicesList[index + 2]]
        );

    public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
        = GrVertexNormalComputationMethod.WeightedNormals;
        
    public bool ReverseNormals { get; set; }

    public GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.TriangleList;

    public int VertexCount 
        => _verticesList.Count;

    public IEnumerable<int> GeometryIndices 
        => _indicesList;

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
        => _verticesList;

    public IEnumerable<ILinFloat64Vector3D> GeometryPoints
        => _verticesList.Select(p => p.Point);

    public IEnumerable<ILinFloat64Vector2D> VertexTextureUVs
        => _verticesList.Select(p => p.ParameterValue.ToLinVector2D());

    public IEnumerable<Color> VertexColors
        => _verticesList.Select(p => p.Color);
        
    public IEnumerable<ILinFloat64Vector3D> VertexNormals
        => _verticesList.Select(p => p.Normal);

    public IEnumerable<Triplet<int>> TriangleVertexIndices
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<int>(
                    _indicesList[i],
                    _indicesList[i + 1],
                    _indicesList[i + 2]
                );
        }
    }

    public IEnumerable<Triplet<IGraphicsSurfaceLocalFrame3D>> TriangleVertices
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<IGraphicsSurfaceLocalFrame3D>(
                    _verticesList[_indicesList[i]],
                    _verticesList[_indicesList[i + 1]],
                    _verticesList[_indicesList[i + 2]]
                );
        }
    }

    public IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<ILinFloat64Vector3D>(
                    _verticesList[_indicesList[i]].Point,
                    _verticesList[_indicesList[i + 1]].Point,
                    _verticesList[_indicesList[i + 2]].Point
                );
        }
    }

    public IEnumerable<Triplet<ILinFloat64Vector2D>> TriangleVertexTextureUVs
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<ILinFloat64Vector2D>(
                    _verticesList[_indicesList[i]].ParameterValue.ToLinVector2D(),
                    _verticesList[_indicesList[i + 1]].ParameterValue.ToLinVector2D(),
                    _verticesList[_indicesList[i + 2]].ParameterValue.ToLinVector2D()
                );
        }
    }
        
    public IEnumerable<Triplet<Color>> TriangleVertexColors
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<Color>(
                    _verticesList[_indicesList[i]].Color,
                    _verticesList[_indicesList[i + 1]].Color,
                    _verticesList[_indicesList[i + 2]].Color
                );
        }
    }

    public IEnumerable<Triplet<LinFloat64Normal3D>> TriangleVertexNormals
    {
        get
        {
            for (var i = 0; i < _indicesList.Count; i += 3)
                yield return new Triplet<LinFloat64Normal3D>(
                    _verticesList[_indicesList[i]].Normal,
                    _verticesList[_indicesList[i + 1]].Normal,
                    _verticesList[_indicesList[i + 2]].Normal
                );
        }
    }

    public int VerticesCount
        => _verticesList.Count;
        
    public int TrianglesCount
        => _indicesList.Count / 3;


    public GrTriangleGeometryComposer3D()
    {
    }


    private IGraphicsVertex3D CreateVertex(ITriplet<Float64Scalar> point)
    {
        var vertexIndex = _verticesList.Count;

        if (VertexNormalsEnabled)
        {
            if (VertexColorsEnabled)
                return new GrNormalColorVertex3D(vertexIndex, point);

            if (VertexTextureUVsEnabled)
                return new GrNormalTextureVertex3D(vertexIndex, point);

            return new GrNormalVertex3D(vertexIndex, point);
        }

        if (VertexColorsEnabled)
            return new GrColorVertex3D(vertexIndex, point);

        if (VertexTextureUVsEnabled)
            return new GrTextureVertex3D(vertexIndex, point);

        return new GrVertex3D(vertexIndex, point);
    }

    private IGraphicsVertex3D CreateVertex(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var vertexIndex = _verticesList.Count;

        if (VertexNormalsEnabled)
        {
            if (VertexColorsEnabled)
                return new GrNormalColorVertex3D(vertexIndex, vertex);

            if (VertexTextureUVsEnabled)
                return new GrNormalTextureVertex3D(vertexIndex, vertex);

            return new GrNormalVertex3D(vertexIndex, vertex);
        }

        if (VertexColorsEnabled)
            return new GrColorVertex3D(vertexIndex, vertex);

        if (VertexTextureUVsEnabled)
            return new GrTextureVertex3D(vertexIndex, vertex);

        return new GrVertex3D(vertexIndex, vertex);
    }

    private void UpdateVertexNormals(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2, IGraphicsSurfaceLocalFrame3D vertex3)
    {
        if (NormalComputationMethod == GrVertexNormalComputationMethod.WeightedNormals)
        {
            //Find inner angles of triangle
            var angle1 = vertex1.GetAngleFromPoints(vertex3, vertex2);
            var angle2 = vertex2.GetAngleFromPoints(vertex1, vertex3);
            var angle3 = vertex3.GetAngleFromPoints(vertex2, vertex1);

            //Find triangle normal, not unit but full normal vector
            var normal = 
                ReverseNormals
                    ? LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                    : LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

            // For debugging only
            Debug.Assert(
                angle1.IsValid() &&
                angle2.IsValid() &&
                angle3.IsValid() &&
                normal.IsValid()
            );

            // Update normals of triangle vertices.
            // See here for more information:
            // http://www.bytehazard.com/articles/vertnorm.html
            // normal.MakeUnitVector();
            vertex1.Normal.Update(angle1.RadiansValue * normal);
            vertex2.Normal.Update(angle2.RadiansValue * normal);
            vertex3.Normal.Update(angle3.RadiansValue * normal);

            return;
        }
            
        if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageUnitNormals)
        {
            //Find triangle unit normal
            var normal = 
                ReverseNormals
                    ? LinFloat64Vector3DAffineUtils.GetTriangleUnitNormal(vertex3, vertex2, vertex1)
                    : LinFloat64Vector3DAffineUtils.GetTriangleUnitNormal(vertex1, vertex2, vertex3);
                
            //For debugging only
            Debug.Assert(normal.IsValid());

            vertex1.Normal.Update(normal);
            vertex2.Normal.Update(normal);
            vertex3.Normal.Update(normal);

            return;
        }
            
        if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageNormals)
        {
            //Find triangle normal, not unit but full normal vector
            var normal = 
                ReverseNormals
                    ? LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex3, vertex2, vertex1)
                    : LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

            //For debugging only
            Debug.Assert(normal.IsValid());

            vertex1.Normal.Update(normal);
            vertex2.Normal.Update(normal);
            vertex3.Normal.Update(normal);

            return;
        }
    }

    private bool StoreTriangle(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2, IGraphicsSurfaceLocalFrame3D vertex3)
    {
        //Make sure all side lengths are far enough from zero
        if (!AllowSmallTriangles)
        {
            if (vertex2.GetDistanceToPoint(vertex1) < DistanceEpsilon) return false;
            if (vertex3.GetDistanceToPoint(vertex2) < DistanceEpsilon) return false;
            if (vertex1.GetDistanceToPoint(vertex3) < DistanceEpsilon) return false;
        }

        //Add triangle vertex indices to indices list
        _indicesList.Add(vertex1.Index);
        _indicesList.Add(vertex2.Index);
        _indicesList.Add(vertex3.Index);

        //Update vertex normals
        if (VertexNormalsEnabled)
            UpdateVertexNormals(vertex1, vertex2, vertex3);

        return true;
    }

        
    public ILinFloat64Vector3D GetGeometryPoint(int index)
    {
        return _verticesList[index];
    }
        
    public LinFloat64Normal3D GetVertexNormal(int index)
    {
        return _verticesList[index].Normal;
    }

    public void ComputeVertexNormals(bool inverseNormals)
    {
        throw new NotImplementedException();
    }

    public GrTriangleGeometryComposer3D Clear()
    {
        _verticesTable.Clear();
        _verticesList.Clear();
        _indicesList.Clear();

        return this;
    }


    public GrTriangleGeometryComposer3D BeginBatch()
    {
        _verticesTable.Clear();

        AllowDuplicatePoints = false;

        return this;
    }

    public GrTriangleGeometryComposer3D EndBatch()
    {
        _verticesTable.Clear();

        AllowDuplicatePoints = true;

        if (!VertexNormalsEnabled) 
            return this;

        foreach (var vertex in GeometryVertices)
            vertex.Normal.MakeUnit();

        return this;
    }


    public IGraphicsVertex3D AddVertex(double x, double y, double z)
    {
        var pointTriplet = new Triplet<Float64Scalar>(x, y, z);

        IGraphicsVertex3D storedVertex;

        if (AllowDuplicatePoints)
        {
            storedVertex = CreateVertex(pointTriplet);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
            return storedVertex;

        storedVertex = CreateVertex(pointTriplet);
        _verticesList.Add(storedVertex);

        _verticesTable.Add(pointTriplet, storedVertex);

        return storedVertex;
    }

    public IGraphicsVertex3D AddVertex(ILinFloat64Vector3D point)
    {
        var pointTriplet = point.ToTriplet();

        IGraphicsVertex3D storedVertex;

        if (AllowDuplicatePoints)
        {
            storedVertex = CreateVertex(point);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
            return storedVertex;

        storedVertex = CreateVertex(point);
        _verticesList.Add(storedVertex);

        _verticesTable.Add(pointTriplet, storedVertex);

        return storedVertex;
    }

    public IGraphicsVertex3D AddVertex(IGraphicsVertex3D vertex)
    {
        var pointTriplet = vertex.ToTriplet();

        IGraphicsVertex3D storedVertex;

        if (AllowDuplicatePoints)
        {
            storedVertex = CreateVertex(vertex);
            _verticesList.Add(storedVertex);

            return storedVertex;
        }

        if (_verticesTable.TryGetValue(pointTriplet, out storedVertex))
            return storedVertex;

        storedVertex = CreateVertex(vertex);
        _verticesList.Add(storedVertex);

        _verticesTable.Add(pointTriplet, storedVertex);

        return storedVertex;
    }


    public GrTriangleGeometryComposer3D AddVertices(params ILinFloat64Vector3D[] pointsList)
    {
        foreach (var point in pointsList)
            AddVertex(point);

        return this;
    }

    public GrTriangleGeometryComposer3D AddVertices(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        foreach (var point in pointsList)
            AddVertex(point);

        return this;
    }

    public GrTriangleGeometryComposer3D AddVertices(IEnumerable<IGraphicsSurfaceLocalFrame3D> verticesList)
    {
        foreach (var point in verticesList)
            AddVertex(point);

        return this;
    }


    public bool AddTriangle(int vertexIndex1, int vertexIndex2, int vertexIndex3)
    {
        if (vertexIndex1 == vertexIndex2 || vertexIndex2 == vertexIndex3 || vertexIndex3 == vertexIndex1)
            return false;

        return StoreTriangle(
            _verticesList[vertexIndex1], 
            _verticesList[vertexIndex2], 
            _verticesList[vertexIndex3]
        );
    }

    public bool AddTriangle(ITriplet<int> vertexIndices)
    {
        if (vertexIndices.Item1 == vertexIndices.Item2 || vertexIndices.Item2 == vertexIndices.Item3 || vertexIndices.Item3 == vertexIndices.Item1)
            return false;

        return StoreTriangle(
            _verticesList[vertexIndices.Item1], 
            _verticesList[vertexIndices.Item2], 
            _verticesList[vertexIndices.Item3]
        );
    }

    public bool AddTriangle(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2, ILinFloat64Vector3D point3)
    {
        return StoreTriangle(
            AddVertex(point1.X, point1.Y, point1.Z), 
            AddVertex(point2.X, point2.Y, point2.Z), 
            AddVertex(point3.X, point3.Y, point3.Z)
        );
    }

    public bool AddTriangle(ITriplet<ILinFloat64Vector3D> points)
    {
        return StoreTriangle(
            AddVertex(points.Item1.X, points.Item1.Y, points.Item1.Z), 
            AddVertex(points.Item2.X, points.Item2.Y, points.Item2.Z), 
            AddVertex(points.Item3.X, points.Item3.Y, points.Item3.Z)
        );
    }

    public bool AddTriangle(IGraphicsSurfaceLocalFrame3D vertex1, IGraphicsSurfaceLocalFrame3D vertex2, IGraphicsSurfaceLocalFrame3D vertex3)
    {
        return StoreTriangle(
            AddVertex(vertex1), 
            AddVertex(vertex2), 
            AddVertex(vertex3)
        );
    }

    public bool AddTriangle(ITriplet<IGraphicsSurfaceLocalFrame3D> vertices)
    {
        return StoreTriangle(
            AddVertex(vertices.Item1), 
            AddVertex(vertices.Item2), 
            AddVertex(vertices.Item3)
        );
    }

    public bool AddTriangle(IFloat64Triangle3D triangle)
    {
        return StoreTriangle(
            AddVertex(triangle.GetPoint1()), 
            AddVertex(triangle.GetPoint2()), 
            AddVertex(triangle.GetPoint3())
        );
    }


    public GrTriangleGeometryComposer3D AddTriangles(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        foreach (var triangle in trianglesList)
        {
            var vertex1 = AddVertex(triangle.GetPoint1());
            var vertex2 = AddVertex(triangle.GetPoint2());
            var vertex3 = AddVertex(triangle.GetPoint3());

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IEnumerable<ITriplet<int>> triangleIndicesList)
    {
        foreach (var indices in triangleIndicesList)
        {
            var vertex1 = _verticesList[indices.Item1];
            var vertex2 = _verticesList[indices.Item2];
            var vertex3 = _verticesList[indices.Item3];

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IReadOnlyList<int> indicesList)
    {
        if (indicesList.Count % 3 != 0)
            throw new InvalidOperationException();

        for (var i = 0; i < indicesList.Count; i += 3)
        {
            var vertex1 = _verticesList[indicesList[i]];
            var vertex2 = _verticesList[indicesList[i + 1]];
            var vertex3 = _verticesList[indicesList[i + 2]];

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if (pointsList.Count % 3 != 0)
            throw new InvalidOperationException();

        for (var i = 0; i < pointsList.Count; i += 3)
        {
            var vertex1 = AddVertex(pointsList[i]);
            var vertex2 = AddVertex(pointsList[i + 1]);
            var vertex3 = AddVertex(pointsList[i + 2]);

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IEnumerable<ITriplet<ILinFloat64Vector3D>> trianglePointsList)
    {
        foreach (var points in trianglePointsList)
        {
            var vertex1 = AddVertex(points.Item1);
            var vertex2 = AddVertex(points.Item2);
            var vertex3 = AddVertex(points.Item3);

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IReadOnlyList<IGraphicsSurfaceLocalFrame3D> verticesList)
    {
        if (verticesList.Count % 3 != 0)
            throw new InvalidOperationException();

        for (var i = 0; i < verticesList.Count; i += 3)
        {
            var vertex1 = AddVertex(verticesList[i]);
            var vertex2 = AddVertex(verticesList[i + 1]);
            var vertex3 = AddVertex(verticesList[i + 2]);

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }

    public GrTriangleGeometryComposer3D AddTriangles(IEnumerable<ITriplet<IGraphicsSurfaceLocalFrame3D>> triangleVerticesList)
    {
        foreach (var vertices in triangleVerticesList)
        {
            var vertex1 = AddVertex(vertices.Item1);
            var vertex2 = AddVertex(vertices.Item2);
            var vertex3 = AddVertex(vertices.Item3);

            StoreTriangle(vertex1, vertex2, vertex3);
        }

        return this;
    }


    //public GrTriangleGeometry3D GenerateGeometry()
    //{
    //    var geometry = 
    //        GrTriangleGeometry3D.Create(GeometryPoints, _indicesList);
            
    //    if (VertexTextureUVsEnabled)
    //        geometry.SetVertexUVs(VertexTextureUVs.ToArray());

    //    if (VertexColorsEnabled)
    //        geometry.SetVertexColors(VertexColors.ToArray());

    //    if (VertexNormalsEnabled)
    //    {
    //        EndBatch();

    //        geometry.SetVertexNormals(VertexNormals.ToArray());
    //    }

    //    return geometry;
    //}

    public IEnumerator<IFloat64Triangle3D> GetEnumerator()
    {
        for (var i = 0; i < _indicesList.Count; i += 3)
            yield return Float64Triangle3D.Create(
                _verticesList[_indicesList[i]],
                _verticesList[_indicesList[i + 1]],
                _verticesList[_indicesList[i + 2]]
            );
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}