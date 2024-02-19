using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.Semantic.Scope;

/// <summary>
/// This class represents a scope that is a child of a language symbol. 
/// The child scope is also made a child of the parent scope of the language symbol automatically
/// </summary>
public sealed class ScopeSymbolChild : LanguageScope
{
    /// <summary>
    /// If this scope is a child of a language symbol this is the parent LanguageSymbol object
    /// </summary>
    public SymbolWithScope ParentLanguageSymbolWithScope { get; }


    public override LanguageSymbol ParentLanguageSymbol => ParentLanguageSymbolWithScope;


    private ScopeSymbolChild(SymbolWithScope parentSymbol)
        : base(parentSymbol.ParentScope)
    {
        ParentLanguageSymbolWithScope = parentSymbol;
    }

    private ScopeSymbolChild(SymbolWithScope parentSymbol, string scopeName)
        : base(parentSymbol.ParentScope, scopeName)
    {
        ParentLanguageSymbolWithScope = parentSymbol;
    }


    public static ScopeSymbolChild Create(SymbolWithScope parentSymbol)
    {
        var scope = new ScopeSymbolChild(parentSymbol);

        RegisterChildScope(scope.ParentScope, scope);

        return scope;
    }

    public static ScopeSymbolChild Create(SymbolWithScope parentSymbol, string scopeName)
    {
        var scope = new ScopeSymbolChild(parentSymbol, scopeName);

        RegisterChildScope(scope.ParentScope, scope);

        return scope;
    }
}