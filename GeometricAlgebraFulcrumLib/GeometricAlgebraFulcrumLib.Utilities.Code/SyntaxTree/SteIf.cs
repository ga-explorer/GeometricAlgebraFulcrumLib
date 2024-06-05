namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteIf : SteSyntaxElement
{
    public ISyntaxTreeElement Condition { get; set; }

    public ISyntaxTreeElement TrueCode { get; set; }


}