//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Random;
//using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

//public static class Float64Vector2DRandomUtils
//{
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector2D<T> GetVector2D<T>(this Random random)
//    {
//        return LinVector2D<T>.CreateFromPolar(
//            random.NextDouble(),
//            random.GetAngle()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector2D<T> GetVector2D<T>(this Random random, double minLength, double maxLength)
//    {
//        return LinVector2D<T>.CreateFromPolar(
//            random.NextDouble(minLength, maxLength),
//            random.GetAngle()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector2D<T> GetUnitVector2D<T>(this Random randomGenerator)
//    {
//        return LinVector2D<T>.CreateFromPolar(
//            randomGenerator.GetAngle()
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector2D<T> GetVector2D<T>(this Random random, int quadrantIndex)
//    {
//        return LinVector2D<T>.CreateFromPolar(
//            random.NextDouble(),
//            random.GetAngle(quadrantIndex)
//        );
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static LinVector2D<T> GetVector2D<T>(this Random random, double maxLength, int quadrantIndex)
//    {
//        return LinVector2D<T>.CreateFromPolar(
//            random.NextDouble() * Math.Abs(maxLength),
//            random.GetAngle(quadrantIndex)
//        );
//    }


//    public static IReadOnlyList<LinVector2D<T>> GetPolygonPoints2D<T>(this Random randomGenerator, int sidesCount, double radius)
//    {
//        var pointsList = new List<LinVector2D<T>>(sidesCount);

//        var deltaAngle = 2.0d * Math.PI / sidesCount;

//        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
//        {
//            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

//            pointsList.Add(
//                LinVector2D<T>.Create(
//                    radius * Math.Cos(angle),
//                    radius * Math.Sin(angle)
//                )
//            );
//        }

//        return pointsList;
//    }

//    public static IReadOnlyList<LinVector2D<T>> GetPolygonPoints2D<T>(this Random randomGenerator, int sidesCount, params double[] radiusList)
//    {
//        var pointsList = new List<LinVector2D<T>>(sidesCount);

//        var deltaAngle = 2.0d * Math.PI / sidesCount;

//        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
//        {
//            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
//            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
//            var radius = radiusList[radiusIndex];

//            pointsList.Add(
//                LinVector2D<T>.Create(
//                    radius * Math.Cos(angle),
//                    radius * Math.Sin(angle)
//                )
//            );
//        }

//        return pointsList;
//    }

//    public static IReadOnlyList<LinVector2D<T>> GetPolygonPoints2D<T>(this Random randomGenerator, int sidesCount, LinVector2D<T> centerPoint, double radius)
//    {
//        var pointsList = new List<LinVector2D<T>>(sidesCount);

//        var deltaAngle = 2.0d * Math.PI / sidesCount;

//        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
//        {
//            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

//            pointsList.Add(
//                LinVector2D<T>.Create(
//                    centerPoint.X + radius * Math.Cos(angle),
//                    centerPoint.Y + radius * Math.Sin(angle)
//                )
//            );
//        }

//        return pointsList;
//    }

//    public static IReadOnlyList<LinVector2D<T>> GetPolygonPoints2D<T>(this Random randomGenerator, int sidesCount, LinVector2D<T> centerPoint, params double[] radiusList)
//    {
//        var pointsList = new List<LinVector2D<T>>(sidesCount);

//        var deltaAngle = 2.0d * Math.PI / sidesCount;

//        for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
//        {
//            var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
//            var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
//            var radius = radiusList[radiusIndex];

//            pointsList.Add(
//                LinVector2D<T>.Create(
//                    centerPoint.X + radius * Math.Cos(angle),
//                    centerPoint.Y + radius * Math.Sin(angle)
//                )
//            );
//        }

//        return pointsList;
//    }


//    public static LinVector2D<T> GetPointInside<T>(this Random randomGenerator, IEnumerable<ILinVector2D<T>> pointsList)
//    {
//        var x = 0.0d;
//        var y = 0.0d;
//        var d = 0.0d;

//        foreach (var point in pointsList)
//        {
//            var weight = randomGenerator.GetNumber();

//            x += weight * point.X;
//            y += weight * point.Y;
//            d += weight;
//        }

//        d = 1 / d;

//        return LinVector2D<T>.Create(
//            d * x,
//            d * y
//        );
//    }

//}