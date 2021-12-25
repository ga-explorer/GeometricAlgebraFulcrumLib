using System;
using System.Diagnostics;
using System.Text;
using NumericalGeometryLib.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space3D
{
    public sealed record EuclideanVector3D : 
        ITuple3D,
        IGeometricAlgebraElement<double>
    {
        public static EuclideanVector3D operator -(EuclideanVector3D v1)
        {
            return new EuclideanVector3D(-v1.X, -v1.Y, -v1.Z);
        }

        public static EuclideanVector3D operator +(EuclideanVector3D v1, EuclideanVector3D v2)
        {
            return new EuclideanVector3D(
                v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        public static EuclideanVector3D operator -(EuclideanVector3D v1, EuclideanVector3D v2)
        {
            return new EuclideanVector3D(
                v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z
            );
        }

        public static EuclideanVector3D operator *(double s1, EuclideanVector3D p2)
        {
            return new EuclideanVector3D(
                s1 * p2.X,
                s1 * p2.Y,
                s1 * p2.Z
            );
        }

        public static EuclideanVector3D operator *(EuclideanVector3D p1, double s2)
        {
            return new EuclideanVector3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        public static EuclideanVector3D operator /(EuclideanVector3D p1, double s2)
        {
            s2 = 1d / s2;

            return new EuclideanVector3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        //public static bool operator ==(GeoEuclideanVector3D v1, GeoEuclideanVector3D v2)
        //{
        //    return v1.Equals(v2);
        //}

        //public static bool operator !=(GeoEuclideanVector3D v1, GeoEuclideanVector3D v2)
        //{
        //    return !v1.Equals(v2);
        //}
        

        public IGeometricAlgebraProcessor<double> GeometricProcessor 
            => GeometricAlgebraEuclideanSpace3DUtils.GeometricProcessor;

        public IScalarAlgebraProcessor<double> ScalarProcessor 
            => ScalarAlgebraFloat64Processor.DefaultProcessor;

        public ILinearAlgebraProcessor<double> LinearProcessor 
            => GeometricProcessor;

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public bool IsValid()
            => !double.IsNaN(X) &&
               !double.IsNaN(Y) && 
               !double.IsNaN(Z);


        public EuclideanVector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }


        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double GetLengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double GetDotProduct(EuclideanVector3D v2)
        {
            return X * v2.X + Y * v2.Y + Z * v2.Z;
        }

        public double GetAngleWith(EuclideanVector3D v2)
        {
            return Math.Acos(
                (X * v2.X + Y * v2.Y + Z * v2.Z) / 
                Math.Sqrt(
                    (X * X + Y * Y + Z * Z) *
                    (v2.X * v2.X + v2.Y * v2.Y + v2.Z * v2.Z)
                )
            );
        }

        public EuclideanPoint3D AsPoint()
        {
            return new EuclideanPoint3D(X, Y, Z);
        }

        //public bool Equals(GeoEuclideanVector3D? other)
        //{
        //    return other.HasValue && Equals(other.Value);
        //}

        //public override bool Equals(object? obj)
        //{
        //    if (ReferenceEquals(obj, null))
        //        return false;

        //    return obj is GeoEuclideanVector3D other && Equals(other);
        //}

        //public bool Equals(GeoEuclideanVector3D other)
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