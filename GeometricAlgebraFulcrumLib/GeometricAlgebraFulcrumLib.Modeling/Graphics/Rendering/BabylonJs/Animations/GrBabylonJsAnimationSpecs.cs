using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

public sealed class GrBabylonJsAnimationSpecs
{
    public int FrameRate { get; }

    public Int32Range1D FrameRange { get; }
    
    public bool Loop { get; init; }

    public GrBabylonJsAnimationLoopMode LoopMode { get; init; }

    public bool EnableBlending { get; init; }

    
    public GrBabylonJsAnimationSpecs(GrVisualAnimationSpecs visualAnimationSpecs)
    {
        var frameRate = visualAnimationSpecs.FrameRate;
        var frameRange = visualAnimationSpecs.FrameIndexRange;

        if (frameRate < 1)
            throw new ArgumentOutOfRangeException(nameof(frameRate));
        
        FrameRate = frameRate;
        FrameRange = frameRange;
    }

    public GrBabylonJsAnimationSpecs(GrVisualElementWithAnimation3D visualElement)
    {
        var frameRate = visualElement.AnimationSpecs.FrameRate;
        var frameRange = visualElement.AnimationSpecs.FrameIndexRange;

        if (frameRate < 1)
            throw new ArgumentOutOfRangeException(nameof(frameRate));
        
        FrameRate = frameRate;
        FrameRange = frameRange;
    }

    public GrBabylonJsAnimationSpecs(int frameRate, Int32Range1D frameRange)
    {
        if (frameRate < 1)
            throw new ArgumentOutOfRangeException(nameof(frameRate));
        
        FrameRate = frameRate;
        FrameRange = frameRange;
    }
}