using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.Irony.Semantic.Symbol;

namespace CodeComposerLib.Irony.Semantic.Scope;

/// <summary>
/// This class represents a language scope (a symbol table). 
/// A scope contains one or more language symbols accessable by unique names (within a single scope).
/// </summary>
public abstract class LanguageScope : IronyAstObjectNamed
{
    /// <summary>
    /// The name of the scope
    /// </summary>
    public override string ObjectName { get; }

    /// <summary>
    /// The full name of the scope including the names of its parent scopes
    /// </summary>
    public virtual string QualifiedScopeName => (ReferenceEquals(ParentScope, null) ? "" : ParentScope.QualifiedScopeName + ".") + ObjectName;

    /// <summary>
    /// The child scopes of this scope
    /// </summary>
    private List<LanguageScope> _childScopes;

    /// <summary>
    /// The internal datastructure containing all symbols for this scope
    /// </summary>
    private readonly Dictionary<string, LanguageSymbol> _symbols = new Dictionary<string, LanguageSymbol>();

    /// <summary>
    /// True if this scope has a parent scope
    /// </summary>
    public bool HasParentScope => ParentScope != null;

    /// <summary>
    /// Number of direct child scopes for this scope
    /// </summary>
    public int ChildScopeCount => ReferenceEquals(_childScopes, null) ? 0 : _childScopes.Count;


    protected LanguageScope(LanguageScope parentScope)
        : base(parentScope)
    {
        ObjectName = "scope_" + ObjectId.ToString("X");
    }

    protected LanguageScope(LanguageScope parentScope, string scopeName)
        : base(parentScope)
    {
        ObjectName = scopeName;
    }


    /// <summary>
    /// Remove all child scopes and child symbols from this scope
    /// </summary>
    public virtual void Clear()
    {
        _symbols.Clear();
        _childScopes.Clear();
    }

    /// <summary>
    /// Go up the scope chain until reaching a scope of a namespace and return that namespace if exists
    /// </summary>
    public override SymbolNamespace NearsestParentNamespace
    {
        get
        {
            if (RootAst.UseNamespaces == false)
                return null;

            var curScope = this;

            while (ReferenceEquals(curScope, null) == false)
            {
                var scope = curScope as ScopeSymbolChild;

                if (ReferenceEquals(scope, null) == false && scope.ParentLanguageSymbol is SymbolNamespace)
                    return (SymbolNamespace)scope.ParentLanguageSymbol;

                curScope = curScope.ParentScope;
            }

            return null;
        }
    }

    /// <summary>
    /// Go up the scope chain until reaching a scope of a symbol with a given role name and return that symbol if exists
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public SymbolWithScope GetNearsestParentSymbolWithRole(string roleName)
    {
        var curScope = this;

        while (ReferenceEquals(curScope, null) == false)
        {
            var scope = curScope as ScopeSymbolChild;

            if (ReferenceEquals(scope, null) == false && scope.ParentLanguageSymbol.SymbolRoleName == roleName)
                return scope.ParentLanguageSymbolWithScope;

            curScope = curScope.ParentScope;
        }

        return null;
    }

    /// <summary>
    /// Go up the scope chain until reaching a scope of a symbol with a role name in the given list of role names and return that symbol if exists
    /// </summary>
    /// <param name="roleNames"></param>
    /// <returns></returns>
    public SymbolWithScope GetNearsestParentSymbolWithRoles(IEnumerable<string> roleNames)
    {
        var curScope = this;

        var roleNamesArray = roleNames.ToArray();

        while (ReferenceEquals(curScope, null) == false)
        {
            var scope = curScope as ScopeSymbolChild;

            if (ReferenceEquals(scope, null) == false && roleNamesArray.Contains(scope.ParentLanguageSymbol.SymbolRoleName))
                return scope.ParentLanguageSymbolWithScope;

            curScope = curScope.ParentScope;
        }

        return null;
    }



    /// <summary>
    /// Returns a child scope given its index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public LanguageScope ChildScope(int index)
    {
        return ReferenceEquals(_childScopes, null) ? null : _childScopes[index];
    }


    /// <summary>
    /// Return the number of symbols in this scope
    /// </summary>
    public int SymbolsCount => _symbols.Count;


    /// <summary>
    /// Get a list of all child scopes of this scope
    /// </summary>
    public IEnumerable<LanguageScope> ChildScopes
    {
        get
        {
            if (ReferenceEquals(_childScopes, null))
                yield break;

            foreach (var scope in _childScopes)
                yield return scope;
        }
    }

    /// <summary>
    /// Get a list of all ancestor scopes of this scope
    /// </summary>
    public IEnumerable<LanguageScope> AncestorScopes
    {
        get
        {
            if (ReferenceEquals(ParentScope, null))
                yield break;

            for (var scope = ParentScope; !ReferenceEquals(scope, null); scope = scope.ParentScope)
                yield return scope;
        }
    }

    /// <summary>
    /// Get a list of scopes starting from this scope then all its ancestors
    /// </summary>
    public IEnumerable<LanguageScope> UpChainScopes
    {
        get
        {
            for (var scope = this; ReferenceEquals(scope, null) == false; scope = scope.ParentScope)
                yield return scope;
        }
    }

    /// <summary>
    /// Get a list of scopes starting from this scope and going down the full scope tree
    /// </summary>
    public IEnumerable<LanguageScope> DownChainScopes
    {
        get
        {
            var scopeStack = new Stack<LanguageScope>();
            scopeStack.Push(this);

            while (scopeStack.Count > 0)
            {
                var scope = scopeStack.Pop();

                yield return scope;

                if (ReferenceEquals(scope._childScopes, null))
                    continue;

                foreach (var childScope in scope._childScopes)
                    scopeStack.Push(childScope);
            }
        }
    }

    /// <summary>
    /// Get a list of all scopes under this scope going down the full scope tree
    /// </summary>
    public IEnumerable<LanguageScope> DescendantScopes
    {
        get
        {
            if (ReferenceEquals(_childScopes, null))
                yield break;

            var scopeStack = new Stack<LanguageScope>();

            foreach (var childScope in _childScopes)
                scopeStack.Push(childScope);

            while (scopeStack.Count > 0)
            {
                var scope = scopeStack.Pop();

                yield return scope;

                if (ReferenceEquals(scope._childScopes, null))
                    continue;

                foreach (var childScope in scope._childScopes)
                    scopeStack.Push(childScope);
            }
        }
    }

    /// <summary>
    /// Defind a new child scope of this scope
    /// </summary>
    /// <returns></returns>
    public ScopeScopeChild DefineChildScope()
    {
        return ScopeScopeChild.Create(this);
    }

    /// <summary>
    /// Defind a new child scope of this scope
    /// </summary>
    /// <param name="childScopeName">The name of the child scope</param>
    /// <returns></returns>
    public ScopeScopeChild DefineChildScope(string childScopeName)
    {
        return ScopeScopeChild.Create(this, childScopeName);
    }


    /// <summary>
    /// Add a language symbol to the symbol dictionary of this scope
    /// </summary>
    /// <param name="symbol">The LanguageSymbol object to be added</param>
    /// <returns></returns>
    internal LanguageSymbol AddLangugeSymbol(LanguageSymbol symbol)
    {
        _symbols.Add(symbol.ObjectName, symbol);

        return symbol;
    }

    /// <summary>
    /// Use this with extreme caution because you may remove a symbol that is used by other active symbols
    /// </summary>
    /// <param name="symbolName"></param>
    internal void RemoveLanguageSymbol(string symbolName)
    {
        _symbols.Remove(symbolName);
    }

    /// <summary>
    /// Remove a local variable from this scope
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    internal bool RemoveLocalVariable(SymbolLocalVariable symbol)
    {
        return _symbols.Remove(symbol.ObjectName);
    }

    /// <summary>
    /// Returns true if any symbol dictionary contains a symbol with the given name
    /// </summary>
    /// <param name="symbolName">The name of the symbol</param>
    /// <returns></returns>
    public bool SymbolExists(string symbolName)
    {
        return _symbols.ContainsKey(symbolName);
    }

    /// <summary>
    /// Tests ths existance of a symbol with the given name and role name in this scope
    /// </summary>
    /// <param name="symbolName">The name of the symbol</param>
    /// <param name="roleName">The role name of the symbol</param>
    /// <returns></returns>
    public bool SymbolExists(string symbolName, string roleName)
    {
        return _symbols.TryGetValue(symbolName, out var symbol) && (symbol.SymbolRoleName == roleName);
    }

    /// <summary>
    /// Tests ths existance of a symbol with the given name and any role in the given list of role names in this scope
    /// </summary>
    /// <param name="symbolName"></param>
    /// <param name="roleNames"></param>
    /// <returns></returns>
    public bool SymbolExists(string symbolName, IEnumerable<string> roleNames)
    {
        return _symbols.TryGetValue(symbolName, out var symbol) && roleNames.Contains(symbol.SymbolRoleName);
    }


    public bool LookupSymbol(string symbolName, out LanguageSymbol symbol)
    {
        return _symbols.TryGetValue(symbolName, out symbol);
    }

    public bool LookupSymbol<T>(string symbolName, out T outSymbol) where T : LanguageSymbol
    {
        if (_symbols.TryGetValue(symbolName, out var symbol) && (symbol is T))
        {
            outSymbol = (T)symbol;
            return true;
        }

        outSymbol = null;
        return false;
    }

    public bool LookupSymbol(string symbolName, string roleName, out LanguageSymbol symbol)
    {
        if (_symbols.TryGetValue(symbolName, out symbol) && (symbol.SymbolRoleName == roleName))
            return true;

        symbol = null;
        return false;
    }
        
    public bool LookupSymbol<T>(string symbolName, string roleName, out T outSymbol) where T : LanguageSymbol
    {
        if (_symbols.TryGetValue(symbolName, out var symbol) && (symbol.SymbolRoleName == roleName) && (symbol is T))
        {
            outSymbol = (T)symbol;
            return true;
        }

        outSymbol = null;
        return false;
    }

    public bool LookupSymbol(string symbolName, IEnumerable<string> roleNames, out LanguageSymbol symbol)
    {
        if (_symbols.TryGetValue(symbolName, out symbol) && roleNames.Contains(symbol.SymbolRoleName))
            return true;
            
        symbol = null;
        return false;
    }


    public LanguageSymbol GetSymbol(string symbolName)
    {
        return _symbols[symbolName];
    }

    public LanguageSymbol GetSymbol(string symbolName, string roleName)
    {
        var symbol = _symbols[symbolName];

        if (symbol.SymbolRoleName == roleName)
            return symbol;
            
        throw new KeyNotFoundException();
    }

    public LanguageSymbol GetSymbol(string symbolName, IEnumerable<string> roleNames)
    {
        var symbol = _symbols[symbolName];

        if (roleNames.Contains(symbol.SymbolRoleName))
            return symbol;
            
        throw new KeyNotFoundException();
    }


    /// <summary>
    /// Enumerate all direct child symbols under this scope
    /// </summary>
    /// <returns>The enumerated symbols</returns>
    public IEnumerable<LanguageSymbol> Symbols()
    {
        return _symbols.Select(pair => pair.Value);
    }

    /// <summary>
    /// Enumerate all direct child symbols with the given role name under this scope
    /// </summary>
    /// <param name="roleName">The role name of the symbols</param>
    /// <returns>The enumerated symbols</returns>
    public IEnumerable<LanguageSymbol> Symbols(string roleName)
    {
        return
            _symbols
                .Where(pair => pair.Value.SymbolRoleName == roleName)
                .Select(pair => pair.Value);
    }

    /// <summary>
    /// Enumerate all direct child symbols with any of the given role names under this scope
    /// </summary>
    /// <param name="roleNames">The list of allowed role names</param>
    /// <returns>The enumerated symbols</returns>
    public IEnumerable<LanguageSymbol> Symbols(IEnumerable<string> roleNames)
    {
        return 
            _symbols
                .Where(pair => roleNames.Contains(pair.Value.SymbolRoleName))
                .Select(pair => pair.Value);
    }


    public override string ToString()
    {
        return QualifiedScopeName;
    }


    /// <summary>
    /// Add a child scope to a parent scope. This should be called in the constructor of the child scope
    /// </summary>
    /// <param name="parentScope">The parent scope</param>
    /// <param name="childScope">The child scope</param>
    protected static void RegisterChildScope(LanguageScope parentScope, LanguageScope childScope)
    {
        if (ReferenceEquals(parentScope._childScopes, null))
            parentScope._childScopes = new List<LanguageScope>();

        parentScope._childScopes.Add(childScope);
    }
}