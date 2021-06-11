using System;
using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.SdfGeometry.Operations;
using EuclideanGeometryLib.SdfGeometry.Primitives;
using EuclideanGeometryLib.SdfGeometry.Transforms;

namespace EuclideanGeometryLib.SdfGeometry
{
    public static class SdfUtils
    {
        public static Tuple3D ToTuple3D(this Color color)
        {
            return new Tuple3D(
                color.R / 255.0d,
                color.G / 255.0d,
                color.B / 255.0d
            );
        }

        public static Color ToColor(this Tuple3D colorVector)
        {
            return Color.FromArgb(
                (int)(colorVector.X.ClampToUnit() * 255),
                (int)(colorVector.Y.ClampToUnit() * 255),
                (int)(colorVector.Z.ClampToUnit() * 255)
            );
        }

        public static double ClampToUnit(this double scalar)
        {
            if (scalar < 0.0d) return 0.0d;
            if (scalar > 1.0d) return 1.0d;
            return scalar;
        }

        public static double ClampTo(this double scalar, double maxValue)
        {
            if (scalar < 0.0d) return 0.0d;
            if (scalar > maxValue) return maxValue;
            return scalar;
        }

        public static double ClampTo(this double scalar, double minValue, double maxValue)
        {
            if (scalar < minValue) return minValue;
            if (scalar > maxValue) return maxValue;
            return scalar;
        }

        public static double ClampToSymmetric(this double scalar, double maxValue)
        {
            if (scalar < -maxValue) return -maxValue;
            if (scalar > maxValue) return maxValue;
            return scalar;
        }

        public static Tuple3D ClampTo(this Tuple3D tuple, Tuple3D maxTuple)
        {
            return new Tuple3D(
                tuple.X.ClampTo(maxTuple.X),
                tuple.Y.ClampTo(maxTuple.Y),
                tuple.Z.ClampTo(maxTuple.Z)
            );
        }

        public static Tuple3D ClampTo(this Tuple3D tuple, Tuple3D minTuple, Tuple3D maxTuple)
        {
            return new Tuple3D(
                tuple.X.ClampTo(minTuple.X, maxTuple.X),
                tuple.Y.ClampTo(minTuple.Y, maxTuple.Y),
                tuple.Z.ClampTo(minTuple.Z, maxTuple.Z)
            );
        }

        public static Tuple3D ClampToSymmetric(this Tuple3D tuple, Tuple3D maxTuple)
        {
            return new Tuple3D(
                tuple.X.ClampToSymmetric(maxTuple.X),
                tuple.Y.ClampToSymmetric(maxTuple.Y),
                tuple.Z.ClampToSymmetric(maxTuple.Z)
            );
        }

        public static Tuple2D Normalize(this Tuple2D tuple)
        {
            var length = Math.Sqrt(
                tuple.X * tuple.X +
                tuple.Y * tuple.Y
            );

            if (length == 0)
                return new Tuple2D(0, 0);

            return new Tuple2D(
                tuple.X / length,
                tuple.Y / length
            );
        }

        //public static Tuple3D Normalize(this Tuple3D tuple)
        //{
        //    var length = Math.Sqrt(
        //        tuple.X * tuple.X +
        //        tuple.Y * tuple.Y +
        //        tuple.Z * tuple.Z
        //    );

        //    if (length == 0)
        //        return new Tuple3D(0, 0, 0);

        //    return new Tuple3D(
        //        tuple.X / length,
        //        tuple.Y / length,
        //        tuple.Z / length
        //    );
        //}

        public static Tuple3D Min(this Tuple3D tuple, double scalar)
        {
            return new Tuple3D(
                Math.Min(tuple.X, scalar),
                Math.Min(tuple.Y, scalar),
                Math.Min(tuple.Z, scalar)
            );
        }
        
        public static Tuple3D Max(this Tuple3D tuple, double scalar)
        {
            return new Tuple3D(
                Math.Max(tuple.X, scalar),
                Math.Max(tuple.Y, scalar),
                Math.Max(tuple.Z, scalar)
            );
        }
        
        public static double ElementsMin(this Tuple3D tuple)
        {
            return Math.Min(tuple.X, Math.Min(tuple.Y, tuple.Z));
        }
        
        public static double ElementsMax(this Tuple3D tuple)
        {
            return Math.Max(tuple.X, Math.Max(tuple.Y, tuple.Z));
        }

        public static Tuple3D Abs(this Tuple3D tuple)
        {
            return new Tuple3D(
                Math.Abs(tuple.X),
                Math.Abs(tuple.Y),
                Math.Abs(tuple.Z)
            );
        }

        public static double DotProduct(this Tuple2D tuple1, Tuple2D tuple2)
        {
            return 
                tuple1.X * tuple2.X +
                tuple1.Y * tuple2.Y;
        }

        public static double DotProduct(this Tuple3D tuple1, Tuple3D tuple2)
        {
            return 
                tuple1.X * tuple2.X +
                tuple1.Y * tuple2.Y +
                tuple1.Z * tuple2.Z;
        }
        
        public static Tuple3D CrossProduct(this Tuple3D v1, Tuple3D v2)
        {
            return new Tuple3D(
                v1.Y * v2.Z - v1.Z * v2.Y,
                -v1.X * v2.Z + v1.Z * v2.X,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public static Tuple3D ElementsProduct(this Tuple3D tuple1, Tuple3D tuple2)
        {
            return new Tuple3D(
                tuple1.X * tuple2.X,
                tuple1.Y * tuple2.Y,
                tuple1.Z * tuple2.Z
            );
        }

        public static Tuple3D ElementsProduct(this Tuple3D tuple1, Tuple3D tuple2, Tuple3D tuple3)
        {
            return new Tuple3D(
                tuple1.X * tuple2.X * tuple3.X,
                tuple1.Y * tuple2.Y * tuple3.Y,
                tuple1.Z * tuple2.Z * tuple3.Z
            );
        }

        public static Tuple4D ElementsProduct(this Tuple4D tuple1, Tuple4D tuple2)
        {
            return new Tuple4D(
                tuple1.X * tuple2.X,
                tuple1.Y * tuple2.Y,
                tuple1.Z * tuple2.Z,
                tuple1.W * tuple2.W
            );
        }

        public static Tuple4D ElementsProduct(this Tuple4D tuple1, Tuple4D tuple2, Tuple4D tuple3)
        {
            return new Tuple4D(
                tuple1.X * tuple2.X * tuple3.X,
                tuple1.Y * tuple2.Y * tuple3.Y,
                tuple1.Z * tuple2.Z * tuple3.Z,
                tuple1.W * tuple2.W * tuple3.W
            );
        }

        public static Tuple3D ReflectVectorOnUnitVector(this Tuple3D vector, Tuple3D unitVector)
        {
            return vector - 2 * vector.DotProduct(unitVector) * unitVector;
        }

        public static double Length(this Tuple2D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y
            );
        }

        public static double Length(this Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y +
                tuple.Z * tuple.Z
            );
        }

        public static double LengthSquared(this Tuple2D tuple)
        {
            return 
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y;
        }

        public static double LengthSquared(this Tuple3D tuple)
        {
            return 
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y +
                tuple.Z * tuple.Z;
        }

        public static double LengthXy(this Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y
            );
        }

        public static double LengthXz(this Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Z * tuple.Z
            );
        }

        public static double LengthYz(this Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.Y * tuple.Y + 
                tuple.Z * tuple.Z
            );
        }

        public static SdfPlaneXy3D PlaneXy { get; }
            = new SdfPlaneXy3D();

        public static SdfPlaneXz3D PlaneXz { get; }
            = new SdfPlaneXz3D();

        public static SdfPlaneYz3D PlaneYz { get; }
            = new SdfPlaneYz3D();
        
        public static SdfSphere3D UnitSphere { get; }
            = new SdfSphere3D();
        
        public static SdfBox3D UnitCube { get; }
            = new SdfBox3D();

        
        public static SdfBinaryOr3D Or(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
        {
            return new SdfBinaryOr3D()
            {
                Surface1 = surface1,
                Surface2 = surface2
            };
        }
        
        public static SdfBinaryOrNot3D OrNot(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
        {
            return new SdfBinaryOrNot3D()
            {
                Surface1 = surface1,
                Surface2 = surface2
            };
        }

        public static SdfBinaryAnd3D And(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
        {
            return new SdfBinaryAnd3D()
            {
                Surface1 = surface1,
                Surface2 = surface2
            };
        }
        
        public static SdfBinaryAndNot3D AndNot(this ISdfGeometry3D surface1, ISdfGeometry3D surface2)
        {
            return new SdfBinaryAndNot3D()
            {
                Surface1 = surface1,
                Surface2 = surface2
            };
        }

        public static SdfRounding3D Rounding(this ISdfGeometry3D surface, double radius)
        {
            return new SdfRounding3D()
            {
                Surface = surface,
                Radius = radius
            };
        }

        public static SdfRotateX3D RotateX(this ISdfGeometry3D surface, double angle)
        {
            return new SdfRotateX3D()
            {
                Surface = surface,
                Angle = angle
            };
        }

        public static SdfRotateY3D RotateY(this ISdfGeometry3D surface, double angle)
        {
            return new SdfRotateY3D()
            {
                Surface = surface,
                Angle = angle
            };
        }

        public static SdfRotateZ3D RotateZ(this ISdfGeometry3D surface, double angle)
        {
            return new SdfRotateZ3D()
            {
                Surface = surface,
                Angle = angle
            };
        }
    }
}
