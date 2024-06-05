using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberSymbolic :
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberSymbolic Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, double number)
    {
        return new MetaExpressionHeadSpecsNumberSymbolic(
            scalarProcessor,
            number.ToString("G"),
            number
        );
    }

    public static MetaExpressionHeadSpecsNumberSymbolic Create(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, string numberText, double numberValue)
    {
        return new MetaExpressionHeadSpecsNumberSymbolic(scalarProcessor, numberText, numberValue);
    }


    public IScalarProcessor<IMetaExpressionAtomic> ScalarProcessor { get; }

    public double NumberFloat64Value { get; }

    public string NumberText { get; }

    public string HeadText
        => NumberText;

    public bool IsNumber
        => true;

    public bool IsSymbolicNumber
        => true;

    public bool IsLiteralNumber
        => false;

    public bool IsSymbolicNumberOrVariable
        => true;

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


    private MetaExpressionHeadSpecsNumberSymbolic(IScalarProcessor<IMetaExpressionAtomic> scalarProcessor, string numberText, double numberValue)
    {
        if (string.IsNullOrEmpty(numberText))
            throw new ArgumentNullException(nameof(numberText), @"Number value not initialized");

        ScalarProcessor = scalarProcessor;
        NumberText = numberText;
        NumberFloat64Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}