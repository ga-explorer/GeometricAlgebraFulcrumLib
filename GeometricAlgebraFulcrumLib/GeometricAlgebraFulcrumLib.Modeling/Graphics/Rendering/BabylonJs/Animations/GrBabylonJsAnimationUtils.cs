using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

public static class GrBabylonJsAnimationUtils
{
    public static bool IsValidForBabylonJs(this Float64SamplingSpecs samplingSpecs)
    {
        return samplingSpecs.IsValid() && 
               samplingSpecs.SamplingRate.IsInteger();
    }
}