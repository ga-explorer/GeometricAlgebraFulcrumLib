#region copyright
/*
 * MIT License
 * 
 * Copyright (c) 2020-2021 WhiteBlackGoose
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#endregion


using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core
{
    /// <summary>
    /// This structure represents a shape of a tensor
    /// </summary>
    public readonly struct TensorShape : 
        IEquatable<TensorShape>
    {
        /// <summary>
        /// Internal variable. Not recommended to change
        /// </summary>
        public readonly int[] Shape;

        /// <summary>
        /// Length of a shape is basically number of dimensions
        /// </summary>
        public int Length 
            => Shape.Length;

        /// <summary>
        /// Synonym for Length
        /// </summary>
        public int DimensionCount 
            => Shape.Length;

        /// <summary>
        /// Synonym for Length
        /// </summary>
        public int Count 
            => Shape.Length;

        /// <summary>
        /// Create a TensorShape for further operations
        /// just listing necessary dimensions
        /// </summary>
        /// <param name="shape"></param>
        public TensorShape(params int[] shape)
        {
            Shape = shape;
        }

        internal TensorShape CutOffset1()
        {
            var newShape = new int[Length - 1];

            for (var i = 0; i < newShape.Length; i++)
                newShape[i] = Shape[i + 1];

            return new TensorShape(newShape);
        }

        /// <summary>
        /// Gets a subshape as a subsequence with the given
        /// left and right offsets
        /// </summary>
        public TensorShape SubShape(int offsetFromLeft, int offsetFromRight)
        {
            var newShape = new int[Length - offsetFromLeft - offsetFromRight];
            for (var i = offsetFromLeft; i < Length - offsetFromRight; i++)
                newShape[i - offsetFromLeft] = Shape[i];
            return new TensorShape(newShape);
        }

        /// <summary>
        /// Copies the shape, including the internal array
        /// </summary>
        public TensorShape Copy()
        {
            var resI = Shape.ToArray();
            return new TensorShape(resI);
        }

        internal void Swap(int id1, int id2)
            => (Shape[id1], Shape[id2]) = (Shape[id2], Shape[id1]);

        /// <summary>
        /// You can only read some dimensions,
        /// otherwise it will cause unintended behaviour
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        public int this[int axisId] => Shape[axisId];

        /// <summary>
        /// Returns the shape's internal array's copy
        /// </summary>
        public int[] ToArray() => Shape.ToArray();

        /// <inheritdoc/>
        public override string ToString()
            => string.Join(" x ", Shape.Select(c => c.ToString()));


        #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(TensorShape sh)
        {
            if (sh.Length != Length)
                return false;
            for (var i = 0; i < sh.Length; i++)
                if (sh.Shape[i] != Shape[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
            => Shape.GetHashCode();

        public override bool Equals(object? obj)
            => obj is TensorShape ts && Equals(ts);

        public static bool operator ==(TensorShape a, TensorShape b)
            => a.Equals(b);

        public static bool operator !=(TensorShape a, TensorShape b)
            => !a.Equals(b);
        #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
