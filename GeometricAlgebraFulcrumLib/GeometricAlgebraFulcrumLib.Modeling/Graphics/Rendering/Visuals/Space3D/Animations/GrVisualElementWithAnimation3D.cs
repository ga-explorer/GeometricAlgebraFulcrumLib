using System.Collections.Immutable;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualElementWithAnimation3D :
    GrVisualElement3D
{
    public Float64SamplingSpecs SamplingSpecs { get; }

    //protected HashSet<int> InvalidFrameIndexSet { get; }
    //    = new HashSet<int>();

    public bool IsStatic 
        => SamplingSpecs.IsStatic ||
           GetAnimatedGeometries().Count == 0;
    
    public bool IsAnimated 
        => !IsStatic;
    
    public GrVisualAnimatedScalar? AnimatedVisibility { get; set; }

    
    protected GrVisualElementWithAnimation3D(string name, Float64SamplingSpecs samplingSpecs) 
        : base(name)
    {
        Debug.Assert(
            samplingSpecs.SamplingRate.IsInteger()
        );

        SamplingSpecs = samplingSpecs;
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

        return SamplingSpecs.SampleIndexRange.ToImmutableSortedSet();

        //var invalidFrameIndexList =
        //    GetInvalidFrameIndices();

        //return invalidFrameIndexList.Count == 0
        //    ? SamplingSpecs
        //        .FrameIndexRange
        //        .ToImmutableSortedSet()
        //    : SamplingSpecs
        //        .FrameIndexRange
        //        .ToImmutableSortedSet()
        //        .Except(invalidFrameIndexList);
    }

    public double GetVisibility(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedVisibility is null
            ? Visibility
            : AnimatedVisibility.GetValue(time);
    }


}