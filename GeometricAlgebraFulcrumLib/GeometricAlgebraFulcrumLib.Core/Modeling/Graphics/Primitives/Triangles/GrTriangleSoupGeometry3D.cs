using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;

public sealed class GrTriangleSoupGeometry3D 
    : GrTriangleGeometryBase3D
{
    public static GrTriangleSoupGeometry3D Create(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if ((pointsList.Count % 3) != 0)
            throw new ArgumentException();

        return new GrTriangleSoupGeometry3D(pointsList);
    }

    public static GrTriangleSoupGeometry3D Create(params ILinFloat64Vector3D[] pointsList)
    {
        if ((pointsList.Length % 3) != 0)
            throw new ArgumentException();

        return new GrTriangleSoupGeometry3D(pointsList);
    }

    public static GrTriangleSoupGeometry3D Create(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        var pointsArray = pointsList.ToArray();

        if ((pointsArray.Length % 3) != 0)
            throw new ArgumentException();

        return new GrTriangleSoupGeometry3D(pointsArray);
    }

    public static GrTriangleSoupGeometry3D Create(params ITriangle3D[] trianglesList)
    {
        var pointsList = new List<ILinFloat64Vector3D>(trianglesList.Length * 3);

        foreach (var triangle in trianglesList)
        {
            pointsList.Add(triangle.GetPoint1());
            pointsList.Add(triangle.GetPoint2());
            pointsList.Add(triangle.GetPoint3());
        }

        return new GrTriangleSoupGeometry3D(pointsList);
    }

    public static GrTriangleSoupGeometry3D Create(IEnumerable<ITriangle3D> trianglesList, bool reversePoints)
    {
        var pointsList = new List<ILinFloat64Vector3D>();

        foreach (var triangle in trianglesList)
        {
            if (reversePoints)
            {
                pointsList.Add(triangle.GetPoint3());
                pointsList.Add(triangle.GetPoint2());
                pointsList.Add(triangle.GetPoint1());
            }
            else                
            {
                pointsList.Add(triangle.GetPoint1());
                pointsList.Add(triangle.GetPoint2());
                pointsList.Add(triangle.GetPoint3());
            }
        }

        return new GrTriangleSoupGeometry3D(pointsList);
    }

        
    public override GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.Triangles;

    public override IEnumerable<int> GeometryIndices
        => Enumerable.Range(0, VertexCount).ToArray();

    public override int Count
        => VertexCount / 3;

    public override ITriangle3D this[int index] 
        => Triangle3D.Create(
            GetGeometryPoint(3 * index),
            GetGeometryPoint(3 * index + 1),
            GetGeometryPoint(3 * index + 2)
        );

    public override IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints
    {
        get
        {
            var pointsList = new List<Triplet<ILinFloat64Vector3D>>(Count);

            for (var i = 0; i < VertexCount; i += 3)
                pointsList.Add(new Triplet<ILinFloat64Vector3D>(
                    GetGeometryPoint(i),
                    GetGeometryPoint(i + 1),
                    GetGeometryPoint(i + 2)
                ));

            return pointsList;
        }
    }

    public override IEnumerable<Triplet<int>> TriangleVertexIndices
    {
        get
        {
            var pointsList = new List<Triplet<int>>(Count);

            for (var i = 0; i < VertexCount; i += 3)
                pointsList.Add(new Triplet<int>(i, i + 1, i + 2));

            return pointsList;
        }
    }


    private GrTriangleSoupGeometry3D(IReadOnlyList<ILinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
        if ((VertexCount % 3) != 0)
            throw new ArgumentException();
    }


    public override IEnumerator<ITriangle3D> GetEnumerator()
    {
        for (var i = 0; i < VertexCount; i += 3)
        {
            var point1 = GetGeometryPoint(i);
            var point2 = GetGeometryPoint(i + 1);
            var point3 = GetGeometryPoint(i + 2);

            yield return Triangle3D.Create(point1, point2, point3);
        }
    }
}