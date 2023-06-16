using System;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space3D
{
    public sealed record EuclideanPoint3D : 
        IFloat64Tuple3D
    {
        public static EuclideanVector3D operator -(EuclideanPoint3D p1)
        {
            return new EuclideanVector3D(-p1.X, -p1.Y, -p1.Z);
        }

        public static EuclideanPoint3D operator +(EuclideanPoint3D p1, EuclideanVector3D v2)
        {
            return new EuclideanPoint3D(
                p1.X + v2.X,
                p1.Y + v2.Y,
                p1.Z + v2.Z
            );
        }

        public static EuclideanPoint3D operator +(EuclideanPoint3D p1, EuclideanPoint3D p2)
        {
            return new EuclideanPoint3D(
                p1.X + p2.X,
                p1.Y + p2.Y,
                p1.Z + p2.Z
            );
        }

        public static EuclideanPoint3D operator -(EuclideanPoint3D p1, EuclideanVector3D v2)
        {
            return new EuclideanPoint3D(
                p1.X - v2.X,
                p1.Y - v2.Y,
                p1.Z - v2.Z
            );
        }

        public static EuclideanVector3D operator -(EuclideanPoint3D p1, EuclideanPoint3D p2)
        {
            return new EuclideanVector3D(
                p1.X - p2.X,
                p1.Y - p2.Y,
                p1.Z - p2.Z
            );
        }

        public static EuclideanPoint3D operator *(double s1, EuclideanPoint3D p2)
        {
            return new EuclideanPoint3D(
                s1 * p2.X,
                s1 * p2.Y,
                s1 * p2.Z
            );
        }

        public static EuclideanPoint3D operator *(EuclideanPoint3D p1, double s2)
        {
            return new EuclideanPoint3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        public static EuclideanPoint3D operator /(EuclideanPoint3D p1, double s2)
        {
            s2 = 1d / s2;

            return new EuclideanPoint3D(
                p1.X * s2,
                p1.Y * s2,
                p1.Z * s2
            );
        }

        //public static bool operator ==(GeoEuclideanPoint3D v1, GeoEuclideanPoint3D v2)
        //{
        //    return v1.Equals(v2);
        //}

        //public static bool operator !=(GeoEuclideanPoint3D v1, GeoEuclideanPoint3D v2)
        //{
        //    return !v1.Equals(v2);
        //}


        public IScalarProcessor<double> ScalarProcessor 
            => ScalarProcessorFloat64.DefaultProcessor;
        
        public RGaFloat64Processor GeometricProcessor 
            => GaEuclideanSpace3DUtils.GeometricProcessor;

        public int VSpaceDimensions 
            => 3;

        public Float64Scalar X { get; }

        public Float64Scalar Y { get; }

        public Float64Scalar Z { get; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public bool IsValid()
            => !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               !double.IsNaN(Z);


        public EuclideanPoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }


        public double GetDistance(EuclideanPoint3D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;
            var z = Z - p2.Z;

            return Math.Sqrt(x * x + y * y + z * z);
        }

        public double GetDistanceSquared(EuclideanPoint3D p2)
        {
            var x = X - p2.X;
            var y = Y - p2.Y;
            var z = Z - p2.Z;

            return x * x + y * y + z * z;
        }

        public EuclideanVector3D AsVector()
        {
            return new EuclideanVector3D(X, Y, Z);
        }
        
        //public bool Equals(GeoEuclideanPoint3D? other)
        //{
        //    return other.HasValue && Equals(other.Value);
        //}

        //public override bool Equals(object? obj)
        //{
        //    if (ReferenceEquals(obj, null))
        //        return false;

        //    return obj is GeoEuclideanPoint3D other && Equals(other);
        //}

        //public bool Equals(GeoEuclideanPoint3D other)
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
