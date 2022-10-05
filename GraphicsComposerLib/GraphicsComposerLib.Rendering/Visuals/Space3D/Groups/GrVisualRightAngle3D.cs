using DataStructuresLib.Basic;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Groups;

public sealed class GrVisualRightAngle3D :
    GrVisualCurve3D
{
    public ITuple3D Center { get; set; } = Tuple3D.Zero;

    public ITuple3D Direction1 { get; set; } = Tuple3D.E1;

    public ITuple3D Direction2 { get; set; } = Tuple3D.E2;

    public double Radius { get; set; } = 1d;
    

    public GrVisualRightAngle3D(string name) 
        : base(name)
    {
    }


    public Triplet<Tuple3D> GetArcPointsTriplet()
    {
        var s = Radius / Math.Sqrt(2d);

        var vector1 = Direction1.ToUnitVector();
        var vector3 = Direction2.ToUnitVector();
        var vector2 = vector1 + vector3;

        return new Triplet<Tuple3D>(
            Center + s * vector1, 
            Center + s * vector2, 
            Center + s * vector3
        );
    }

    public Tuple3D GetArcStartUnitVector()
    {
        return Direction1.ToUnitVector();
    }
    
    public Tuple3D GetArcEndUnitVector()
    {
        return Direction2.ToUnitVector();
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
        return Math.Sqrt(2) * Radius;
    } 
}