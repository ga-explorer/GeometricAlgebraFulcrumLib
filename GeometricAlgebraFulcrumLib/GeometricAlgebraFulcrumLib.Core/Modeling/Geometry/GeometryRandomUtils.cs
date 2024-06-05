using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry;

public static class GeometryRandomUtils
{
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInside(this Random randomGenerator, IBoundingBox2D limitsBoundingBox)
    {
        return LinFloat64Vector2D.Create(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInside(this Random randomGenerator, ILineSegment2D lineSegment)
    {
        return lineSegment.GetPointAt(
            randomGenerator.GetNumber()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInside(this Random randomGenerator, IBoundingBox3D limitsBoundingBox)
    {
        return LinFloat64Vector3D.Create(
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInside(this Random randomGenerator, ITriangle3D triangle)
    {
        return triangle.GetPointAt(
            randomGenerator.GetNumber(),
            randomGenerator.GetNumber(),
            randomGenerator.GetNumber()
        );
    }
    
    public static IReadOnlyList<LinFloat64Vector2D> GetPointsInside(this Random randomGenerator, IBoundingBox2D limitsBoundingBox, int pointsCount)
    {
        var pointsArray = new LinFloat64Vector2D[pointsCount];

        for (var i = 0; i < pointsCount; i++)
            pointsArray[i] = randomGenerator.GetPointInside(limitsBoundingBox);

        return pointsArray;
    }

}