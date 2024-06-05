using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using SixLabors.ImageSharp;
using GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public sealed class GrVisualLaTeXText3D :
    GrVisualImageWithAnimation3D
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
        

    public static GrVisualLaTeXText3D CreateStatic(string name, WclHtmlImageDataUrlCache pngCache, ILinFloat64Vector3D position, double scalingFactor)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            position, 
            scalingFactor, 
            GrVisualAnimationSpecs.Static
        );
    }
        
    public static GrVisualLaTeXText3D CreateStatic(string name, WclHtmlImageDataUrlCache pngCache, string key, ILinFloat64Vector3D position, double scalingFactor)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            key, 
            position, 
            scalingFactor,
            GrVisualAnimationSpecs.Static
        );
    }

    public static GrVisualLaTeXText3D Create(string name, WclHtmlImageDataUrlCache pngCache, ILinFloat64Vector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            position, 
            scalingFactor, 
            animationSpecs
        );
    }
        
    public static GrVisualLaTeXText3D Create(string name, WclHtmlImageDataUrlCache pngCache, string key, ILinFloat64Vector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            key, 
            position, 
            scalingFactor,
            animationSpecs
        );
    }
        
    public static GrVisualLaTeXText3D CreateAnimated(string name, WclHtmlImageDataUrlCache pngCache, GrVisualAnimatedVector3D position, double scalingFactor)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            LinFloat64Vector3D.Zero, 
            scalingFactor, 
            position.AnimationSpecs
        ).SetAnimatedPosition(position);
    }
        
    public static GrVisualLaTeXText3D CreateAnimated(string name, WclHtmlImageDataUrlCache pngCache, string key, GrVisualAnimatedVector3D position, double scalingFactor)
    {
        return new GrVisualLaTeXText3D(
            name, 
            pngCache, 
            key, 
            LinFloat64Vector3D.Zero, 
            scalingFactor,
            position.AnimationSpecs
        ).SetAnimatedPosition(position);
    }


    public WclHtmlImageDataUrlCache ImageCache { get; }

    public string Key { get; }

    public double ScalingFactor { get; }

    public ILinFloat64Vector3D Position { get; }

    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }


    private GrVisualLaTeXText3D(string name, WclHtmlImageDataUrlCache pngCache, ILinFloat64Vector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs) 
        : base(name, animationSpecs)
    {
        ImageCache = pngCache;
        Key = name;
        Position = position;
        ScalingFactor = scalingFactor;
    }

    private GrVisualLaTeXText3D(string name, WclHtmlImageDataUrlCache pngCache, string key, ILinFloat64Vector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs) 
        : base(name, animationSpecs)
    {
        ImageCache = pngCache;
        Key = key;
        Position = position;
        ScalingFactor = scalingFactor;
    }


    public override Pair<int> GetSize()
    {
        var image = ImageCache[Key];

        return new Pair<int>(image.Width, image.Height);
    }

    public WclHtmlImageUrl GetImageData()
    {
        return ImageCache[Key];
    }
    
    public override Image GetImage()
    {
        throw new NotImplementedException();
    }
        
    public override bool IsValid()
    {
        return Position.IsValid() &&
               AnimatedPosition.IsNullOrValid(AnimationSpecs.FrameTimeRange);
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
        
    public GrVisualLaTeXText3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualLaTeXText3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
    {
        AnimatedPosition = position;

        return this;
    }
        
    public LinFloat64Vector3D GetPosition(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition is null
            ? Position.ToLinVector3D()
            : AnimatedPosition.GetPoint(time);
    }

    public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / AnimationSpecs.FrameRate;

            yield return new KeyFrameRecord(
                frameIndex, 
                time, 
                GetVisibility(time),
                GetPosition(time)
            );
        }
    }
}