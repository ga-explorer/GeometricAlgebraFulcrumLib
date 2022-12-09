using System.Linq;
using System.Runtime.CompilerServices;
using AngouriMath;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;

namespace GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra
{
    public static class ScalarAlgebraSymbolicProcessorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity ArrayToMatrixExpr(this Entity[,] exprArray)
        {
            return MathS.Matrix(exprArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity MatrixDeterminant(this Entity[,] exprArray)
        {
            return MathS.Matrix(exprArray).Determinant;
        }

        /// <summary>
        /// Convert the given Mathematica Entity object into a SteExpression object
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static SteExpression ToSimpleTextExpression(this Entity expr)
        {
            var isNumber = expr.EvaluableNumerical;
            var isSymbol = expr.IsSymbolic;

            if (isNumber)
            {
                return isSymbol
                    ? SteExpression.CreateSymbolicNumber(expr.ToString())
                    : SteExpression.CreateLiteralNumber(expr.ToString());
            }

            if (isSymbol)
                return SteExpression.CreateVariable(expr.ToString());

            if (expr.DirectChildren.Count == 0)
                return SteExpression.CreateFunction(expr.ToString());

            var args = new SteExpression[expr.DirectChildren.Count];

            for (var i = 0; i < expr.DirectChildren.Count; i++)
                args[i] = expr.DirectChildren[i].ToSimpleTextExpression();

            return SteExpression.CreateFunction(expr.GetType().Name, args);
        }

        public static IMetaExpression ToSymbolicExpression(this Entity expr, MetaContext context)
        {
            var isNumber = expr.EvaluableNumerical;
            var isSymbol = expr.IsSymbolic;

            if (isNumber)
                return context.GetOrDefineSymbolicNumber(
                    expr.ToString(),
                    (double)expr.EvalNumerical()
                );

            if (isSymbol)
                return context.GetVariable(expr.ToString());

            if (expr.DirectChildren.Count == 0)
                return MetaExpressionFunction.CreateNonAssociative(
                    context,
                    expr.GetType().Name
                );

            var args = expr.DirectChildren.Select(
                argExpr => argExpr.ToSymbolicExpression(context)
            );

            var functionName = expr.GetType().Name;
            return functionName switch
            {
                "Minus" => context.FunctionHeadSpecsFactory.Negative.CreateFunction(args),

                "Plus" => context.FunctionHeadSpecsFactory.Plus.CreateFunction(args),
                "Subtract" => context.FunctionHeadSpecsFactory.Subtract.CreateFunction(args),
                "Times" => context.FunctionHeadSpecsFactory.Times.CreateFunction(args),
                "Divide" => context.FunctionHeadSpecsFactory.Divide.CreateFunction(args),

                _ => MetaExpressionFunction.CreateNonAssociative(context, functionName, args)
            };
        }

        public static IMetaExpression ToSymbolicExpression(this MetaContext context, Entity expr)
        {
            return expr.ToSymbolicExpression(context);
        }

        ///// <summary>
        ///// Convert this symbolic expression into a Mathematica expression object
        ///// </summary>
        ///// <param name="symbolicExpr"></param>
        ///// <returns></returns>
        //public static Entity ToExpr(this SteExpression symbolicExpr)
        //{
        //    return MathematicaInterface.DefaultCas[symbolicExpr.ToString()];
        //}
    }
}