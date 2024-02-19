using GeometricAlgebraFulcrumLib.MetaProgramming.Context;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Expressions.HeadSpecs;

public sealed record MetaExpressionHeadSpecsNumberFloat32 : 
    IMetaExpressionHeadSpecsNumber
{
    public static MetaExpressionHeadSpecsNumberFloat32 Create(MetaContext context, float number)
    {
        return new MetaExpressionHeadSpecsNumberFloat32(
            context,
            number
        );
    }

        
    public MetaContext Context { get; }


    public float NumberFloat32Value { get; }

    public double NumberFloat64Value 
        => NumberFloat32Value;

    public string NumberText 
        => NumberFloat32Value.ToString("G");

    public string HeadText 
        => NumberFloat32Value.ToString("G");

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


    private MetaExpressionHeadSpecsNumberFloat32(MetaContext context, float numberValue)
    {
        Context = context;
        NumberFloat32Value = numberValue;
    }


    public override string ToString()
    {
        return NumberText;
    }
}