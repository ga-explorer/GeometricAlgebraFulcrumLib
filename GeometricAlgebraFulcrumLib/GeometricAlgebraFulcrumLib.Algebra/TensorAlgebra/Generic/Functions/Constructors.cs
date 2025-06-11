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
using TensorShape = GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.TensorShape;
using Threading = GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.Threading;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Functions
{
    internal static class Constructors<T, TWrapper> where TWrapper : struct, Core.IOperations<T>
    {
        /// <summary>
        /// Creates a tensor whose all matrices are identity matrices
        /// <para>1 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateOne"/></para>
        /// <para>0 is achieved with <see cref="GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.IOperations{T}.CreateZero"/></para>
        /// </summary>
        public static Core.GenTensor<T, TWrapper> CreateIdentityTensor(int[] dimensions, int finalMatrixDiag)
        {
            var newDims = new int[dimensions.Length + 2];
            for (var i = 0; i < dimensions.Length; i++)
                newDims[i] = dimensions[i];
            newDims[newDims.Length - 2] = newDims[newDims.Length - 1] = finalMatrixDiag;
            var res = new Core.GenTensor<T, TWrapper>(newDims);
            foreach (var index in res.IterateOverMatrices())
            {
                var iden = CreateIdentityMatrix(finalMatrixDiag);
                res.SetSubtensor(iden, index);
            }
            return res;
        }

        public static Core.GenTensor<T, TWrapper> CreateIdentityMatrix(int diag)
        {
            var res = new Core.GenTensor<T, TWrapper>(diag, diag);
            for (var i = 0; i < res.Data.Length; i++)
                res.Data[i] = default(TWrapper).CreateZero();

            for (var i = 0; i < diag; i++)
                res.SetValueNoCheck(default(TWrapper).CreateOne, i, i);
            return res;
        }

        public static Core.GenTensor<T, TWrapper> CreateVector(params T[] elements)
        {
            var res = new Core.GenTensor<T, TWrapper>(elements.Length);
            for (var i = 0; i < elements.Length; i++)
                res.SetValueNoCheck(elements[i], i);
            return res;
        }

        public static Core.GenTensor<T, TWrapper> CreateVector(int length)
        {
            var res = new Core.GenTensor<T, TWrapper>(length);
            return res;
        }

        private static (int height, int width) ExtractAndCheck(T[,] data)
        {
            var width = data.GetLength(0);
            #if ALLOW_EXCEPTIONS
            if (width < 0)
                throw new InvalidShapeException();
            #endif
            var height = data.GetLength(1);
            #if ALLOW_EXCEPTIONS
            if (height < 0)
                throw new InvalidShapeException();
            #endif
            return (width, height);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateMatrix(T[,] data)
        {
            var (width, height) = ExtractAndCheck(data);
            var res = new Core.GenTensor<T, TWrapper>(width, height);
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    res.SetValueNoCheck(data[x, y], x, y);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateSquareMatrix(int diagLength)
            => CreateMatrix(diagLength, diagLength);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateMatrix(int width, int height, Func<int, int, T> stepper)
        {
            var res = CreateMatrix(width, height);
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                    res.SetValueNoCheck(stepper(x, y), x, y);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateMatrix(int width, int height)
            => new Core.GenTensor<T, TWrapper>(width, height);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateTensor(TensorShape shape, Func<int[], T> operation, Threading threading)
        {
            var res = new Core.GenTensor<T, TWrapper>(shape);

            if (threading == Threading.Multi || threading == Threading.Auto && shape.Shape[0] > 5)
            {
                var inds = res.IterateOverCopy(0).ToArray();
                Parallel.For(0, inds.Length, id =>
                {
                    var ind = inds[id];
                    res.SetValueNoCheck(operation(ind), ind);
                });
            }
            else
            {
                foreach (var ind in res.IterateOverElements())
                    res.SetValueNoCheck(operation(ind), ind);
            }
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateTensor(T[] data)
        {
            var res = new Core.GenTensor<T, TWrapper>(data.GetLength(0));
            for (var x = 0; x < data.GetLength(0); x++)
                res.SetValueNoCheck(data[x], x);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateTensor(T[,] data)
        {
            var res = new Core.GenTensor<T, TWrapper>(data.GetLength(0), data.GetLength(1));
            for (var x = 0; x < data.GetLength(0); x++)
                for (var y = 0; y < data.GetLength(1); y++)
                    res.SetValueNoCheck(data[x, y], x, y);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateTensor(T[,,] data)
        {
            var res = new Core.GenTensor<T, TWrapper>(data.GetLength(0),
                data.GetLength(1), data.GetLength(2));
            for (var x = 0; x < data.GetLength(0); x++)
                for (var y = 0; y < data.GetLength(1); y++)
                    for (var z = 0; z < data.GetLength(2); z++)
                        res.SetValueNoCheck(data[x, y, z], x, y, z);
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Core.GenTensor<T, TWrapper> CreateTensor(Array data)
        {
            var dimensions = new int[data.Rank];
            for (var i = 0; i < data.Rank; i++)
                dimensions[i] = data.GetLength(i);
            var res = new Core.GenTensor<T, TWrapper>(dimensions);
            if (res.Volume == 0) return res;

            dimensions = new int[data.Rank]; // Don't modify res
            var normalizedIndices = new int[data.Rank];
            var indices = new int[data.Rank];
            for (var i = 0; i < data.Rank; i++)
            {
                dimensions[i] = data.GetUpperBound(i);
                indices[i] = data.GetLowerBound(i);
            }
            var increment = indices.Length - 1;
            while (true)
            {
                for (var i = increment; indices[i] > dimensions[i]; i--)
                    if (i == 0)
                        return res;
                    else
                    {
                        indices[i - 1]++;
                        indices[i] = data.GetLowerBound(i);
                        normalizedIndices[i - 1]++;
                        normalizedIndices[i] = 0;
                    }
                res.SetValueNoCheck((T)data.GetValue(indices), normalizedIndices);
                indices[increment]++;
                normalizedIndices[increment]++;
            }
        }
    }
}
