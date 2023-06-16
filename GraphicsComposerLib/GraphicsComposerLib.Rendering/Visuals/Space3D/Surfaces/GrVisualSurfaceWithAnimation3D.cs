using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public abstract class GrVisualSurfaceWithAnimation3D :
    GrVisualElementWithAnimation3D
{
    public GrVisualSurfaceStyle3D Style { get; }

    
    protected GrVisualSurfaceWithAnimation3D(string name, GrVisualSurfaceStyle3D style, GrVisualAnimationSpecs animationSpecs)
        : base(name, animationSpecs)
    {
        Style = style;
    }
}