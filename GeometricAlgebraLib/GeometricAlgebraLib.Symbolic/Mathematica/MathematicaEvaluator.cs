using System;
using GeometricAlgebraLib.Symbolic.Mathematica.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraLib.Symbolic.Mathematica
{
    public class MathematicaEvaluator
    {
        public MathematicaInterface Cas { get; }

        public MathematicaConnection CasConnection => Cas.Connection;

        public MathematicaConstants CasConstants => Cas.Constants;


        internal MathematicaEvaluator(MathematicaInterface parentCas)
        {
            Cas = parentCas;
        }


        //public Expr EvaluateFunction(Expr FuncNameSymbol, params object[] Args)
        //{
        //    Expr e = new Expr(FuncNameSymbol, Args);

        //    return CASConnection.EvaluateToExpr(e);
        //}

        //public Expr EvaluateFunction(string FuncName, params object[] Args)
        //{
        //    Expr e = new Expr(new Expr(ExpressionType.Symbol, FuncName), Args);

        //    return CASConnection.EvaluateToExpr(e);
        //}

        //public bool EvaluateFunctionIsTrue(Expr FuncNameSymbol, params object[] Args)
        //{
        //    Expr e = new Expr(FuncNameSymbol, Args);

        //    return CASConnection.EvaluateToExpr(e).TrueQ();
        //}

        //public bool EvaluateFunctionIsTrue(string FuncName, params object[] Args)
        //{
        //    Expr e = new Expr(new Expr(ExpressionType.Symbol, FuncName), Args);

        //    return CASConnection.EvaluateToExpr(e).TrueQ();
        //}

        //public bool EvaluateFunctionIsFalse(Expr FuncNameSymbol, params object[] Args)
        //{
        //    Expr e = new Expr(FuncNameSymbol, Args);

        //    return !CASConnection.EvaluateToExpr(e).TrueQ();
        //}

        //public bool EvaluateFunctionIsFalse(string FuncName, params object[] Args)
        //{
        //    Expr e = new Expr(new Expr(ExpressionType.Symbol, FuncName), Args);

        //    return !CASConnection.EvaluateToExpr(e).TrueQ();
        //}


        //public Expr Evaluate(Expr expr)
        //{
        //    return CASConnection.EvaluateToExpr(expr);
        //}

        //public bool EvaluateIsTrue(Expr expr)
        //{
        //    return CASConnection.EvaluateToExpr(expr).TrueQ();
        //}

        //public bool EvaluateIsFalse(Expr expr)
        //{
        //    return !CASConnection.EvaluateToExpr(expr).TrueQ();
        //}

        //public Expr Evaluate(string expr_text)
        //{
        //    return CASConnection.EvaluateToExpr(expr_text);
        //}


        public Expr Simplify(Expr expr)
        {
            return CasConnection.EvaluateToExpr(Mfs.Simplify[expr]);
        }

        public Expr Simplify(string exprText)
        {
            return CasConnection.EvaluateToExpr(Mfs.Simplify[exprText]);
        }

        public Expr Simplify(Expr expr, Expr rulesExpr)
        {
            return CasConnection.EvaluateToExpr(Mfs.Simplify[expr, rulesExpr]);
        }

        public Expr FullySimplify(Expr expr)
        {
            return CasConnection.EvaluateToExpr(Mfs.FullSimplify[expr]);
        }

        public Expr FullySimplify(string exprText)
        {
            return CasConnection.EvaluateToExpr(Mfs.FullSimplify[exprText]);
        }

        public Expr FullySimplify(Expr expr, Expr rulesExpr)
        {
            return CasConnection.EvaluateToExpr(Mfs.FullSimplify[expr, rulesExpr]);
        }


        public bool IsAtomicBoolean(Expr expr, Expr ruleExpr = null)
        {
            var e = Mfs.Element[expr, DomainSymbols.Booleans];

            return 
                ruleExpr == null 
                ? CasConnection.EvaluateToExpr(Mfs.FullSimplify[e]).TrueQ() 
                : CasConnection.EvaluateToExpr(Mfs.FullSimplify[expr, ruleExpr]).TrueQ();
        }

        public bool IsAtomicScalar(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsAtomicReal(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsAtomicInteger(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsMatrix(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsFullMatrix(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsSparseMatrix(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsVector(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsFullVectror(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }

        public bool IsSparseVector(Expr expr, Expr ruleExpr = null)
        {
            throw new NotImplementedException();
        }


        public Expr ReplaceVariableWithExpr(Expr srcExpr, string varName, Expr subExpr)
        {
            var replaceallExpr = Mfs.ReplaceAll[srcExpr, Mfs.Rule[varName.ToSymbolExpr(), subExpr]];

            return CasConnection.EvaluateToExpr(replaceallExpr);
        }


    }
}
