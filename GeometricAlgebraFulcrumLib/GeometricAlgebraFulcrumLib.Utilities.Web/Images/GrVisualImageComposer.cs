using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images;

public interface IGrVisualImageComposer
{
    Pair<int> GetImageSize();

    Image GetImage();
}