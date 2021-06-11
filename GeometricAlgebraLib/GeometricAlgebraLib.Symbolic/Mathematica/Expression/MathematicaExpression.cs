using System.Collections.Generic;
using CodeComposerLib.SyntaxTree.Expressions;
using GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Mathematica.Expression
{
    public class MathematicaExpression : ISymbolicObject
    {
        private static int _exprIdCounter;

        private static int GetNewExprId()
        {
            return _exprIdCounter++;
        }


        public static MathematicaExpression Create(MathematicaInterface parentCas, Expr mathExpr)
        {
            return new MathematicaExpression(parentCas, mathExpr);
        }

        public static MathematicaExpression Create(MathematicaInterface parentCas, string mathExprText)
        {
            var mathExpr = parentCas.Connection.EvaluateToExpr(mathExprText);

            return new MathematicaExpression(parentCas, mathExpr);
        }


        public MathematicaInterface CasInterface { get; }

        public MathematicaConnection CasConnection => CasInterface.Connection;

        public MathematicaEvaluator CasEvaluator => CasInterface.Evaluator;

        public MathematicaConstants CasConstants => CasInterface.Constants;

        public int ExpressionId { get; private set; }

        public Expr Expression { get; private set; }

        public object Tag { get; set; }


        public string ExpressionText 
            => Expression.ToString();


        protected MathematicaExpression(MathematicaInterface parentCas, Expr mathExpr)
        {
            CasInterface = parentCas;
            ExpressionId = GetNewExprId();
            Expression = mathExpr;
        }


        public Dictionary<string, MathematicaExpression> GetAllSubexpressions()
        {
            var subList = new Dictionary<string, MathematicaExpression>();

            var stk = new Stack<Expr>();

            var rootExpr = Expression;

            stk.Push(rootExpr);
            subList.Add(rootExpr.ToString(), Create(CasInterface, rootExpr));

            while (stk.Count > 0)
            {
                var curExpr = stk.Pop();

                foreach (var childExpr in curExpr.Args)
                {
                    var childExprText = childExpr.ToString();

                    if (subList.ContainsKey(childExprText))
                        continue;

                    stk.Push(childExpr);
                    subList.Add(childExprText, Create(CasInterface, childExpr));
                }
            }

            return subList;
        }

        public SteExpression ToSymbolicTextExpression()
        {
            return Expression.ToSymbolicTextExpression();
        }


        public bool IsSymbol()
        {
            return Expression.SymbolQ();
        }

        /// <summary>
        /// Replace the internal Mathematica expression by a simpler one using the 
        /// Simplify[] Mathematica function
        /// </summary>
        public void Simplify()
        {
            if (Expression.AtomQ()) return;

            Expression = CasInterface[Mfs.Simplify[Expression]];
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equal(obj as MathematicaExpression);
        }

        public bool Equal(MathematicaExpression expr2)
        {
            if (ReferenceEquals(expr2, null))
                return false;

            return (ToString() == expr2.ToString());
        }

        public sealed override int GetHashCode()
        {
            return ExpressionText.GetHashCode();
        }

        public override string ToString()
        {
            return ExpressionText;
        }
    }
}
