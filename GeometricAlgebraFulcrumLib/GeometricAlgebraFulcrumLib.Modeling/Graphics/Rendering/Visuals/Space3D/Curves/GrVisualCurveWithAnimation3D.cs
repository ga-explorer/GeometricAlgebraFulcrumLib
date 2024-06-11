using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Curves;

public abstract class GrVisualCurveWithAnimation3D :
    GrVisualElementWithAnimation3D
{
    public sealed record KeyPointsPathRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        IPointsPath3D PointsPath
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public GrVisualCurveStyle3D Style { get; }

    public abstract int PathPointCount { get; }

    public abstract double Length { get; }
    
    
    protected GrVisualCurveWithAnimation3D(string name, GrVisualCurveStyle3D style, GrVisualAnimationSpecs animationSpecs)
        : base(name, animationSpecs)
    {
        Style = style;
    }


    public abstract IPointsPath3D GetPositionsPath();

    public abstract IPointsPath3D GetPositionsPath(double time);

    
    public IEnumerable<KeyPointsPathRecord> GetKeyPointsPathRecords()
    {
        Debug.Assert(IsValid());

        foreach (var frameIndex in GetValidFrameIndexSet())
        {
            var time = (double)frameIndex / AnimationSpecs.FrameRate;
                
            yield return new KeyPointsPathRecord(
                frameIndex, 
                time, 
                GetVisibility(time),
                GetPositionsPath(time)
            );
        }
    }
}