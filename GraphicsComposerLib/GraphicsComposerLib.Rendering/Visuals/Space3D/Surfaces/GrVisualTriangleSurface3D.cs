using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualTriangleSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        Float64Vector3D Position1,
        Float64Vector3D Position2,
        Float64Vector3D Position3
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );

    
    public static GrVisualTriangleSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D position1, IFloat64Tuple3D position2, IFloat64Tuple3D position3)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            position1,
            position2,
            position3,
            GrVisualAnimationSpecs.Static
        );
    }
    
    public static GrVisualTriangleSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D position1, IFloat64Tuple3D position2, IFloat64Tuple3D position3, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            position1,
            position2,
            position3,
            animationSpecs
        );
    }
    
    public static GrVisualTriangleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2, GrVisualAnimatedVector3D position3, GrVisualAnimationSpecs animationSpecs)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            Float64Vector3D.E3,
            animationSpecs
        ).SetAnimatedPositions(position1, position2, position3);
    }

        
    public IFloat64Tuple3D Position1 { get; }
        
    public IFloat64Tuple3D Position2 { get; }

    public IFloat64Tuple3D Position3 { get; }
        
    public IFloat64Tuple3D Direction1 
        => Position1.GetDirectionTo(Position2);
            
    public IFloat64Tuple3D Direction2 
        => Position2.GetDirectionTo(Position3);

    public IFloat64Tuple3D Direction3 
        => Position3.GetDirectionTo(Position1);
    
    public Float64Vector3D Normal
        => (Position2.VectorCross(Position1) +
           Position3.VectorCross(Position2) +
           Position1.VectorCross(Position3)).ToUnitVector();
    
    public double Area
        => (Position1.VectorCross(Position2) +
            Position2.VectorCross(Position3) +
            Position3.VectorCross(Position1)).ENorm() * 0.5d;

    public double Length1 
        => Direction1.ENorm();
    
    public double Length2 
        => Direction2.ENorm();
 
    public double Length3 
        => Direction3.ENorm();

    public GrVisualAnimatedVector3D? AnimatedPosition1 { get; set; }

    public GrVisualAnimatedVector3D? AnimatedPosition2 { get; set; }
        
    public GrVisualAnimatedVector3D? AnimatedPosition3 { get; set; }
    
    public GrVisualAnimatedVector3D? AnimatedDirection1
    {
        get
        {
            if (AnimatedPosition2 is not null && AnimatedPosition1 is not null)
                return AnimatedPosition2 - AnimatedPosition1;

            if (AnimatedPosition2 is not null && AnimatedPosition1 is null)
                return AnimatedPosition2 - Position1;

            if (AnimatedPosition2 is null && AnimatedPosition1 is not null)
                return Position2 - AnimatedPosition1;

            return null;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedDirection2
    {
        get
        {
            if (AnimatedPosition3 is not null && AnimatedPosition2 is not null)
                return AnimatedPosition3 - AnimatedPosition2;

            if (AnimatedPosition3 is not null && AnimatedPosition2 is null)
                return AnimatedPosition3 - Position2;

            if (AnimatedPosition3 is null && AnimatedPosition2 is not null)
                return Position3 - AnimatedPosition2;

            return null;
        }
    }
    
    public GrVisualAnimatedVector3D? AnimatedDirection3
    {
        get
        {
            if (AnimatedPosition1 is not null && AnimatedPosition3 is not null)
                return AnimatedPosition1 - AnimatedPosition3;

            if (AnimatedPosition1 is not null && AnimatedPosition3 is null)
                return AnimatedPosition1 - Position3;

            if (AnimatedPosition1 is null && AnimatedPosition3 is not null)
                return Position1 - AnimatedPosition3;

            return null;
        }
    }


    private GrVisualTriangleSurface3D(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D position1, IFloat64Tuple3D position2, IFloat64Tuple3D position3, GrVisualAnimationSpecs animationSpecs)
        : base(name, style, animationSpecs)
    {
        Position1 = position1;
        Position2 = position2;
        Position3 = position3;

        Debug.Assert(IsValid());
    }

    
    public override bool IsValid()
    {
        return Position1.IsValid() &&
               Position2.IsValid() &&
               Position3.IsValid() &&
               !Area.IsZero() &&
               GetAnimatedGeometries().All(g => 
                   g.IsValid(AnimationSpecs.TimeRange)
               );
    }
    
    public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
    {
        var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

        if (AnimatedVisibility is not null)
            animatedGeometries.Add(AnimatedVisibility);

        if (AnimatedPosition1 is not null)
            animatedGeometries.Add(AnimatedPosition1);

        if (AnimatedPosition2 is not null)
            animatedGeometries.Add(AnimatedPosition2);

        if (AnimatedPosition3 is not null)
            animatedGeometries.Add(AnimatedPosition3);

        return animatedGeometries;
    }
        
    public GrVisualTriangleSurface3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
    {
        AnimatedVisibility = visibility;

        return this;
    }

    public GrVisualTriangleSurface3D SetAnimatedPosition1(GrVisualAnimatedVector3D position1)
    {
        AnimatedPosition1 = position1;

        return this;
    }

    public GrVisualTriangleSurface3D SetAnimatedPosition2(GrVisualAnimatedVector3D position2)
    {
        AnimatedPosition2 = position2;

        return this;
    }
        
    public GrVisualTriangleSurface3D SetAnimatedPosition3(GrVisualAnimatedVector3D position3)
    {
        AnimatedPosition3 = position3;

        return this;
    }
    
    public GrVisualTriangleSurface3D SetAnimatedPositions(GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2, GrVisualAnimatedVector3D position3)
    {
        AnimatedPosition1 = position1;
        AnimatedPosition2 = position2;
        AnimatedPosition3 = position3;

        return this;
    }

    public Float64Vector3D GetPosition1(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition1 is null
            ? Position1.ToVector3D()
            : AnimatedPosition1.GetPoint(time);
    }
        
    public Float64Vector3D GetPosition2(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition2 is null
            ? Position2.ToVector3D()
            : AnimatedPosition2.GetPoint(time);
    }
        
    public Float64Vector3D GetPosition3(double time)
    {
        return AnimationSpecs.IsStatic || AnimatedPosition3 is null
            ? Position3.ToVector3D()
            : AnimatedPosition3.GetPoint(time);
    }
    
    public Float64Vector3D GetDirection1(double time)
    {
        return GetPosition2(time) - GetPosition1(time);
    }
        
    public Float64Vector3D GetDirection2(double time)
    {
        return GetPosition3(time) - GetPosition2(time);
    }

    public Float64Vector3D GetDirection3(double time)
    {
        return GetPosition1(time) - GetPosition3(time);
    }
    
    public double GetArea(double time)
    {
        var position1 = GetPosition1(time);
        var position2 = GetPosition2(time);
        var position3 = GetPosition3(time);

        return (position2.VectorCross(position1) +
                position3.VectorCross(position2) +
                position1.VectorCross(position3)).ENorm() * 0.5d;
    }
    
    public Float64Vector3D GetNormal(double time)
    {
        var position1 = GetPosition1(time);
        var position2 = GetPosition2(time);
        var position3 = GetPosition3(time);

        return (position2.VectorCross(position1) +
                position3.VectorCross(position2) +
                position1.VectorCross(position3)).ToUnitVector();
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
                GetPosition1(time), 
                GetPosition2(time), 
                GetPosition3(time)
            );
        }
    }
}