using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualAnimatedGeometry :
    IAlgebraicElement
{
    public GrVisualAnimationSpecs AnimationSpecs { get; }

    //public IReadOnlyList<int> InvalidFrameIndexList { get; private set; }
    //    = ImmutableArray<int>.Empty;

    public Float64ScalarRange FrameTimeRange 
        => AnimationSpecs.FrameTimeRange;
    
    public Int32Range1D FrameIndexRange 
        => AnimationSpecs.FrameIndexRange;

    public int FrameRate 
        => AnimationSpecs.FrameRate;
    
    public double FrameTime
        => AnimationSpecs.FrameTime;

    public double MinFrameTime 
        => AnimationSpecs.MinFrameTime;

    public double MaxFrameTime 
        => AnimationSpecs.MaxFrameTime;
    
    public int MinFrameIndex 
        => AnimationSpecs.MinFrameIndex;
    
    public int MaxFrameIndex 
        => AnimationSpecs.MaxFrameIndex;

    public IEnumerable<KeyValuePair<int, double>> FrameIndexTimePairs
        => AnimationSpecs.FrameIndexTimePairs;

    
    protected GrVisualAnimatedGeometry(GrVisualAnimationSpecs animationSpecs)
    {
        if (animationSpecs.IsStatic)
            throw new InvalidOperationException();

        AnimationSpecs = animationSpecs;

        //SetInvalidFrameIndices(invalidFrameIndices);

        //Debug.Assert(
        //    InvalidFrameIndexList.Count == 0 ||
        //    InvalidFrameIndexList.All(animationSpecs.FrameIndexRange.Contains)
        //);
    }


    public abstract bool IsValid();
    
    //public void RemoveInvalidFrameIndices()
    //{
    //    InvalidFrameIndexList = ImmutableArray<int>.Empty;
    //}

    //public void SetInvalidFrameIndices(IReadOnlyList<int> invalidFrameIndexList)
    //{
    //    if (invalidFrameIndexList.Count == 0)
    //        InvalidFrameIndexList = ImmutableArray<int>.Empty;

    //    else if (invalidFrameIndexList.All(AnimationSpecs.FrameIndexRange.Contains))
    //        InvalidFrameIndexList = invalidFrameIndexList;

    //    else
    //        throw new InvalidOperationException();
    //}
}