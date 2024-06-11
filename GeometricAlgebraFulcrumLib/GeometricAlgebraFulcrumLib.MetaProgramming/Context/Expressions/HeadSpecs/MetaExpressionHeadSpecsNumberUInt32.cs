using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberUInt32 :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberUInt32 Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, uint number)
    {
        return new MetaExpressionHeadSpecsNumberUInt32(scalarProcessor, number);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value
        => NumberUInt32Value;

    public uint NumberUInt32Value { get; }

    public string NumberText
        => NumberUInt32Value.ToString();

    public string HeadText
        => NumberUInt32Value.ToString();

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


    private MetaExpressionHeadSpecsNumberUInt32(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, uint numberValue)
    {
        ScalarProcessor = scalarProcessor;
        NumberUInt32Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}