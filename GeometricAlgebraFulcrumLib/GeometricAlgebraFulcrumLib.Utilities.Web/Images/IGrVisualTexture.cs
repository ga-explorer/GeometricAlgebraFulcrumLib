using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public interface IGrVisualTexture
{
    int ImageWidth { get; }

    int ImageHeight { get; }

    Pair<int> ImageSize { get; }

    double ImageWidthToHeight { get; }

    double ImageHeightToWidth { get; }

    Image GetImage();

    string PngImageFilePath { get; }

    string GetImageUrl();
}