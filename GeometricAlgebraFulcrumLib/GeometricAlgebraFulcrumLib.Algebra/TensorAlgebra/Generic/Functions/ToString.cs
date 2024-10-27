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


using System.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Functions
{
    internal static class DefaultOverridings<T, TWrapper> where TWrapper : struct, Core.IOperations<T>
    {
        public static string InToString(Core.GenTensor<T, TWrapper> t)
        {
            if (t.IsMatrix)
            {
                var maxLength = -1;
                var stringArray = new string[t.Shape[0], t.Shape[1]];
                for (var i = 0; i < t.Shape[0]; i++)
                for (var j = 0; j < t.Shape[1]; j++)
                {
                    stringArray[i, j] = default(TWrapper).ToString(t.GetValueNoCheck(i, j));
                    maxLength = Math.Max(maxLength, stringArray[i, j].Length);
                }
                var rows = new List<string> { "Matrix[" + t.Shape + "]" };
                for (var i = 0; i < t.Shape[0]; i++)
                {
                    var s = "";
                    for (var j = 0; j < t.Shape[1]; j++)
                    {
                        var count = maxLength + 3 - stringArray[i, j].Length;
                        count = Math.Max(0, count);
                        s += stringArray[i, j];
                        for (var k = 0; k < count; k++)
                            s += " ";
                    }
                    rows.Add(s);
                }
                return string.Join("\n", rows);
            }
            if (t.IsVector)
            {
                var els = new List<string>();
                for (var i = 0; i < t.Shape[0]; i++)
                    els.Add(default(TWrapper).ToString(t.GetValueNoCheck(i)));
                return string.Join(" ", els);
            }
            var sb = new StringBuilder();
            sb.Append("Tensor[" + t.Shape + "] {\n");
            foreach (var index in t.IterateOverMatrices())
            {
                sb.Append("    ");
                sb.Append(t.GetSubtensor(index).ToString().Replace("\n", "\n    "));
                sb.Append("\n\n");
            }
            sb.Append("}");
            return sb.ToString();
        }

        // TODO: make it faster
        public static int GetHashCode(Core.GenTensor<T, TWrapper> t)
        {
            var res = 0;
            unchecked
            {
                res += t.Shape.GetHashCode();
                foreach (var (_, value) in t.Iterate())
                    res += value?.GetHashCode() ?? 0;
            }
            return res;
        }
    }
}
