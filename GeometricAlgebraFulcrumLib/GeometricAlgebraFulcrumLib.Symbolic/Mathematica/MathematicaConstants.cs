using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.Expression;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Symbolic.Mathematica
{
    public sealed class MathematicaConstants
    {
        private readonly Dictionary<string, MathematicaExpression> _constantsDictionary = new Dictionary<string, MathematicaExpression>();

        public MathematicaInterface Cas { get; }

        public MathematicaConnection CasConnection => Cas.Connection;

        public MathematicaEvaluator CasEvaluator => Cas.Evaluator;


        public MathematicaCondition True { get; }

        public Expr ExprTrue => True.Expression;

        public MathematicaCondition False { get; }

        public Expr ExprFalse => False.Expression;

        public MathematicaScalar Zero { get; }

        public Expr ExprZero => Zero.Expression;

        public MathematicaScalar One { get; }

        public Expr ExprOne => One.Expression;

        public MathematicaScalar MinusOne { get; }

        public Expr ExprMinusOne => MinusOne.Expression;

        public MathematicaScalar Pi { get; }

        public Expr ExprPi => Pi.Expression;

        public MathematicaScalar TwoPi { get; }

        public Expr ExprTwoPi => TwoPi.Expression;


        public MathematicaExpression this[string constName] => _constantsDictionary[constName];


        internal MathematicaConstants(MathematicaInterface parentCas)
        {
            Cas = parentCas;

            True = MathematicaCondition.Create(parentCas, true);
            _constantsDictionary.Add("true", True);

            False = MathematicaCondition.Create(parentCas, false);
            _constantsDictionary.Add("false", False);

            Zero = MathematicaScalar.Create(parentCas, 0);
            _constantsDictionary.Add("zero", Zero);

            One = MathematicaScalar.Create(parentCas, 1);
            _constantsDictionary.Add("one", One);

            MinusOne = MathematicaScalar.Create(parentCas, -1);
            _constantsDictionary.Add("minusone", MinusOne);

            Pi = MathematicaScalar.Create(parentCas, CasConnection.EvaluateToExpr("Pi"));
            _constantsDictionary.Add("pi", Pi);

            TwoPi = MathematicaScalar.Create(parentCas, CasConnection.EvaluateToExpr("Times[2, Pi]"));
            _constantsDictionary.Add("twopi", TwoPi);
        }


        public MathematicaExpression Add(string constName, MathematicaExpression casExpr)
        {
            _constantsDictionary.Add(constName, casExpr);

            return casExpr;
        }
    }
}
