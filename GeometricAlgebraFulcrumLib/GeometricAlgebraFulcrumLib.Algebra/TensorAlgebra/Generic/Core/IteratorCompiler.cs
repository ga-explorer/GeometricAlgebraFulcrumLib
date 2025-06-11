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


using System.ComponentModel;
using System.Linq.Expressions;

namespace GeometricAlgebraFulcrumLib.Algebra.TensorAlgebra.Generic.Core
{
    internal static class ExpressionCompiler<T, TWrapper> where TWrapper : struct, IOperations<T>
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
        public static readonly Func<Expression, Expression, Expression> Addition
            = (a, b) => Expression.Call(
                Expression.Default(typeof(TWrapper)),
                typeof(IOperations<T>).GetMethod(nameof(IOperations<T>.Add)) ?? throw new InvalidOperationException(),
                a, b
            );

        public static readonly Func<Expression, Expression, Expression> Subtraction
            = (a, b) => Expression.Call(
                Expression.Default(typeof(TWrapper)),
                typeof(IOperations<T>).GetMethod(nameof(IOperations<T>.Subtract))!,
                a, b
            );

        public static readonly Func<Expression, Expression, Expression> Multiplication
            = (a, b) => Expression.Call(
                Expression.Default(typeof(TWrapper)),
                typeof(IOperations<T>).GetMethod(nameof(IOperations<T>.Multiply))!,
                a, b
            );

        public static readonly Func<Expression, Expression, Expression> Division
            = (a, b) => Expression.Call(
                Expression.Default(typeof(TWrapper)),
                typeof(IOperations<T>).GetMethod(nameof(IOperations<T>.Divide))!,
                a, b
            );
#pragma warning restore CA2211 // Non-constant fields should not be visible
        private static Expression CreateLoop(ParameterExpression var, Expression until, Expression onIter)
        {
            var label = Expression.Label();

            var loop = Expression.Loop(
                Expression.Block(
                    Expression.IfThenElse(

                    Expression.LessThan(var, until),

                    Expression.Block(
                        onIter
                    ),

                    Expression.Break(label)
                ),
                Expression.PostIncrementAssign(var)
                    ),
                label);


            return Expression.Block(
                Expression.Assign(var, Expression.Constant(0)),
                loop
                );
        }

        private static Expression CompileNestedLoops(Expression[] shapes, Func<ParameterExpression[], Expression> onIter)
        {
            var acts = new List<Expression>();

            var locals = new ParameterExpression[shapes.Length];
            for (var i = 0; i < shapes.Length; i++)
                locals[i] = Expression.Parameter(typeof(int), "x_" + i);

            var localShapes = new ParameterExpression[shapes.Length];
            for (var i = 0; i < shapes.Length; i++)
                localShapes[i] = Expression.Parameter(typeof(int), "shape_" + i);
            var localShapesAssigned = new Expression[shapes.Length];
            for (var i = 0; i < shapes.Length; i++)
                localShapesAssigned[i] = Expression.Assign(localShapes[i], shapes[i]);

            var currExpr = onIter(locals);

            for (var i = shapes.Length - 1; i >= 0; i--)
            {
                currExpr = CreateLoop(locals[i], localShapes[i], currExpr);
            }

            acts.AddRange(localShapesAssigned);
            acts.Add(currExpr);

            var localVariables = new List<ParameterExpression>();
            localVariables.AddRange(locals);
            localVariables.AddRange(localShapes);

            return Expression.Block(localVariables, Expression.Block(acts));
        }

        private static Expression CompileNestedLoops(BinaryExpression[] shapes, Func<ParameterExpression[], Expression> onIter,
            bool parallel, ParameterExpression[] localsToPass)
        {
            if (!parallel)
                return CompileNestedLoops(shapes, onIter);
            var x = Expression.Parameter(typeof(int), "outerLoopVar");

            var shape0 = shapes[0];
            var others = new ArraySegment<Expression>(shapes, 1, shapes.Length - 1).ToArray();
            var compiledLoops = CompileNestedLoops(others, NewOnIter);

            var newArgs = localsToPass.ToList().Append(x).ToArray();

            var del = Expression.Lambda(compiledLoops, newArgs);

            var actions = new List<Expression>();

            var localDelCall = Expression.Invoke(del, newArgs);

            var finalLambda = Expression.Lambda(
                localDelCall,
                x
            );

            var enu = typeof(Parallel)
                .GetMethods()
                .Where(mi => mi.Name == nameof(Parallel.For))
                .Where(mi => mi.GetParameters().Length == 3)
                .Where(mi => mi.GetParameters()[2].ParameterType == typeof(Action<int>)).ToArray();
            if (enu.Length != 1)
                throw new InvalidEnumArgumentException();

            var mi = enu.FirstOrDefault();

            var call = Expression.Call(null, mi, Expression.Constant(0), shape0, finalLambda);
            return call;

            Expression NewOnIter(ParameterExpression[] expressions)
            {
                var arr = new ParameterExpression[expressions.Length + 1];
                arr[0] = x;
                for (var i = 0; i < expressions.Length; i++)
                    arr[i + 1] = expressions[i];
                return onIter(arr);
            }
        }

        private static Expression BuildIndexToData(ParameterExpression[] vars, ParameterExpression[] blocks)
        {
            if (vars.Length != blocks.Length)
                throw new Exceptions.InvalidShapeException();
            Expression res = Expression.Multiply(vars[0], blocks[0]);
            for (var i = 1; i < blocks.Length; i++)
                res = Expression.Add(Expression.Multiply(vars[i], blocks[i]), res);
            return res;
        }

        private static (ParameterExpression[] locals, Expression[] assignes) CompileLocalBlocks(ParameterExpression arr, int n, string pref)
        {
            var blocks = new ParameterExpression[n];
            for (var i = 0; i < n; i++)
                blocks[i] = Expression.Parameter(typeof(int), pref + "blocks_" + i);
            var assignes = new Expression[n];
            for (var i = 0; i < n; i++)
            {
                assignes[i] = Expression.Assign(blocks[i],
                    Expression.ArrayIndex(
                        Expression.Field(arr, typeof(GenTensor<T, TWrapper>).GetField(nameof(GenTensor<T, TWrapper>.Blocks))!)
                        ,
                        Expression.Constant(i)
                    )
                );
            }
            return (blocks, assignes);
        }

        private static Action<GenTensor<T, TWrapper>, GenTensor<T, TWrapper>, GenTensor<T, TWrapper>> CompileForNDimensions(int n, Func<Expression, Expression, Expression> operation, bool parallel)
        {
            var a = Expression.Parameter(typeof(GenTensor<T, TWrapper>), "a");
            var b = Expression.Parameter(typeof(GenTensor<T, TWrapper>), "b");
            var res = Expression.Parameter(typeof(GenTensor<T, TWrapper>), "res");

            var aData = Expression.Parameter(typeof(T[]), "aData");
            var bData = Expression.Parameter(typeof(T[]), "bData");
            var resData = Expression.Parameter(typeof(T[]), "resData");

            var actions = new List<Expression>();

            var (localABlocks, actABlocks) = CompileLocalBlocks(a, n, "a");

            // ablocks_0 = a.blocks[0]
            // ablocks_1 = a.blocks[1]
            // ...
            actions.AddRange(actABlocks);

            var (localBBlocks, actBBlocks) = CompileLocalBlocks(b, n, "b");

            // bblocks_0 = b.blocks[0]
            // bblocks_1 = b.blocks[1]
            // ...
            actions.AddRange(actBBlocks);
            var (localResBlocks, actResBlocks) = CompileLocalBlocks(res, n, "res");

            // resblocks_0 = res.blocks[0]
            // resblocks_1 = res.blocks[1]
            // ...
            actions.AddRange(actResBlocks);

            var fieldInfo = typeof(GenTensor<T, TWrapper>).GetField(nameof(GenTensor<T, TWrapper>.Data));

            // aData = a.data
            actions.Add(Expression.Assign(aData, Expression.Field(a, fieldInfo)));

            // bData = b.data
            actions.Add(Expression.Assign(bData, Expression.Field(b, fieldInfo)));

            // resData = res.data
            actions.Add(Expression.Assign(resData, Expression.Field(res, fieldInfo)));

            var localALinOffset = Expression.Parameter(typeof(int), "aLin");

            // aLin = a.LinOffset
            actions.Add(Expression.Assign(localALinOffset,
                Expression.Field(a, typeof(GenTensor<T, TWrapper>).GetField(nameof(GenTensor<T, TWrapper>.LinOffset))!)));

            var localBLinOffset = Expression.Parameter(typeof(int), "bLin");

            // bLin = b.LinOffset
            actions.Add(Expression.Assign(localBLinOffset,
                Expression.Field(b, typeof(GenTensor<T, TWrapper>).GetField(nameof(GenTensor<T, TWrapper>.LinOffset))!)));

            Expression OnIter(ParameterExpression[] vars)
            {
                // ablocks_0 * x_0 + ablocks_1 * x_1 + ...
                var aIndex = BuildIndexToData(vars, localABlocks);

                // bblocks_0 * x_0 + bblocks_1 * x_1 + ...
                var bIndex = BuildIndexToData(vars, localBBlocks);

                // + aLinOffset
                aIndex = Expression.Add(aIndex, localALinOffset);

                // + bLinOffset
                bIndex = Expression.Add(bIndex, localBLinOffset);

                // a.data

                // d.data

                // a.data[aIndex]
                var aDataIndex = Expression.ArrayIndex(aData, aIndex);

                // b.data[bIndex]
                var bDataIndex = Expression.ArrayIndex(bData, bIndex);

                // a.data[aIndex] + b.data[bIndex]
                var added = operation(aDataIndex, bDataIndex);

                // resblocks_0 * x_0 + ...
                var resIndex = BuildIndexToData(vars, localResBlocks);

                // res.data

                // res.data[resIndex] = 
                var accessRes = Expression.ArrayAccess(resData, resIndex);

                var assign = Expression.Assign(accessRes, added);

                return assign;
            }

            var locals = new List<ParameterExpression>();
            locals.AddRange(localABlocks);
            locals.AddRange(localBBlocks);
            locals.AddRange(localResBlocks);
            locals.Add(localALinOffset);
            locals.Add(localBLinOffset);
            locals.Add(aData);
            locals.Add(bData);
            locals.Add(resData);

            var shapeInfo = typeof(GenTensor<T, TWrapper>).GetProperty(nameof(GenTensor<T, TWrapper>.Shape));
            var shapeFieldInfo = typeof(TensorShape).GetField(nameof(TensorShape.Shape));
            var shapeProperty = Expression.Property(res, shapeInfo!);
            var shapeField = Expression.Field(shapeProperty, shapeFieldInfo!);

            var loops = CompileNestedLoops(
                Enumerable.Range(0, n).Select(
                    id =>
                        Expression.ArrayIndex(
                            shapeField,
                            Expression.Constant(id))
                ).ToArray(),
                OnIter, parallel, locals.ToArray()
            );

            actions.Add(loops);

            Expression bl = Expression.Block(
                locals, actions.ToArray()
                );

            if (bl.CanReduce)
                bl = bl.Reduce();

            return Expression.Lambda<Action<GenTensor<T, TWrapper>, GenTensor<T, TWrapper>, GenTensor<T, TWrapper>>>(bl, a, b, res).Compile();
        }

        public enum OperationType
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }

        private static readonly Dictionary<(OperationType opId, int N, bool parallel), Action<GenTensor<T, TWrapper>, GenTensor<T, TWrapper>, GenTensor<T, TWrapper>>> Storage
                 = new Dictionary<(OperationType opId, int N, bool parallel), Action<GenTensor<T, TWrapper>, GenTensor<T, TWrapper>, GenTensor<T, TWrapper>>>();

        private static Action<GenTensor<T, TWrapper>, GenTensor<T, TWrapper>, GenTensor<T, TWrapper>> GetFunc(int n, Func<Expression, Expression, Expression> operation, bool parallel, OperationType ot)
        {
            var key = (ot, N: n, parallel);
            if (!Storage.ContainsKey(key))
                Storage[key] = CompileForNDimensions(n, operation, parallel);
            return Storage[key];
        }

        public static GenTensor<T, TWrapper> PiecewiseAdd(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, bool parallel)
        {

#if ALLOW_EXCEPTIONS
            if (a.Shape != b.Shape)
                throw new InvalidShapeException();
#endif
            if (a.Shape.Length == 0)
                return GenTensor<T, TWrapper>.CreateTensor(a.Shape, _ => default(TWrapper).Add(a.Data[a.LinOffset], b.Data[b.LinOffset]));
            var res = new GenTensor<T, TWrapper>(a.Shape);
            GetFunc(a.Shape.Length, Addition, parallel, OperationType.Addition)(a, b, res);
            return res;
        }

        public static GenTensor<T, TWrapper> PiecewiseSubtract(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, bool parallel)
        {
#if ALLOW_EXCEPTIONS
            if (a.Shape != b.Shape)
                throw new InvalidShapeException();
#endif
            if (a.Shape.Length == 0)
                return GenTensor<T, TWrapper>.CreateTensor(a.Shape, _ => default(TWrapper).Subtract(a.Data[a.LinOffset], b.Data[b.LinOffset]));
            var res = new GenTensor<T, TWrapper>(a.Shape);
            GetFunc(a.Shape.Length, Subtraction, parallel, OperationType.Subtraction)(a, b, res);
            return res;
        }

        public static GenTensor<T, TWrapper> PiecewiseMultiply(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, bool parallel)
        {
#if ALLOW_EXCEPTIONS
            if (a.Shape != b.Shape)
                throw new InvalidShapeException();
#endif
            if (a.Shape.Length == 0)
                return GenTensor<T, TWrapper>.CreateTensor(a.Shape, _ => default(TWrapper).Multiply(a.Data[a.LinOffset], b.Data[b.LinOffset]));
            var res = new GenTensor<T, TWrapper>(a.Shape);
            GetFunc(a.Shape.Length, Multiplication, parallel, OperationType.Multiplication)(a, b, res);
            return res;
        }

        public static GenTensor<T, TWrapper> PiecewiseDivision(GenTensor<T, TWrapper> a, GenTensor<T, TWrapper> b, bool parallel)
        {
#if ALLOW_EXCEPTIONS
            if (a.Shape != b.Shape)
                throw new InvalidShapeException();
#endif
            if (a.Shape.Length == 0)
                return GenTensor<T, TWrapper>.CreateTensor(a.Shape, _ => default(TWrapper).Divide(a.Data[a.LinOffset], b.Data[b.LinOffset]));
            var res = new GenTensor<T, TWrapper>(a.Shape);
            GetFunc(a.Shape.Length, Division, parallel, OperationType.Division)(a, b, res);
            return res;
        }
    }
}
