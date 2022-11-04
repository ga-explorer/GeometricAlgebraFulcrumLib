using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualVector3D :
    GrVisualElement3D,
    ITuple3D
{
    public double Item1 => Direction.X;
        
    public double Item2 => Direction.Y;
        
    public double Item3 => Direction.Z;
        
    public double X => Direction.X;
        
    public double Y => Direction.Y;
        
    public double Z => Direction.Z;

    public ITuple3D Origin { get; } 

    public ITuple3D Direction { get; }
    
    public GrVisualVectorStyle3D Style { get; set; } 
    
    
    public GrVisualVector3D(string name, ITuple3D direction) 
        : base(name)
    {
        Origin = Tuple3D.Zero;
        Direction = direction;
    }

    public GrVisualVector3D(string name, ITuple3D origin, ITuple3D direction) 
        : base(name)
    {
        Origin = origin;
        Direction = direction;
    }


    public double GetLength()
    {
        return Direction.GetLength();
    }
    
    public Tuple3D GetUnitDirection()
    {
        return Direction.ToUnitVector();
    }

    public bool IsValid()
    {
        return Origin.IsValid() &&
               Direction.IsValid();
    }
}