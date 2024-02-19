using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;

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