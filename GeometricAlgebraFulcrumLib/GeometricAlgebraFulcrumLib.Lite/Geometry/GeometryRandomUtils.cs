using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry
{
    public static class GeometryRandomUtils
    {
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
    {
        return Float64Vector2D.Create(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetPointInside(this System.Random randomGenerator, ILineSegment2D lineSegment)
    {
        return lineSegment.GetPointAt(
            randomGenerator.GetNumber()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointInside(this System.Random randomGenerator, IBoundingBox3D limitsBoundingBox)
    {
        return Float64Vector3D.Create(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector3D GetPointInside(this System.Random randomGenerator, ITriangle3D triangle)
    {
        return triangle.GetPointAt(
            randomGenerator.GetNumber(),
            randomGenerator.GetNumber(),
            randomGenerator.GetNumber()
        );
    }
    
    public static IReadOnlyList<Float64Vector2D> GetPointsInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox, int pointsCount)
    {
        var pointsArray = new Float64Vector2D[pointsCount];

        for (var i = 0; i < pointsCount; i++)
            pointsArray[i] = randomGenerator.GetPointInside(limitsBoundingBox);

        return pointsArray;
    }

    }
}
