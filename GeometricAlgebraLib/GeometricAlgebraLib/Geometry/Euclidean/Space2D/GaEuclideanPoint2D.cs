using System;
using System.Diagnostics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using GeometricAlgebraLib.Processing.Implementations.Float64;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Geometry.Euclidean.Space2D
{
    public readonly struct GaEuclideanPoint2D : 
        ITuple2D,
        IGaEuclideanGeometry<double>,
        IEquatable<GaEuclideanPoint2D>, 
        IEquatable<GaEuclideanPoint2D?>
    {
        public static GaEuclideanVector2D operator -(GaEuclideanPoint2D p1)
        {
            return new GaEuclideanVector2D(-p1.X, -p1.Y);
        }

        public static GaEuclideanPoint2D operator +(GaEuclideanPoint2D p1, GaEuclideanVector2D v2)
        {
            return new GaEuclideanPoint2D(
                p1.X + v2.X,
                p1.Y + v2.Y
            );
        }

        public static GaEuclideanPoint2D operator +(GaEuclideanPoint2D p1, GaEuclideanPoint2D p2)
        {
            return new GaEuclideanPoint2D(
                p1.X + p2.X,
                p1.Y + p2.Y
            );
        }

        public static GaEuclideanPoint2D operator -(GaEuclideanPoint2D p1, GaEuclideanVector2D v2)
        {
            return new GaEuclideanPoint2D(
                p1.X - v2.X,
                p1.Y - v2.Y
            );
        }

        public static GaEuclideanVector2D operator -(GaEuclideanPoint2D p1, GaEuclideanPoint2D p2)
        {
            return new GaEuclideanVector2D(
                p1.X - p2.X,
                p1.Y - p2.Y
            );
        }

        public static GaEuclideanPoint2D operator *(double s1, GaEuclideanPoint2D p2)
        {
            return new GaEuclideanPoint2D(
                s1 * p2.X,
                s1 * p2.Y
            );
        }

        public static GaEuclideanPoint2D operator *(GaEuclideanPoint2D p1, double s2)
        {
            return new GaEuclideanPoint2D(
                p1.X * s2,
                p1.Y * s2
            );
        }

        public static GaEuclideanPoint2D operator /(GaEuclideanPoint2D p1, double s2)
        {
            s2 = 1d / s2;

            return new GaEuclideanPoint2D(
                p1.X * s2,
                p1.Y * s2
            );
        }

        public static bool operator ==(GaEuclideanPoint2D p1, GaEuclideanPoint2D p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(GaEuclideanPoint2D p1, GaEuclideanPoint2D p2)
        {
            return !p1.Equals(p2);
        }


        public IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;

        public double X { get; }

        public double Y { get; }

        public bool IsValid 
            => !double.IsNaN(X) && 
               !double.IsNaN(Y);

        public bool IsInvalid 
            => double.IsNaN(X) || 
               double.IsNaN(Y);

        public double Item1 => X;

        public double Item2 => Y;


        public GaEuclideanPoint2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid);
        }


        public double GetDistance(GaEuclideanPoint2D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;

            return Math.Sqrt(x * x + y * y);
        }

        public double GetDistanceSquared(GaEuclideanPoint2D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;

            return x * x + y * y;
        }

        public GaEuclideanVector2D AsVector()
        {
            return new GaEuclideanVector2D(X, Y);
        }
        
        public bool Equals(GaEuclideanPoint2D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj is GaEuclideanPoint2D other && Equals(other);
        }

        public bool Equals(GaEuclideanPoint2D other)
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
                .Append("Point(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}