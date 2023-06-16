using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples
{
    /// <summary>
    /// A 3-tuple of integer coordinates
    /// </summary>
    public sealed record IntTuple3D :
        IEnumerable<int>
    {
        public static IntTuple3D operator -(IntTuple3D v1)
        {
            return new IntTuple3D(-v1.ItemX, -v1.ItemY, -v1.ItemZ);
        }

        public static IntTuple3D operator +(IntTuple3D v1, IntTuple3D v2)
        {
            return new IntTuple3D(v1.ItemX + v2.ItemX, v1.ItemY + v2.ItemY, v1.ItemZ + v2.ItemZ);
        }

        public static IntTuple3D operator -(IntTuple3D v1, IntTuple3D v2)
        {
            return new IntTuple3D(v1.ItemX - v2.ItemX, v1.ItemY - v2.ItemY, v1.ItemZ - v2.ItemZ);
        }

        public static IntTuple3D operator *(IntTuple3D v1, int s)
        {
            return new IntTuple3D(v1.ItemX * s, v1.ItemY * s, v1.ItemZ * s);
        }

        public static IntTuple3D operator *(int s, IntTuple3D v1)
        {
            return new IntTuple3D(v1.ItemX * s, v1.ItemY * s, v1.ItemZ * s);
        }


        public int ItemX { get; }

        public int ItemY { get; }

        public int ItemZ { get; }

        /// <summary>
        /// The squared Euclidean length of this tuple if it represents a vector
        /// </summary>
        public int VectorLengthSquared
        {
            get { return ItemX * ItemX + ItemY * ItemY + ItemZ * ItemZ; }
        }

        /// <summary>
        /// True if the components of this tuple are zero
        /// </summary>
        public bool IsZeroVector
        {
            get { return VectorLengthSquared == 0; }
        }

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index <= 2);

                if (index == 0) return ItemX;
                if (index == 1) return ItemY;
                if (index == 2) return ItemZ;

                return 0;
            }
        }

        /// <summary>
        /// The value of the smallest component in this tuple
        /// </summary>
        public int MinComponent
        {
            get { return ItemX < ItemY ? ItemX < ItemZ ? ItemX : ItemZ : ItemY < ItemZ ? ItemY : ItemZ; }
        }

        /// <summary>
        /// The value of the largest component in this tuple
        /// </summary>
        public int MaxComponent
        {
            get { return ItemX > ItemY ? ItemX > ItemZ ? ItemX : ItemZ : ItemY > ItemZ ? ItemY : ItemZ; }
        }

        /// <summary>
        /// The index of the smallest component in this tuple
        /// </summary>
        public int MinComponentIndex
        {
            get { return ItemX < ItemY ? ItemX < ItemZ ? 0 : 2 : ItemY < ItemZ ? 1 : 2; }
        }

        /// <summary>
        /// The index of the largest component in this tuple
        /// </summary>
        public int MaxComponentIndex
        {
            get { return ItemX > ItemY ? ItemX > ItemZ ? 0 : 2 : ItemY > ItemZ ? 1 : 2; }
        }


        public IntTuple3D(int x, int y, int z)
        {
            ItemX = x;
            ItemY = y;
            ItemZ = z;
        }

        public IntTuple3D(IntTuple3D v)
        {
            ItemX = v.ItemX;
            ItemY = v.ItemY;
            ItemZ = v.ItemZ;
        }


        public IEnumerator<int> GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
            yield return ItemZ;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
            yield return ItemZ;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(ItemX.ToString())
                .Append(", ")
                .Append(ItemY.ToString())
                .Append(", ")
                .Append(ItemZ.ToString())
                .Append(")")
                .ToString();
        }
    }
}
