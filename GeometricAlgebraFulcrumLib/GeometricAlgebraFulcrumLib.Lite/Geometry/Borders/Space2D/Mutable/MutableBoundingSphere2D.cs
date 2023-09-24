using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Mutable
{
    public sealed class MutableBoundingSphere2D : IBorderCurve2D
    {
        public static MutableBoundingSphere2D Create()
        {
            return new MutableBoundingSphere2D();
        }

        public static MutableBoundingSphere2D Create(double centerX, double centerY, double radius)
        {
            return new MutableBoundingSphere2D(centerX, centerY, radius);
        }

        public static MutableBoundingSphere2D Create(IFloat64Vector2D center, double radius)
        {
            return new MutableBoundingSphere2D(center.X, center.Y, radius);
        }

        public static MutableBoundingSphere2D CreateFromPoints(IEnumerable<IFloat64Vector2D> pointsList, bool tightBound = true)
        {
            var pointsArray = pointsList.ToArray();

            if (!tightBound)
            {
                var center = pointsArray.GetCenterPoint();
                var radius = 
                    Math.Sqrt(
                        center
                            .GetDistancesSquaredToPoints(pointsArray)
                            .Max()
                    );

                //This is the fast but not tight method
                return new MutableBoundingSphere2D(center, radius);
            }

            //This is the slower but tighter method
            var maxDistance = 0.0d;
            var maxPoint1 = pointsArray[0];
            var maxPoint2 = pointsArray[0];

            for (var i = 0; i < pointsArray.Length - 1; i++)
            {
                var p1 = pointsArray[i];

                for (var j = i + 1; j < pointsArray.Length; j++)
                {
                    var p2 = pointsArray[j];
                    var distance = p1.GetDistanceToPoint(p2);

                    if (distance <= maxDistance)
                        continue;

                    maxDistance = distance;
                    maxPoint1 = p1;
                    maxPoint2 = p2;
                }
            }

            var center1 = Float64Vector2D.Create(0.5 * (maxPoint1.X + maxPoint2.X),
                0.5 * (maxPoint1.Y + maxPoint2.Y));

            var radius1 = 0.5 * maxDistance;

            return new MutableBoundingSphere2D(center1, radius1);
        }


        public double Radius { get; private set; }

        public double CenterX { get; private set; }

        public double CenterY { get; private set; }

        public Float64Vector2D Center
        {
            get { return Float64Vector2D.Create((Float64Scalar)CenterX, (Float64Scalar)CenterY); }
        }

        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid()
        {
            return !double.IsNaN(Radius) &&
                   !double.IsNaN(CenterX) &&
                   !double.IsNaN(CenterY);
        }


        internal MutableBoundingSphere2D()
        {
        }

        internal MutableBoundingSphere2D(IFloat64Vector2D center, double radius)
        {
            CenterX = center.X;
            CenterY = center.Y;
            Radius = radius;

            ValidateValues();
        }

        internal MutableBoundingSphere2D(double centerX, double centerY, double radius)
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius;

            ValidateValues();
        }


        private void ValidateValues()
        {
            Debug.Assert(IsValid());

            if (Radius < 0) Radius = -Radius;
        }


        public MutableBoundingSphere2D SetRadius(double radius)
        {
            Radius = radius;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetCenter(IFloat64Vector2D center)
        {
            CenterX = center.X;
            CenterY = center.Y;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetCenter(double centerX, double centerY)
        {
            CenterX = centerX;
            CenterY = centerY;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetTo(IFloat64Vector2D center, double radius)
        {
            CenterX = center.X;
            CenterY = center.Y;
            Radius = radius;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D SetTo(double centerX, double centerY, double radius)
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = radius;

            ValidateValues();

            return this;
        }


        public MutableBoundingSphere2D UpdateRadiusBy(double radiusDelta)
        {
            Radius = Radius + radiusDelta;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D UpdateRadiusByFactor(double radiusFactor)
        {
            Radius = Radius * radiusFactor;

            ValidateValues();

            return this;
        }


        public MutableBoundingSphere2D MoveCenterBy(double xDelta, double yDelta)
        {
            CenterX += xDelta;
            CenterY += yDelta;

            ValidateValues();

            return this;
        }

        public MutableBoundingSphere2D MoveCenterBy(IFloat64Vector2D delta)
        {
            CenterX += delta.X;
            CenterY += delta.Y;

            ValidateValues();

            return this;
        }


        public BoundingBox2D GetBoundingBox()
        {
            var point1 = Float64Vector2D.Create((Float64Scalar)(CenterX - Radius), (Float64Scalar)(CenterY - Radius));
            var point2 = Float64Vector2D.Create((Float64Scalar)(CenterX + Radius), (Float64Scalar)(CenterY + Radius));

            return BoundingBox2D.Create(point1, point2);
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            var point1 = Float64Vector2D.Create((Float64Scalar)(CenterX - Radius), (Float64Scalar)(CenterY - Radius));
            var point2 = Float64Vector2D.Create((Float64Scalar)(CenterX + Radius), (Float64Scalar)(CenterY + Radius));

            return MutableBoundingBox2D.CreateFromPoints(point1, point2);
        }

        public IBorderCurve2D MapUsing(IAffineMap2D affineMap)
        {
            var s1 = Float64Vector2DComposerUtils.ToVector2D((IPair<double>)affineMap.MapVector(Float64Vector2D.E1)).ENorm();
            var s2 = Float64Vector2DComposerUtils.ToVector2D((IPair<double>)affineMap.MapVector(Float64Vector2D.E2)).ENorm();

            var sMax = s1 > s2 ? s1 : s2;

            var center = affineMap.MapPoint(Center);

            return new MutableBoundingSphere2D(
                center.X, 
                center.Y, 
                sMax * Radius
            );
        }
    }
}