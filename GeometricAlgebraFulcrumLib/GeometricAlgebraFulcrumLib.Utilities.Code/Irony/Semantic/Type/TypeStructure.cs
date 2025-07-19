using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Scope;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

/// <summary>
/// This class is a special kind of type language symbol that may contain data members of different types that can be accessed by name
/// </summary>
public abstract class TypeStructure : SymbolWithScope, ILanguageType
{
    /// <summary>
    /// The data members of the structure
    /// </summary>
    public IEnumerable<SymbolStructureDataMember> DataMembers { get; private set; }


    protected TypeStructure(string symbolName, LanguageScope parentScope)
        : this(symbolName, parentScope, parentScope.RootAst.StructureRoleName, parentScope.RootAst.StructureDataMemberRoleName)
    {
    }

    protected TypeStructure(string symbolName, LanguageScope parentScope, string symbolRoleName, string dataMemberRoleName)
        : base(symbolName, parentScope, symbolRoleName)
    {
        //if (this.ChildScope.ContainsSymbolDictionary(data_member_role_name) == false)
        //    throw new Exception("Illegal symbol dictionary name: " + data_member_role_name);

        DataMembers = ChildSymbolScope.Symbols(dataMemberRoleName).Cast<SymbolStructureDataMember>();
    }


    public SymbolStructureDataMember DefineDataMember(string symbolName, ILanguageType symbolType, string symbolRoleName, bool canRead, bool canWrite)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, canRead, canWrite);
    }

    public SymbolStructureDataMember DefineDataMember(string symbolName, ILanguageType symbolType, bool canRead, bool canWrite)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, canRead, canWrite);
    }

    public SymbolStructureDataMember DefineReadWriteDataMember(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, true, true);
    }

    public SymbolStructureDataMember DefineReadWriteDataMember(string symbolName, ILanguageType symbolType)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, true, true);
    }

    public SymbolStructureDataMember DefineReadOnlyDataMember(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, true, false);
    }

    public SymbolStructureDataMember DefineReadOnlyDataMember(string symbolName, ILanguageType symbolType)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, true, false);
    }

    public SymbolStructureDataMember DefineWriteOnlyDataMember(string symbolName, ILanguageType symbolType, string symbolRoleName)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, symbolRoleName, false, true);
    }

    public SymbolStructureDataMember DefineWriteOnlyDataMember(string symbolName, ILanguageType symbolType)
    {
        return SymbolStructureDataMember.Create(symbolName, symbolType, ChildSymbolScope, false, true);
    }


    public string TypeSignature => SymbolAccessName;

    public virtual bool IsSameType(ILanguageType languageType)
    {
        if (!(languageType is TypeStructure))
            return false;

        return ((TypeStructure)languageType).ObjectId == ObjectId;
    }

    public virtual bool IsCompatibleType(ILanguageType languageType)
    {
        if (!(languageType is TypeStructure))
            return false;

        return ((TypeStructure)languageType).ObjectId == ObjectId;
    }

    //public virtual ILanguageValue CreateInitializedValue()
    //{
    //    Dictionary<string, ILanguageValue> value = new Dictionary<string, ILanguageValue>();

    //    foreach (var data_member in this.DataMembers)
    //        value.Add(data_member.ObjectName, data_member.SymbolType.CreateInitializedValue());

    //    return ValueStructure.Create(this, value);
    //}

    //public string OperatorName
    //{
    //    get { return this.SymbolAccessName; }
    //}

    public SymbolStructureDataMember GetDataMember(string memberName)
    {
        if (ChildSymbolScope.LookupSymbol(memberName, out SymbolStructureDataMember dataMember))
            return dataMember;

        throw new KeyNotFoundException("Structure data member " + memberName + " not found");
    }

    public ILanguageType GetDataMemberType(string memberName)
    {
        if (ChildSymbolScope.LookupSymbol(memberName, out SymbolStructureDataMember dataMember))
            return dataMember.SymbolType;

        throw new KeyNotFoundException("Structure data member " + memberName + " not found");
    }
}