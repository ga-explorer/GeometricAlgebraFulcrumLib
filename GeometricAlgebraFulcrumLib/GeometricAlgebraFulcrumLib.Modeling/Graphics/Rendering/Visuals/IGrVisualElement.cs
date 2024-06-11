using GeometricAlgebraFulcrumLib.Algebra;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;

public interface IGrVisualElement :
    IAlgebraicElement
{
    public string Name { get; }
}