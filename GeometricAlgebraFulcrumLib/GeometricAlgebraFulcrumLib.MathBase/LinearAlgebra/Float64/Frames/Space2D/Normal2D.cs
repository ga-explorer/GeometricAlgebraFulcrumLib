using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space2D
{
    public sealed class Normal2D :
        IFloat64Tuple2D
    {
        public Float64Scalar X { get; private set; }

        public Float64Scalar Y { get; private set; }
        
        public int VSpaceDimensions 
            => 2;

        public double Item1
            => X.Value;

        public double Item2
            => Y.Value;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Normal2D()
        {
            X = 0;
            Y = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Normal2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Normal2D(IFloat64Tuple2D normal)
        {
            X = normal.X;
            Y = normal.Y;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y);
        }

        /// <summary>
        /// Reset the normal to zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            X = 0;
            Y = 0;
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

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Set the normal to the given value
        /// </summary>
        /// <param name="normalPair"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(IPair<double> normalPair)
        {
            X = normalPair.Item1;
            Y = normalPair.Item2;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Add the given vector to the normal of this vertex
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(double dx, double dy)
        {
            X += dx;
            Y += dy;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Add the given vector to this normal
        /// </summary>
        /// <param name="normalPair"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(IPair<double> normalPair)
        {
            X += normalPair.Item1;
            Y += normalPair.Item2;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Make the normal vector of this vertex a unit vector if not near zero
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y);
            if (s.IsAlmostZero())
                return;

            s = 1.0d / s;
            X *= s;
            Y *= s;

            Debug.Assert(IsValid());
        }

        /// <summary>
        /// Reverse the direction of the normal and make its length 1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MakeNegativeUnit()
        {
            var s = Math.Sqrt(X * X + Y * Y);
            if (s.IsAlmostZero())
                return;

            s = -1.0d / s;
            X *= s;
            Y *= s;

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

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Normal2D GetNegative()
        {
            return new Normal2D(-X, -Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G})";
        }
    }
}