﻿using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualCircleArcCurve3D :
    GrVisualCurve3D
{
    public ITuple3D Center { get; set; } = Tuple3D.Zero;

    public ITuple3D Direction1 { get; set; } = Tuple3D.E1;

    public ITuple3D Direction2 { get; set; } = Tuple3D.E2;

    public double Radius { get; set; } = 1d;

    public bool InnerArc { get; set; } = true;

    public bool OuterArc
    {
        get => !InnerArc;
        set => InnerArc = !value;
    }


    public GrVisualCircleArcCurve3D(string name) 
        : base(name)
    {
    }


    public Triplet<Tuple3D> GetArcPointsTriplet()
    {
        if (InnerArc)
        {
            var vector1 = Direction1.ToUnitVector();
            var vector3 = Direction2.ToUnitVector();
            var vector2 = 0.5d * (vector1 + vector3);

            vector2 = vector2.GetLengthSquared().IsNearZero()
                ? vector1.GetUnitNormal()
                : vector2.ToUnitVector();

            return new Triplet<Tuple3D>(
                Center + Radius * vector1, 
                Center + Radius * vector2, 
                Center + Radius * vector3
            );
        }
        else
        {
            var vector1 = Direction2.ToUnitVector();
            var vector3 = Direction1.ToUnitVector();
            var vector2 = -0.5d * (vector1 + vector3);

            vector2 = vector2.GetLengthSquared().IsNearZero()
                ? vector1.GetUnitNormal()
                : vector2.ToUnitVector();

            return new Triplet<Tuple3D>(
                Center + Radius * vector1, 
                Center + Radius * vector2, 
                Center + Radius * vector3
            );
        }
    }

    public Tuple3D GetArcStartUnitVector()
    {
        return InnerArc 
            ? Direction1.ToUnitVector() 
            : Direction2.ToUnitVector();
    }
    
    public Tuple3D GetArcEndUnitVector()
    {
        return InnerArc 
            ? Direction2.ToUnitVector() 
            : Direction1.ToUnitVector();
    }
    
    public Tuple3D GetUnitNormal()
    {
        var normal = 
            Direction1.VectorCross(Direction2);

        return normal.GetLengthSquared().IsNearZero() 
            ? Direction1.GetUnitNormal() 
            : normal.ToUnitVector();
    }

    public double GetLength()
    {
        return 2d * Math.PI * Radius * GetArcRatio();
    } 

    public double GetAngle()
    {
        var arcRatio = 
            Direction1.GetVectorsAngle(Direction2);

        return InnerArc ? arcRatio : 2d * Math.PI - arcRatio;
    }

    public double GetArcRatio()
    {
        var arcRatio = 
            Direction1.GetVectorsAngle(Direction2) / (2d * Math.PI);

        return InnerArc ? arcRatio : 1d - arcRatio;
    }
}