using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberFloat64 :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberFloat64 Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, double number)
    {
        return new MetaExpressionHeadSpecsNumberFloat64(scalarProcessor, number);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value { get; }

    public string NumberText
        => NumberFloat64Value.ToString("G");

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


    private MetaExpressionHeadSpecsNumberFloat64(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, double numberValue)
    {
        ScalarProcessor = scalarProcessor;
        NumberFloat64Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}