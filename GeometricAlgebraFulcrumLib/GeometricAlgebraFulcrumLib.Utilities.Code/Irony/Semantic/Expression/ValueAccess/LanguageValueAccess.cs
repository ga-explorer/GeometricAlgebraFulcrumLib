using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Symbol;
using GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Type;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Irony.Semantic.Expression.ValueAccess;

/// <summary>
/// This class is used to represent an access (read or write) operation on a language symbol or any of its parts
/// For example a local variable of type 'structure' can be read as a whole or partially through one of its access_steps.
/// </summary>
public sealed class LanguageValueAccess : ILanguageExpressionAtomic
{
    /// <summary>
    /// A list of access_steps used in the access process
    /// </summary>
    private List<ValueAccessStep> _accessSteps = new List<ValueAccessStep>();


    /// <summary>
    /// Returns the number of access steps (including the root symbol access step) for this value access process
    /// </summary>
    public int AccessStepsCount => _accessSteps.Count;

    /// <summary>
    /// Returns the number of partial access steps for this value access process
    /// </summary>
    public int PartialAccessStepsCount => _accessSteps.Count - 1;

    /// <summary>
    /// The first access_step of the list of access_steps. This access_step must always be a language symbol
    /// </summary>
    public ValueAccessStepAsRootSymbol FirstAccessStep => ((ValueAccessStepAsRootSymbol)_accessSteps[0]);

    /// <summary>
    /// The last access_step of the list of access_steps. This access_step is the main target for access process
    /// </summary>
    public ValueAccessStep LastAccessStep => _accessSteps[_accessSteps.Count - 1];

    /// <summary>
    /// The next-to last access step if exists, or null if not
    /// </summary>
    public ValueAccessStep NextToLastAccessStep => _accessSteps.Count > 1 ? _accessSteps[_accessSteps.Count - 2] : null;

    /// <summary>
    /// The language symbol associated with the first access_step of the access_steps list
    /// </summary>
    public LanguageSymbol RootSymbol => FirstAccessStep.AccessSymbol;

    /// <summary>
    /// True if the access is for the whole language symbol of the first access_step (i.e. the access_steps list only has one access_step)
    /// </summary>
    public bool IsFullAccess => _accessSteps.Count == 1;

    /// <summary>
    /// True if this value access is a direct local variable
    /// </summary>
    public bool IsFullAccessLocalVariable 
    { 
        get 
        {
            if (_accessSteps.Count > 1)
                return false;

            return (FirstAccessStep.AccessSymbol is SymbolLocalVariable);
        } 
    }

    /// <summary>
    /// True if this value access is a direct local variable
    /// </summary>
    public bool IsFullAccessProcedureParameter
    {
        get
        {
            if (_accessSteps.Count > 1)
                return false;

            return (FirstAccessStep.AccessSymbol is SymbolProcedureParameter);
        }
    }

    /// <summary>
    /// True if this value access is a direct l-value symbol
    /// </summary>
    public bool IsFullAccessLValue => (_accessSteps.Count == 1) && (FirstAccessStep.AccessSymbol is SymbolLValue);

    /// <summary>
    /// True if this value access can act as an l-value (meaning its root symbol is an l-value)
    /// </summary>
    public bool IsLValue => (FirstAccessStep.AccessSymbol is SymbolLValue);

    public bool IsNamedValue => (FirstAccessStep.AccessSymbol is SymbolNamedValue);

    /// <summary>
    /// If this value access is a direct local variable, extract and return the local variable
    /// </summary>
    public SymbolLocalVariable AsDirectLocalVariable
    {
        get
        {
            if (_accessSteps.Count > 1)
                return null;

            return (FirstAccessStep.AccessSymbol as SymbolLocalVariable);
        }
    }

    /// <summary>
    /// True if the access is for part of the language symbol of the first access_step (i.e. the access_steps list has more than one access_step)
    /// </summary>
    public bool IsPartialAccess => _accessSteps.Count > 1;

    /// <summary>
    /// True if this is a partial access to an l-value symbol
    /// </summary>
    public bool IsPartialAccessLValue => (_accessSteps.Count > 1) && (FirstAccessStep.AccessSymbol is SymbolLValue);

    /// <summary>
    /// True if all partial access steps are independent of any symbols
    /// </summary>
    public bool IsFixedAccess
    {
        get
        {
            return !_accessSteps.Exists(accessStep => accessStep is ValueAccessStepByLValue);
        }
    }

    /// <summary>
    /// True if any partial access step is dependent on a symbol
    /// </summary>
    public bool IsVariableAccess
    {
        get
        {
            return _accessSteps.Exists(accessStep => accessStep is ValueAccessStepByLValue);
        }
    }

    /// <summary>
    /// True if the root symbol is a procedur input parameter
    /// </summary>
    public bool IsInputParameter
    {
        get
        {
            if (!(RootSymbol is SymbolProcedureParameter))
                return false;

            return ((SymbolProcedureParameter)RootSymbol).DirectionIn;
        }
    }

    /// <summary>
    /// True if the root symbol is a procedur output parameter
    /// </summary>
    public bool IsOutputParameter
    {
        get
        {
            if (!(RootSymbol is SymbolProcedureParameter))
                return false;

            return ((SymbolProcedureParameter)RootSymbol).DirectionOut;
        }
    }

    /// <summary>
    /// True the whole value access is of primitive type
    /// </summary>
    public bool IsPrimitive => ExpressionType is TypePrimitive;

    /// <summary>
    /// True if the root symbol is a procedur parameter
    /// </summary>
    public bool IsParameter => (RootSymbol is SymbolProcedureParameter);

    /// <summary>
    /// True if the root symbol is a procedur parameter and the whole value access is of primitive type
    /// </summary>
    public bool IsPrimitiveParameter
    {
        get
        {
            if (!(RootSymbol is SymbolProcedureParameter))
                return false;

            return ExpressionType is TypePrimitive;
        }
    }

    /// <summary>
    /// /// True if the root symbol is a local variable
    /// </summary>
    public bool IsLocalVariable => RootSymbol is SymbolLocalVariable;


    /// <summary>
    /// Return the language symbol associated with the first access_step of the access_steps list as a SymbolDataStore object
    /// </summary>
    public SymbolDataStore RootSymbolAsDataStore => RootSymbol as SymbolDataStore;

    /// <summary>
    /// Return the language symbol associated with the first access_step of the access_steps list as a SymbolLValue object
    /// </summary>
    public SymbolLValue RootSymbolAsLValue => RootSymbol as SymbolLValue;

    /// <summary>
    /// Return the language symbol associated with the first access_step of the access_steps list as a SymbolLocalVariable object
    /// </summary>
    public SymbolLocalVariable RootSymbolAsLocalVariable => RootSymbol as SymbolLocalVariable;

    /// <summary>
    /// Return the language symbol associated with the first access_step of the access_steps list as a SymbolProcedureParameter object
    /// </summary>
    public SymbolProcedureParameter RootSymbolAsParameter => RootSymbol as SymbolProcedureParameter;

    /// <summary>
    /// Returns a list of value access access_steps
    /// </summary>
    public IEnumerable<ValueAccessStep> AccessSteps => _accessSteps;

    /// <summary>
    /// Returns a list of access_steps used for partial value access (i.e. all access_steps except for the first one)
    /// </summary>
    public IEnumerable<ValueAccessStep> PartialAccessSteps => _accessSteps.Skip(1);

    /// <summary>
    /// Returns a list of access_steps used for partial value access (i.e. all access_steps except for the first one)
    /// </summary>
    public IEnumerable<ValueAccessStep> PartialAccessStepsExceptLast 
    { 
        get 
        { 
            for (var i = 1; i < _accessSteps.Count - 1; i++)
                yield return _accessSteps[i];
        } 
    }

    /// <summary>
    /// Returns a list of all partial access-by-symbol steps where the access symbol is identical to the given symbol
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public IEnumerable<ValueAccessStepByLValue> PartialAccessStepsByLValue(LanguageSymbol symbol)
    {
        for (var i = 1; i < _accessSteps.Count; i++)
        {
            var step = _accessSteps[i] as ValueAccessStepByLValue;

            if (step?.AccessLValue.ObjectId == symbol.ObjectId)
                yield return step;
        }
    }

    /// <summary>
    /// Returns a list of all partial access-by-symbol steps
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ValueAccessStepByLValue> PartialAccessStepsByLValue()
    {
        for (var i = 1; i < _accessSteps.Count; i++)
        {
            var step = _accessSteps[i] as ValueAccessStepByLValue;

            if (step == null) 
                continue;

            yield return step;
        }
    }

    /// <summary>
    /// Returns a list of all l-values used in all partial access-by-symbol steps
    /// </summary>
    public IEnumerable<SymbolLValue> PartialAccessLValues
    {
        get
        {
            for (var i = 1; i < _accessSteps.Count; i++)
            {
                var step = _accessSteps[i] as ValueAccessStepByLValue;

                if (step == null) 
                    continue;

                yield return step.AccessLValue;
            }
        }
    }

    /// <summary>
    /// Returns a list of all l-values used in all access-by-symbol steps (including the root step)
    /// </summary>
    public IEnumerable<SymbolLValue> AccessLValues
    {
        get
        {
            var value = RootSymbol as SymbolLValue;

            if (value != null)
                yield return value;

            for (var i = 1; i < _accessSteps.Count; i++)
            {
                var step = _accessSteps[i] as ValueAccessStepByLValue;

                if (step == null) 
                    continue;

                yield return step.AccessLValue;
            }
        }
    }

    /// <summary>
    /// Returns a list of all symbols used in all partial access-by-symbol steps
    /// </summary>
    public IEnumerable<LanguageSymbol> PartialAccessSymbols
    {
        get
        {
            for (var i = 1; i < _accessSteps.Count; i++)
            {
                var step = _accessSteps[i] as ValueAccessStepByLValue;

                if (step != null)
                    yield return step.AccessLValue;
            }
        }
    }

    /// <summary>
    /// Returns a list of all symbols used in all access-by-symbol steps (including the root step)
    /// </summary>
    public IEnumerable<LanguageSymbol> AccessSymbols
    {
        get
        {
            yield return RootSymbol;

            for (var i = 1; i < _accessSteps.Count; i++)
            {
                var step = _accessSteps[i] as ValueAccessStepByLValue;

                if (step != null)
                    yield return step.AccessLValue;
            }
        }
    }

    /// <summary>
    /// True if the root symbol is identical to the given symbol
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public bool HasRootSymbol(LanguageSymbol symbol)
    {
        return (RootSymbol.ObjectId == symbol.ObjectId);
    }

    /// <summary>
    /// True if any access-by-symbol step (including the root step) depends on the given symbol
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public bool HasAccessStepWithSymbol(LanguageSymbol symbol)
    {
        if (RootSymbol.ObjectId == symbol.ObjectId)
            return true;

        return 
            PartialAccessSymbols
                .Any(accessStepSymbol => accessStepSymbol.ObjectId == symbol.ObjectId);
    }


    /// <summary>
    /// The parent Irony DSL associated with the root language symbol
    /// </summary>
    public IronyAst RootAst => RootSymbol.RootAst;

    /// <summary>
    /// Returns the language type associated wit the target of the access process (i.e. the last access_step in the access_steps list)
    /// </summary>
    public ILanguageType ExpressionType => LastAccessStep.AccessStepType;

    public bool IsSimpleExpression => ReferenceEquals(AccessLValues.FirstOrDefault(), null);


    /// <summary>
    /// True if the last access_step has a type
    /// </summary>
    public bool HasType => LastAccessStep.HasType;


    private LanguageValueAccess(LanguageSymbol rootSymbol)
    {
        var firstAccessStep = ValueAccessStepAsRootSymbol.Create(rootSymbol);

        _accessSteps.Add(firstAccessStep);
    }


    public LanguageValueAccess Duplicate()
    {
        var newValueAccess = Create(RootSymbol);

        if (IsPartialAccess)
            newValueAccess.Append(PartialAccessSteps);
        //{
        //    for (int i = 1; i < this._AccessSteps.Count; i++)
        //    {
        //        ValueAccessStep new_access_step =
        //            this._AccessSteps[i].Duplicate();

        //        new_value_access.Append(new_access_step);
        //    }
        //}

        return newValueAccess;
    }

    public LanguageValueAccess DuplicateExceptLast()
    {
        var newValueAccess = Create(RootSymbol);

        if (IsPartialAccess)
            newValueAccess.Append(PartialAccessStepsExceptLast);
        //{
        //    for (int i = 1; i < this._AccessSteps.Count - 1; i++)
        //    {
        //        ValueAccessStep new_access_step =
        //            this._AccessSteps[i].Duplicate();

        //        new_value_access.Append(new_access_step);
        //    }
        //}

        return newValueAccess;
    }

    /// <summary>
    /// Replace the root symbol in this value access by the given symbol
    /// for example replacing 'x' in 'x.y.z' by 'a' gives 'a.y.z'
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public LanguageValueAccess ReplaceRootSymbol(LanguageSymbol symbol)
    {
        ((ValueAccessStepAsRootSymbol)_accessSteps[0]).ReplaceComponentSymbol(symbol);

        return this;
    }

    public LanguageValueAccess ReplaceRootSymbol(LanguageValueAccess valueAccess)
    {
        var newAccessStepsList = new List<ValueAccessStep>();

        newAccessStepsList.AddRange(valueAccess.AccessSteps);

        if (IsPartialAccess)
        {
            //this.LastAccessComponent.ReplaceParentComponent(new_access_steps_list.Last());

            newAccessStepsList.AddRange(PartialAccessSteps);
        }

        _accessSteps = newAccessStepsList;

        return this;
    }


    /// <summary>
    /// Create a new ValueComponentAccessByIndex object and append it to this LanguageValueAccess
    /// </summary>
    /// <param name="accessStepIndex"></param>
    /// <param name="accessStepType"></param>
    /// <returns></returns>
    public LanguageValueAccess Append<TK>(TK accessStepIndex, ILanguageType accessStepType) where TK : IComparable<TK>
    {
        var accessStep = ValueAccessStepByKey<TK>.Create(accessStepType, accessStepIndex);

        _accessSteps.Add(accessStep);

        return this;
    }

    /// <summary>
    /// Create a new ValueComponentAccessByIndexList object and append it to this LanguageValueAccess
    /// </summary>
    /// <param name="accessStepIndexList"></param>
    /// <param name="accessStepType"></param>
    /// <returns></returns>
    public LanguageValueAccess Append<TK>(IEnumerable<TK> accessStepIndexList, ILanguageType accessStepType) where TK : IComparable<TK>
    {
        var accessStep = ValueAccessStepByKeyList<TK>.Create(accessStepType, accessStepIndexList);

        _accessSteps.Add(accessStep);

        return this;
    }

    /// <summary>
    /// Create a new ValueComponentAccessBySymbol object and append it to this LanguageValueAccess
    /// </summary>
    /// <param name="accessStepSymbol"></param>
    /// <param name="accessStepType"></param>
    /// <returns></returns>
    public LanguageValueAccess Append(SymbolLValue accessStepSymbol, ILanguageType accessStepType)
    {
        var accessStep = ValueAccessStepByLValue.Create(accessStepType, accessStepSymbol);

        _accessSteps.Add(accessStep);

        return this;
    }

    /// <summary>
    /// Append an arbitrary ValueComponentAccess to this LanguageValueAccess
    /// </summary>
    /// <param name="accessStep"></param>
    /// <returns></returns>
    public LanguageValueAccess Append(ValueAccessStep accessStep)
    {
        if (accessStep is ValueAccessStepAsRootSymbol)
            throw new InvalidOperationException();

        _accessSteps.Add(accessStep);

        return this;
    }

    public LanguageValueAccess Append(IEnumerable<ValueAccessStep> accessSteps)
    {
        foreach (var accessStep in accessSteps)
        {
            if (accessStep is ValueAccessStepAsRootSymbol)
                throw new InvalidOperationException();

            _accessSteps.Add(accessStep.Duplicate());
        }

        return this;
    }
        
    public override string ToString()
    {
        return RootAst.Describe(this);
    }


    //public void AcceptVisitor(IASTNodeAcyclicVisitor visitor)
    //{
    //    if (visitor is IASTNodeAcyclicVisitor<LanguageValueAccess>)
    //        ((IASTNodeAcyclicVisitor<LanguageValueAccess>)visitor).Visit(this);

    //    //You can write fall back logic here if needed.
    //}

    /// <summary>
    /// Create a new LanguageValueAccess object
    /// </summary>
    /// <param name="rootSymbol"></param>
    /// <returns></returns>
    public static LanguageValueAccess Create(LanguageSymbol rootSymbol)
    {
        return new LanguageValueAccess(rootSymbol);
    }

    public static LanguageValueAccess Create(LanguageSymbol rootSymbol, IEnumerable<ValueAccessStep> accessSteps)
    {
        return (new LanguageValueAccess(rootSymbol)).Append(accessSteps);
    }
}