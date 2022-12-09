using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrNormal3D : 
        IFloat64Tuple3D
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrNormal3D()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrNormal3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrNormal3D(IFloat64Tuple3D normal)
        {
            X = normal.X;
            Y = normal.Y;
            Z = normal.Z;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y) &&
                   !double.IsNaN(Z);
        }

        /// <summary>
        /// Reset the normal to zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="normalTriplet"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(ITriplet<double> normalTriplet)
        {
            X = normalTriplet.Item1;
            Y = normalTriplet.Item2;
            Z = normalTriplet.Item3;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Add the given vector to the normal of this vertex
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(double dx, double dy, double dz)
        {
            X += dx;
            Y += dy;
            Z += dz;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Add the given vector to this normal
        /// </summary>
        /// <param name="normalTriplet"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ITriplet<double> normalTriplet)
        {
            X += normalTriplet.Item1;
            Y += normalTriplet.Item2;
            Z += normalTriplet.Item3;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Make the normal vector of this vertex a unit vector if not near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return;

            s = 1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Reverse the direction of the normal and make its length 1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeNegativeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y + Z * Z);
            if (s.IsAlmostZero())
                return;

            s = -1.0d / s;
            X *= s;
            Y *= s;
            Z *= s;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Reverse the direction of the normal
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrNormal3D GetNegative()
        {
            return new GrNormal3D(-X, -Y, -Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G}, {Z:G})";
        }
    }
}