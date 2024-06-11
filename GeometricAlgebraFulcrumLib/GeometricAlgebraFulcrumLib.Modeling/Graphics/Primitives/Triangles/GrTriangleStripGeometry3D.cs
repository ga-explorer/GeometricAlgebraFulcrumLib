using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;

public sealed class GrTriangleStripGeometry3D 
    : GrTriangleGeometryBase3D
{
    public static GrTriangleStripGeometry3D Create(IReadOnlyList<ILinFloat64Vector3D> pointsList)
    {
        if (pointsList.Count < 3)
            throw new ArgumentException();

        return new GrTriangleStripGeometry3D(pointsList);
    }

    public static GrTriangleStripGeometry3D Create(params ILinFloat64Vector3D[] pointsList)
    {
        if (pointsList.Length < 3)
            throw new ArgumentException();

        return new GrTriangleStripGeometry3D(pointsList);
    }

    public static GrTriangleStripGeometry3D Create(IEnumerable<ILinFloat64Vector3D> pointsList)
    {
        var pointsArray = pointsList.ToArray();

        if (pointsArray.Length < 3)
            throw new ArgumentException();

        return new GrTriangleStripGeometry3D(pointsArray);
    }
        

    public override GraphicsPrimitiveType3D PrimitiveType 
        => GraphicsPrimitiveType3D.TriangleStrip;

    public override IEnumerable<int> GeometryIndices
        => Enumerable.Range(0, VertexCount).ToArray();

    public override int Count
        => VertexCount - 2;

    public override ITriangle3D this[int index] 
        => Triangle3D.Create(
            GetGeometryPoint(index),
            GetGeometryPoint(index + 1),
            GetGeometryPoint(index + 2)
        );

    public override IEnumerable<Triplet<ILinFloat64Vector3D>> TriangleVertexPoints
    {
        get
        {
            var pointsList = new List<Triplet<ILinFloat64Vector3D>>(Count);

            for (var i = 0; i < VertexCount - 2; i++)
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

            for (var i = 0; i < VertexCount - 2; i++)
                pointsList.Add(new Triplet<int>(i, i + 1, i + 2));

            return pointsList;
        }
    }


    private GrTriangleStripGeometry3D(IReadOnlyList<ILinFloat64Vector3D> pointsList)
        : base(pointsList)
    {
    }


    public override IEnumerator<ITriangle3D> GetEnumerator()
    {
        for (var i = 0; i < VertexCount - 2; i++)
        {
            var point1 = GetGeometryPoint(i);
            var point2 = GetGeometryPoint(i + 1);
            var point3 = GetGeometryPoint(i + 2);

            yield return Triangle3D.Create(point1, point2, point3);
        }
    }
}