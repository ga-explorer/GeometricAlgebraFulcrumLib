namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

public abstract class DifferentialBasicFunction :
    DifferentialFunction
{
    public override int TreeDepth
        => 1;

    public override bool IsBasic
        => true;

    public override bool IsComposite
        => false;

    public override bool IsUnary
        => false;

    public override bool IsBinary
        => false;

    public override bool IsNary
        => false;

    public override bool HasArguments
        => false;

    public override int ArgumentCount
        => 0;

    public override IReadOnlyList<DifferentialFunction> Arguments
        => Array.Empty<DifferentialFunction>();
}