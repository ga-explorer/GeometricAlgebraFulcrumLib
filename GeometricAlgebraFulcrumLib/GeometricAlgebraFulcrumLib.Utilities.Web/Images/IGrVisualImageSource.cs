using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public interface IGrVisualImageSource
{
    int ImageWidth { get; }

    int ImageHeight { get; }

    double ImageWidthToHeight { get; }

    double ImageHeightToWidth { get; }

    Pair<int> ImageSize { get; }

    Image GetImage();

    string GetImageDataUrlBase64();
    
    string GetPngImageFilePath();
}