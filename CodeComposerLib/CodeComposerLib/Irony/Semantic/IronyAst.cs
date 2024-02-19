using System;
using System.Collections.Generic;
using CodeComposerLib.Irony.DSLDebug;
using CodeComposerLib.Irony.Semantic.Expression.Value;
using CodeComposerLib.Irony.Semantic.Operator;
using CodeComposerLib.Irony.Semantic.Scope;
using CodeComposerLib.Irony.Semantic.Type;
using DataStructuresLib;

namespace CodeComposerLib.Irony.Semantic;

/// <summary>
/// This class represents the root of the Irony DSL Abstract Syntax Tree (AST). 
/// </summary>
public abstract class IronyAst : IIronyAstObject
{
    private static readonly IntegerSequenceGenerator IdCounter = new IntegerSequenceGenerator();

    private static int CreateNewId()
    {
        return IdCounter.GetNewCountId();
    }


    /// <summary>
    /// The name of the root scope of the Irony DSL
    /// </summary>
    public static readonly string RootScopeName = "root_scope";


    /// <summary>
    /// A unique ID for this Irony DSL
    /// </summary>
    public int IronyDslId { get; private set; }

    /// <summary>
    /// The creation time of this AST
    /// </summary>
    public DateTime CreationTime { get; private set; }

    /// <summary>
    /// The root global scope for this Irony DSL. Only one root scope is allowed
    /// </summary>
    public ScopeRoot RootScope { get; }


    /// <summary>
    /// A dictionary holding all LanguageRole objects for this Irony DSL
    /// </summary>
    public SortedDictionary<string, LanguageRole> RoleDictionary { get; } = new SortedDictionary<string, LanguageRole>();

    /// <summary>
    /// A dictionary holding all primitive language operators definitions for this Irony DSL
    /// </summary>
    public SortedDictionary<string, OperatorPrimitive> OperatorPrimitiveDictionary { get; } = new SortedDictionary<string, OperatorPrimitive>();

    /// <summary>
    /// True if the use of namespaces is allowed in this Irony DSL
    /// </summary>
    public bool UseNamespaces { get; protected set; }

    /// <summary>
    /// True if the use of breakpoints is allowed in this Irony DSL
    /// </summary>
    public bool EnableBreakpoints = false;


    private TypePrimitive _unitType;

    /// <summary>
    /// A reference to the 'unit' primitive type for this language (like 'void' in C++ and 'unit' in F#)
    /// </summary>
    public TypePrimitive UnitType 
    {
        get
        {
            return _unitType;
        }
        set
        {
            if (ReferenceEquals(_unitType, null) && ReferenceEquals(value, null) == false)
                _unitType = value;
        }
    }


    private string _typePrimitiveRoleName;

    /// <summary>
    /// The name of the role assigned to any primitive type symbol in this language
    /// </summary>
    public string TypePrimitiveRoleName
    {
        get
        {
            return _typePrimitiveRoleName;
        }
        set
        {
            if (ReferenceEquals(_typePrimitiveRoleName, null))
                _typePrimitiveRoleName = value;
        }
    }

    private string _namespaceRoleName;

    /// <summary>
    /// The name of the role assigned to any namespace symbol in this language
    /// </summary>
    public string NamespaceRoleName
    {
        get
        {
            return _namespaceRoleName;
        }
        set
        {
            if (ReferenceEquals(_namespaceRoleName, null))
                _namespaceRoleName = value;
        }
    }

    private string _procedureRoleName;

    /// <summary>
    /// The name of the role assigned to any procedure symbol in this language
    /// </summary>
    public string ProcedureRoleName
    {
        get
        {
            return _procedureRoleName;
        }
        set
        {
            if (ReferenceEquals(_procedureRoleName, null))
                _procedureRoleName = value;
        }
    }

    private string _procedureParameterRoleName;

    /// <summary>
    /// The name of the role assigned to any procedure parameter symbol in this language
    /// </summary>
    public string ProcedureParameterRoleName
    {
        get
        {
            return _procedureParameterRoleName;
        }
        set
        {
            if (ReferenceEquals(_procedureParameterRoleName, null))
                _procedureParameterRoleName = value;
        }
    }

    private string _localVariableRoleName;

    /// <summary>
    /// The name of the role assigned to any local variable symbol in this language
    /// </summary>
    public string LocalVariableRoleName
    {
        get
        {
            return _localVariableRoleName;
        }
        set
        {
            if (ReferenceEquals(_localVariableRoleName, null))
                _localVariableRoleName = value;
        }
    }

    private string _structureRoleName;

    /// <summary>
    /// The name of the role assigned to any structure symbol in this language
    /// </summary>
    public string StructureRoleName
    {
        get
        {
            return _structureRoleName;
        }
        set
        {
            if (ReferenceEquals(_structureRoleName, null))
                _structureRoleName = value;
        }
    }

    private string _structureDataMemberRoleName;

    /// <summary>
    /// The name of the role assigned to any structure data member symbol in this language
    /// </summary>
    public string StructureDataMemberRoleName
    {
        get
        {
            return _structureDataMemberRoleName;
        }
        set
        {
            if (ReferenceEquals(_structureDataMemberRoleName, null))
                _structureDataMemberRoleName = value;
        }
    }

    public IronyAst RootAst => this;

    private readonly IronyAstDescription _astDescription;


    /// <summary>
    /// Create a Irony DSL with a root global scope
    /// </summary>
    protected IronyAst(IronyAstDescription astDescription)
    {
        IronyDslId = CreateNewId();

        CreationTime = DateTime.Now;

        _astDescription = astDescription;

        RootScope = ScopeRoot.Create(this, RootScopeName);
    }



    protected abstract void InitializeLanguageRoles();

    protected abstract void InitializeFixedLanguageRoleNames();

    protected abstract void InitializeLanguageOperators();

    protected abstract void InitializePrimitiveLanguageTypes();
    //{
    //    this.RootScope.DefineSymbolDictionary(this.TypePrimitiveRoleName);
    //}

    public virtual void InitializeAst()
    {
        InitializeFixedLanguageRoleNames();

        InitializeLanguageRoles();

        InitializePrimitiveLanguageTypes();

        InitializeLanguageOperators();
    }

    /// <summary>
    /// Perform any finalizations after fully translating the AST
    /// </summary>
    public abstract void FinalizeAst();


    /// <summary>
    /// Define all language roles given in the list of role names
    /// </summary>
    /// <param name="roleNames">A list of role names to be defined</param>
    /// <param name="enableQual">A flag for full symbol qualification of the roles</param>
    public void DefineLanguageRoles(IEnumerable<string> roleNames, bool enableQual)
    {
        foreach (var roleName in roleNames)
            DefineLanguageRole(roleName, enableQual);
    }

    /// <summary>
    /// Define a language role with a given name
    /// </summary>
    /// <param name="roleName">The role name</param>
    /// <param name="enableQual">A flag for full symbol qualification of the role</param>
    /// <returns></returns>
    public LanguageRole DefineLanguageRole(string roleName, bool enableQual)
    {
        if (RoleDictionary.ContainsKey(roleName))
            return RoleDictionary[roleName];

        var newRole = new LanguageRole(roleName, enableQual);

        RoleDictionary.Add(roleName, newRole);

        return newRole;
    }

    /// <summary>
    /// Define a language role with a given name and description
    /// </summary>
    /// <param name="roleName">The role name</param>
    /// <param name="roleSecription">The role description</param>
    /// <param name="enableQual">A flag for full symbol qualification of the role</param>
    /// <returns></returns>
    public LanguageRole DefineLanguageRole(string roleName, string roleSecription, bool enableQual)
    {
        if (RoleDictionary.ContainsKey(roleName))
            return RoleDictionary[roleName];

        var newRole = new LanguageRole(roleName, roleSecription, enableQual);

        RoleDictionary.Add(roleName, newRole);

        return newRole;
    }

    /// <summary>
    /// Define a new primitive language operator for this language
    /// </summary>
    /// <param name="opName">The operator name</param>
    /// <param name="opSymbolString">The operator symbol</param>
    /// <returns></returns>
    public OperatorPrimitive DefineLanguageOperatorPrimitive(string opName, string opSymbolString)
    {
        if (OperatorPrimitiveDictionary.ContainsKey(opName))
            return OperatorPrimitiveDictionary[opName];

        var newOp = OperatorPrimitive.Create(opName, opSymbolString);

        OperatorPrimitiveDictionary.Add(opName, newOp);

        return newOp;
    }

    /// <summary>
    /// Define a new primitive language operator for this language
    /// </summary>
    /// <param name="opName">The operator name</param>
    /// <returns></returns>
    public OperatorPrimitive DefineLanguageOperatorPrimitive(string opName)
    {
        if (OperatorPrimitiveDictionary.ContainsKey(opName))
            return OperatorPrimitiveDictionary[opName];

        var newOp = OperatorPrimitive.Create(opName);

        OperatorPrimitiveDictionary.Add(opName, newOp);

        //if (EnableLogger)
        //    _Logger.AddFullLine("Defining language role " + new_op.OperatorName);

        return newOp;
    }

    /// <summary>
    /// Define the 'unit' primitive type for this language. There can only be one such type per Irony DSL
    /// </summary>
    /// <param name="symbolName"></param>
    /// <returns></returns>
    public TypePrimitive DefineTypePrimitiveUnit(string symbolName)
    {
        UnitType = DefineTypePrimitive(symbolName);

        return UnitType;
    }

    /// <summary>
    /// Define a primitive type for this language
    /// </summary>
    /// <param name="symbolName">The name of the primitive type</param>
    /// <returns></returns>
    public TypePrimitive DefineTypePrimitive(string symbolName)
    {
        return new TypePrimitive(symbolName, RootScope, TypePrimitiveRoleName);
    }


    /// <summary>
    /// Return a reference to a primitive type with the given name
    /// </summary>
    /// <param name="typeName">The name of the primitive type</param>
    /// <returns></returns>
    public TypePrimitive GetTypePrimitive(string typeName)
    {
        return RootScope.GetSymbol(typeName, TypePrimitiveRoleName) as TypePrimitive;
    }


    public abstract ILanguageValue CreateDefaultValue(ILanguageType langType);

    //public abstract ILanguageValue GetTypeInitializedValue(ILanguageType lang_type);

    //public abstract ILanguageValue CreateInitializedValue(TypePrimitive lang_type);

    public virtual string Describe(IIronyAstObject astItem)
    {
        if (ReferenceEquals(astItem, null))
            return "<null ast item>";

        _astDescription.Log.Clear();

        astItem.AcceptVisitor(_astDescription);

        return _astDescription.Log.ToString();
    }
}