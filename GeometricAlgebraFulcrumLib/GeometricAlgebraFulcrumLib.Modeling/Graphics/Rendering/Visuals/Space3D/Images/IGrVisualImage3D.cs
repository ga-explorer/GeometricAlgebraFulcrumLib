using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Images;

public interface IGrVisualImage3D
{
    Pair<int> GetSize();

    Image GetImage();

    void SaveImage(string filePath);

    void SaveImage(string filePath, IImageEncoder format);

    string Name { get; }
}