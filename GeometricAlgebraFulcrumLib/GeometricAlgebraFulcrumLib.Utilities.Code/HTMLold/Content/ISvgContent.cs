namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Content;

public interface IHtmlContent
{
    bool IsContentText { get; }

    bool IsContentComment { get; }

    bool IsContentElement { get; }
}