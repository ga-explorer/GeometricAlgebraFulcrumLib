using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Triangles.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Random
{
    public static class RandomUtils
    {
        public static IEnumerable<IFloat64Tuple2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, double radius)
        {
            var pointsList = new List<IFloat64Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

                pointsList.Add(new Float64Vector2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static List<Float64Vector2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, params double[] radiusList)
        {
            var pointsList = new List<Float64Vector2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
                var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Float64Vector2D(
                    radius * Math.Cos(angle),
                    radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static IEnumerable<IFloat64Tuple2D> GetPolygonPoints(this System.Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, double radius)
        {
            var pointsList = new List<IFloat64Tuple2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());

                pointsList.Add(new Float64Vector2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }

        public static List<Float64Vector2D> GetPolygonPoints2D(this System.Random randomGenerator, int sidesCount, Float64Vector2D centerPoint, params double[] radiusList)
        {
            var pointsList = new List<Float64Vector2D>(sidesCount);

            var deltaAngle = 2.0d * Math.PI / sidesCount;

            for (var sideIndex = 0; sideIndex < sidesCount; sideIndex++)
            {
                var angle = deltaAngle * (sideIndex + randomGenerator.GetNumber());
                var radiusIndex = randomGenerator.GetInteger(radiusList.Length);
                var radius = radiusList[radiusIndex];

                pointsList.Add(new Float64Vector2D(
                    centerPoint.X + radius * Math.Cos(angle),
                    centerPoint.Y + radius * Math.Sin(angle)
                ));
            }

            return pointsList;
        }


        public static Float64Vector2D GetPointInside(this System.Random randomGenerator, IEnumerable<IFloat64Tuple2D> pointsList)
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

            return new Float64Vector2D(d * x, d * y);
        }

        public static Float64Vector2D GetPointInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new Float64Vector2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static Float64Vector2D GetPointInside(this System.Random randomGenerator, ILineSegment2D lineSegment)
        {
            return lineSegment.GetPointAt(
                randomGenerator.GetNumber()
            );
        }

        public static Float64Vector2D[] GetPointsInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox, int pointsCount)
        {
            var pointsArray = new Float64Vector2D[pointsCount];

            for (var i = 0; i < pointsCount; i++)
                pointsArray[i] = randomGenerator.GetPointInside(limitsBoundingBox);

            return pointsArray;
        }

        public static Float64Vector3D GetPointInside(this System.Random randomGenerator, IBoundingBox3D limitsBoundingBox)
        {
            return Float64Vector3D.Create(randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinZ, limitsBoundingBox.MaxZ));
        }

        public static Float64Vector3D GetPointInside(this System.Random randomGenerator, ITriangle3D triangle)
        {
            return triangle.GetPointAt(
                randomGenerator.GetNumber(),
                randomGenerator.GetNumber(),
                randomGenerator.GetNumber()
            );
        }


        public static Float64Vector2D GetUnitVector2D(this System.Random randomGenerator)
        {
            var angle = randomGenerator.GetAngle();

            return new Float64Vector2D(Math.Cos(angle), Math.Sin(angle));
        }

        public static Float64Vector3D GetUnitVector3D(this System.Random randomGenerator)
        {
            var phi = randomGenerator.GetNumber() * 2d * Math.PI;
            var theta = randomGenerator.GetNumber() * Math.PI;

            return new Float64SphericalUnitVector3D(theta, phi).ToVector3D();
        }


        public static LineSegment2D GetLineSegmentInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new LineSegment2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static Triangle2D GetTriangleInside(this System.Random randomGenerator, IBoundingBox2D limitsBoundingBox)
        {
            return new Triangle2D(
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
            );
        }

        public static List<Triangle2D> GetTrianglesInside(this System.Random randomGenerator, int trianglesCount, IBoundingBox2D limitsBoundingBox)
        {
            var result = new List<Triangle2D>(trianglesCount);

            for (var i = 0; i < trianglesCount; i++)
                result.Add(
                    new Triangle2D(
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinX, limitsBoundingBox.MaxX),
                        randomGenerator.GetLinearMappedNumber(limitsBoundingBox.MinY, limitsBoundingBox.MaxY)
                    )
                );

            return result;
        }
    }
}
