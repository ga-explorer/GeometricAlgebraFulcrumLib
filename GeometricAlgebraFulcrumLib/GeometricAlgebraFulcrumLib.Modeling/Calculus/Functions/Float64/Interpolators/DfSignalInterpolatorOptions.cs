using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Interpolators;

public class DfSignalInterpolatorOptions
{
    public int SectionCount { get; set; }
        = 1;

    public IReadOnlyList<int> SmoothingFactors { get; set; }
        = [];

    public bool HasSmoothing
        => SmoothingFactors.Count > 0;


    public IEnumerable<int> GetSmoothingSampleCountList()
    {
        if (SmoothingFactors.Count == 0) return [];

        var smoothingFactorsLcm =
            SmoothingFactors.Lcm();

        return SmoothingFactors.Select(f =>
            (smoothingFactorsLcm / f - 1) / 2
        );
    }
}