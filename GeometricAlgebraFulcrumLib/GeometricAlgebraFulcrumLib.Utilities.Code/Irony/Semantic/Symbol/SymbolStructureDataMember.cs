using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

/// <summary>
/// This class represents a structure data member data store language symbol.
/// </summary>
public class SymbolStructureDataMember : SymbolDataStore
{
    /// <summary>
    /// The order of the definition of this data member inside the scope of its structure
    /// </summary>
    public int DefinitionIndex { get; private set; }

    /// <summary>
    /// When True, this data member can be read from.
    /// </summary>
    public bool CanRead { get; }

    /// <summary>
    /// When True, this data member can be written to.
    /// </summary>
    public bool CanWrite { get; }

    /// <summary>
    /// Return true if this data member is read-only.
    /// </summary>
    public bool ReadOnly => (CanRead && !CanWrite);

    /// <summary>
    /// Return true if this data member is write-only.
    /// </summary>
    public bool WriteOnly => (!CanRead && CanWrite);

    /// <summary>
    /// Return true if this data member is rread-write.
    /// </summary>
    public bool CanReadWrite => (CanRead && CanWrite);


    protected SymbolStructureDataMember(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName, bool canRead, bool canWrite)
        : base(symbolName, symbolType, parentScope, symbolRoleName)
    {
        DefinitionIndex = parentScope.Symbols(symbolRoleName).Count();

        CanRead = canRead;
        CanWrite = canWrite;
    }

    protected SymbolStructureDataMember(string symbolName, ILanguageType symbolType, LanguageScope parentScope, bool canRead, bool canWrite)
        : this(symbolName, symbolType, parentScope, parentScope.RootAst.StructureDataMemberRoleName, canRead, canWrite)
    {
    }


    public static SymbolStructureDataMember Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope, string symbolRoleName, bool canRead, bool canWrite)
    {
        return new SymbolStructureDataMember(symbolName, symbolType, parentScope, symbolRoleName, canRead, canWrite);
    }

    public static SymbolStructureDataMember Create(string symbolName, ILanguageType symbolType, LanguageScope parentScope, bool canRead, bool canWrite)
    {
        return new SymbolStructureDataMember(symbolName, symbolType, parentScope, canRead, canWrite);
    }
}