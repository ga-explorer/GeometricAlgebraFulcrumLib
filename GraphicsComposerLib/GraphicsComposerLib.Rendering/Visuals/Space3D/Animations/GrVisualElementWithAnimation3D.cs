using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualElementWithAnimation3D :
    GrVisualElement3D
{
    public GrVisualAnimationSpecs AnimationSpecs { get; }

    public Int32Range1D KeyFrameRange 
        => AnimationSpecs.FrameRange;

    public bool IsStatic 
        => AnimationSpecs.IsStatic ||
           GetAnimatedGeometries().Count == 0;
    
    public bool IsAnimated 
        => !IsStatic;
    
    public GrVisualAnimatedVector1D? AnimatedVisibility { get; set; }

    
    protected GrVisualElementWithAnimation3D(string name, GrVisualAnimationSpecs animationSpecs) 
        : base(name)
    {
        AnimationSpecs = animationSpecs;
    }

    
    public abstract IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries();

    
    public double GetVisibility(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedVisibility is null
            ? Visibility
            : AnimatedVisibility.GetPoint(time);
    }


}