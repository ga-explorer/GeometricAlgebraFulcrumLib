using System.Collections;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;

public abstract class GrTriangleGeometryBase3D 
    : IGraphicsTriangleGeometry3D
{
    public abstract GraphicsPrimitiveType3D PrimitiveType { get; }

    public int VertexCount 
        => _vertexPoints.Count;

    public IEnumerable<IGraphicsVertex3D> GeometryVertices
    {
        get
        {
            if (VertexNormalsEnabled)
            {
                if (VertexTextureUVsEnabled)
                {
                    return GeometryPoints.Select(
                        (p, i) => new GrNormalTextureVertex3D(i, p, _vertexUVs[i], _vertexNormals[i])
                    );
                }
                else
                {
                    return GeometryPoints.Select(
                        (p, i) => new GrNormalVertex3D(i, p, _vertexNormals[i])
                    );
                }
            }
            else
            {
                if (VertexTextureUVsEnabled)
                {
                    return GeometryPoints.Select(
                        (p, i) => new GrTextureVertex3D(i, p, _vertexUVs[i])
                    );
                }
                else
                {
                    return GeometryPoints.Select(
                        (p, i) => new GrVertex3D(i, p)
                    );
                }
            }
        }
    }
            

    private readonly IReadOnlyList<ILinFloat64Vector3D> _vertexPoints;
    public IEnumerable<ILinFloat64Vector3D> GeometryPoints 
        => _vertexPoints;

    public abstract IEnumerable<int> GeometryIndices { get; }

    public abstract int Count { get; }

    public abstract ITriangle3D this[int index] { get; }

    public abstract IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints { get; }

    public abstract IEnumerable<Triplet<int>> TriangleVertexIndices { get; }

    public LinFloat64Normal3D GetVertexNormal(int index)
    {
        return _vertexNormals[index];
    }

    public GrVertexNormalComputationMethod NormalComputationMethod { get; set; }
        = GrVertexNormalComputationMethod.AverageNormals;

    private LinFloat64Normal3D[] _vertexNormals;
    public IEnumerable<ILinFloat64Vector3D> VertexNormals 
        => _vertexNormals;

    private ILinFloat64Vector2D[] _vertexUVs;
    public IEnumerable<ILinFloat64Vector2D> VertexTextureUVs
        => _vertexUVs;

    private Color[] _vertexColors;
    public IEnumerable<Color> VertexColors 
        => _vertexColors;

    public bool VertexNormalsEnabled
        => !ReferenceEquals(_vertexNormals, null);

    public bool VertexTextureUVsEnabled
        => !ReferenceEquals(_vertexUVs, null);

    public bool VertexColorsEnabled
        => !ReferenceEquals(_vertexColors, null);

    public bool AutoVertexNormals { get; set; }


    protected GrTriangleGeometryBase3D(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        _vertexPoints = pointsList;
    }

        
    public ILinFloat64Vector3D GetGeometryPoint(int index)
    {
        return _vertexPoints[index];
    }

    public void ClearVertexNormals()
    {
        _vertexNormals = null;
    }

    public void ClearVertexUVs()
    {
        _vertexUVs = null;
    }

    public void ClearVertexColors()
    {
        _vertexColors = null;
    }

    public void ClearVertexData()
    {
        _vertexNormals = null;
        _vertexUVs = null;
        _vertexColors = null;
    }

    public void SetVertexNormals(LinFloat64Normal3D[] vertexNormals)
    {
        if (vertexNormals.Length != _vertexPoints.Count)
            throw new InvalidOperationException();

        _vertexNormals = vertexNormals;
    }

    public void SetVertexUVs(ILinFloat64Vector2D[] vertexUVs)
    {
        if (vertexUVs.Length != _vertexPoints.Count)
            throw new InvalidOperationException();

        _vertexUVs = vertexUVs;
    }

    public void SetVertexColors(Color[] vertexColors)
    {
        if (vertexColors.Length != _vertexPoints.Count)
            throw new InvalidOperationException();

        _vertexColors = vertexColors;
    }

    public void ReverseNormals()
    {
        if (ReferenceEquals(_vertexNormals, null))
            return;

        foreach (var normal in _vertexNormals)
            normal.MakeNegative();
    }

        
    private void UpdateVertexNormals(int vertexIndex1, int vertexIndex2, int vertexIndex3)
    {
        var vertex1 = _vertexPoints[vertexIndex1];
        var vertex2 = _vertexPoints[vertexIndex2];
        var vertex3 = _vertexPoints[vertexIndex3];

        var normal1 = GetVertexNormal(vertexIndex1);
        var normal2 = GetVertexNormal(vertexIndex2);
        var normal3 = GetVertexNormal(vertexIndex3);

        if (NormalComputationMethod == GrVertexNormalComputationMethod.WeightedNormals)
        {
            //Find inner angles of triangle
            var angle1 = vertex1.GetAngleFromPoints(vertex3, vertex2);
            var angle2 = vertex2.GetAngleFromPoints(vertex1, vertex3);
            var angle3 = vertex3.GetAngleFromPoints(vertex2, vertex1);

            //Find triangle normal, not unit but full normal vector
            var normal = 
                LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

            //For debugging only
            Debug.Assert(
                angle1.IsValid() &&
                angle2.IsValid() &&
                angle3.IsValid() &&
                normal.IsValid()
            );

            //Update normals of triangle vertices. See here for more information:
            //http://www.bytehazard.com/articles/vertnorm.html
            //normal.MakeUnitVector();
            normal1.Update(angle1.RadiansValue * normal);
            normal2.Update(angle2.RadiansValue * normal);
            normal3.Update(angle3.RadiansValue * normal);

            return;
        }
            
        if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageUnitNormals)
        {
            //Find triangle unit normal
            var normal = 
                LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);
                
            //For debugging only
            Debug.Assert(normal.IsValid());

            normal1.Update(normal);
            normal2.Update(normal);
            normal3.Update(normal);

            return;
        }
            
        if (NormalComputationMethod == GrVertexNormalComputationMethod.AverageNormals)
        {
            //Find triangle normal, not unit but full normal vector
            var normal = 
                LinFloat64Vector3DAffineUtils.GetTriangleNormal(vertex1, vertex2, vertex3);

            //For debugging only
            Debug.Assert(normal.IsValid());

            normal1.Update(normal);
            normal2.Update(normal);
            normal3.Update(normal);

            return;
        }
    }

    public void ComputeVertexNormals(bool reverseNormals)
    {
        _vertexNormals = new LinFloat64Normal3D[_vertexPoints.Count];

        foreach (var (index1, index2, index3) in TriangleVertexIndices)
            UpdateVertexNormals(index1, index2, index3);

        if (reverseNormals)
        {
            foreach (var normal in _vertexNormals)
                normal.MakeNegativeUnit();
        }
        else
        {
            foreach (var normal in _vertexNormals)
                normal.MakeUnit();
        }
    }

    public abstract IEnumerator<ITriangle3D> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}