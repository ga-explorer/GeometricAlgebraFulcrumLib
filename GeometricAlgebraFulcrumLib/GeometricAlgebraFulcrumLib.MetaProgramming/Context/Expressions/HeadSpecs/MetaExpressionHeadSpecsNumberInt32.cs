using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberInt32 :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberInt32 Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, int number)
    {
        return new MetaExpressionHeadSpecsNumberInt32(scalarProcessor, number);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value
        => NumberInt32Value;

    public int NumberInt32Value { get; }

    public string NumberText
        => NumberInt32Value.ToString();

    public string HeadText
        => NumberInt32Value.ToString();

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


    private MetaExpressionHeadSpecsNumberInt32(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, int numberValue)
    {
        ScalarProcessor = scalarProcessor;
        NumberInt32Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}