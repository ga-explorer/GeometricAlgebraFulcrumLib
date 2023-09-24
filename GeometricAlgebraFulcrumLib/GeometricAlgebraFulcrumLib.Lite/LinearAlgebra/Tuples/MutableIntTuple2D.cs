using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples
{
    public sealed class MutableIntTuple2D : IEnumerable<int>
    {
        /// <summary>
        /// First Component of tuple
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Second Component of tuple
        /// </summary>
        public int Y { get; set; }


        /// <summary>
        /// The squared Euclidean length of this tuple if it represents a vector
        /// </summary>
        public int VectorLengthSquared => X * X + Y * Y;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0;
            }
        }

        /// <summary>
        /// True of the Euclidean squared length of this vector is zero
        /// </summary>
        public bool IsZeroVector => VectorLengthSquared == 0;

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        public int MinComponent => X < Y ? X : Y;

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        public int MaxComponent => X > Y ? X : Y;

        /// <summary>
        /// The index of the smallest component in this tuple
        /// </summary>
        public int MinComponentIndex => X < Y ? 0 : 1;

        /// <summary>
        /// The index of the largest component in this tuple
        /// </summary>
        public int MaxComponentIndex => X > Y ? 0 : 1;


        public MutableIntTuple2D()
        {
        }

        public MutableIntTuple2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MutableIntTuple2D(IntTuple2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
        }


        public IEnumerator<int> GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        public bool Equals(IntTuple2D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString())
                .Append(", ")
                .Append(Y.ToString())
                .Append(")")
                .ToString();
        }
    }
}