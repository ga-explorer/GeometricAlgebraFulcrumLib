using System;
using System.Diagnostics;
using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsNormal3D : 
        IGraphicsNormal3D
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public bool IsValid 
            => !double.IsNaN(X) &&
               !double.IsNaN(Y) &&
               !double.IsNaN(Z);

        public bool IsInvalid 
            => double.IsNaN(X) ||
               double.IsNaN(Y) ||
               double.IsNaN(Z);


        public GraphicsNormal3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid);
        }


        public void Reset()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        public void Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid);
        }

        public void Set(ITuple3D normal)
        {
            X = normal.X;
            Y = normal.Y;
            Z = normal.Z;

            Debug.Assert(IsValid);
        }

        public void Update(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;

            Debug.Assert(IsValid);
        }

        public void Update(ITuple3D dNormal)
        {
            X += dNormal.X;
            Y += dNormal.Y;
            Z += dNormal.Z;

            Debug.Assert(IsValid);
        }

        public void MakeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return;

            s = 1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(IsValid);
        }

        public void MakeNegativeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return;

            s = -1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(IsValid);
        }

        public void MakeNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            Debug.Assert(IsValid);
        }
    }
}