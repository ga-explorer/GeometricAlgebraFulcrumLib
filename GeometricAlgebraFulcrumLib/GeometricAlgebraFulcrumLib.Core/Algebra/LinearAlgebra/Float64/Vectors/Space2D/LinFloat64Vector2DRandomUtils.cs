using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public static class LinFloat64Vector2DRandomUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetLinVector2D(this Random random)
    {
        return LinFloat64Vector2D.CreateFromPolar(
            random.NextDouble(),
            random.GetPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetLinVector2D(this Random random, double minLength, double maxLength)
    {
        return LinFloat64Vector2D.CreateFromPolar(
            random.NextDouble(minLength, maxLength),
            random.GetPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetUnitLinVector2D(this Random randomGenerator)
    {
        return LinFloat64Vector2D.CreateFromPolar(
            randomGenerator.GetPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetLinVector2D(this Random random, int quadrantIndex)
    {
        return LinFloat64Vector2D.CreateFromPolar(
            random.NextDouble(),
            random.GetPolarAngleInQuadrant(quadrantIndex)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D GetLinVector2D(this Random random, double maxLength, int quadrantIndex)
    {
        return LinFloat64Vector2D.CreateFromPolar(
            random.NextDouble() * Math.Abs(maxLength),
            random.GetPolarAngleInQuadrant(quadrantIndex)
        );
    }


    public static IReadOnlyList<LinFloat64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, double radius)
    {
        var pointsList = new List<LinFloat64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

            pointsList.Add(
                LinFloat64Vector2D.Create(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<LinFloat64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, params double[] radiusList)
    {
        var pointsList = new List<LinFloat64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
            var radius = radiusList[radiusIndex];

            pointsList.Add(
                LinFloat64Vector2D.Create(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<LinFloat64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, LinFloat64Vector2D centerPoint, double radius)
    {
        var pointsList = new List<LinFloat64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

            pointsList.Add(
                LinFloat64Vector2D.Create(
                    centerPoint.Item1 + radius * Math.Cos(angle),
                    centerPoint.Item2 + radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }

    public static IReadOnlyList<LinFloat64Vector2D> GetPolygonPoints2D(this Random randomGenerator, int sidesCount, LinFloat64Vector2D centerPoint, params double[] radiusList)
    {
        var pointsList = new List<LinFloat64Vector2D>(sidesCount);

        var deltaAngle = 2.0d * Math.PI / sidesCount;

        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
        {
            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
            var radius = radiusList[radiusIndex];

            pointsList.Add(
                LinFloat64Vector2D.Create(
                    centerPoint.Item1 + radius * Math.Cos(angle),
                    centerPoint.Item2 + radius * Math.Sin(angle)
                )
            );
        }

        return pointsList;
    }


    public static LinFloat64Vector2D GetPointInside(this Random randomGenerator, IEnumerable<IPair<Float64Scalar>> pointsList)
    {
        var x = 0.0d;
        var y = 0.0d;
        var d = 0.0d;

        foreach (var point in pointsList)
        {
            var weight = randomGenerator.GetNumber();

            x += weight * point.Item1;
            y += weight * point.Item2;
            d += weight;
        }

        d = 1 / d;

        return LinFloat64Vector2D.Create(
            d * x,
            d * y
        );
    }

}