using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

public abstract class DifferentialNaryFunction
    : DifferentialCompositeFunction
{
    public override bool IsUnary
        => false;

    public override bool IsBinary
        => false;

    public override bool IsNary
        => true;
    
    public override int ArgumentCount 
        => Arguments.Count;

    public override IReadOnlyList<DifferentialFunction> Arguments { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialNaryFunction(IReadOnlyList<DifferentialFunction> baseFunctions, bool canBeSimplified)
        : base(canBeSimplified)
    {
        Arguments = baseFunctions;
    }
}