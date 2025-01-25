using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Meshes.PointsMesh;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualPointMeshSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex, 
        double Time,
        double Visibility,
        IPointsMesh3D PositionMesh
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex,
        Time,
        Visibility
    );


    public static GrVisualPointMeshSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IPointsMesh3D positionMesh)
    {
        return new GrVisualPointMeshSurface3D(
            name,
            style,
            positionMesh,
            Float64SamplingSpecs.Static
        );
    }
    
    public static GrVisualPointMeshSurface3D Create(string name, GrVisualSurfaceStyle3D style, IPointsMesh3D positionMesh, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualPointMeshSurface3D(
            name,
            style,
            positionMesh,
            samplingSpecs
        );
    }

    public static GrVisualPointMeshSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVectorMesh3D positionMesh)
    {
        return new GrVisualPointMeshSurface3D(
            name,
            style,
            positionMesh.GetPointsMesh(positionMesh.SamplingSpecs.MinTime),
            positionMesh.SamplingSpecs
        ).SetAnimatedPositionMesh(positionMesh);
    }


    public IPointsMesh3D PositionMesh { get; }

    public GrVisualAnimatedVectorMesh3D? AnimatedPositionMesh { get; set; }

    
    private GrVisualPointMeshSurface3D(string name, GrVisualSurfaceStyle3D style, IPointsMesh3D positionMesh, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
    {
        PositionMesh = positionMesh;

        Debug.Assert(IsValid());
    }

    
    public override bool IsValid()
    {
        return PositionMesh.IsValid() &&
               AnimatedPositionMesh.IsNullOrValid(SamplingSpecs.TimeRange);
    }
    
    public GrVisualPointMeshSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualPointMeshSurface3D SetAnimatedPositionMesh(GrVisualAnimatedVectorMesh3D positionMesh)
    {
        AnimatedPositionMesh = positionMesh;
        
        return this;
    }
    
    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPositionMesh is not null)
            animatedGeometries.Add(AnimatedPositionMesh);

        return animatedGeometries;
    }
    
    public IPointsMesh3D GetPositionMesh(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPositionMesh is null
            ? PositionMesh
            : AnimatedPositionMesh.GetPointsMesh(time);
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
                GetPositionMesh(time)
            );
        }
    }

}