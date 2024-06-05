using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.LatticeShapes.Curves;

public sealed class GrLatticeCurveList3D :
    IReadOnlyList<GrLatticeCurveLocalFrame3D>
{
    private readonly List<GrLatticeCurve3D> _gridsList
        = new List<GrLatticeCurve3D>();


    public int Count 
        => IsReady
            ? _gridsList.Select(b => b.Count).Sum()
            : VertexList.Count;
        
    public bool VertexNormalsEnabled { get; set; }

    public bool VertexTextureUsEnabled { get; set; }

    public bool VertexColorsEnabled { get; set; }

    public IReadOnlyList<GrLatticeCurveLocalFrame3D> VertexList { get; private set; } 
        = Array.Empty<GrLatticeCurveLocalFrame3D>();
        
    public IEnumerable<GrLatticeCurveLocalFrame3D> Vertices 
        => IsReady
            ? VertexList
            : _gridsList.SelectMany(b => b.Vertices);
        
    public IEnumerable<ILinFloat64Vector3D> VertexPoints 
        => Vertices.Select(v => v.Point);

    public IEnumerable<ILinFloat64Vector3D> VertexNormals 
        => Vertices.Select(v => v.Normal1);

    public IEnumerable<double> VertexTextureUs 
        => Vertices.Select(v => v.ParameterValue.ScalarValue);

    public IEnumerable<Color> VertexColors 
        => Vertices.Select(v => v.Color);

    public IReadOnlyList<Pair<GrLatticeCurveLocalFrame3D>> LineVerticesList { get; private set; } 
        = Array.Empty<Pair<GrLatticeCurveLocalFrame3D>>();

    public IEnumerable<Pair<ILinFloat64Vector3D>> LineVertexPoints
        => LineVerticesList.Select(t => 
            new Pair<ILinFloat64Vector3D>(
                t.Item1.Point,
                t.Item2.Point
            )
        );

    public IEnumerable<Pair<int>> LineVertexIndices 
        => LineVerticesList.Select(t => 
            new Pair<int>(
                t.Item1.Index,
                t.Item2.Index
            )
        );

    public bool IsReady 
        => VertexList.Count > 0;

    public GrLatticeCurveLocalFrame3D this[int index] 
        => VertexList[index];

    public int CurveCount 
        => _gridsList.Count;

    public IEnumerable<GrLatticeCurve3D> Curvees 
        => _gridsList;

    public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
        = GrVertexNormalComputationMethod.WeightedNormals;

    public bool ReverseNormals { get; set; } = false;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D Clear()
    {
        foreach (var curve in _gridsList)
            curve.Clear();

        _gridsList.Clear();
        VertexList = Array.Empty<GrLatticeCurveLocalFrame3D>();
        LineVerticesList = Array.Empty<Pair<GrLatticeCurveLocalFrame3D>>();

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D GetCurve(int curveIndex)
    {
        return _gridsList[curveIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurve3D AddCurve(int countU, bool closeU)
    {
        var curve = new GrLatticeCurve3D(
            this, 
            _gridsList.Count,
            countU, 
            closeU
        );

        _gridsList.Add(curve);

        return curve;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D MapPoints(Func<LinFloat64Vector3D, LinFloat64Vector3D> mappingFunc)
    {
        foreach (var curve in _gridsList)
            curve.MapPoints(mappingFunc);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D TranslatePointsBy(double dx, double dy, double dz)
    {
        return MapPoints(p => 
            p.TranslateBy(dx, dy, dz)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D ScalePointsBy(double s)
    {
        return MapPoints(p => 
            p.ScaleBy(s)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D ScalePointsBy(double sx, double sy, double sz)
    {
        return MapPoints(p => 
            p.ScaleBy(sx, sy, sz)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D XRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.XRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D YRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.YRotateBy(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D ZRotatePointsBy(LinFloat64Angle angle)
    {
        return MapPoints(p => 
            p.ZRotateBy(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D XRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.XRotateByDegrees(angle)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D YRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.YRotateByDegrees(angle)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GrLatticeCurveList3D ZRotatePointsByDegrees(double angle)
    {
        return MapPoints(p => 
            p.ZRotateByDegrees(angle)
        );
    }


    //private void ComputeAverageNormals()
    //{
    //    foreach (var (vertex1, vertex2) in LineVerticesList)
    //    {
    //        //Find line normal, not unit but full normal vector
    //        var normal = vertex1.ParentCurve.ReverseNormals
    //            ? VectorAlgebraUtils.GetLineNormal(vertex3, vertex2, vertex1)
    //            : VectorAlgebraUtils.GetLineNormal(vertex1, vertex2, vertex3);

    //        //For debugging only
    //        Debug.Assert(!normal.IsInvalid);

    //        vertex1.Normal.Update(normal);
    //        vertex2.Normal.Update(normal);
    //        vertex3.Normal.Update(normal);
    //    }
    //}

    //private void ComputeAverageUnitNormals()
    //{
    //    foreach (var (vertex1, vertex2) in LineVerticesList)
    //    {
    //        //Find line unit normal
    //        var normal = vertex1.ParentCurve.ReverseNormals
    //            ? VectorAlgebraUtils.GetLineUnitNormal(vertex3, vertex2, vertex1)
    //            : VectorAlgebraUtils.GetLineUnitNormal(vertex1, vertex2, vertex3);
                
    //        //For debugging only
    //        Debug.Assert(!normal.IsInvalid);

    //        vertex1.Normal.Update(normal);
    //        vertex2.Normal.Update(normal);
    //        vertex3.Normal.Update(normal);
    //    }
    //}

    //private void ComputeWeightedNormals()
    //{
    //    foreach (var (vertex1, vertex2) in LineVerticesList)
    //    {
    //        //Find inner angles of line
    //        var angle1 = vertex1.GetPointsAngle(vertex3, vertex2);
    //        var angle2 = vertex2.GetPointsAngle(vertex1, vertex3);
    //        var angle3 = vertex3.GetPointsAngle(vertex2, vertex1);

    //        //Find line normal, not unit but full normal vector
    //        var normal = vertex1.ParentCurve.ReverseNormals
    //            ? VectorAlgebraUtils.GetLineNormal(vertex3, vertex2, vertex1)
    //            : VectorAlgebraUtils.GetLineNormal(vertex1, vertex2, vertex3);

    //        // For debugging only
    //        Debug.Assert(
    //            !double.IsNaN(angle1) &&
    //            !double.IsNaN(angle2) &&
    //            !double.IsNaN(angle3) &&
    //            normal.IsValid
    //        );

    //        // Update normals of line vertices.
    //        // See here for more information:
    //        // http://www.bytehazard.com/articles/vertnorm.html
    //        // normal.MakeUnitVector();
    //        vertex1.Normal.Update(angle1 * normal);
    //        vertex2.Normal.Update(angle2 * normal);
    //        vertex3.Normal.Update(angle3 * normal);
    //    }
    //}

    public GrLatticeCurveList3D FinalizeSet()
    {
        if (IsReady) 
            return this;

        var vertexList = new List<GrLatticeCurveLocalFrame3D>();

        var setIndexOffset = 0;
        foreach (var curve in _gridsList)
        {
            curve.FinalizeCurve(setIndexOffset);

            vertexList.AddRange(curve.VertexList);

            setIndexOffset += curve.Count;
        }

        VertexList = vertexList;

        LineVerticesList = _gridsList
            .SelectMany(b => b.GetVertexLines())
            .ToArray();

        // Compute vertex normals
        foreach (var vertex in VertexList)
            vertex.Normal1.Reset();

        //switch (NormalComputationMethod)
        //{
        //    case GrVertexNormalComputationMethod.AverageNormals:
        //        ComputeAverageNormals();
        //        break;

        //    case GrVertexNormalComputationMethod.AverageUnitNormals:
        //        ComputeAverageUnitNormals();
        //        break;

        //    case GrVertexNormalComputationMethod.WeightedNormals:
        //        ComputeWeightedNormals();
        //        break;
        //}

        foreach (var vertex in VertexList)
            vertex.Normal1.MakeUnit();

        return this;
    }

    //public GrLatticeCurveLineGeometry3D GetLineGeometry()
    //{
    //    return new GrLatticeCurveLineGeometry3D(this);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<GrLatticeCurveLocalFrame3D> GetEnumerator()
    {
        return IsReady
            ? VertexList.GetEnumerator()
            : _gridsList.SelectMany(b => b.Vertices).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        var composer = new LinearTextComposer();

        foreach (var curve in Curvees)
        {
            composer
                .AppendLine($"Curve <{curve.CurveIndex.ToString().PadLeft(3)}>:")
                .IncreaseIndentation();

            foreach (var vertex in curve.VertexList)
            {
                var c = vertex.Color.ToPixel<Rgb24>();

                composer
                    .AppendAtNewLine($"Vertex <{vertex.Index,4}>:")
                    .Append($" Point({vertex.X:F5}, {vertex.Y:F5}, {vertex.Z:F5})")
                    .Append($" Normal({vertex.Normal1.X:F5}, {vertex.Normal1.Y:F5}, {vertex.Normal1.Z:F5})")
                    .Append($" TextureU({vertex.ParameterValue:F5})")
                    .Append($" Color({c.R}, {c.G}, {c.B})");
            }

            composer.AppendLineAtNewLine();

            composer
                .DecreaseIndentation()
                .AppendAtNewLine();
        }

        composer
            .AppendLineAtNewLine($"Lines:")
            .IncreaseIndentation();

        foreach (var (i1, i2) in LineVertexIndices)
        {
            composer.AppendAtNewLine($"<{i1.ToString().PadLeft(4)}, {i2.ToString().PadLeft(4)}>");
        }
            
        composer
            .DecreaseIndentation()
            .AppendAtNewLine();

        return composer.ToString();
    }
}