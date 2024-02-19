using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberInt32 : 
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberInt32 Create(MetaContext context, int number)
    {
        return new MetaExpressionHeadSpecsNumberInt32(
            context,
            number
        );
    }

        
    public MetaContext Context { get; }

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


    private MetaExpressionHeadSpecsNumberInt32(MetaContext context, int numberValue)
    {
        Context = context;
        NumberInt32Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}