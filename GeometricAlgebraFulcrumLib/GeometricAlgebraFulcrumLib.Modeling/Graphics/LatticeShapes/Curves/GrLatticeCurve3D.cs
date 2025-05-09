using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Curves;

/// <summary>
/// This class describes the shape of a 3D surface using a mapping
/// from a finite set of discrete coordinates (u, v) to 3D points (x, y, z)
/// </summary>
public sealed class GrLatticeCurve3D :
    IReadOnlyList<GrLatticeCurveLocalFrame3D>
{
    private Dictionary<Triplet<Float64Scalar>, GrLatticeCurveLocalFrame3D> _pointToVertexDictionary
        = new Dictionary<Triplet<Float64Scalar>, GrLatticeCurveLocalFrame3D>();

    private GrLatticeCurveLocalFrame3D[] _indexUToVertexArray;


    public GrLatticeCurveList3D ParentList { get; }

    public bool ReverseNormals { get; set; } = false;

    public int CurveIndex { get; }

    public int LatticeSize { get; }

    public int Count 
        => IsReady ? VertexList.Count : _pointToVertexDictionary.Count;
        
    public bool LatticeClosed { get; }

    public IEnumerable<GrLatticeCurveLocalFrame3D> Vertices 
        => IsReady 
            ? VertexList 
            : _pointToVertexDictionary.Values;

    public IEnumerable<LinFloat64Vector3D> VertexPoints 
        => Vertices.Select(v => v.Point);

    public IEnumerable<ILinFloat64Vector3D> VertexNormals 
        => Vertices.Select(v => v.Normal1);

    public IEnumerable<double> VertexTextureUvs 
        => Vertices.Select(v => v.TimeValue);

    public IEnumerable<Color> VertexColors 
        => Vertices.Select(v => v.Color);

    public bool IsReady 
        => VertexList.Count > 0;

    public GrLatticeCurveLocalFrame3D this[int index] 
        => IsReady
            ? VertexList[index]
            : _pointToVertexDictionary.Skip(index).First().Value;

    public GrLatticeCurveLocalFrame3D this[double x, double y, double z] 
        => this[new Triplet<Float64Scalar>(x, y, z)];

    public GrLatticeCurveLocalFrame3D this[ITriplet<Float64Scalar> triplet] 
        => this[triplet.ToTriplet()];

    public GrLatticeCurveLocalFrame3D this[Triplet<Float64Scalar> key] 
        => _pointToVertexDictionary.TryGetValue(key, out var vertex)
            ? vertex
            : throw new KeyNotFoundException();
        
    public IReadOnlyList<GrLatticeCurveLocalFrame3D> VertexList { get; private set; } 
        = Array.Empty<GrLatticeCurveLocalFrame3D>();


    internal GrLatticeCurve3D(GrLatticeCurveList3D parentList, int curveIndex, int countU, bool closeU)
    {
        if (countU < 2)
            throw new ArgumentOutOfRangeException(nameof(countU));
            
        ParentList = parentList;
        CurveIndex = curveIndex;

        LatticeSize = countU;
        LatticeClosed = closeU;

        _indexUToVertexArray = new GrLatticeCurveLocalFrame3D[countU];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D Clear()
    {
        _pointToVertexDictionary.Clear();
        _indexUToVertexArray = new GrLatticeCurveLocalFrame3D[LatticeSize];
        VertexList = Array.Empty<GrLatticeCurveLocalFrame3D>();

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
    public bool TryGetVertex(double x, double y, double z, out GrLatticeCurveLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(new Triplet<Float64Scalar>(x, y, z), out vertex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVertex(ITriplet<Float64Scalar> triplet, out GrLatticeCurveLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(triplet.ToTriplet(), out vertex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVertex(Triplet<Float64Scalar> key, out GrLatticeCurveLocalFrame3D vertex)
    {
        return _pointToVertexDictionary.TryGetValue(key, out vertex);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetVertex(int index)
    {
        return this[index];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetLatticeVertex(int indexU)
    {
        return _indexUToVertexArray[
            indexU.Mod(LatticeSize)
        ];
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetLatticeVertex(IPair<int> indexUvPair)
    {
        return _indexUToVertexArray[
            indexUvPair.Item1.Mod(LatticeSize)
        ];
    }

    public double GetLatticeTextureU(int indexU)
    {
        return indexU / (double)(LatticeSize - 1);
    }

    public LinFloat64Vector3D GetLatticeTangentU(int indexU)
    {
        if (LatticeClosed || (indexU > 0 && indexU < LatticeSize - 1))
        {
            var p1 = GetLatticeVertex(indexU - 1).Point;
            var p2 = GetLatticeVertex(indexU + 1).Point;

            return p2 - p1;
        }
        else if (indexU == 0)
        {
            var p1 = GetLatticeVertex(0).Point;
            var p2 = GetLatticeVertex(1).Point;

            return p2 - p1;
        }
        else // indexU == CountU - 1
        {
            var p1 = GetLatticeVertex(LatticeSize - 2).Point;
            var p2 = GetLatticeVertex(LatticeSize - 1).Point;

            return p2 - p1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetVertex(double x, double y, double z)
    {
        return _pointToVertexDictionary[new Triplet<Float64Scalar>(x, y, z)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetVertex(ITriplet<Float64Scalar> triplet)
    {
        return _pointToVertexDictionary[triplet.ToTriplet()];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D GetVertex(Triplet<Float64Scalar> key)
    {
        return _pointToVertexDictionary[key];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D SetLatticeVertex(int indexU, double x, double y, double z)
    {
        return SetLatticeVertex(indexU, new Triplet<Float64Scalar>(x, y, z));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveLocalFrame3D SetLatticeVertex(int indexU, ITriplet<Float64Scalar> newPointPair)
    {
        return SetLatticeVertex(indexU, newPointPair.ToTriplet());
    }

    public GrLatticeCurveLocalFrame3D SetLatticeVertex(int indexU, Triplet<Float64Scalar> newPointPair)
    {
        if (IsReady)
            throw new InvalidOperationException();

        var oldVertex = _indexUToVertexArray[indexU];

        // Rounding components
        newPointPair = new Triplet<Float64Scalar>(
            Math.Round(newPointPair.Item1, 7),
            Math.Round(newPointPair.Item2, 7),
            Math.Round(newPointPair.Item3, 7)
        );

        if (oldVertex is null)
        {
            // There is no vertex stored at (indexU)
            if (!_pointToVertexDictionary.TryGetValue(newPointPair, out var vertex))
            {
                // The new vertex point is not stored in the set, so add it
                vertex = new GrLatticeCurveLocalFrame3D(this, indexU, newPointPair);

                _pointToVertexDictionary.Add(newPointPair, vertex);
            }

            _indexUToVertexArray[indexU] = vertex;

            return vertex;
        }
        else
        {
            // There is a vertex stored at (indexU)
            var oldPointTriplet = oldVertex.PointTriplet;

            // If the new vertex point is the same as the old vertex point, do nothing
            if (oldPointTriplet == newPointPair)
                return oldVertex;

            // Remove (indexU) from the old vertex data
            oldVertex.LatticeIndexSet.Remove(indexU);

            // If old vertex has no (indexU), remove it from storage
            if (oldVertex.LatticeIndexSet.Count == 0) 
                _pointToVertexDictionary.Remove(oldPointTriplet);

            // Create a new vertex and add it to storage
            var vertex = new GrLatticeCurveLocalFrame3D(this, indexU, newPointPair);

            _pointToVertexDictionary.Add(newPointPair, vertex);

            _indexUToVertexArray[indexU] = vertex;

            return vertex;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D SetLatticeVertices(Func<int, Triplet<Float64Scalar>> pointFactory)
    {
        for (var indexU = 0; indexU < LatticeSize; indexU++)
            SetLatticeVertex(indexU, pointFactory(indexU));

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D SetLatticeVertices(Func<double, Triplet<Float64Scalar>> pointFactory)
    {
        var s = 1d / (LatticeSize - 1);

        for (var indexU = 0; indexU < LatticeSize; indexU++)
            SetLatticeVertex(indexU, pointFactory(s * indexU));

        return this;
    }
        

    private void UpdateInternalVertexKeys()
    {
        if (IsReady)
            throw new InvalidOperationException();

        var pointToVertexDictionary = new Dictionary<Triplet<Float64Scalar>, GrLatticeCurveLocalFrame3D>();

        foreach (var vertex in _pointToVertexDictionary.Values)
        {
            var newKey = vertex.PointTriplet;

            if (!pointToVertexDictionary.TryGetValue(newKey, out var prevVertex))
            {
                pointToVertexDictionary.Add(newKey, vertex);
                continue;
            }
                
            // Merge data of vertex into prevVertex because they have the same point
            foreach (var indexU in vertex.LatticeIndexSet)
            {
                _indexUToVertexArray[indexU] = prevVertex;

                prevVertex.LatticeIndexSet.Add(indexU);
            }
        }

        _pointToVertexDictionary = pointToVertexDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> mappingFunc)
    {
        foreach (var vertex in _pointToVertexDictionary.Values)
            vertex.Point = mappingFunc(vertex.Point);

        UpdateInternalVertexKeys();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D TranslatePointsBy(double dx, double dy, double dz)
    {
        return MapPoints(p => 
            p.TranslateBy(dx, dy, dz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D ScalePointsBy(double s)
    {
        return MapPoints(p => 
            p.ScaleBy(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D ScalePointsBy(double sx, double sy, double sz)
    {
        return MapPoints(p => 
            p.ScaleBy(sx, sy, sz)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D XRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.XRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D YRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.YRotateBy(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D ZRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.ZRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D XRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.XRotateByDegrees(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D YRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.YRotateByDegrees(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D ZRotatePointsByDegrees(double angle)
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

        for (var indexU = 0; indexU < LatticeSize; indexU++)
            if (_indexUToVertexArray[indexU] is null)
                return false;

        return true;
    }

    internal GrLatticeCurve3D FinalizeCurve(int setIndexOffset)
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
            //vertex.CurveIndex = i;
            vertex.Index = setIndexOffset + i;

            i++;
        }

        _pointToVertexDictionary.Clear();

        foreach (var vertex in VertexList)
        {
            //vertex.ComputeLocalFrame();
            vertex.ComputeTextureU();

            // TODO: Compute color values per vertex
            // TODO: Make computations dynamic through specialized classes
        }

        return this;
    }

    private static bool TryAddLine(Dictionary<Pair<int>, Pair<GrLatticeCurveLocalFrame3D>> linesSet, GrLatticeCurveLocalFrame3D vertex1, GrLatticeCurveLocalFrame3D vertex2)
    {
        var index1 = vertex1.Index;
        var index2 = vertex2.Index;

        if (index1 == index2)
            return false;

        Pair<int> indexPair;
        Pair<GrLatticeCurveLocalFrame3D> vertexPair;

        if (index1 < index2)
        {
            indexPair = new Pair<int>(index1, index2);
            vertexPair = new Pair<GrLatticeCurveLocalFrame3D>(vertex1, vertex2);
        }
        else
        {
            indexPair = new Pair<int>(index2, index1);
            vertexPair = new Pair<GrLatticeCurveLocalFrame3D>(vertex2, vertex1);
        }
            
        return linesSet.TryAdd(indexPair, vertexPair);
    }

    public IEnumerable<Pair<GrLatticeCurveLocalFrame3D>> GetVertexLines()
    {
        if (!IsReady)
            throw new InvalidOperationException();

        var linesSet = 
            new Dictionary<Pair<int>, Pair<GrLatticeCurveLocalFrame3D>>();

        var countU = LatticeClosed ? LatticeSize + 1 : LatticeSize;

        for (var indexU = 1; indexU < countU; indexU++)
        {
            var u1 = indexU - 1;
            var u2 = indexU.Mod(LatticeSize);

            var vertex1 = _indexUToVertexArray[u1];
            var vertex2 = _indexUToVertexArray[u2];

            if (ParentList.ReverseNormals)
                TryAddLine(linesSet, vertex2, vertex1);
            else
                TryAddLine(linesSet, vertex1, vertex2);
        }

        return linesSet.Values;
    }
        
    public IEnumerable<Pair<int>> GetIndexLines()
    {
        if (!IsReady)
            throw new InvalidOperationException();
            
        var linesSet = 
            new Dictionary<Pair<int>, Pair<GrLatticeCurveLocalFrame3D>>();

        var countU = LatticeClosed ? LatticeSize + 1 : LatticeSize;

        for (var indexU = 1; indexU < countU; indexU++)
        {
            var u1 = indexU - 1;
            var u2 = indexU.Mod(LatticeSize);
                
            var vertex1 = _indexUToVertexArray[u1];
            var vertex2 = _indexUToVertexArray[u2];
                
            if (ParentList.ReverseNormals)
                TryAddLine(linesSet, vertex2, vertex1);
            else
                TryAddLine(linesSet, vertex1, vertex2);
        }

        return linesSet.Keys;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<GrLatticeCurveLocalFrame3D> GetEnumerator()
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