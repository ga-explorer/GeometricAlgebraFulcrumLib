namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteWhileLoop : SteSyntaxElement
{
    public bool DoLoop { get; set; }

    public ISyntaxTreeElement LoopCondition { get; set; }

    public ISyntaxTreeElement LoopCode { get; set; }

}