using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;

public sealed class GrBabylonJsAnimationSpecs
{
    public int FrameRate { get; }

    public Int32Range1D FrameRange { get; }
    
    public bool Loop { get; init; }

    public GrBabylonJsAnimationLoopMode LoopMode { get; init; }

    public bool EnableBlending { get; init; }

    
    public GrBabylonJsAnimationSpecs(Float64SamplingSpecs visualAnimationSpecs)
    {
        var frameRate = (int)visualAnimationSpecs.SamplingRate;
        var frameRange = visualAnimationSpecs.SampleIndexRange;

        if (frameRate < 1)
            throw new ArgumentOutOfRangeException(nameof(frameRate));
        
        FrameRate = frameRate;
        FrameRange = frameRange;
    }

    public GrBabylonJsAnimationSpecs(GrVisualElementWithAnimation3D visualElement)
    {
        var frameRate = (int)visualElement.SamplingSpecs.SamplingRate;
        var frameRange = visualElement.SamplingSpecs.SampleIndexRange;

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