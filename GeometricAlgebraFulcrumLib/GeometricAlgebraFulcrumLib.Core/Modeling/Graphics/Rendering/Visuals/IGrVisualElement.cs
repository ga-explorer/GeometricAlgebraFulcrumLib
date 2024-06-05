using GeometricAlgebraFulcrumLib.Core.Algebra;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Visuals;

public interface IGrVisualElement :
    IAlgebraicElement
{
    public string Name { get; }
}