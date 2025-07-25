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


using HonkPerf.NET.Core;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Functions
{
    internal static class Composition<T, TWrapper> where TWrapper : struct, Core.IOperations<T>
    {
        public static Core.GenTensor<T, TWrapper> Stack(params Core.GenTensor<T, TWrapper>[] elements)
        {
            #if ALLOW_EXCEPTIONS
            if (elements.Length < 1)
                throw new InvalidShapeException("Shoud be at least one element to stack");
            #endif
            var desiredShape = elements[0].Shape;
            #if ALLOW_EXCEPTIONS
            for (int i = 1; i < elements.Length; i++)
                if (elements[i].Shape != desiredShape)
                    throw new InvalidShapeException($"Tensors in {nameof(elements)} should be of the same shape");
            #endif
            var newShape = new int[desiredShape.Count + 1];
            newShape[0] = elements.Length;
            for (var i = 1; i < newShape.Length; i++)
                newShape[i] = desiredShape[i - 1];
            var res = new Core.GenTensor<T, TWrapper>(newShape);
            for (var i = 0; i < elements.Length; i++)
                res.SetSubtensor(elements[i], i);
            return res;
        }

        public static Core.GenTensor<T, TWrapper> Concat(Core.GenTensor<T, TWrapper> a, Core.GenTensor<T, TWrapper> b)
        {
            #if ALLOW_EXCEPTIONS
            if (a.Shape.SubShape(1, 0) != b.Shape.SubShape(1, 0))
                throw new InvalidShapeException("Excluding the first dimension, all others should match");
            #endif

            if (a.IsVector)
            {
                var resultingVector = Core.GenTensor<T, TWrapper>.CreateVector(a.Shape.Shape[0] + b.Shape.Shape[0]);
                for (var i = 0; i < a.Shape.Shape[0]; i++)
                    resultingVector.SetValueNoCheck(a.GetValueNoCheck(i), i);

                for (var i = 0; i < b.Shape.Shape[0]; i++)
                    resultingVector.SetValueNoCheck(b.GetValueNoCheck(i), i + a.Shape.Shape[0]);

                return resultingVector;
            }
            else
            {
                var newShape = a.Shape.Copy();
                newShape.Shape[0] = a.Shape.Shape[0] + b.Shape.Shape[0];

                var res = new Core.GenTensor<T, TWrapper>(newShape);
                for (var i = 0; i < a.Shape.Shape[0]; i++)
                    res.SetSubtensor(a.GetSubtensor(i), i);

                for (var i = 0; i < b.Shape.Shape[0]; i++)
                    res.SetSubtensor(b.GetSubtensor(i), i + a.Shape.Shape[0]);

                return res;
            }
        }

        private struct AggregateFunctor<U, UWrapper, TAggregatorFunc> : IValueAction<int[], T>
            where TAggregatorFunc : struct, IValueDelegate<U, T, U>
            where UWrapper : struct, Core.IOperations<U>
        {
            private TAggregatorFunc collapse;
            private Core.GenTensor<U, UWrapper> acc;
            public AggregateFunctor(TAggregatorFunc collapse, Core.GenTensor<U, UWrapper> acc)
            {
                this.collapse = collapse;
                this.acc = acc;
            }
            public void Invoke(int[] arg1, T arg2)
            {
                acc.SetValueNoCheck(collapse.Invoke(acc.GetValueNoCheck(arg1), arg2), arg1);
            }
        }
        
        public static void Aggregate<TAggregatorFunc, U, UWrapper>(Core.GenTensor<T, TWrapper> t, Core.GenTensor<U, UWrapper> acc, TAggregatorFunc collapse, int axis)
            where TAggregatorFunc : struct, IValueDelegate<U, T, U>
            where UWrapper : struct, Core.IOperations<U>
        {
            for (var i = axis; i > 0; i--)
                t.Transpose(i, i - 1); // Move the axis we want to reduce to the front for GetSubtensor. Order is important since it is directly reflected in the output shape.
            /*
            // old code with iterate
            // not removing for now
            for (int i = 0; i < t.Shape[0]; i++)
                foreach (var (id, value) in t.GetSubtensor(i).Iterate())
                    acc[id] = collapse.Invoke(acc[id], value);
            */
            for (var i = 0; i < t.Shape[0]; i++)
                t.GetSubtensor(i).ForEach(new AggregateFunctor<U, UWrapper, TAggregatorFunc>(collapse, acc));
            for (var i = 0; i < axis; i++)
                t.Transpose(i, i + 1);
        }
    }
}
    