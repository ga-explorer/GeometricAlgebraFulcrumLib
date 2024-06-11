﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public static class Float64GeometryRandomUtils
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