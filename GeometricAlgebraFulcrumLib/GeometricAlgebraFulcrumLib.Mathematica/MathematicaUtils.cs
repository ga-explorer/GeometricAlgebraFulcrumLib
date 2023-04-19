using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MathBase.Signals;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.MetaExpressions;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.Composite;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using Wolfram.NETLink;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Arrays.Generic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.Mathematica
{
    public static class MathematicaUtils
    {
        public static ScalarProcessorExpr ScalarProcessor
            => ScalarProcessorExpr.DefaultProcessor;
        
        public static MatrixAlgebraMathematicaProcessor MatrixProcessor
            => MatrixAlgebraMathematicaProcessor.DefaultProcessor;
        
        public static TextComposerExpr TextComposer
            => TextComposerExpr.DefaultComposer;


        public static MathematicaInterface Cas 
            => MathematicaInterface.DefaultCas;

        public static MathematicaEvaluator Evaluator 
            => Cas.Evaluator;

        public static MathematicaConstants Constants 
            => Cas.Constants;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEqualZero(this MathematicaScalar scalar)
        {
            return ReferenceEquals(scalar, null) || scalar.IsEqualZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this Expr e)
        {
            return ReferenceEquals(e, null)
                ? Constants.Zero
                : MathematicaScalar.Create(Cas, e);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this double e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MathematicaScalar ToMathematicaScalar(this int e)
        {
            return MathematicaScalar.Create(Cas, e.ToExpr());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> ToSymbolic(this IMultivectorStorage<double> storage)
        {
            return storage.MapScalars(number => number.ToExpr());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<double> ToNumeric(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(number => number.ToNumber());
        }


        public static double ToNumber(this Expr value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.NumberQ())
                return 0.0d;

            var exprText = value.ToString();
            if (exprText == "0")
                return 0.0d;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }

        public static double ToNumber(this MathematicaScalar value)
        {
            if (ReferenceEquals(value, null))
                return 0.0d;

            if (!value.Expression.NumberQ())
                return 0.0d;

            var exprText = value.ExpressionText;
            if (exprText == "0")
                return 0.0d;

            var textValue =
                value.CasConnection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value.Expression]]);

            return double.TryParse(textValue, out var doubleValue)
                ? doubleValue : 0.0d;
        }

        public static bool IsNullOrNearNumericZero(this Expr value, double epsilon)
        {
            if (ReferenceEquals(value, null))
                return true;

            if (!value.NumberQ())
                return false;

            var exprText = value.ToString();
            if (exprText == "0")
                return true;

            var textValue =
                Cas.Connection.EvaluateToOutputForm(Mfs.CForm[Mfs.N[value]]);

            if (!double.TryParse(textValue, out var doubleValue))
                return false;

            return Math.Abs(doubleValue) <= epsilon;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> Dot(this Scalar<Expr> v1, Scalar<Expr> v2)
        {
            return Mfs.Dot[
                v1.ScalarValue,
                v2.ScalarValue
            ].TensorReduce().CreateScalar(ScalarProcessor);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> NormSquared(this Scalar<Expr> v1)
        {
            return v1.Dot(v1);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> Norm(this Scalar<Expr> v1)
        {
            return v1.Dot(v1).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> ProjectOn(this Scalar<Expr> v1, Scalar<Expr> v2)
        {
            return (v1.Dot(v2) / v2.NormSquared()) * v2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Expr> SimplifyScalars(this IReadOnlyList<Expr> array)
        {
            return array.MapScalars(s => s.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Expr> SimplifyScalars(this IReadOnlyList<Expr> array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] SimplifyScalars(this Expr[,] array)
        {
            return array.MapScalars(s => s.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] SimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.Simplify(assumptionsExpr));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> SimplifyScalars(this ILinVectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> SimplifyScalars(this ILinVectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> SimplifyScalars(this ILinMatrixStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> SimplifyScalars(this ILinMatrixStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.Simplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> SimplifyScalars(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.Simplify());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> SimplifyScalar(this Scalar<Expr> storage)
        {
            return storage.ScalarValue.Simplify().CreateScalar(storage.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> SimplifyScalar(this Scalar<Expr> storage, Expr assumeExpr)
        {
            return storage.ScalarValue.Simplify(assumeExpr).CreateScalar(storage.ScalarProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr TrigExpand(this Expr scalar)
        {
            return Mfs.TrigExpand[scalar].FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr TrigReduce(this Expr scalar)
        {
            return Mfs.TrigReduce[scalar].FullSimplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> TrigReduceScalar(this Scalar<Expr> scalar)
        {
            return scalar.MapScalar(s => s.TrigReduce());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr SimplifyCollect(this Expr scalar, Expr t)
        {
            return Mfs.Collect[Mfs.Simplify[scalar], t].Evaluate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> SimplifyCollectScalar(this Scalar<Expr> scalar, Expr t)
        {
            return Mfs.Collect[Mfs.Simplify[scalar.ScalarValue], t].Evaluate().CreateScalar(scalar.ScalarProcessor);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Expr> FullSimplifyScalars(this IReadOnlyList<Expr> array)
        {
            return array.MapScalars(s => s.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<Expr> FullSimplifyScalars(this IReadOnlyList<Expr> array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] FullSimplifyScalars(this Expr[,] array)
        {
            return array.MapScalars(s => s.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr[,] FullSimplifyScalars(this Expr[,] array, Expr assumptionsExpr)
        {
            return array.MapScalars(s => s.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> FullSimplifyScalars(this ILinVectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinVectorStorage<Expr> FullSimplifyScalars(this ILinVectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> FullSimplifyScalars(this ILinMatrixStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinMatrixStorage<Expr> FullSimplifyScalars(this ILinMatrixStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> FullSimplifyScalars(this IMultivectorStorage<Expr> storage)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<Expr> FullSimplifyScalars(this IMultivectorStorage<Expr> storage, Expr assumptionsExpr)
        {
            return storage.MapScalars(scalar => scalar.FullSimplify(assumptionsExpr));
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr DifferentiateScalar(this Expr scalar, string variableName, int degree = 1)
        {
            var variableExpr = variableName.ToExpr();

            return scalar.DifferentiateScalar(variableExpr, degree);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr DifferentiateScalar(this Expr scalar, Expr variableExpr, int degree = 1)
        {
            return Mfs.D[
                scalar, 
                Mfs.List[variableExpr, degree.ToExpr()]
            ].FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> DifferentiateScalar(this Scalar<Expr> scalar, string variableName, int degree = 1)
        {
            var variableExpr = variableName.ToExpr();

            return scalar.DifferentiateScalar(variableExpr, degree);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> DifferentiateScalar(this Scalar<Expr> scalar, Expr variableExpr, int degree = 1)
        {
            return Mfs.D[
                scalar.ScalarValue, 
                Mfs.List[variableExpr, degree.ToExpr()]
            ].CreateScalar(scalar.ScalarProcessor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr IntegrateScalar([NotNull] this Expr scalar, [NotNull] string variableName)
        {
            var variableExpr = variableName.ToExpr();

            return Mfs
                .Integrate[scalar, variableExpr]
                .FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr IntegrateScalar([NotNull] this Expr scalar, [NotNull] string variableName, [NotNull] Expr limitExpr1, [NotNull] Expr limitExpr2)
        {
            var variableExpr = variableName.ToExpr();

            return Mfs
                .Integrate[scalar, Mfs.List[variableExpr, limitExpr1, limitExpr2]]
                .FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr IntegrateScalar([NotNull] this Expr scalar, [NotNull] Expr variableExpr, [NotNull] Expr limitExpr1, [NotNull] Expr limitExpr2)
        {
            return Mfs
                .Integrate[scalar, Mfs.List[variableExpr, limitExpr1, limitExpr2]]
                .FullSimplify();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr HilbertTransformScalar(this Expr scalar, string timeVariableName, string freqVariableName)
        {
            var timeVariableExpr = timeVariableName.ToExpr();
            var freqVariableExpr = freqVariableName.ToExpr();

            return Mfs
                .HilbertTransform[scalar, timeVariableExpr, timeVariableExpr]
                .FullSimplify(Mfs.Greater[freqVariableExpr, Expr.INT_ZERO]);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MatrixProduct(this Expr matrix1, Expr matrix2)
        {
            return MatrixProcessor.TimesMatrices(matrix1, matrix2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr MatrixDeterminant(this Expr[,] array)
        {
            return Mfs.Det[array.ToMatrixExpr()];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetText(this Expr[,] array)
        {
            return TextComposer.GetArrayText(array);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AttachMathematicaExpressionEvaluator(this MetaContext context)
        {
            context.SymbolicEvaluator = 
                new MathematicaMetaExpressionEvaluator(context);
        }


        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this bool value)
        {
            return new Expr(ExpressionType.Boolean, value ? "True" : "False");
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this int value)
        {
            return new Expr(value);
        }
        
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this uint value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this long value)
        {
            return new Expr(value);
        }
        
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this ulong value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this float value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this double value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this string value)
        {
            return MathematicaInterface.DefaultCas.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <param name="mathematicaInterface"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this string value, MathematicaInterface mathematicaInterface)
        {
            return mathematicaInterface.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given symbol name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToSymbolExpr(this string symbolName)
        {
            return new Expr(ExpressionType.Symbol, symbolName);
        }

        public static Expr[,] MatrixExprToArray(this Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            var rowsCount = (int) dimensionsExpr.Args[0].AsInt64();
            var colsCount = (int) dimensionsExpr.Args[1].AsInt64();

            var array = new Expr[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, (i + 1).ToExpr()].Evaluate();

                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = Mfs.Part[rowExpr, (j + 1).ToExpr()].Evaluate();
                }
            }

            return array;
        }
        
        public static Expr ToVectorExpr(this IReadOnlyList<Expr> exprArray)
        {
            var colsCount = exprArray.Count;
            
            var rowItems = new Expr[colsCount];

            for (var j = 0; j < colsCount; j++)
                rowItems[j] = exprArray[j] ?? Expr.INT_ZERO;
            
            return Mfs.ListExpr(rowItems);
        }

        public static Expr ToRowVectorMatrixExpr(this IReadOnlyList<Expr> exprArray)
        {
            var colsCount = exprArray.Count;

            var rowsExprArray = new Expr[1];
            
            var rowItems = new Expr[colsCount];

            for (var j = 0; j < colsCount; j++)
                rowItems[j] = exprArray[j] ?? Expr.INT_ZERO;

            rowsExprArray[0] = Mfs.ListExpr(rowItems);
            
            return Mfs.ListExpr(rowsExprArray);
        }
        
        public static Expr ToColumnVectorMatrixExpr(this IReadOnlyList<Expr> exprArray)
        {
            var rowsCount = exprArray.Count;

            var rowsExprArray = new Expr[rowsCount];
            
            for (var i = 0; i < rowsCount; i++)
            {
                var rowItems = new Expr[1];

                rowItems[0] = exprArray[i] ?? Expr.INT_ZERO;

                rowsExprArray[i] = Mfs.ListExpr(rowItems);
            }
            
            return Mfs.ListExpr(rowsExprArray);
        }

        public static Expr ToMatrixExpr(this Expr[,] exprArray)
        {
            var rowsCount = exprArray.GetLength(0);
            var colsCount = exprArray.GetLength(1);

            var rowsExprArray = new Expr[rowsCount];
            
            for (var i = 0; i < rowsCount; i++)
            {
                var rowItems = new Expr[colsCount];

                for (var j = 0; j < colsCount; j++)
                    rowItems[j] = exprArray[i, j] ?? Expr.INT_ZERO;

                rowsExprArray[i] = Mfs.ListExpr(rowItems);
            }
            
            return Mfs.ListExpr(rowsExprArray);
        }
        
        public static Scalar<Expr> ArrayToMatrixExprScalar(this IScalarProcessor<Expr> processor, Expr[,] exprArray)
        {
            return processor.CreateScalar(exprArray.ToMatrixExpr());
        }

        public static Scalar<Expr> ArrayToMatrixExprScalar(this Expr[,] exprArray, IScalarProcessor<Expr> processor)
        {
            return processor.CreateScalar(exprArray.ToMatrixExpr());
        }

        /// <summary>
        /// Create a list of Mathematica Expr objects from the given symbol names
        /// </summary>
        /// <param name="symbolNames"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Expr> ToSymbolExprList(this IEnumerable<string> symbolNames)
        {
            return symbolNames.Select(symbolName => new Expr(ExpressionType.Symbol, symbolName));
        }

        /// <summary>
        /// Construct an Expr object from a head expression and some arguments
        /// </summary>
        /// <param name="funcNameSymbol"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ApplyTo(this Expr funcNameSymbol, params object[] args)
        {
            return new Expr(funcNameSymbol, args);
        }

        /// <summary>
        /// Construct an Expr object from a head symbol string and some arguments
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ApplyTo(this string funcName, params object[] args)
        {
            return new Expr(new Expr(ExpressionType.Symbol, funcName), args);
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. The original
        /// expression is the first on the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            yield return rootExpr;

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The original expression is not included in the list
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (rootExpr.Args.Length == 0)
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                yield return expr;

                if (expr.Args.Length == 0)
                    continue;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> SubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            yield return rootExpr;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        /// <summary>
        /// Get a list of all sub-expressions of the given expression using depth-first order. 
        /// The root expression is not included in the list.
        /// The sub-expression is accepted for output and traversal if skipFunc is false for that sub-expression.
        /// If a sub-expression is skiped so is all its sub-expressions
        /// </summary>
        /// <param name="rootExpr"></param>
        /// <param name="skipFunc"></param>
        /// <returns></returns>
        public static IEnumerable<Expr> ProperSubExpressions(this Expr rootExpr, Func<Expr, bool> skipFunc)
        {
            if (ReferenceEquals(rootExpr, null))
                throw new ArgumentNullException(nameof(rootExpr));

            if (skipFunc(rootExpr))
                yield break;

            var stack = new Stack<Expr>(rootExpr.Args);

            while (stack.Count > 0)
            {
                var expr = stack.Pop();

                if (skipFunc(rootExpr))
                    continue;

                yield return expr;

                foreach (var subExpr in expr.Args)
                    stack.Push(subExpr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr N(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.N[expr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Round(this Expr expr, int places)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Round[Mfs.N[expr], Math.Pow(10, -places).ToExpr()]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Simplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Simplify[expr, assumptionsExpr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Simplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Simplify[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr SimplifyToExpr(this string exprText)
        {
            return $"Simplify[{exprText}]".ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplifyToExpr(this string exprText)
        {
            return $"FullSimplify[{exprText}]".ToExpr();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ReplaceAll(this Expr inputExpr, string subExprText1, string subExprText2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExprText1.ToExpr(), subExprText2.ToExpr()]
            ].FullSimplify();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ReplaceAll(this Expr inputExpr, Expr subExpr1, Expr subExpr2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExpr1, subExpr2]
            ].FullSimplify();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Refine(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Refine[expr, assumptionsExpr]
            ];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr TrigToExp(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.TrigToExp[expr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ExpToTrig(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.ExpToTrig[expr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.FullSimplify[expr]];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Collect(this Expr expr, Expr symbolExpr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Collect[expr, symbolExpr]];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Collect(this Expr expr, string symbolExprText)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Collect[expr, symbolExprText.ToExpr()]];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr TensorReduce(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[Mfs.TensorReduce[expr]];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr TensorExpand(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[Mfs.TensorExpand[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> Collect(this Scalar<Expr> expr, Expr symbolExpr)
        {
            return expr.ScalarValue.Collect(symbolExpr).CreateScalar(expr.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.FullSimplify[expr, assumptionsExpr]
            ];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr FullSimplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.FullSimplify[expr]];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> FullSimplifyScalar(this Scalar<Expr> expr)
        {
            return expr.ScalarValue.FullSimplify().CreateScalar(expr.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> FullSimplifyScalar(this Scalar<Expr> expr, Expr assumptionsExpr)
        {
            return expr.ScalarValue.FullSimplify(assumptionsExpr).CreateScalar(expr.ScalarProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> TensorReduceScalar(this Scalar<Expr> expr)
        {
            return expr.ScalarValue.TensorReduce().CreateScalar(expr.ScalarProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<Expr> TensorExpandScalar(this Scalar<Expr> expr)
        {
            return expr.ScalarValue.TensorExpand().CreateScalar(expr.ScalarProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Evaluate(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[expr];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string EvaluateToText(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr.ToString() 
                : MathematicaInterface.DefaultCasConnection.EvaluateToString(expr);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double EvaluateToDouble(this Expr expr)
        {
            return MathematicaInterface.DefaultCasConnection.EvaluateToDouble(expr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Expand(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Expand[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr Expand(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Expand[expr]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZero(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "0" || exprText == "0.";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.IsZero() ||
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrZero(this Expr expr)
        {
            return ReferenceEquals(expr, null) ||
                   expr.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return ReferenceEquals(expr, null) || 
                   expr.IsZero() || 
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOne(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "1" || exprText == "1.";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMinusOne(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "-1" || exprText == "-1.";
        }


        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalTrueQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "True";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is anything else it returns false and raises no errors
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalFalseQ(this MathematicaInterface casInterface, Expr e)
        {
            return casInterface.Connection.EvaluateToExpr(e).ToString() == "False";
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'True' it returns true
        /// If the result is 'False' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalIsTrue(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            return result switch
            {
                "True" => true,
                "False" => false,
                _ => throw new InvalidOperationException("Expression did not evaluate to a constant boolean value")
            };
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is 'True' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EvalIsFalse(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            return result switch
            {
                "True" => false,
                "False" => true,
                _ => throw new InvalidOperationException("Expression did not evaluate to a constant boolean value")
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Expr CreateElementExpr(List<Expr> items, Expr domainNameSymbol)
        {
            if (items.Count == 1)
                return Mfs.Element[items[0], domainNameSymbol];

            return
                items.Count > 1
                ? Mfs.Element[Mfs.Alternatives[items.Cast<object>().ToArray()], domainNameSymbol]
                : null;
        }

        public static Expr CreateAssumeExpr(this MathematicaInterface parentCas, Dictionary<string, MathematicaAtomicType> varTypes)
        {
            var complexesList = new List<Expr>();
            var realsList = new List<Expr>();
            var rationalsList = new List<Expr>();
            var integersList = new List<Expr>();
            var booleansList = new List<Expr>();

            foreach (var (key, value) in varTypes)
            {
                switch (value)
                {
                    case MathematicaAtomicType.Complex:
                        complexesList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Real:
                        realsList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Rational:
                        rationalsList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Integer:
                        integersList.Add(key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Boolean:
                        booleansList.Add(key.ToSymbolExpr());
                        break;
                }
            }

            var domainElementsExpr = new List<Expr>(4);

            if (complexesList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(complexesList, DomainSymbols.Complexes));

            if (realsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(realsList, DomainSymbols.Reals));

            if (rationalsList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(rationalsList, DomainSymbols.Rationals));

            if (integersList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(integersList, DomainSymbols.Integers));

            if (booleansList.Count > 0)
                domainElementsExpr.Add(CreateElementExpr(booleansList, DomainSymbols.Booleans));

            if (domainElementsExpr.Count == 0)
                return null;

            var expr = domainElementsExpr.Count == 1
                ? parentCas[domainElementsExpr[0]]
                : parentCas[Mfs.And[domainElementsExpr.Cast<object>().ToArray()]];

            return expr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBooleanScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Booleans, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsIntegerScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Integers, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRealScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Reals, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsComplexScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Complexes, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRationalScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Rationals, assumptionsExpr);

            return cond.IsConstantTrue();
        }


        /// <summary>
        /// Convert the given Mathematica Expr object into a SteExpression object
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static SteExpression ToSimpleTextExpression(this Expr expr)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
            {
                return isSymbol
                    ? SteExpression.CreateSymbolicNumber(expr.ToString())
                    : SteExpression.CreateLiteralNumber(expr.ToString());
            }

            if (isSymbol)
                return SteExpression.CreateVariable(expr.ToString());

            if (expr.Args.Length == 0)
                return SteExpression.CreateFunction(expr.ToString());

            var args = new SteExpression[expr.Args.Length];

            for (var i = 0; i < expr.Args.Length; i++)
                args[i] = ToSimpleTextExpression(expr.Args[i]);

            return SteExpression.CreateFunction(expr.Head.ToString(), args);
        }

        public static IMetaExpression ToSymbolicExpression(this Expr expr, MetaContext context)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
            {
                if (expr.Head.ToString() == "Rational")
                    return context.GetOrDefineRationalNumber(
                        long.Parse(expr.Args[0].ToString()),
                        long.Parse(expr.Args[1].ToString())
                    );

                return context.GetOrDefineSymbolicNumber(
                    expr.ToString(),
                    expr.ToNumber()
                );
            }

            if (isSymbol)
            {
                var exprText = expr.ToString();

                return exprText switch
                {
                    "Pi" => context.GetOrDefineSymbolicNumber(exprText, Math.PI),
                    "E" => context.GetOrDefineSymbolicNumber(exprText, Math.E),

                    _ => context.GetVariable(exprText)
                };
            }

            if (expr.Args.Length == 0)
                return MetaExpressionFunction.CreateNonAssociative(
                    context, 
                    expr.Head.ToString()
                );

            var args = expr.Args.Select(
                argExpr => ToSymbolicExpression(argExpr, context)
            );
            
            var functionName = expr.Head.ToString();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMetaExpression ToSymbolicExpression(this MetaContext context, Expr expr)
        {
            return ToSymbolicExpression(expr, context);
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this SteExpression symbolicExpr)
        {
            return MathematicaInterface.DefaultCas[symbolicExpr.ToString()];
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[symbolicExpr.ToString()];
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <param name="cas"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr SimplifyToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[Mfs.Simplify[symbolicExpr.ToString()]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeX(this Expr expr)
        {
            return Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeXInlineEquation(this Expr expr)
        {
            return "$" + Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim() + "$";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLaTeXDisplayEquation(this Expr expr)
        {
            return new StringBuilder()
                .AppendLine(@"\[")
                .AppendLine(Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim())
                .AppendLine(@"\]")
                .ToString();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToMathematicaExpr(this ScalarFourierSeriesTerm term, Expr t)
        {
            var angleExpr = Mfs.Times[term.Frequency, t];

            return Mfs.Plus[
                Mfs.Times[term.CosScalar, Mfs.Cos[angleExpr]],
                Mfs.Times[term.SinScalar, Mfs.Sin[angleExpr]]
            ].Evaluate();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expr ToMathematicaExpr(this ScalarFourierSeries interpolator, Expr t)
        {
            return Mfs.Plus[
                interpolator.Terms.Select(term => (object) term.ToMathematicaExpr(t)).ToArray()
            ].Evaluate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ScalarSignalFloat64 GetSampledSignal(this Expr scalar1, Expr t, double samplingRate, int sampleCount)
        {
            return ScalarSignalFloat64.CreatePeriodic(
                sampleCount,
                sampleCount / samplingRate,
                d =>
                    scalar1
                        .ReplaceAll(t, d.ToExpr())
                        .EvaluateToDouble(),
                false
            );
        }

        public static XGaVector<ScalarSignalFloat64> GetSampledSignal(this XGaProcessor<ScalarSignalFloat64> processor, XGaVector<Expr> vector, Expr t, double samplingRate, int sampleCount)
        {
            var composer = processor.CreateComposer();

            foreach (var (id, exprScalar) in vector.IdScalarPairs)
            {
                composer.SetTerm(
                    id,
                    exprScalar.GetSampledSignal(t, samplingRate, sampleCount)
                );
            }

            return composer.GetVector();
        }
        
        public static XGaBivector<ScalarSignalFloat64> GetSampledSignal(this XGaProcessor<ScalarSignalFloat64> processor, XGaBivector<Expr> bivector, Expr t, double samplingRate, int sampleCount)
        {
            var composer = processor.CreateComposer();
                
            foreach (var (id, exprScalar) in bivector.IdScalarPairs)
                composer.SetTerm(
                    id,
                    exprScalar.GetSampledSignal(t, samplingRate, sampleCount)
                );

            return composer.GetBivector();
        }

    }
}
