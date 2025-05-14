using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

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
    
    public IndexSet GetValidFrameIndexSet()
    {
        return IsStatic 
            ? IndexSet.EmptySet 
            : SamplingSpecs.SampleIndexRange.ToDenseIndexSet();
    }

    public double GetVisibility(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedVisibility is null
            ? Visibility
            : AnimatedVisibility.GetValue(time);
    }


}