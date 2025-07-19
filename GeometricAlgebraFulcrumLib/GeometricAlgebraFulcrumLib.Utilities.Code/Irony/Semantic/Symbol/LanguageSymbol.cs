using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.SourceCode;
using Irony.Parsing;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This abstract class is the base for all language symbols
/// </summary>
public abstract class LanguageSymbol : IronyAstObjectNamed, ILanguageSymbol, IEquatable<LanguageSymbol>
{
    /// <summary>
    /// Role of this symbol
    /// </summary>
    public LanguageRole SymbolRole { get; }

    /// <summary>
    /// Role name of this symbol
    /// </summary>
    public string SymbolRoleName => SymbolRole.RoleName;

    public override string ObjectName { get; }

    /// <summary>
    /// The list of parse tree nodes on which the symbol is based
    /// </summary>
    //private List<ParseTreeNode> _parseNodes;

    /// <summary>
    /// The list of code locations on which the symbol is based
    /// </summary>
    private List<IronyAstObjectCodeLocation> _codeLocations;

    /// <summary>
    /// The list of parse tree nodes on which the symbol is based.
    /// </summary>
    public IEnumerable<ParseTreeNode> ParseNodes
    {
        get
        {
            return _codeLocations.Select(item => item.CodeParseNode);
        }
    }

    /// <summary>
    /// The list of code locations on which the symbol is based.
    /// </summary>
    public IEnumerable<IronyAstObjectCodeLocation> CodeLocations => _codeLocations;

    /// <summary>
    /// The first parse node for this language symbol
    /// </summary>
    public ParseTreeNode ParseNode
    {
        get
        {
            if (ReferenceEquals(_codeLocations, null) || _codeLocations.Count == 0)
                return null;

            return _codeLocations[0].CodeParseNode;
        }
    }

    /// <summary>
    /// The first code location for this symbol
    /// </summary>
    public IronyAstObjectCodeLocation CodeLocation
    {
        get
        {
            if (ReferenceEquals(_codeLocations, null) || _codeLocations.Count == 0)
                return null;

            return _codeLocations[0];
        }
        set
        {
            if (ReferenceEquals(_codeLocations, null))
            {
                if (ReferenceEquals(value, null))
                    return;

                _codeLocations = new List<IronyAstObjectCodeLocation> { value };

                return;
            }

            if (_codeLocations.Count != 0)
                throw new Exception("First code location already assigned");

            if (ReferenceEquals(value, null) == false)
                _codeLocations.Add(value);
        }
    }

    /// <summary>
    /// Add a parse node for this language symbol
    /// </summary>
    /// <param name="codeLocation"></param>
    /// <returns></returns>
    public IronyAstObjectCodeLocation AddCodeLocation(IronyAstObjectCodeLocation codeLocation)
    {
        if (ReferenceEquals(_codeLocations, null))
            _codeLocations = new List<IronyAstObjectCodeLocation>();

        _codeLocations.Add(codeLocation);

        return codeLocation;
    }

    //public SymbolNamespace ParentNamespace
    //{
    //    get
    //    {
    //        if (this.ParentDSL.UseNamespaces)
    //        {
    //            LanguageSymbol cur_symbol = this.ParentLanguageSymbol;

    //            while (ReferenceEquals(cur_symbol, null) == false)
    //            {
    //                if (cur_symbol is SymbolNamespace)
    //                    return (SymbolNamespace)cur_symbol;

    //                cur_symbol = cur_symbol.ParentLanguageSymbol;
    //            }
    //        }

    //        return null;
    //    }
    //}

    /// <summary>
    /// Return the suitable string used to access this symbol. For exmple, local variables can only be
    /// accessed within their blocks not from outside and only thier names are used for access. Where
    /// classes can be accessed from scopes other than the scope of definition and a full qualification 
    /// is required for access.
    /// </summary>
    public string SymbolAccessName => SymbolRole.EnableAccessByQualification ? 
        SymbolQualifiedName : 
        ObjectName;

    /// <summary>
    /// Return the fully qualified name of this symbol by following the ParentLanguageSymbol property 
    /// until the root symbol is reached. Only parent symbols having a LanguageRole's EnableQualification property 
    /// set to true are included in the fully qualified name.
    /// </summary>
    public string SymbolQualifiedName
    {
        get
        {
            var s = new StringBuilder(256);

            var qualList = new List<string>(16) {ObjectName};

            //The addition of the this symbol name is always independent of the this.SymbolRole.EnableQualification attribute

            var curSymbol = ParentLanguageSymbol;

            while (ReferenceEquals(curSymbol, null) == false)
            {
                if (curSymbol.SymbolRole.EnableAccessByQualification)
                    qualList.Add(curSymbol.ObjectName);

                curSymbol = curSymbol.ParentLanguageSymbol;
            }

            for (var i = qualList.Count - 1; i >= 0; i--)
            {
                s.Append(qualList[i]);

                if (i > 0) s.Append(".");
            }

            return s.ToString();
        }
    }


    protected LanguageSymbol(string symbolName, LanguageScope parentScope, string symbolRoleName)
        : base(parentScope)
    {
        ObjectName = symbolName;
        SymbolRole = ParentScope.RootAst.RoleDictionary[symbolRoleName];

        ParentScope.AddLangugeSymbol(this);
    }

    protected LanguageSymbol(LanguageScope parentScope, string symbolRoleName)
        : base(parentScope)
    {
        ObjectName = "tmp_" + ObjectId.ToString("X");//.PadLeft(9, '_');
        SymbolRole = ParentScope.RootAst.RoleDictionary[symbolRoleName];

        ParentScope.AddLangugeSymbol(this);
    }


    public override string ToString()
    {
        return SymbolAccessName;
    }

    public bool Equals(LanguageSymbol other)
    {
        return other != null && (other.ObjectId == ObjectId);
    }
}