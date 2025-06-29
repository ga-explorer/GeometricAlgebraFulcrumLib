﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry;

public static class Float64GeometryRandomUtils
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInside(this Random randomGenerator, IFloat64BoundingBox2D limitsBoundingBox)
    {
        return LinFloat64Vector2D.Create(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetPointInside(this Random randomGenerator, IFloat64LineSegment2D lineSegment)
    {
        return lineSegment.GetPointAt(
            randomGenerator.GetFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInside(this Random randomGenerator, IFloat64BoundingBox3D limitsBoundingBox)
    {
        return LinFloat64Vector3D.Create(
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
            randomGenerator.GetLinearMappedFloat64(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D GetPointInside(this Random randomGenerator, IFloat64Triangle3D triangle)
    {
        return triangle.GetPointAt(
            randomGenerator.GetFloat64(),
            randomGenerator.GetFloat64(),
            randomGenerator.GetFloat64()
        );
    }

    public static IReadOnlyList<LinFloat64Vector2D> GetPointsInside(this Random randomGenerator, IFloat64BoundingBox2D limitsBoundingBox, int pointsCount)
    {
        var pointsArray = new LinFloat64Vector2D[pointsCount];

        for (var i = 0; i < pointsCount; i++)
            pointsArray[i] = randomGenerator.GetPointInside(limitsBoundingBox);

        return pointsArray;
    }

}