using System;
using System.Diagnostics;
using System.Text;
using EuclideanGeometryLib.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space3D
{
    public sealed record GaEuclideanPoint3D : 
        ITuple3D,
        IGaGeometry<double>
    {
        public static GaEuclideanVector3D operator -(GaEuclideanPoint3D p1)
        {
            return new GaEuclideanVector3D(-p1.X, -p1.Y, -p1.Z);
        }

        public static GaEuclideanPoint3D operator +(GaEuclideanPoint3D p1, GaEuclideanVector3D v2)
        {
            return new GaEuclideanPoint3D(
                p1.X + v2.X,
                p1.Y + v2.Y,
                p1.Z + v2.Z
            );
        }

        public static GaEuclideanPoint3D operator +(GaEuclideanPoint3D p1, GaEuclideanPoint3D p2)
        {
            return new GaEuclideanPoint3D(
                p1.X + p2.X,
                p1.Y + p2.Y,
                p1.Z + p2.Z
            );
        }

        public static GaEuclideanPoint3D operator -(GaEuclideanPoint3D p1, GaEuclideanVector3D v2)
        {
            return new GaEuclideanPoint3D(
                p1.X - v2.X,
                p1.Y - v2.Y,
                p1.Z - v2.Z
            );
        }

        public static GaEuclideanVector3D operator -(GaEuclideanPoint3D p1, GaEuclideanPoint3D p2)
        {
            return new GaEuclideanVector3D(
                p1.X - p2.X,
                p1.Y - p2.Y,
                p1.Z - p2.Z
            );
        }

        public static GaEuclideanPoint3D operator *(double s1, GaEuclideanPoint3D p2)
        {
            return new GaEuclideanPoint3D(
                s1 * p2.X,
                s1 * p2.Y,
                s1 * p2.Z
            );
        }

        public static GaEuclideanPoint3D operator *(GaEuclideanPoint3D p1, double s2)
        {
            return new GaEuclideanPoint3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        public static GaEuclideanPoint3D operator /(GaEuclideanPoint3D p1, double s2)
        {
            s2 = 1d / s2;

            return new GaEuclideanPoint3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        //public static bool operator ==(GaEuclideanPoint3D v1, GaEuclideanPoint3D v2)
        //{
        //    return v1.Equals(v2);
        //}

        //public static bool operator !=(GaEuclideanPoint3D v1, GaEuclideanPoint3D v2)
        //{
        //    return !v1.Equals(v2);
        //}


        public uint VSpaceDimension 
            => 3;

        public ulong GaSpaceDimension
            => 8;

        public IGaProcessor<double> Processor 
            => GaEuclideanSpace3DUtils.Processor;

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


        public GaEuclideanPoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid);
        }


        public double GetDistance(GaEuclideanPoint3D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;
            var z = Z - p2.Z;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public double GetDistanceSquared(GaEuclideanPoint3D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;
            var z = Z - p2.Z;

            return x * x + y * y + z * z;
        }

        public GaEuclideanVector3D AsVector()
        {
            return new GaEuclideanVector3D(X, Y, Z);
        }
        
        //public bool Equals(GaEuclideanPoint3D? other)
        //{
        //    return other.HasValue && Equals(other.Value);
        //}

        //public override bool Equals(object? obj)
        //{
        //    if (ReferenceEquals(obj, null))
        //        return false;

        //    return obj is GaEuclideanPoint3D other && Equals(other);
        //}

        //public bool Equals(GaEuclideanPoint3D other)
        //{
        //    return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(X, Y, Z);
        //}

        public override string ToString()
        {
            return new StringBuilder()
                .Append("Point(")
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
