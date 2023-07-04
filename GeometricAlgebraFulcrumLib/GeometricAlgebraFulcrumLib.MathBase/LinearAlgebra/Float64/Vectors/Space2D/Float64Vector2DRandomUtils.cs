using System.Runtime.CompilerServices;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

public static class Float64Vector2DRandomUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetVector2D(this System.Random random)
    {
        return Float64Vector2D.CreateFromPolar(
            random.NextDouble(),
            random.GetAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitVector2D(this System.Random randomGenerator)
    {
        return Float64Vector2D.CreateFromPolar(
            randomGenerator.GetAngle()
        );
    }


    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, double radius)
    {
        var pointsList = new List<Float64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

            pointsList.Add(
                Float64Vector2D.Create(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, params double[] radiusList)
    {
        var pointsList = new List<Float64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
            var radius = radiusList[radiusIndex];

            pointsList.Add(
                Float64Vector2D.Create(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, double radius)
    {
        var pointsList = new List<Float64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

            pointsList.Add(
                Float64Vector2D.Create(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, params double[] radiusList)
    {
        var pointsList = new List<Float64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
            var radius = radiusList[radiusIndex];

            pointsList.Add(
                Float64Vector2D.Create(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }
    

    public static Float64Vector2D GetPointInside(this System.Random randomGenerator, IEnumerable<IFloat64Vector2D> pointsList)
    {
        var x = 0.0d;
        var y = 0.0d;
        var d = 0.0d;

        foreach (var point in pointsList)
        {
            var weight = randomGenerator.GetNumber();

            x += weight * point.X;
            y += weight * point.Y;
            d += weight;
        }

        d = 1 / d;

        return Float64Vector2D.Create(
            d * x, 
            d * y
        );
    }

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