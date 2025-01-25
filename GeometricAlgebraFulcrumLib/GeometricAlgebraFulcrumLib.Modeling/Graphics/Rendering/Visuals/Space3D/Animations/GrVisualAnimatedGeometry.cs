using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public abstract class GrVisualAnimatedGeometry :
    IAlgebraicElement
{
    public Float64SamplingSpecs SamplingSpecs { get; }

    //public IReadOnlyList<int> InvalidFrameIndexList { get; private set; }
    //    = ImmutableArray<int>.Empty;

    public Float64ScalarRange TimeRange 
        => SamplingSpecs.TimeRange;
    
    public Int32Range1D SampleIndexRange 
        => SamplingSpecs.SampleIndexRange;

    public int SamplingRate 
        => (int)SamplingSpecs.SamplingRate;
    
    public double TimeResolution
        => SamplingSpecs.TimeResolution;

    public double MinTime 
        => SamplingSpecs.MinTime;

    public double MaxTime 
        => SamplingSpecs.MaxTime;
    
    public int MinSampleIndex 
        => SamplingSpecs.MinSampleIndex;
    
    public int MaxSampleIndex 
        => SamplingSpecs.MaxSampleIndex;

    public IEnumerable<KeyValuePair<int, double>> SampleIndexTimePairs
        => SamplingSpecs.SampleIndexTimePairs;

    
    protected GrVisualAnimatedGeometry(Float64SamplingSpecs samplingSpecs)
    {
        if (samplingSpecs.IsStatic || !samplingSpecs.SamplingRate.IsInteger())
            throw new InvalidOperationException();

        SamplingSpecs = samplingSpecs;

        //SetInvalidFrameIndices(invalidFrameIndices);

        //Debug.Assert(
        //    InvalidFrameIndexList.Count == 0 ||
        //    InvalidFrameIndexList.All(samplingSpecs.FrameIndexRange.Contains)
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

    //    else if (invalidFrameIndexList.All(SamplingSpecs.FrameIndexRange.Contains))
    //        InvalidFrameIndexList = invalidFrameIndexList;

    //    else
    //        throw new InvalidOperationException();
    //}
}