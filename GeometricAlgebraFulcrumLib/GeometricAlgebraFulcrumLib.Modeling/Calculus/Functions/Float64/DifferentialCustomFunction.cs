using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;

public abstract class DifferentialCustomFunction :
    DifferentialBasicFunction
{
    public override bool CanBeSimplified { get; }

    public string Name { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected DifferentialCustomFunction(bool canBeSimplified)
    {
        CanBeSimplified = canBeSimplified;
        Name = $"F{Guid.NewGuid()}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Name;
    }
}