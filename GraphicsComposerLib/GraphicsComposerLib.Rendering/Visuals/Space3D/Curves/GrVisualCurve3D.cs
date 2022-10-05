namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public abstract class GrVisualCurve3D :
    GrVisualElement3D
{
    public GrVisualCurveStyle3D Style { get; set; }


    protected GrVisualCurve3D(string name) 
        : base(name)
    {
    }
}