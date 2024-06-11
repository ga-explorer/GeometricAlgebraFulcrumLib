namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space2D;

public abstract class GrVisualElement2D :
    IGrVisualElement2D
{
    public string Name { get; }
    

    protected GrVisualElement2D(string name)
    {
        Name = name;
    }


    public abstract bool IsValid();
}