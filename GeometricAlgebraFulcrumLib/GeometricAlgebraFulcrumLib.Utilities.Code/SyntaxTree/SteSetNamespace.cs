namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteSetNamespace : SteSyntaxElement
{
    public string NamespaceName { get; set; }

    public ISyntaxTreeElement SubCode { get; set; }
}