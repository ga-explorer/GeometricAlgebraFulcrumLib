using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Html.Media;

public interface IWclHtmlImageDataUrlCache :
    IWclHtmlDataUrlCache
{
    Color WhitespaceColor { get; }

    int MarginSize { get; set; }

    Color BackgroundColor { get; set; }
}