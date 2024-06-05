using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Interpolators;

public class DfSignalInterpolator :
    DifferentialInterpolatorFunction
{
    public double MinVarValue 
        => SectionInterpolators[0].MinVarValue;

    public double MaxVarValue 
        => SectionInterpolators[^1].MaxVarValue;

    public IReadOnlyList<DifferentialSignalInterpolatorFunction> SectionInterpolators { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DfSignalInterpolator(IReadOnlyList<DifferentialSignalInterpolatorFunction> sectionInterpolators)
    {
        SectionInterpolators = sectionInterpolators;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        if (t <= MinVarValue)
            return SectionInterpolators[0].GetValue(t);

        if (t >= MaxVarValue)
            return SectionInterpolators[^1].GetValue(t);

        return SectionInterpolators
            .Where(f => f.ContainsVarValue(t))
            .Select(f => f.GetValue(t))
            .Average();
    }

    public override DifferentialFunction GetDerivative1()
    {
        throw new NotImplementedException();
    }
}