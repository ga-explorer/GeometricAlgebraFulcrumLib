namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteAccessArrayItem : SteSyntaxElement
{
    public bool ReadAccess { get; set; }

    public string VariableName { get; set; }

    public ISyntaxTreeElement ItemIndex { get; set; }
}