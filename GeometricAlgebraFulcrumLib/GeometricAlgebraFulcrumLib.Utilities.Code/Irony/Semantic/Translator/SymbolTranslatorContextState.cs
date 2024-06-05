using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Irony.Parsing;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Translator;

/// <summary>
/// This class represents a single state in the parent AST translation context
/// </summary>
public sealed class SymbolTranslatorContextState
{
    private static readonly IntegerSequenceGenerator IdCounter = new IntegerSequenceGenerator();

    private static int CreateNewId()
    {
        return IdCounter.GetNewCountId();
    }


    public int StateId { get; }

    /// <summary>
    /// The role name of the language symbol being created
    /// </summary>
    public string SymbolRoleName { get; }

    /// <summary>
    /// The corresponding (source) parse tree node for the symbol being created
    /// </summary>
    public ParseTreeNode ParseNode { get; }

    /// <summary>
    /// The parent scope of the symbol being created
    /// </summary>
    public LanguageScope ParentScope { get; }

    /// <summary>
    /// True for check-pont states; used for error recovery
    /// </summary>
    public bool IsCheckPointState;


    internal SymbolTranslatorContextState(LanguageScope parentScope, string curSymbolRoleName, ParseTreeNode curParseNode)
    {
        StateId = CreateNewId();
        ParentScope = parentScope;
        ParseNode = curParseNode;
        SymbolRoleName = curSymbolRoleName;
        IsCheckPointState = false;
    }


    public override string ToString()
    {
        var s = new StringBuilder();

        if (IsCheckPointState)
            s.AppendLine("======< Checkpoint State >======");

        s.Append("State ID:   ");
        s.AppendLine(StateId.ToString());
            
        s.Append("Role Name:  ");
        s.AppendLine(SymbolRoleName);

        s.Append("Parse Node: ");
        s.AppendLine(ParseNode.ToString());

        s.Append("Scope:      ");
        s.AppendLine(ParentScope.QualifiedScopeName);

        s.AppendLine();

        return s.ToString();
    }
}