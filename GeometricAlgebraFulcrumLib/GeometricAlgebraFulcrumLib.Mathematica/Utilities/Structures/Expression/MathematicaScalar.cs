using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;

public sealed class MathematicaScalar : MathematicaExpression
{
    public static MathematicaScalar CreateRational(MathematicaInterface parentCas, int numerator, int denominator)
    {
        var e = parentCas[Mfs.Rational[numerator.ToExpr(), denominator.ToExpr()]];

        return new MathematicaScalar(parentCas, e);
    }

    public static MathematicaScalar CreateSymbol(MathematicaInterface parentCas, string symbolName)
    {
        return new MathematicaScalar(parentCas, symbolName.ToSymbolExpr());
    }

    public static MathematicaScalar Create(MathematicaInterface parentCas, int value)
    {
        return new MathematicaScalar(parentCas, value.ToExpr());
    }

    public static MathematicaScalar Create(MathematicaInterface parentCas, float value)
    {
        return new MathematicaScalar(parentCas, value.ToExpr());
    }

    public static MathematicaScalar Create(MathematicaInterface parentCas, double value)
    {
        return new MathematicaScalar(parentCas, value.ToExpr());
    }

    public new static MathematicaScalar Create(MathematicaInterface parentCas, Expr mathExpr)
    {
        return new MathematicaScalar(parentCas, mathExpr);
    }

    public new static MathematicaScalar Create(MathematicaInterface parentCas, string mathExprText)
    {
        return new MathematicaScalar(parentCas, parentCas.Connection.EvaluateToExpr(mathExprText));
    }


    public static MathematicaScalar operator -(MathematicaScalar e1)
    {
        var e = e1.CasInterface[Mfs.Minus[e1.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }

    public static MathematicaScalar operator +(MathematicaScalar e1, MathematicaScalar e2)
    {
        var e = e1.CasInterface[Mfs.Plus[e1.Expression, e2.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }

    public static MathematicaScalar operator -(MathematicaScalar e1, MathematicaScalar e2)
    {
        var e = e1.CasInterface[Mfs.Subtract[e1.Expression, e2.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }

    public static MathematicaScalar operator *(MathematicaScalar e1, MathematicaScalar e2)
    {
        var e = e1.CasInterface[Mfs.Times[e1.Expression, e2.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }

    public static MathematicaScalar operator /(MathematicaScalar e1, MathematicaScalar e2)
    {
        var e = e1.CasInterface[Mfs.Divide[e1.Expression, e2.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }

    public static MathematicaScalar operator ^(MathematicaScalar e1, MathematicaScalar e2)
    {
        var e = e1.CasInterface[Mfs.Power[e1.Expression, e2.Expression]];

        return new MathematicaScalar(e1.CasInterface, e);
    }


    public static MathematicaCondition operator ==(MathematicaScalar s1, MathematicaScalar s2)
    {
        if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) return null;

        var e = s1.CasInterface[Mfs.Equal[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }

    public static MathematicaCondition operator !=(MathematicaScalar s1, MathematicaScalar s2)
    {
        if (ReferenceEquals(s1, null) || ReferenceEquals(s2, null)) return null;

        var e = s1.CasInterface[Mfs.Unequal[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }

    public static MathematicaCondition operator >(MathematicaScalar s1, MathematicaScalar s2)
    {
        var e = s1.CasInterface[Mfs.Greater[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }

    public static MathematicaCondition operator >=(MathematicaScalar s1, MathematicaScalar s2)
    {
        var e = s1.CasInterface[Mfs.GreaterEqual[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }

    public static MathematicaCondition operator <(MathematicaScalar s1, MathematicaScalar s2)
    {
        var e = s1.CasInterface[Mfs.Less[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }

    public static MathematicaCondition operator <=(MathematicaScalar s1, MathematicaScalar s2)
    {
        var e = s1.CasInterface[Mfs.LessEqual[s1.Expression, s2.Expression]];

        return MathematicaCondition.Create(s1.CasInterface, e);
    }


    private MathematicaScalar(MathematicaInterface parentCas, Expr mathExpr)
        : base(parentCas, mathExpr)
    {
    }


    public bool IsPossibleZero()
    {
        return CasInterface.EvalIsTrue(Mfs.PossibleZeroQ[Expression]);
    }

    public bool IsZero()
    {
        return Expression.IsZero();
    }

    public bool IsOne()
    {
        return Expression.NumberQ() && Expression.ToString() == "1";
    }

    public bool IsMinusOne()
    {
        return Expression.NumberQ() && Expression.ToString() == "-1";
    }

    public bool IsEqualZero()
    {
        return Expression.IsEqualZero(CasInterface);
        //CasInterface.EvalTrueQ(Mfs.Equal[MathExpr, CasConstants.ExprZero]);
    }

    public bool IsPossibleScalar(MathematicaScalar expr)
    {
        return CasInterface.EvalIsTrue(Mfs.PossibleZeroQ[Mfs.Subtract[Expression, expr.Expression]]);
    }

    //TODO: Add more overloads to accept integers
    public bool IsEqualScalar(MathematicaScalar expr)
    {
        return CasInterface.EvalTrueQ(Mfs.Equal[Expression, expr.Expression]);
    }

    public bool IsConstant()
    {
        return CasInterface.EvalTrueQ(Mfs.NumericQ[Expression]);
    }

    public bool IsNonZeroRealConstant()
    {
        return CasInterface.EvalTrueQ(
            Mfs.And[
                Mfs.NumericQ[Expression],
                Mfs.Element[Expression, DomainSymbols.Reals],
                Mfs.Not[Mfs.PossibleZeroQ[Expression]]
            ]
        );
    }

    public bool IsRealConstant()
    {
        return CasInterface.EvalTrueQ(
            Mfs.And[
                Mfs.NumericQ[Expression],
                Mfs.Element[Expression, DomainSymbols.Reals]
            ]
        );
    }


    public MathematicaScalar N()
    {
        var e = CasInterface[Mfs.N[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar N(int percision)
    {
        var e = CasInterface[Mfs.N[Expression, percision.ToExpr()]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Abs()
    {
        var e = CasInterface[Mfs.Abs[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Sqrt()
    {
        var e = CasInterface[Mfs.Sqrt[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Sin()
    {
        var e = CasInterface[Mfs.Sin[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Cos()
    {
        var e = CasInterface[Mfs.Cos[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Tan()
    {
        var e = CasInterface[Mfs.Tan[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Sinh()
    {
        var e = CasInterface[Mfs.Sinh[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Cosh()
    {
        var e = CasInterface[Mfs.Cosh[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Tanh()
    {
        var e = CasInterface[Mfs.Tanh[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Log()
    {
        var e = CasInterface[Mfs.Log[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Log10()
    {
        var e = CasInterface[Mfs.Log10[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Log2()
    {
        var e = CasInterface[Mfs.Log2[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar Exp()
    {
        var e = CasInterface[Mfs.Exp[Expression]];

        return new MathematicaScalar(CasInterface, e);
    }

    public MathematicaScalar DiffBy(MathematicaScalar expr2)
    {
        var e = CasInterface[Mfs.D[Expression, expr2.Expression]];

        return new MathematicaScalar(CasInterface, e);
    }
}