using System;
using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Geometry.Graphics.Space2D
{
    public sealed record EuclideanVector2D : 
        IFloat64Tuple2D
    {
        public static EuclideanVector2D operator -(EuclideanVector2D v1)
        {
            return new EuclideanVector2D(-v1.X, -v1.Y);
        }

        public static EuclideanVector2D operator +(EuclideanVector2D v1, EuclideanVector2D v2)
        {
            return new EuclideanVector2D(
                v1.X + v2.X,
                v1.Y + v2.Y
            );
        }

        public static EuclideanVector2D operator -(EuclideanVector2D v1, EuclideanVector2D v2)
        {
            return new EuclideanVector2D(
                v1.X - v2.X,
                v1.Y - v2.Y
            );
        }

        public static EuclideanVector2D operator *(double s1, EuclideanVector2D p2)
        {
            return new EuclideanVector2D(
                s1 * p2.X,
                s1 * p2.Y
            );
        }

        public static EuclideanVector2D operator *(EuclideanVector2D v1, double s2)
        {
            return new EuclideanVector2D(
                v1.X * s2,
                v1.Y * s2
            );
        }

        public static EuclideanVector2D operator /(EuclideanVector2D v1, double s2)
        {
            s2 = 1d / s2;

            return new EuclideanVector2D(
                v1.X * s2,
                v1.Y * s2
            );
        }

        //public static bool operator ==(GeoEuclideanVector2D v1, GeoEuclideanVector2D v2)
        //{
        //    return v1.Equals(v2);
        //}

        //public static bool operator !=(GeoEuclideanVector2D v1, GeoEuclideanVector2D v2)
        //{
        //    return !v1.Equals(v2);
        //}


        public IScalarProcessor<double> ScalarProcessor 
            => ScalarProcessorFloat64.DefaultProcessor;
        
        public RGaFloat64Processor GeometricProcessor 
            => GraphicsUtils.GeometricProcessor;

        public double X { get; }

        public double Y { get; }

        public double Item1 => X;

        public double Item2 => Y;


        public EuclideanVector2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }


        public bool IsValid() 
            => !double.IsNaN(X) &&
               !double.IsNaN(Y);


        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double GetLengthSquared()
        {
            return X * X + Y * Y;
        }

        public double GetDotProduct(EuclideanVector2D v2)
        {
            return X * v2.X + Y * v2.Y;
        }

        public double GetAngleWith(EuclideanVector2D v2)
        {
            return Math.Acos(
                (X * v2.X + Y * v2.Y) / 
                Math.Sqrt(
                    (X * X + Y * Y) *
                    (v2.X * v2.X + v2.Y * v2.Y)
                )
            );
        }

        public EuclideanPoint2D AsPoint()
        {
            return new EuclideanPoint2D(X, Y);
        }

        //public bool Equals(GeoEuclideanVector2D? other)
        //{
        //    return other.HasValue && Equals(other.Value);
        //}

        //public override bool Equals(object? obj)
        //{
        //    if (ReferenceEquals(obj, null))
        //        return false;

        //    return obj is GeoEuclideanVector2D other && Equals(other);
        //}

        //public bool Equals(GeoEuclideanVector2D other)
        //{
        //    return X.Equals(other.X) && Y.Equals(other.Y);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(X, Y);
        //}

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