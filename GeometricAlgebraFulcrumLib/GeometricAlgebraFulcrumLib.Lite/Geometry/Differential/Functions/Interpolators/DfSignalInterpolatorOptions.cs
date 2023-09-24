using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions.Interpolators
{
    public class DfSignalInterpolatorOptions
    {
        public int SectionCount { get; set; } 
            = 1;

        public IReadOnlyList<int> SmoothingFactors { get; set; } 
            = Array.Empty<int>();

        public bool HasSmoothing 
            => SmoothingFactors.Count > 0;


        public IEnumerable<int> GetSmoothingSampleCountList()
        {
            if (SmoothingFactors.Count == 0) return Enumerable.Empty<int>();

            var smoothingFactorsLcm = 
                SmoothingFactors.Lcm();

            return SmoothingFactors.Select(f => 
                (smoothingFactorsLcm / f - 1) / 2
            );
        }
    }
}