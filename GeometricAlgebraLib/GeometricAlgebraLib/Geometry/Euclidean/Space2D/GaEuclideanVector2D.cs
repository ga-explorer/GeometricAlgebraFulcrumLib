using System;
using System.Diagnostics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using GeometricAlgebraLib.Processors.Scalars;

namespace GeometricAlgebraLib.Geometry.Euclidean.Space2D
{
    public readonly struct GaEuclideanVector2D : 
        ITuple2D,
        IGaEuclideanGeometry<double>,
        IEquatable<GaEuclideanVector2D>, 
        IEquatable<GaEuclideanVector2D?>
    {
        public static GaEuclideanVector2D operator -(GaEuclideanVector2D v1)
        {
            return new GaEuclideanVector2D(-v1.X, -v1.Y);
        }

        public static GaEuclideanVector2D operator +(GaEuclideanVector2D v1, GaEuclideanVector2D v2)
        {
            return new GaEuclideanVector2D(
                v1.X + v2.X,
                v1.Y + v2.Y
            );
        }

        public static GaEuclideanVector2D operator -(GaEuclideanVector2D v1, GaEuclideanVector2D v2)
        {
            return new GaEuclideanVector2D(
                v1.X - v2.X,
                v1.Y - v2.Y
            );
        }

        public static GaEuclideanVector2D operator *(double s1, GaEuclideanVector2D p2)
        {
            return new GaEuclideanVector2D(
                s1 * p2.X,
                s1 * p2.Y
            );
        }

        public static GaEuclideanVector2D operator *(GaEuclideanVector2D v1, double s2)
        {
            return new GaEuclideanVector2D(
                v1.X * s2,
                v1.Y * s2
            );
        }

        public static GaEuclideanVector2D operator /(GaEuclideanVector2D v1, double s2)
        {
            s2 = 1d / s2;

            return new GaEuclideanVector2D(
                v1.X * s2,
                v1.Y * s2
            );
        }

        public static bool operator ==(GaEuclideanVector2D v1, GaEuclideanVector2D v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(GaEuclideanVector2D v1, GaEuclideanVector2D v2)
        {
            return !v1.Equals(v2);
        }


        public IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;

        public double X { get; }

        public double Y { get; }

        public double Item1 => X;

        public double Item2 => Y;


        public GaEuclideanVector2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid);
        }


        public bool IsValid 
            => !double.IsNaN(X) &&
               !double.IsNaN(Y);

        public bool IsInvalid 
            => double.IsNaN(X) ||
               double.IsNaN(Y);


        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double GetLengthSquared()
        {
            return X * X + Y * Y;
        }

        public double GetDotProduct(GaEuclideanVector2D v2)
        {
            return X * v2.X + Y * v2.Y;
        }

        public double GetAngleWith(GaEuclideanVector2D v2)
        {
            return Math.Acos(
                (X * v2.X + Y * v2.Y) / 
                Math.Sqrt(
                    (X * X + Y * Y) *
                    (v2.X * v2.X + v2.Y * v2.Y)
                )
            );
        }

        public GaEuclideanPoint2D AsPoint()
        {
            return new GaEuclideanPoint2D(X, Y);
        }

        public bool Equals(GaEuclideanVector2D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj is GaEuclideanVector2D other && Equals(other);
        }

        public bool Equals(GaEuclideanVector2D other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Vector(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}