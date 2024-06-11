using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberInt64 :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberInt64 Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, long number)
    {
        return new MetaExpressionHeadSpecsNumberInt64(scalarProcessor, number);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value
        => NumberInt64Value;

    public long NumberInt64Value { get; }

    public string NumberText
        => NumberInt64Value.ToString();

    public string HeadText
        => NumberInt64Value.ToString();

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


    private MetaExpressionHeadSpecsNumberInt64(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, long numberValue)
    {
        ScalarProcessor = scalarProcessor;
        NumberInt64Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}