using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberRational :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberRational Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, long numerator, long denominator)
    {
        return new MetaExpressionHeadSpecsNumberRational(scalarProcessor, numerator, denominator);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public long Numerator { get; }

    public long Denominator { get; }

    public double NumberFloat64Value
        => Numerator / (double)Denominator;

    public string NumberText
        => MetaExpressionNumber.GetRationalNumberText(Numerator, Denominator);

    public string HeadText
        => NumberFloat64Value.ToString("G");

    public bool IsNumber
        => true;

    public bool IsSymbolicNumber
        => false;

    public bool IsLiteralNumber
        => true;

    public bool IsSymbolicNumberOrVariable
        => false;

    public bool IsVariable
        => false;

    public bool IsAtomic
        => true;

    public bool IsComposite
        => false;

    public bool IsFunction
        => false;

    public bool IsOperator
        => false;

    public bool IsArrayAccess
        => false;


    private MetaExpressionHeadSpecsNumberRational(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, long numerator, long denominator)
    {
        ScalarProcessor = scalarProcessor;
        Numerator = numerator;
        Denominator = denominator;
    }


    public override string ToString()
    {
        return NumberText;
    }
}