﻿using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Surfaces;

public sealed class GrVisualTriangleSurface3D :
    GrVisualSurfaceWithAnimation3D
{
    public sealed record KeyFrameRecord(
        int FrameIndex,
        double Time,
        double Visibility,
        LinFloat64Vector3D Position1,
        LinFloat64Vector3D Position2,
        LinFloat64Vector3D Position3
    ) : GrVisualAnimatedGeometryKeyFrameRecord(
        FrameIndex, 
        Time, 
        Visibility
    );

    
    public static GrVisualTriangleSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            position1,
            position2,
            position3,
            Float64SamplingSpecs.Static
        );
    }
    
    public static GrVisualTriangleSurface3D Create(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, Float64SamplingSpecs samplingSpecs)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            position1,
            position2,
            position3,
            samplingSpecs
        );
    }
    
    public static GrVisualTriangleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2, GrVisualAnimatedVector3D position3)
    {
        return new GrVisualTriangleSurface3D(
            name,
            style,
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64Vector3D.E3,
            position1.SamplingSpecs
        ).SetAnimatedPositions(position1, position2, position3);
    }

        
    public ILinFloat64Vector3D Position1 { get; }
        
    public ILinFloat64Vector3D Position2 { get; }

    public ILinFloat64Vector3D Position3 { get; }
        
    public ILinFloat64Vector3D Direction1 
        => Position1.GetDirectionTo(Position2);
            
    public ILinFloat64Vector3D Direction2 
        => Position2.GetDirectionTo(Position3);

    public ILinFloat64Vector3D Direction3 
        => Position3.GetDirectionTo(Position1);
    
    public LinFloat64Vector3D Normal
        => (Position2.VectorCross(Position1) +
           Position3.VectorCross(Position2) +
           Position1.VectorCross(Position3)).ToUnitLinVector3D();
    
    public double Area
        => (Position1.VectorCross(Position2) +
            Position2.VectorCross(Position3) +
            Position3.VectorCross(Position1)).VectorENorm() * 0.5d;

    public double Length1 
        => Direction1.VectorENorm();
    
    public double Length2 
        => Direction2.VectorENorm();
 
    public double Length3 
        => Direction3.VectorENorm();

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


    private GrVisualTriangleSurface3D(string name, GrVisualSurfaceStyle3D style, ILinFloat64Vector3D position1, ILinFloat64Vector3D position2, ILinFloat64Vector3D position3, Float64SamplingSpecs samplingSpecs)
        : base(name, style, samplingSpecs)
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
               GetAnimatedGeometries().All(g => g.IsValid());
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
        
    public GrVisualTriangleSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
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

    public LinFloat64Vector3D GetPosition1(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPosition1 is null
            ? Position1.ToLinVector3D()
            : AnimatedPosition1.GetValue(time);
    }
        
    public LinFloat64Vector3D GetPosition2(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPosition2 is null
            ? Position2.ToLinVector3D()
            : AnimatedPosition2.GetValue(time);
    }
        
    public LinFloat64Vector3D GetPosition3(double time)
    {
        return SamplingSpecs.IsStatic || AnimatedPosition3 is null
            ? Position3.ToLinVector3D()
            : AnimatedPosition3.GetValue(time);
    }
    
    public LinFloat64Vector3D GetDirection1(double time)
    {
        return GetPosition2(time) - GetPosition1(time);
    }
        
    public LinFloat64Vector3D GetDirection2(double time)
    {
        return GetPosition3(time) - GetPosition2(time);
    }

    public LinFloat64Vector3D GetDirection3(double time)
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
                position1.VectorCross(position3)).VectorENorm() * 0.5d;
    }
    
    public LinFloat64Vector3D GetNormal(double time)
    {
        var position1 = GetPosition1(time);
        var position2 = GetPosition2(time);
        var position3 = GetPosition3(time);

        return (position2.VectorCross(position1) +
                position3.VectorCross(position2) +
                position1.VectorCross(position3)).ToUnitLinVector3D();
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
                GetPosition1(time), 
                GetPosition2(time), 
                GetPosition3(time)
            );
        }
    }
}