using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using WebComposerLib.Html.Media;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Images
{
    public sealed class GrVisualLaTeXText3D :
        GrVisualImageWithAnimation3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex,
            double Time,
            double Visibility,
            Float64Vector3D Position
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex, 
            Time, 
            Visibility
        );
        

        public static GrVisualLaTeXText3D CreateStatic(string name, WclHtmlImageDataUrlCache pngCache, IFloat64Tuple3D position, double scalingFactor)
        {
            return new GrVisualLaTeXText3D(
                name, 
                pngCache, 
                position, 
                scalingFactor, 
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualLaTeXText3D CreateStatic(string name, WclHtmlImageDataUrlCache pngCache, string key, IFloat64Tuple3D position, double scalingFactor)
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

        public static GrVisualLaTeXText3D Create(string name, WclHtmlImageDataUrlCache pngCache, IFloat64Tuple3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualLaTeXText3D(
                name, 
                pngCache, 
                position, 
                scalingFactor, 
                animationSpecs
            );
        }
        
        public static GrVisualLaTeXText3D Create(string name, WclHtmlImageDataUrlCache pngCache, string key, IFloat64Tuple3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
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
        
        public static GrVisualLaTeXText3D CreateAnimated(string name, WclHtmlImageDataUrlCache pngCache, GrVisualAnimatedVector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualLaTeXText3D(
                name, 
                pngCache, 
                Float64Vector3D.Zero, 
                scalingFactor, 
                animationSpecs
            ).SetAnimatedPosition(position);
        }
        
        public static GrVisualLaTeXText3D CreateAnimated(string name, WclHtmlImageDataUrlCache pngCache, string key, GrVisualAnimatedVector3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualLaTeXText3D(
                name, 
                pngCache, 
                key, 
                Float64Vector3D.Zero, 
                scalingFactor,
                animationSpecs
            ).SetAnimatedPosition(position);
        }


        public WclHtmlImageDataUrlCache ImageCache { get; }

        public string Key { get; }

        public double ScalingFactor { get; }

        public IFloat64Tuple3D Position { get; }

        public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }


        private GrVisualLaTeXText3D(string name, WclHtmlImageDataUrlCache pngCache, IFloat64Tuple3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs) 
            : base(name, animationSpecs)
        {
            ImageCache = pngCache;
            Key = name;
            Position = position;
            ScalingFactor = scalingFactor;
        }

        private GrVisualLaTeXText3D(string name, WclHtmlImageDataUrlCache pngCache, string key, IFloat64Tuple3D position, double scalingFactor, GrVisualAnimationSpecs animationSpecs) 
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
                   AnimatedPosition.IsNullOrValid(AnimationSpecs.TimeRange);
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
        
        public GrVisualLaTeXText3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualLaTeXText3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
        {
            AnimatedPosition = position;

            return this;
        }
        
        public Float64Vector3D GetPosition(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPosition is null
                ? Position.ToVector3D()
                : AnimatedPosition.GetPoint(time);
        }

        public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
        {
            Debug.Assert(IsValid());

            foreach (var frameIndex in KeyFrameRange)
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
}