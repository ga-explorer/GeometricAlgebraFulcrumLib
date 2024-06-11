using System.Collections.Immutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualElementWithAnimation3D :
    GrVisualElement3D
{
    public GrVisualAnimationSpecs AnimationSpecs { get; }

    //protected HashSet<int> InvalidFrameIndexSet { get; }
    //    = new HashSet<int>();

    public bool IsStatic 
        => AnimationSpecs.IsStatic ||
           GetAnimatedGeometries().Count == 0;
    
    public bool IsAnimated 
        => !IsStatic;
    
    public GrVisualAnimatedScalar? AnimatedVisibility { get; set; }

    
    protected GrVisualElementWithAnimation3D(string name, GrVisualAnimationSpecs animationSpecs) 
        : base(name)
    {
        AnimationSpecs = animationSpecs;
    }

    
    public abstract IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries();
    
    //public void ClearInvalidFrameIndices()
    //{
    //    InvalidFrameIndexSet.Clear();
    //}

    //public void AddInvalidFrameIndices(IEnumerable<int> invalidFrameIndices)
    //{
    //    InvalidFrameIndexSet.AddRange(invalidFrameIndices);
    //}

    //public IReadOnlyList<int> GetInvalidFrameIndices()
    //{
    //    if (IsStatic)
    //        return ImmutableSortedSet<int>.Empty;

    //    return InvalidFrameIndexSet.Union(
    //        GetAnimatedGeometries().SelectMany(g => 
    //            g.InvalidFrameIndexList
    //        )
    //    ).ToImmutableArray();
    //}

    public ImmutableSortedSet<int> GetValidFrameIndexSet()
    {
        if (IsStatic)
            return ImmutableSortedSet<int>.Empty;

        return AnimationSpecs.FrameIndexRange.ToImmutableSortedSet();

        //var invalidFrameIndexList =
        //    GetInvalidFrameIndices();

        //return invalidFrameIndexList.Count == 0
        //    ? AnimationSpecs
        //        .FrameIndexRange
        //        .ToImmutableSortedSet()
        //    : AnimationSpecs
        //        .FrameIndexRange
        //        .ToImmutableSortedSet()
        //        .Except(invalidFrameIndexList);
    }

    public double GetVisibility(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedVisibility is null
            ? Visibility
            : AnimatedVisibility.GetValue(time);
    }


}