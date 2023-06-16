using GeometricAlgebraFulcrumLib.MathBase.BasicMath;

namespace GraphicsComposerLib.Rendering.Visuals
{
    public interface IGrVisualElement :
        IGeometricElement
    {
        public string Name { get; }
    }
}