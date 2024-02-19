using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

public abstract class DifferentialUnaryFunction :
    DifferentialCompositeFunction
{
    public override bool IsUnary
        => true;

    public override bool IsBinary
        => false;

    public override bool IsNary
        => false;
    
    public DifferentialFunction Argument { get; }

    public override int ArgumentCount 
        => 1;

    public override IReadOnlyList<DifferentialFunction> Arguments 
        => new[] { Argument };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialUnaryFunction(DifferentialFunction baseFunction, bool canBeSimplified)
        : base(canBeSimplified)
    {
        Argument = baseFunction;
    }
}