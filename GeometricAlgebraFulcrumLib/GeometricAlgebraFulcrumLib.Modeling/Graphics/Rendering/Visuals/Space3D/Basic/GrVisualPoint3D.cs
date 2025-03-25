using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualPoint3D :
    GrVisualElementWithAnimation3D,
    ILinFloat64Vector3D
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
        

    public static GrVisualPoint3D CreateStatic(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            position, 
            Float64SamplingSpecs.Static
        );
    }
        
    public static GrVisualPoint3D Create(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            position,
            samplingSpecs
        );
    }
        
    public static GrVisualPoint3D CreateAnimated(string name, GrVisualSurfaceThickStyle3D style, GrVisualAnimatedVector3D position)
    {
        return new GrVisualPoint3D(
            name, 
            style, 
            LinFloat64Vector3D.Zero,
            position.SamplingSpecs
        ).SetAnimatedPosition(position);
    }


    public GrVisualSurfaceThickStyle3D Style { get; }

    public ILinFloat64Vector3D Position { get; } 
        
    public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }
        
    public int VSpaceDimensions 
        => 3;

    public Float64Scalar Item1 
        => Position.X;
        
    public Float64Scalar Item2 
        => Position.Y;
        
    public Float64Scalar Item3 
        => Position.Z;
        
    public Float64Scalar X 
        => Position.X;
        
    public Float64Scalar Y 
        => Position.Y;
        
    public Float64Scalar Z 
        => Position.Z;


    private GrVisualPoint3D(string name, GrVisualSurfaceThickStyle3D style, ILinFloat64Vector3D position, Float64SamplingSpecs samplingSpecs) 
        : base(name, samplingSpecs)
    {
        Position = position;
        Style = style;

        Debug.Assert(IsValid());
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
        
    public GrVisualPoint3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;
            
        return this;
    }

    public GrVisualPoint3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
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