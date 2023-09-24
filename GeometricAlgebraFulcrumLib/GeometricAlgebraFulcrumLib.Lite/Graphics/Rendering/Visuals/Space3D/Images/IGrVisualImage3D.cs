using DataStructuresLib.Basic;
using SixLabors.ImageSharp.Formats;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Images;

public interface IGrVisualImage3D
{
    Pair<int> GetSize();

    Image GetImage();

    void SaveImage(string filePath);

    void SaveImage(string filePath, IImageEncoder format);

    string Name { get; }
}