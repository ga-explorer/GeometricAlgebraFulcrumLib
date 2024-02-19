using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a namespace language symbol that can be used to group together other symbols to form a nested tree of symbol definitions
/// </summary>
public class SymbolNamespace : SymbolWithScope
{
    protected SymbolNamespace(string symbolName, LanguageScope parentScope)
        : base(symbolName, parentScope, parentScope.RootAst.NamespaceRoleName)
    {
    }

    protected SymbolNamespace(string symbolName, LanguageScope parentScope, string symbolRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
    }

}