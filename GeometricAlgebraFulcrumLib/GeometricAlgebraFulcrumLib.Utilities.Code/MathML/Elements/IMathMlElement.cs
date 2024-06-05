namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements;

public interface IMathMlElement
{
    string XmlTagName { get; }

    string ContentsText { get; }

    string Class { get; }

    string Id { get; }

    string Style { get; }

    bool IsToken { get; }

    bool IsLayout { get; }
}