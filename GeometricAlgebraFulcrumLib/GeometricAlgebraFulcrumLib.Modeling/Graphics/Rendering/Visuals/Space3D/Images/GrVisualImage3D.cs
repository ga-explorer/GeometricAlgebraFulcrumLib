using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualImage3D :
    GrVisualElementWithAnimation3D, 
    IGrVisualImage3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Position
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );
        
    
    public static GrVisualImage3D CreateStatic(string name, IGrVisualImageSource texture, ILinFloat64Vector3D position, double scalingFactor)
    {
        return new GrVisualImage3D(
            name, 
            texture, 
            position, 
            scalingFactor, 
            Float64SamplingSpecs.Static
        );
    }

    public static GrVisualImage3D Create(string name, IGrVisualImageSource texture, ILinFloat64Vector3D position, double scalingFactor, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualImage3D(
            name, 
            texture, 
            position, 
            scalingFactor, 
            samplingSpecs
        );
    }
        
    public static GrVisualImage3D CreateAnimated(string name, IGrVisualImageSource texture, GrVisualAnimatedVector3D position, double scalingFactor)
    {
        return new GrVisualImage3D(
            name, 
            texture, 
            LinFloat64Vector3D.Zero, 
            scalingFactor, 
            position.SamplingSpecs
        ).SetAnimatedPosition(position);
    }
        

    public IGrVisualImageSource Texture { get; }

    public double ScalingFactor { get; }

    public ILinFloat64Vector3D Position { get; }

    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }


    private GrVisualImage3D(string name, IGrVisualImageSource texture, ILinFloat64Vector3D position, double scalingFactor, Float64SamplingSpecs samplingSpecs) 
        : base(name, samplingSpecs)
    {
        Texture = texture;
        Position = position;
        ScalingFactor = scalingFactor;
    }


    public Pair<int> GetImageSize()
    {
        return Texture.ImageSize;
    }

    public Image GetImage()
    {
        return Texture.GetImage();
    }
    
    public override bool IsValid()
    {
        return Position.IsValid() &&
               AnimatedPosition.IsNullOrValid(SamplingSpecs.TimeRange);
    }

    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPosition is not null)
            animatedGeometries.Add(AnimatedPosition);

        return animatedGeometries;
    }
        
    public GrVisualImage3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualImage3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }
        
    public LinFloat64Vector3D GetPosition(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToLinVector3D()
            : AnimatedPosition.GetValue(time);
    }

    public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / SamplingSpecs.SamplingRate;

            yield return new KeyFrameRecord(
                frameIndex, 
                time, 
                GetVisibility(time),
                GetPosition(time)
            );
        }
    }
}