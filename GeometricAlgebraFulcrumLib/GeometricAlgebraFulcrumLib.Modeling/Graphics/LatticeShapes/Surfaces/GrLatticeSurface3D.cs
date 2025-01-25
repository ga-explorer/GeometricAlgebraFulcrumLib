using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Surfaces;

/// <summary>
/// This class describes the shape of a 3D surface using a mapping
/// from a finite set of discrete coordinates (u, v) to 3D points (x, y, z)
/// </summary>
public sealed class GrLatticeSurface3D :
    IReadOnlyList<GrLatticeSurfaceLocalFrame3D>
{
    private Dictionary<Triplet<Float64Scalar>, GrLatticeSurfaceLocalFrame3D> _pointToVertexDictionary
        = new Dictionary<Triplet<Float64Scalar>, GrLatticeSurfaceLocalFrame3D>();

    private GrLatticeSurfaceLocalFrame3D[,] _indexUvToVertexArray;


    public GrLatticeSurfaceList3D ParentList { get; }

    public bool ReverseNormals { get; set; } = false;

    public int CurveIndex { get; }

    public int LatticeSizeU { get; }

    public int LatticeSizeV { get; }

    public int Count 
        => IsReady ? VertexList.Count : _pointToVertexDictionary.Count;
        
    public bool LatticeClosedU { get; }

    public bool LatticeClosedV { get; }

    public IEnumerable<GrLatticeSurfaceLocalFrame3D> Vertices 
        => IsReady 
            ? VertexList 
            : _pointToVertexDictionary.Values;

    public IEnumerable<LinFloat64Vector3D> VertexPoints 
        => Vertices.Select(v => v.Point);

    public IEnumerable<ILinFloat64Vector3D> VertexNormals 
        => Vertices.Select(v => v.Normal);

    public IEnumerable<LinFloat64Vector2D> VertexTextureUvs 
        => Vertices.Select(v => v.ParameterValue);

    public IEnumerable<Color> VertexColors 
        => Vertices.Select(v => v.Color);

    public bool IsReady 
        => VertexList.Count > 0;

    public GrLatticeSurfaceLocalFrame3D this[int index] 
        => IsReady
            ? VertexList[index]
            : _pointToVertexDictionary.Skip(index).First().Value;

    public GrLatticeSurfaceLocalFrame3D this[double x, double y, double z] 
        => this[new Triplet<Float64Scalar>(x, y, z)];

    public GrLatticeSurfaceLocalFrame3D this[ITriplet<Float64Scalar> triplet] 
        => this[triplet.ToTriplet()];

    public GrLatticeSurfaceLocalFrame3D this[Triplet<Float64Scalar> key] 
        => _pointToVertexDictionary.TryGetValue(key, out var vertex)
            ? vertex
            : throw new KeyNotFoundException();
        
    public IReadOnlyList<GrLatticeSurfaceLocalFrame3D> VertexList { get; private set; } 
        = Array.Empty<GrLatticeSurfaceLocalFrame3D>();


    internal GrLatticeSurface3D(GrLatticeSurfaceList3D parentList, int batchIndex, int countU, int countV, bool closeU, bool closeV)
    {
        if (countU < 2)
            throw new ArgumentOutOfRangeException(nameof(countU));

        if (countV < 2)
            throw new ArgumentOutOfRangeException(nameof(countV));

        ParentList = parentList;
        CurveIndex = batchIndex;

        LatticeSizeU = countU;
        LatticeSizeV = countV;

        LatticeClosedU = closeU;
        LatticeClosedV = closeV;

        _indexUvToVertexArray = new GrLatticeSurfaceLocalFrame3D[countU, countV];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D Clear()
    {
        _pointToVertexDictionary.Clear();
        _indexUvToVertexArray = new GrLatticeSurfaceLocalFrame3D[LatticeSizeU, LatticeSizeV];
        VertexList = Array.Empty<GrLatticeSurfaceLocalFrame3D>();

        return this;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(double x, double y, double z)
    {
        var key = new Triplet<Float64Scalar>(x, y, z);

        return _pointToVertexDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(ITriplet<Float64Scalar> triplet)
    {
        return _pointToVertexDictionary.ContainsKey(triplet.ToTriplet());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsPoint(Triplet<Float64Scalar> key)
    {
        return _pointToVertexDictionary.ContainsKey(key);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVertex(double x, double y, double z, out GrLatticeSurfaceLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(new Triplet<Float64Scalar>(x, y, z), out vertex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVertex(ITriplet<Float64Scalar> triplet, out GrLatticeSurfaceLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(triplet.ToTriplet(), out vertex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVertex(Triplet<Float64Scalar> key, out GrLatticeSurfaceLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(key, out vertex);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetVertex(int index)
    {
        return this[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetLatticeVertex(int indexU, int indexV)
    {
        return _indexUvToVertexArray[
            indexU.Mod(LatticeSizeU), 
            indexV.Mod(LatticeSizeV)
        ];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetLatticeVertex(IPair<int> indexUvPair)
    {
        return _indexUvToVertexArray[
            indexUvPair.Item1.Mod(LatticeSizeU), 
            indexUvPair.Item2.Mod(LatticeSizeV)
        ];
    }

    public LinFloat64Vector2D GetLatticeTextureUv(int indexU, int indexV)
    {
        return LinFloat64Vector2D.Create((Float64Scalar)(indexU / (double)(LatticeSizeU - 1)),
            (Float64Scalar)(indexV / (double)(LatticeSizeV - 1)));
    }

    public LinFloat64Vector3D GetLatticeTangentU(int indexU, int indexV)
    {
        if (LatticeClosedU || (indexU > 0 && indexU < LatticeSizeU - 1))
        {
            var p1 = GetLatticeVertex(indexU - 1, indexV).Point;
            var p2 = GetLatticeVertex(indexU + 1, indexV).Point;

            return p2 - p1;
        }
        else if (indexU == 0)
        {
            var p1 = GetLatticeVertex(0, indexV).Point;
            var p2 = GetLatticeVertex(1, indexV).Point;

            return p2 - p1;
        }
        else // indexU == CountU - 1
        {
            var p1 = GetLatticeVertex(LatticeSizeU - 2, indexV).Point;
            var p2 = GetLatticeVertex(LatticeSizeU - 1, indexV).Point;

            return p2 - p1;
        }
    }
        
    public LinFloat64Vector3D GetLatticeTangentV(int indexU, int indexV)
    {
        if (LatticeClosedV || (indexV > 0 && indexV < LatticeSizeV - 1))
        {
            var p1 = GetLatticeVertex(indexU, indexV - 1).Point;
            var p2 = GetLatticeVertex(indexU, indexV + 1).Point;

            return p2 - p1;
        }
        else if (indexV == 0)
        {
            var p1 = GetLatticeVertex(indexU, 0).Point;
            var p2 = GetLatticeVertex(indexU, 1).Point;

            return p2 - p1;
        }
        else // indexV == CountV - 1
        {
            var p1 = GetLatticeVertex(indexU, LatticeSizeV - 2).Point;
            var p2 = GetLatticeVertex(indexU, LatticeSizeV - 1).Point;

            return p2 - p1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetVertex(double x, double y, double z)
    {
        return _pointToVertexDictionary[new Triplet<Float64Scalar>(x, y, z)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetVertex(ITriplet<Float64Scalar> triplet)
    {
        return _pointToVertexDictionary[triplet.ToTriplet()];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D GetVertex(Triplet<Float64Scalar> key)
    {
        return _pointToVertexDictionary[key];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D SetLatticeVertex(int indexU, int indexV, double x, double y, double z)
    {
        return SetLatticeVertex(indexU, indexV, new Triplet<Float64Scalar>(x, y, z));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D SetLatticeVertex(int indexU, int indexV, ITriplet<Float64Scalar> newPointTriplet)
    {
        return SetLatticeVertex(indexU, indexV, newPointTriplet.ToTriplet());
    }

    public GrLatticeSurfaceLocalFrame3D SetLatticeVertex(int indexU, int indexV, Triplet<Float64Scalar> newPointTriplet)
    {
        if (IsReady)
            throw new InvalidOperationException();

        var uvIndexPair = new Pair<int>(indexU, indexV);
        var oldVertex = _indexUvToVertexArray[indexU, indexV];

        // Rounding components
        newPointTriplet = new Triplet<Float64Scalar>(
            Math.Round(newPointTriplet.Item1, 7),
            Math.Round(newPointTriplet.Item2, 7),
            Math.Round(newPointTriplet.Item3, 7)
        );

        if (oldVertex is null)
        {
            // There is no vertex stored at (indexU, indexV)
            if (!_pointToVertexDictionary.TryGetValue(newPointTriplet, out var vertex))
            {
                // The new vertex point is not stored in the set, so add it
                vertex = new GrLatticeSurfaceLocalFrame3D(this, uvIndexPair, newPointTriplet);

                _pointToVertexDictionary.Add(newPointTriplet, vertex);
            }

            _indexUvToVertexArray[indexU, indexV] = vertex;

            return vertex;
        }
        else
        {
            // There is a vertex stored at (indexU, indexV)
            var oldPointTriplet = oldVertex.PointTriplet;

            // If the new vertex point is the same as the old vertex point, do nothing
            if (oldPointTriplet == newPointTriplet)
                return oldVertex;

            // Remove (indexU, indexV) from the old vertex data
            oldVertex.LatticeIndexSet.Remove(uvIndexPair);

            // If old vertex has no (indexU, indexV), remove it from storage
            if (oldVertex.LatticeIndexSet.Count == 0) 
                _pointToVertexDictionary.Remove(oldPointTriplet);

            // Create a new vertex and add it to storage
            var vertex = new GrLatticeSurfaceLocalFrame3D(this, uvIndexPair, newPointTriplet);

            _pointToVertexDictionary.Add(newPointTriplet, vertex);

            _indexUvToVertexArray[indexU, indexV] = vertex;

            return vertex;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D SetLatticeVerticesAtIndexU(int indexU, Triplet<Float64Scalar> newPointTriplet)
    {
        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            SetLatticeVertex(indexU, indexV, newPointTriplet);

        return _pointToVertexDictionary[newPointTriplet];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurfaceLocalFrame3D SetLatticeVerticesAtIndexV(int indexV, Triplet<Float64Scalar> newPointTriplet)
    {
        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
            SetLatticeVertex(indexU, indexV, newPointTriplet);

        return _pointToVertexDictionary[newPointTriplet];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVerticesAtIndexU(int indexU, Func<int, Triplet<Float64Scalar>> pointFactory)
    {
        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            SetLatticeVertex(indexU, indexV, pointFactory(indexV));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVerticesAtIndexV(int indexV, Func<int, Triplet<Float64Scalar>> pointFactory)
    {
        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
            SetLatticeVertex(indexU, indexV, pointFactory(indexU));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVerticesAtIndexU(int indexU, Func<double, Triplet<Float64Scalar>> pointFactory)
    {
        var s = 1d / (LatticeSizeV - 1);

        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            SetLatticeVertex(indexU, indexV, pointFactory(s * indexV));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVerticesAtIndexV(int indexV, Func<double, Triplet<Float64Scalar>> pointFactory)
    {
        var s = 1d / (LatticeSizeU - 1);

        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
            SetLatticeVertex(indexU, indexV, pointFactory(s * indexU));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVertices(Func<int, int, Triplet<Float64Scalar>> pointFactory)
    {
        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            SetLatticeVertex(indexU, indexV, pointFactory(indexU, indexV));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D SetLatticeVertices(Func<double, double, Triplet<Float64Scalar>> pointFactory)
    {
        var sU = 1d / (LatticeSizeU - 1);
        var sV = 1d / (LatticeSizeV - 1);

        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            SetLatticeVertex(indexU, indexV, pointFactory(sU * indexU, sV * indexV));

        return this;
    }


    private void UpdateInternalVertexKeys()
    {
        if (IsReady)
            throw new InvalidOperationException();

        var pointToVertexDictionary = new Dictionary<Triplet<Float64Scalar>, GrLatticeSurfaceLocalFrame3D>();

        foreach (var vertex in _pointToVertexDictionary.Values)
        {
            var newKey = vertex.PointTriplet;

            if (!pointToVertexDictionary.TryGetValue(newKey, out var prevVertex))
            {
                pointToVertexDictionary.Add(newKey, vertex);
                continue;
            }
                
            // Merge data of vertex into prevVertex because they have the same point
            foreach (var uvIndexPair in vertex.LatticeIndexSet)
            {
                _indexUvToVertexArray[uvIndexPair.Item1, uvIndexPair.Item2] = prevVertex;

                prevVertex.LatticeIndexSet.Add(uvIndexPair);
            }
        }

        _pointToVertexDictionary = pointToVertexDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> mappingFunc)
    {
        foreach (var vertex in _pointToVertexDictionary.Values)
            vertex.Point = mappingFunc(vertex.Point);

        UpdateInternalVertexKeys();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D TranslatePointsBy(double dx, double dy, double dz)
    {
        return MapPoints(p => 
            p.TranslateBy(dx, dy, dz)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D TranslatePointsBy(ILinFloat64Vector3D translationVector)
    {
        return MapPoints(p => 
            p.TranslateBy(translationVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D ScalePointsBy(double s)
    {
        return MapPoints(p => 
            p.ScaleBy(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D ScalePointsBy(double sx, double sy, double sz)
    {
        return MapPoints(p => 
            p.ScaleBy(sx, sy, sz)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D XRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.XRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D YRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.YRotateBy(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D ZRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.ZRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D XRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.XRotateByDegrees(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D YRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.YRotateByDegrees(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeSurface3D ZRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.ZRotateByDegrees(angle)
        );
    }


    private bool IsInternalDataValid()
    {
        Debug.Assert(
            _pointToVertexDictionary.Values.All(vertex => vertex.LatticeIndexSet.Count > 0)
        );

        for (var indexU = 0; indexU < LatticeSizeU; indexU++)
        for (var indexV = 0; indexV < LatticeSizeV; indexV++)
            if (_indexUvToVertexArray[indexU, indexV] is null)
                return false;

        return true;
    }

    internal GrLatticeSurface3D FinalizeSurface(int setIndexOffset)
    {
        if (IsReady) 
            return this;

        // Validate internal data
        if (!IsInternalDataValid())
            throw new InvalidOperationException();

        VertexList = _pointToVertexDictionary.Values.ToArray();

        var i = 0;
        foreach (var vertex in VertexList)
        {
            //vertex.BatchIndex = i;
            vertex.Index = setIndexOffset + i;

            i++;
        }

        _pointToVertexDictionary.Clear();

        foreach (var vertex in VertexList)
        {
            //vertex.ComputeLocalFrame();
            vertex.ComputeTextureUv();

            // TODO: Compute color values per vertex
            // TODO: Make computations dynamic through specialized classes
        }

        return this;
    }

    public IEnumerable<Quad<GrLatticeSurfaceLocalFrame3D>> GetVertexQuads()
    {
        if (!IsReady)
            throw new InvalidOperationException();

        var countU = LatticeClosedU ? LatticeSizeU + 1 : LatticeSizeU;
        var countV = LatticeClosedV ? LatticeSizeV + 1 : LatticeSizeV;

        for (var indexU = 1; indexU < countU; indexU++)
        {
            var u1 = indexU - 1;
            var u2 = indexU.Mod(LatticeSizeU);

            for (var indexV = 1; indexV < countV; indexV++)
            {
                var v1 = indexV - 1;
                var v2 = indexV % LatticeSizeV;

                yield return new Quad<GrLatticeSurfaceLocalFrame3D>(
                    _indexUvToVertexArray[u1, v1],
                    _indexUvToVertexArray[u1, v2],
                    _indexUvToVertexArray[u2, v1],
                    _indexUvToVertexArray[u2, v2]
                );
            }
        }
    }


    private static bool TryAddTriangle(Dictionary<Triplet<int>, Triplet<GrLatticeSurfaceLocalFrame3D>> trianglesSet, GrLatticeSurfaceLocalFrame3D vertex1, GrLatticeSurfaceLocalFrame3D vertex2, GrLatticeSurfaceLocalFrame3D vertex3)
    {
        var index1 = vertex1.Index;
        var index2 = vertex2.Index;
        var index3 = vertex3.Index;

        if (index1 == index2 || index2 == index3 || index3 == index1)
            return false;

        Triplet<int> indexTriplet;
        Triplet<GrLatticeSurfaceLocalFrame3D> vertexTriplet;

        if (index1 < index2 && index1 < index3)
        {
            indexTriplet = new Triplet<int>(index1, index2, index3);
            vertexTriplet = new Triplet<GrLatticeSurfaceLocalFrame3D>(vertex1, vertex2, vertex3);
        }
        else if (index2 < index1 && index2 < index3)
        {
            indexTriplet = new Triplet<int>(index2, index3, index1);
            vertexTriplet = new Triplet<GrLatticeSurfaceLocalFrame3D>(vertex2, vertex3, vertex1);
        }
        else
        {
            indexTriplet = new Triplet<int>(index3, index1, index2);
            vertexTriplet = new Triplet<GrLatticeSurfaceLocalFrame3D>(vertex3, vertex1, vertex2);
        }
            
        if (trianglesSet.ContainsKey(indexTriplet))
            return false;

        trianglesSet.Add(indexTriplet, vertexTriplet);

        return true;
    }

    public IEnumerable<Triplet<GrLatticeSurfaceLocalFrame3D>> GetVertexTriangles()
    {
        if (!IsReady)
            throw new InvalidOperationException();

        var trianglesSet = 
            new Dictionary<Triplet<int>, Triplet<GrLatticeSurfaceLocalFrame3D>>();

        var countU = LatticeClosedU ? LatticeSizeU + 1 : LatticeSizeU;
        var countV = LatticeClosedV ? LatticeSizeV + 1 : LatticeSizeV;

        for (var indexU = 1; indexU < countU; indexU++)
        {
            var u1 = indexU - 1;
            var u2 = indexU.Mod(LatticeSizeU);

            for (var indexV = 1; indexV < countV; indexV++)
            {
                var v1 = indexV - 1;
                var v2 = indexV % LatticeSizeV;

                var vertex1 = _indexUvToVertexArray[u1, v1];
                var vertex2 = _indexUvToVertexArray[u1, v2];
                var vertex3 = _indexUvToVertexArray[u2, v1];
                var vertex4 = _indexUvToVertexArray[u2, v2];

                if (ParentList.ReverseNormals)
                {
                    TryAddTriangle(trianglesSet, vertex3, vertex1, vertex4);
                    TryAddTriangle(trianglesSet, vertex2, vertex4, vertex1);
                }
                else
                {
                    TryAddTriangle(trianglesSet, vertex3, vertex4, vertex1);
                    TryAddTriangle(trianglesSet, vertex2, vertex1, vertex4);
                }
            }
        }

        return trianglesSet.Values;
    }
        
    public IEnumerable<Triplet<int>> GetIndexTriangles()
    {
        if (!IsReady)
            throw new InvalidOperationException();
            
        var trianglesSet = 
            new Dictionary<Triplet<int>, Triplet<GrLatticeSurfaceLocalFrame3D>>();

        var countU = LatticeClosedU ? LatticeSizeU + 1 : LatticeSizeU;
        var countV = LatticeClosedV ? LatticeSizeV + 1 : LatticeSizeV;

        for (var indexU = 1; indexU < countU; indexU++)
        {
            var u1 = indexU - 1;
            var u2 = indexU.Mod(LatticeSizeU);

            for (var indexV = 1; indexV < countV; indexV++)
            {
                var v1 = indexV - 1;
                var v2 = indexV % LatticeSizeV;

                var vertex1 = _indexUvToVertexArray[u1, v1];
                var vertex2 = _indexUvToVertexArray[u1, v2];
                var vertex3 = _indexUvToVertexArray[u2, v1];
                var vertex4 = _indexUvToVertexArray[u2, v2];
                    
                if (ParentList.ReverseNormals)
                {
                    TryAddTriangle(trianglesSet, vertex3, vertex1, vertex4);
                    TryAddTriangle(trianglesSet, vertex2, vertex4, vertex1);
                }
                else
                {
                    TryAddTriangle(trianglesSet, vertex3, vertex4, vertex1);
                    TryAddTriangle(trianglesSet, vertex2, vertex1, vertex4);
                }
                //yield return new Triplet<int>(vertex3, vertex1, vertex4);
                //yield return new Triplet<int>(vertex2, vertex4, vertex1);
            }
        }

        return trianglesSet.Keys;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<GrLatticeSurfaceLocalFrame3D> GetEnumerator()
    {
        return IsReady
            ? VertexList.GetEnumerator()
            : _pointToVertexDictionary.Values.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}