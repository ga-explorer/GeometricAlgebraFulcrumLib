using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.LatticeShapes.Surfaces;

public sealed class GrLatticeSurfaceTriangleGeometry3D :
    IGraphicsTriangleGeometry3D
{
    public GrLatticeSurfaceList3D LatticeSurfaceList { get; }

    public GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.Triangles;

    public int VertexCount 
        => LatticeSurfaceList.Count;

    public IEnumerable<int> GeometryIndices 
        => LatticeSurfaceList.TriangleVertexIndices.SelectMany(t => t.GetItems());

    public IEnumerable<IGraphicsVertex3D> GeometryVertices 
        => LatticeSurfaceList.VertexList;

    public IEnumerable<ILinFloat64Vector3D> GeometryPoints 
        => LatticeSurfaceList.VertexList;

    public int Count 
        => LatticeSurfaceList.TriangleVerticesList.Count;

    public ITriangle3D this[int index]
    {
        get
        {
            var (v1, v2, v3) = 
                LatticeSurfaceList.TriangleVerticesList[index];

            return Triangle3D.Create(v1.Point, v2.Point, v3.Point);
        }
    }

    public IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints
        => LatticeSurfaceList.TriangleVerticesList.Select(t => 
            new Triplet<ILinFloat64Vector3D>(t.Item1, t.Item2, t.Item3)
        );

    public IEnumerable<Triplet<int>> TriangleVertexIndices 
        => LatticeSurfaceList.TriangleVertexIndices;

    public IEnumerable<ILinFloat64Vector3D> VertexNormals
        => LatticeSurfaceList.VertexList.Select(v => v.Normal);
        
    public IEnumerable<ILinFloat64Vector2D> VertexTextureUVs 
        => LatticeSurfaceList.VertexList.Select(v => v.ParameterValue.ToLinVector2D());
        
    public IEnumerable<Color> VertexColors 
        => LatticeSurfaceList.VertexList.Select(v => v.Color);

    public bool VertexNormalsEnabled { get; set; }
        
    public bool VertexTextureUVsEnabled { get; set; }
        
    public bool VertexColorsEnabled { get; set; }

    public GrVertexNormalComputationMethod NormalComputationMethod 
        => LatticeSurfaceList.NormalComputationMethod;


    internal GrLatticeSurfaceTriangleGeometry3D(GrLatticeSurfaceList3D parentSet)
    {
        if (!parentSet.IsReady)
            throw new InvalidOperationException();

        LatticeSurfaceList = parentSet;
    }


    public ILinFloat64Vector3D GetGeometryPoint(int index)
    {
        return LatticeSurfaceList[index];
    }

    public LinFloat64Normal3D GetVertexNormal(int index)
    {
        return LatticeSurfaceList[index].Normal;
    }

    public void ComputeVertexNormals(bool inverseNormals)
    {
    }
        
    public IEnumerator<ITriangle3D> GetEnumerator()
    {
        return LatticeSurfaceList.TriangleVerticesList.Select(t => 
            Triangle3D.Create(t.Item1, t.Item2, t.Item3)
        ).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}