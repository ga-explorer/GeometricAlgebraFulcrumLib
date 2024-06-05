namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteImportNamespaces : SteSyntaxElement
{
    public List<string> ImportedNamespaces { get; private set; }


    public SteImportNamespaces()
    {
        ImportedNamespaces = new List<string>();
    }

    public SteImportNamespaces(IEnumerable<string> importedNamespaces)
    {
        ImportedNamespaces = new List<string>(importedNamespaces);
    }
}