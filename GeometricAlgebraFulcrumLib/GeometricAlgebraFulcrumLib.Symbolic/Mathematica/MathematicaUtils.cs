using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Composite;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica
{
    public static class MathematicaUtils
    {
        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this bool value)
        {
            return new Expr(ExpressionType.Boolean, value ? "True" : "False");
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this int value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this float value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expr ToExpr(this double value)
        {
            return new Expr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given text expression using the Mathematica interface evaluator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        public static Expr ToExpr(this string value, MathematicaInterface mathematicaInterface)
        {
            return mathematicaInterface.Connection.EvaluateToExpr(value);
        }

        /// <summary>
        /// Create a Mathematica Expr object from the given symbol name
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public static Expr ToSymbolExpr(this string symbolName)
        {
            return new Expr(ExpressionType.Symbol, symbolName);
        }

        public static Expr[,] MatrixExprToArray(this Expr matrix)
        {
            var dimensionsExpr = Mfs.Dimensions[matrix].Simplify();

            var rowsCount = (int) dimensionsExpr[0].AsInt64();
            var colsCount = (int) dimensionsExpr[1].AsInt64();

            var array = new Expr[rowsCount, colsCount];

            for (var i = 0; i < rowsCount; i++)
            {
                var rowExpr = Mfs.Part[matrix, i.ToExpr()];

                for (var j = 0; j < colsCount; j++)
                {
                    array[i, j] = Mfs.Part[rowExpr, j.ToExpr()].Simplify();
                }
            }

            return array;
        }

        public static Expr ArrayToMatrixExpr(this Expr[,] exprArray)
        {
            var rowsCount = exprArray.GetLength(0);
            var colsCount = exprArray.GetLength(1);

            var rowsExprArray = new Expr[rowsCount];
            
            for (var i = 0; i < rowsCount; i++)
            {
                var rowItems = new Expr[colsCount];

                for (var j = 0; j < colsCount; j++)
                    rowItems[j] = exprArray[i, j];

                rowsExprArray[i] = Mfs.ListExpr(rowItems);
            }
            
            return Mfs.ListExpr(rowsExprArray);
        }
        
        /// <summary>
        /// Create a list of Mathematica Expr objects from the given symbol names
        /// </summary>
        /// <param name="symbolNames"></param>
        /// <returns></returns>
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

        public static Expr N(this Expr expr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.N[expr]
            ];
        }

        public static Expr Round(this Expr expr, int places)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Round[Mfs.N[expr], Math.Pow(10, -places).ToExpr()]
            ];
        }

        public static Expr Simplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Simplify[expr]];
        }

        public static Expr Simplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.Simplify[expr, assumptionsExpr]
            ];
        }

        public static Expr Simplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Simplify[expr]];
        }

        public static Expr SimplifyToExpr(this string exprText)
        {
            return $"Simplify[{exprText}]".ToExpr();
        }

        public static Expr FullSimplifyToExpr(this string exprText)
        {
            return $"FullSimplify[{exprText}]".ToExpr();
        }

        public static Expr ReplaceAll(this Expr inputExpr, string subExprText1, string subExprText2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExprText1.ToExpr(), subExprText2.ToExpr()]
            ].FullSimplify();
        }

        public static Expr ReplaceAll(this Expr inputExpr, Expr subExpr1, Expr subExpr2)
        {
            return Mfs.ReplaceAll[
                inputExpr,
                Mfs.Rule[subExpr1, subExpr2]
            ].FullSimplify();
        }

        public static Expr FullSimplify(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.FullSimplify[expr]];
        }

        public static Expr FullSimplify(this Expr expr, Expr assumptionsExpr)
        {
            return MathematicaInterface.DefaultCas[
                Mfs.FullSimplify[expr, assumptionsExpr]
            ];
        }

        public static Expr FullSimplify(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.FullSimplify[expr]];
        }

        public static Expr Evaluate(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[expr];
        }

        public static string EvaluateToText(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr.ToString() 
                : MathematicaInterface.DefaultCasConnection.EvaluateToString(expr);
        }

        public static Expr Expand(this Expr expr)
        {
            return expr.AtomQ() 
                ? expr 
                : MathematicaInterface.DefaultCas[Mfs.Expand[expr]];
        }

        public static Expr Expand(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.AtomQ() 
                ? expr 
                : casInterface[Mfs.Expand[expr]];
        }

        public static bool IsZero(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "0" || exprText == "0.";
        }

        public static bool IsEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return expr.IsZero() ||
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        public static bool IsNullOrZero(this Expr expr)
        {
            return ReferenceEquals(expr, null) ||
                   expr.IsZero();
        }


        public static bool IsNullOrEqualZero(this Expr expr, MathematicaInterface casInterface)
        {
            return ReferenceEquals(expr, null) || 
                   expr.IsZero() || 
                   casInterface.EvalTrueQ(Mfs.Equal[expr, Expr.INT_ZERO]);
        }

        public static bool IsOne(this Expr expr)
        {
            if (!expr.NumberQ())
                return false;

            var exprText = expr.ToString();

            return exprText == "1" || exprText == "1.";
        }

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
        public static bool EvalIsTrue(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            switch (result)
            {
                case "True":
                    return true;

                case "False":
                    return false;
            }

            throw new InvalidOperationException("Expression did not evaluate to a constant boolean value");
        }

        /// <summary>
        /// Evaluates the given expression and if the result is 'False' it returns true
        /// If the result is 'True' it returns false
        /// If the result is anything else it raises an error
        /// </summary>
        /// <param name="casInterface"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool EvalIsFalse(this MathematicaInterface casInterface, Expr e)
        {
            var result = casInterface.Connection.EvaluateToExpr(e).ToString();

            switch (result)
            {
                case "True":
                    return false;

                case "False":
                    return true;
            }

            throw new InvalidOperationException("Expression did not evaluate to a constant boolean value");
        }


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

            foreach (var pair in varTypes)
            {
                switch (pair.Value)
                {
                    case MathematicaAtomicType.Complex:
                        complexesList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Real:
                        realsList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Rational:
                        rationalsList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Integer:
                        integersList.Add(pair.Key.ToSymbolExpr());
                        break;

                    case MathematicaAtomicType.Boolean:
                        booleansList.Add(pair.Key.ToSymbolExpr());
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

        public static bool IsBooleanScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Booleans, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsIntegerScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Integers, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsRealScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Reals, assumptionsExpr);

            return cond.IsConstantTrue();
        }

        public static bool IsComplexScalar(this MathematicaScalar sc, Expr assumptionsExpr)
        {
            var cond = MathematicaCondition.CreateIsDomainMemberTest(sc, DomainSymbols.Complexes, assumptionsExpr);

            return cond.IsConstantTrue();
        }

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

        public static ISymbolicExpression ToSymbolicExpression(this Expr expr, SymbolicContext context)
        {
            var isNumber = expr.NumberQ();
            var isSymbol = expr.SymbolQ();

            if (isNumber)
                return context.GetOrDefineSymbolicNumber(
                    expr.ToString(), 
                    expr.ToNumber()
                );

            if (isSymbol)
                return context.GetVariable(expr.ToString());

            if (expr.Args.Length == 0)
                return SymbolicFunction.CreateNonAssociative(
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
                
                _ => SymbolicFunction.CreateNonAssociative(context, functionName, args)
            };
        }

        public static ISymbolicExpression ToSymbolicExpression(this SymbolicContext context, Expr expr)
        {
            return ToSymbolicExpression(expr, context);
        }

        /// <summary>
        /// Convert this symbolic expression into a Mathematica expression object
        /// </summary>
        /// <param name="symbolicExpr"></param>
        /// <returns></returns>
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
        public static Expr SimplifyToExpr(this SteExpression symbolicExpr, MathematicaInterface cas)
        {
            return cas[Mfs.Simplify[symbolicExpr.ToString()]];
        }


        public static string GetLaTeX(this Expr expr)
        {
            return Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim();
        }

        public static string GetLaTeXInlineEquation(this Expr expr)
        {
            return "$" + Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim() + "$";
        }

        public static string GetLaTeXDisplayEquation(this Expr expr)
        {
            var textComposer = new StringBuilder();

            textComposer.AppendLine(@"\[");
            textComposer.AppendLine(Mfs.EToString[Mfs.TeXForm[expr]].EvaluateToText().Trim());
            textComposer.AppendLine(@"\]");

            return textComposer.ToString();
        }
    }
}
