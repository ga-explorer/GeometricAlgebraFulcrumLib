using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Primitives.Triangles;

public sealed class GrTriangleFanGeometry3D 
    : GrTriangleGeometryBase3D
{
    public static GrTriangleFanGeometry3D Create(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if (pointsList.Count < 3)
            throw new ArgumentException();

        return new GrTriangleFanGeometry3D(pointsList);
    }

    public static GrTriangleFanGeometry3D Create(params ILinFloat64Vector3D[] pointsList)
    {
        if (pointsList.Length < 3)
            throw new ArgumentException();

        return new GrTriangleFanGeometry3D(pointsList);
    }

    public static GrTriangleFanGeometry3D Create(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        var pointsArray = pointsList.ToArray();

        if (pointsArray.Length < 3)
            throw new ArgumentException();

        return new GrTriangleFanGeometry3D(pointsArray);
    }
        

    public override GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.TriangleFan;

    public override IEnumerable<int> GeometryIndices
        => Enumerable.Range(0, VertexCount).ToArray();

    public override int Count
        => VertexCount - 2;

    public override ITriangle3D this[int index] 
        => Triangle3D.Create(
            GetGeometryPoint(0),
            GetGeometryPoint(index + 1),
            GetGeometryPoint(index + 2)
        );

    public override IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints
    {
        get
        {
            var pointsList = new List<Triplet<ILinFloat64Vector3D>>(Count);

            var point1 = GetGeometryPoint(0);
            for (var i = 0; i < VertexCount - 2; i++)
                pointsList.Add(new Triplet<ILinFloat64Vector3D>(
                    point1,
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

            for (var i = 0; i < VertexCount - 2; i++)
                pointsList.Add(new Triplet<int>(0, i + 1, i + 2));

            return pointsList;
        }
    }
        

    private GrTriangleFanGeometry3D(IReadOnlyList<ILinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }


    public override IEnumerator<ITriangle3D> GetEnumerator()
    {
        var point1 = GetGeometryPoint(0);
        for (var i = 0; i < VertexCount - 2; i++)
        {
            var point2 = GetGeometryPoint(i + 1);
            var point3 = GetGeometryPoint(i + 2);

            yield return Triangle3D.Create(point1, point2, point3);
        }
    }
}