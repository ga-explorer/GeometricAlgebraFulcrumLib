using System;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.SdfShapes.Operations;
using GraphicsComposerLib.Geometry.SdfShapes.Primitives;
using GraphicsComposerLib.Geometry.SdfShapes.Transforms;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace GraphicsComposerLib.Geometry.SdfShapes
{
    public static class SdfUtils
    {
        public static Float64Tuple3D ToTuple3D(this Color color)
        {
            var c = color.ToPixel<Rgb24>();

            return new Float64Tuple3D(
                c.R / 255.0d,
                c.G / 255.0d,
                c.B / 255.0d
            );
        }

        public static Color ToColor(this IFloat64Tuple3D colorVector)
        {
            return Color.FromRgb(
                (byte) (colorVector.X.ClampToUnit() * 255),
                (byte) (colorVector.Y.ClampToUnit() * 255),
                (byte) (colorVector.Z.ClampToUnit() * 255)
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

        public static Float64Tuple3D ClampTo(this IFloat64Tuple3D tuple, IFloat64Tuple3D maxTuple)
        {
            return new Float64Tuple3D(
                tuple.X.ClampTo(maxTuple.X),
                tuple.Y.ClampTo(maxTuple.Y),
                tuple.Z.ClampTo(maxTuple.Z)
            );
        }

        public static Float64Tuple3D ClampTo(this IFloat64Tuple3D tuple, IFloat64Tuple3D minTuple, IFloat64Tuple3D maxTuple)
        {
            return new Float64Tuple3D(
                tuple.X.ClampTo(minTuple.X, maxTuple.X),
                tuple.Y.ClampTo(minTuple.Y, maxTuple.Y),
                tuple.Z.ClampTo(minTuple.Z, maxTuple.Z)
            );
        }

        public static Float64Tuple3D ClampToSymmetric(this IFloat64Tuple3D tuple, IFloat64Tuple3D maxTuple)
        {
            return new Float64Tuple3D(
                tuple.X.ClampToSymmetric(maxTuple.X),
                tuple.Y.ClampToSymmetric(maxTuple.Y),
                tuple.Z.ClampToSymmetric(maxTuple.Z)
            );
        }
        
        public static double LengthXy(this IFloat64Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Y * tuple.Y
            );
        }

        public static double LengthXz(this IFloat64Tuple3D tuple)
        {
            return Math.Sqrt(
                tuple.X * tuple.X + 
                tuple.Z * tuple.Z
            );
        }

        public static double LengthYz(this IFloat64Tuple3D tuple)
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
