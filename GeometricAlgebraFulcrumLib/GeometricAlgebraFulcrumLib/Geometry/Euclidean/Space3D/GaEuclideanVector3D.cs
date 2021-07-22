using System;
using System.Diagnostics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D
{
    public readonly struct GaEuclideanVector3D : 
        ITuple3D,
        IGaEuclideanGeometry<double>,
        IEquatable<GaEuclideanVector3D>, 
        IEquatable<GaEuclideanVector3D?>
    {
        public static GaEuclideanVector3D operator -(GaEuclideanVector3D v1)
        {
            return new GaEuclideanVector3D(-v1.X, -v1.Y, -v1.Z);
        }

        public static GaEuclideanVector3D operator +(GaEuclideanVector3D v1, GaEuclideanVector3D v2)
        {
            return new GaEuclideanVector3D(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        public static GaEuclideanVector3D operator -(GaEuclideanVector3D v1, GaEuclideanVector3D v2)
        {
            return new GaEuclideanVector3D(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z
            );
        }

        public static GaEuclideanVector3D operator *(double s1, GaEuclideanVector3D p2)
        {
            return new GaEuclideanVector3D(
                s1 * p2.X,
                s1 * p2.Y,
                s1 * p2.Z
            );
        }

        public static GaEuclideanVector3D operator *(GaEuclideanVector3D p1, double s2)
        {
            return new GaEuclideanVector3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        public static GaEuclideanVector3D operator /(GaEuclideanVector3D p1, double s2)
        {
            s2 = 1d / s2;

            return new GaEuclideanVector3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        public static bool operator ==(GaEuclideanVector3D v1, GaEuclideanVector3D v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(GaEuclideanVector3D v1, GaEuclideanVector3D v2)
        {
            return !v1.Equals(v2);
        }


        public uint VSpaceDimension 
            => 3;

        public ulong GaSpaceDimension
            => 8;

        public IGaProcessor<double> Processor 
            => GaEuclideanSpace3DUtils.MultivectorProcessor;

        public IGaScalarProcessor<double> ScalarProcessor 
            => GaScalarProcessorFloat64.DefaultProcessor;

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public bool IsValid
            => !double.IsNaN(X) && 
               !double.IsNaN(Y) && 
               !double.IsNaN(Z);
        
        public bool IsInvalid
            => double.IsNaN(X) || 
               double.IsNaN(Y) || 
               double.IsNaN(Z);


        public GaEuclideanVector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid);
        }


        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double GetLengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double GetDotProduct(GaEuclideanVector3D v2)
        {
            return X * v2.X + Y * v2.Y + Z * v2.Z;
        }

        public double GetAngleWith(GaEuclideanVector3D v2)
        {
            return Math.Acos(
                (X * v2.X + Y * v2.Y + Z * v2.Z) / 
                Math.Sqrt(
                    (X * X + Y * Y + Z * Z) *
                    (v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z)
                )
            );
        }

        public GaEuclideanPoint3D AsPoint()
        {
            return new GaEuclideanPoint3D(X, Y, Z);
        }

        public bool Equals(GaEuclideanVector3D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj is GaEuclideanVector3D other && Equals(other);
        }

        public bool Equals(GaEuclideanVector3D other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Vector(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(", ")
                .Append(Z.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}