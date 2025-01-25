using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public abstract class GrVisualSurfaceWithAnimation3D :
    GrVisualElementWithAnimation3D
{
    public GrVisualSurfaceStyle3D Style { get; }

    
    protected GrVisualSurfaceWithAnimation3D(string name, GrVisualSurfaceStyle3D style, Float64SamplingSpecs samplingSpecs)
        : base(name, samplingSpecs)
    {
        Style = style;
    }
}