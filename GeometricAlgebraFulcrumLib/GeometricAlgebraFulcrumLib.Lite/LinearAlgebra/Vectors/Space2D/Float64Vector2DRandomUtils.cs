using System.Runtime.CompilerServices;
using DataStructuresLib.Random;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

public static class Float64Vector2DRandomUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetVector2D(this Random random)
    {
        return Float64Vector2D.CreateFromPolar(
            random.NextDouble(),
            random.GetAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetVector2D(this Random random, double minLength, double maxLength)
    {
        return Float64Vector2D.CreateFromPolar(
            random.NextDouble(minLength, maxLength),
            random.GetAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector2D GetUnitVector2D(this Random randomGenerator)
    {
        return Float64Vector2D.CreateFromPolar(
            randomGenerator.GetAngle()
        );
    }


    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, double radius)
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

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, params double[] radiusList)
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

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, double radius)
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

    public static IReadOnlyList<Float64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, params double[] radiusList)
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
    

    public static Float64Vector2D GetPointInside(this Random randomGenerator, IEnumerable<IFloat64Vector2D> pointsList)
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

}