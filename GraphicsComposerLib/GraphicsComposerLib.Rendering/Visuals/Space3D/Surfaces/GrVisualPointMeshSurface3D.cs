using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsMesh;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

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
            GrVisualAnimationSpecs.Static
        );
    }
    
    public static GrVisualPointMeshSurface3D Create(string name, GrVisualSurfaceStyle3D style, IPointsMesh3D positionMesh, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualPointMeshSurface3D(
            name,
            style,
            positionMesh,
            animationSpecs
        );
    }

    public static GrVisualPointMeshSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVectorMesh3D positionMesh, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualPointMeshSurface3D(
            name,
            style,
            positionMesh.GetPointsMesh(animationSpecs.MinTime),
            animationSpecs
        ).SetAnimatedPositionMesh(positionMesh);
    }


    public IPointsMesh3D PositionMesh { get; }

    public GrVisualAnimatedVectorMesh3D? AnimatedPositionMesh { get; set; }

    
    private GrVisualPointMeshSurface3D(string name, GrVisualSurfaceStyle3D style, IPointsMesh3D positionMesh, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        PositionMesh = positionMesh;

        Debug.Assert(IsValid());
    }

    
    public override bool IsValid()
    {
        return PositionMesh.IsValid() &&
               AnimatedPositionMesh.IsNullOrValid(AnimationSpecs.TimeRange);
    }
    
    public GrVisualPointMeshSurface3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
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
        return AnimationSpecs.IsStatic || AnimatedPositionMesh is null
            ? PositionMesh
            : AnimatedPositionMesh.GetPointsMesh(time);
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
                GetPositionMesh(time)
            );
        }
    }

}