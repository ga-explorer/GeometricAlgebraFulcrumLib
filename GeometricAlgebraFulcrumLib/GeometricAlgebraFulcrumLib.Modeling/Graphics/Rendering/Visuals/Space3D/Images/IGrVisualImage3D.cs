using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public interface IGrVisualImage3D :
    IGrVisualElement3D
{
    Image GetImage();
}