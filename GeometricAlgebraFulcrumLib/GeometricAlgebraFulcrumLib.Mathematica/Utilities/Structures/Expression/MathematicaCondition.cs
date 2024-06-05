using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.Expression;

public class MathematicaCondition : MathematicaExpression
{
    public static MathematicaCondition CreateIsDomainMemberTest(MathematicaExpression testedExpr, Expr domainExpr)
    {
        var expr = testedExpr.CasInterface[Mfs.Element[testedExpr.Expression, domainExpr]];

        return new MathematicaCondition(testedExpr.CasInterface, expr);
    }

    public static MathematicaCondition CreateIsDomainMemberTest(MathematicaInterface parentCas, Expr testedExpr, Expr domainExpr)
    {
        var expr = parentCas[Mfs.Element[testedExpr, domainExpr]];

        return new MathematicaCondition(parentCas, expr);
    }

    public static MathematicaCondition CreateIsDomainMemberTest(MathematicaExpression testedExpr, Expr domainExpr, Expr assumeExpr)
    {
        var expr = testedExpr.CasInterface[Mfs.FullSimplify[Mfs.Element[testedExpr.Expression, domainExpr], assumeExpr]];

        return new MathematicaCondition(testedExpr.CasInterface, expr);
    }

    public static MathematicaCondition CreateIsDomainMemberTest(MathematicaInterface parentCas, Expr testedExpr, Expr domainExpr, Expr assumeExpr)
    {
        var expr = parentCas[Mfs.FullSimplify[Mfs.Element[testedExpr, domainExpr], assumeExpr]];

        return new MathematicaCondition(parentCas, expr);
    }

    public static MathematicaCondition Create(MathematicaInterface parentCas, bool value)
    {
        return new MathematicaCondition(parentCas, value.ToExpr());
    }

    public new static MathematicaCondition Create(MathematicaInterface parentCas, Expr mathExpr)
    {
        return new MathematicaCondition(parentCas, mathExpr);
    }

    public new static MathematicaCondition Create(MathematicaInterface parentCas, string mathExprText)
    {
        return new MathematicaCondition(parentCas, parentCas[mathExprText]);
    }


    public static MathematicaCondition operator !(MathematicaCondition s1)
    {
        var e = s1.CasInterface[Mfs.Not[s1.Expression]];

        return new MathematicaCondition(s1.CasInterface, e);
    }

    public static MathematicaCondition operator &(MathematicaCondition s1, MathematicaCondition s2)
    {
        var e = s1.CasInterface[Mfs.And[s1.Expression, s2.Expression]];

        return new MathematicaCondition(s1.CasInterface, e);
    }

    public static MathematicaCondition operator |(MathematicaCondition s1, MathematicaCondition s2)
    {
        var e = s1.CasInterface[Mfs.Or[s1.Expression, s2.Expression]];

        return new MathematicaCondition(s1.CasInterface, e);
    }


    public static MathematicaCondition And(params MathematicaCondition[] args)
    {
        var parentCas = args[0].CasInterface;
        var exprArgs = new object[args.Length];

        for (var i = 0; i < args.Length; i++)
            exprArgs[i] = args[i].Expression;

        var e = parentCas[Mfs.And[exprArgs]];

        return new MathematicaCondition(parentCas, e);
    }

    public static MathematicaCondition Or(params MathematicaCondition[] args)
    {
        var parentCas = args[0].CasInterface;
        var exprArgs = new object[args.Length];

        for (var i = 0; i < args.Length; i++)
            exprArgs[i] = args[i].Expression;

        var e = parentCas[Mfs.Or[exprArgs]];

        return new MathematicaCondition(parentCas, e);
    }

    public static MathematicaCondition Nand(params MathematicaCondition[] args)
    {
        var parentCas = args[0].CasInterface;
        var exprArgs = new object[args.Length];

        for (var i = 0; i < args.Length; i++)
            exprArgs[i] = args[i].Expression;

        var e = parentCas[Mfs.Nand[exprArgs]];

        return new MathematicaCondition(parentCas, e);
    }

    public static MathematicaCondition Nor(params MathematicaCondition[] args)
    {
        var parentCas = args[0].CasInterface;
        var exprArgs = new object[args.Length];

        for (var i = 0; i < args.Length; i++)
            exprArgs[i] = args[i].Expression;

        var e = parentCas[Mfs.Nor[exprArgs]];

        return new MathematicaCondition(parentCas, e);
    }


    protected MathematicaCondition(MathematicaInterface parentCas, Expr mathExpr)
        : base(parentCas, mathExpr)
    {
    }


    public bool IsConstant()
    {
        if (Expression.SymbolQ() == false) return false;

        var exprText = Expression.ToString();

        return exprText == "True" || exprText == "False";
    }

    public bool IsConstant(bool value)
    {
        if (Expression.SymbolQ() == false) return false;

        return
            value
                ? Expression.ToString() == "True"
                : Expression.ToString() == "False";
    }

    public bool IsConstantTrue()
    {
        return Expression.SymbolQ() && Expression.ToString() == "True";
    }

    public bool IsConstantFalse()
    {
        return Expression.SymbolQ() && Expression.ToString() == "False";
    }
}