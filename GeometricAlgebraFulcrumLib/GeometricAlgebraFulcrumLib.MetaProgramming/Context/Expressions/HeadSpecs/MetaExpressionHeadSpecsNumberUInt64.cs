using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberUInt64 :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberUInt64 Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, ulong number)
    {
        return new MetaExpressionHeadSpecsNumberUInt64(scalarProcessor, number);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value
        => NumberUInt64Value;

    public ulong NumberUInt64Value { get; }

    public string NumberText
        => NumberUInt64Value.ToString();

    public string HeadText
        => NumberUInt64Value.ToString();

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


    private MetaExpressionHeadSpecsNumberUInt64(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, ulong numberValue)
    {
        ScalarProcessor = scalarProcessor;
        NumberUInt64Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}