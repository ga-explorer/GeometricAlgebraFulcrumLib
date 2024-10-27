﻿#region copyright
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


using Threading = GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core.Threading;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Functions
{
    internal static class MatrixMultiplication<T, TWrapper> where TWrapper : struct, Core.IOperations<T>
    {
        internal static Core.GenTensor<T, TWrapper> Multiply(Core.GenTensor<T, TWrapper> a, Core.GenTensor<T, TWrapper> b, Threading threading = Threading.Single)
        {
            #if ALLOW_EXCEPTIONS
            if (!a.IsMatrix || !b.IsMatrix)
                throw new InvalidShapeException($"Both {nameof(a)} and {nameof(b)} should be matrices");
            if (a.Shape[1] != b.Shape[0])
                throw new InvalidShapeException($"{nameof(a)}'s height must be equal to {nameof(b)}'s width");
            #endif

            var width = a.Shape[0];
            var height = b.Shape[1];
            var row = a.Shape[1];
            var res = Constructors<T, TWrapper>.CreateMatrix(width, height);

            var parallel = threading == Threading.Multi || (threading == Threading.Auto && a.Volume > 125);

            
            var aBlocks0 = a.Blocks[0];
            var aBlocks1 = a.Blocks[1];
            var bBlocks0 = b.Blocks[0];
            var bBlocks1 = b.Blocks[1];
            var aLinoffset = a.LinOffset;
            var bLinoffset = b.LinOffset;
            
            if (!parallel)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        var s = default(TWrapper).CreateZero();
                        for (int i = 0; i < row; i++)
                        {
                            var v1 = a.Data[x * aBlocks0 + i * aBlocks1 + aLinoffset];
                            var v2 = b.Data[i * bBlocks0 + y * bBlocks1 + bLinoffset];
                            s = default(TWrapper).Add(s, default(TWrapper).Multiply(v1, v2));
                        }
                        res.Data[x * height + y] = s;
                    }
                }
            }
            else
            {
                Parallel.For(0, width, x =>
                {
                    for (int y = 0; y < height; y++)
                    {
                        var s = default(TWrapper).CreateZero();
                        for (int i = 0; i < row; i++)
                        {
                            var v1 = a.Data[x * aBlocks0 + i * aBlocks1 + aLinoffset];
                            var v2 = b.Data[i * bBlocks0 + y * bBlocks1 + bLinoffset];
                            s = default(TWrapper).Add(s, default(TWrapper).Multiply(v1, v2));
                        }
                        res.Data[x * height + y] = s;
                    }
                });
            }

            return res;
        }

        public static Core.GenTensor<T, TWrapper> TensorMultiply(Core.GenTensor<T, TWrapper> a, Core.GenTensor<T, TWrapper> b, Threading threading = Threading.Single)
        {
            #if ALLOW_EXCEPTIONS
            if (a.Shape.Count < 2 || b.Shape.Count < 2)
                throw new InvalidShapeException($"Arguments should be at least matrices while their shapes are {a.Shape} and {b.Shape}");
            if (a.Shape.SubShape(0, 2) != b.Shape.SubShape(0, 2))
                throw new InvalidShapeException("Other dimensions of tensors should be equal");
            #endif
            var oldShape = a.Shape.SubShape(0, 2).ToArray();
            var newShape = new int[oldShape.Length + 2];
            for (int i = 0; i < oldShape.Length; i++)
                newShape[i] = oldShape[i];
            newShape[newShape.Length - 2] = a.Shape[a.Shape.Length - 2];
            newShape[newShape.Length - 1] = b.Shape[b.Shape.Length - 1];
            var resTensor = new Core.GenTensor<T, TWrapper>(newShape);

            var parallel = threading == Threading.Multi || (threading == Threading.Auto && a.Volume > 300 && a.Shape[0] > 2);

            if (!parallel)
            {
                foreach (var subDimensions in a.IterateOverMatrices())
                {
                    var product = Multiply(a.GetSubtensor(subDimensions), b.GetSubtensor(subDimensions));
                    resTensor.SetSubtensor(product, subDimensions);
                }
            }
            else
            {
                var subdims = a.IterateOverCopy(2).ToArray();

                Parallel.For(0, subdims.Length, subId =>
                {
                    var subDimensions = subdims[subId];
                    var product = Multiply(a.GetSubtensor(subDimensions), b.GetSubtensor(subDimensions), Threading.Single);
                    resTensor.SetSubtensor(product, subDimensions);
                });
            }
            return resTensor;
        }
    }
}
