using GraphicsComposerLib.Rendering.Visuals.Space3D.Images;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic;

public sealed class GrVisualVector3D :
    GrVisualElement3D
{
    public ITuple3D Origin { get; set; } 
        = Tuple3D.Zero;

    public ITuple3D Direction { get; set; }
        = Tuple3D.E1;
    
    public GrVisualVectorStyle3D Style { get; set; } 
    

    public GrVisualVector3D(string name) 
        : base(name)
    {
    }


    public double GetLength()
    {
        return Direction.GetLength();
    }
    
    public Tuple3D GetUnitDirection()
    {
        return Direction.ToUnitVector();
    }
}