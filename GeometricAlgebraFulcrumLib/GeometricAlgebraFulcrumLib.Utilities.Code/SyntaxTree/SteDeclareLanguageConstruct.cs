using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree;

public class SteDeclareLanguageConstruct : SteSyntaxElement
{
    public string Identifier { get; set; }

    public string ConstructKind { get; set; }

    public string ConstructScope { get; set; }

    public List<string> ModifiersList { get; private set; }

    public ISyntaxTreeElement DeclarationCode { get; set; }

    public List<ISyntaxTreeElement> Inherits { get; private set; }

    public List<ISyntaxTreeElement> Implements { get; private set; }

    public List<ISyntaxTreeElement> Conditions { get; private set; }


    public SteDeclareLanguageConstruct()
    {
        ModifiersList = new List<string>();
        Inherits = new List<ISyntaxTreeElement>();
        Implements = new SteSyntaxElementsList();
        Conditions = new SteSyntaxElementsList();
    }
}