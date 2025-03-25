using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public interface IGrVisualImageSequenceSource
{
    int ImageCount { get; }

    int ImageWidth { get; }

    int ImageHeight { get; }

    double ImageWidthToHeight { get; }

    double ImageHeightToWidth { get; }

    Pair<int> ImageSize { get; }

    Image GetImage(int imageIndex);

    string GetImageDataUrlBase64(int imageIndex);

    string GetImageFilePath(int imageIndex);
}