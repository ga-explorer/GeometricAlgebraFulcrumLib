using System;
using CodeComposerLib.Irony.Semantic.Type;

namespace CodeComposerLib.Irony.Semantic.Expression.ValueAccess;

/// <summary>
/// This class represents a single step in the value access process
/// </summary>
public abstract class ValueAccessStep
{
    ///// <summary>
    ///// The parent component (previous access step) of this access step. The first step has no parent
    ///// </summary>
    //public ValueAccessStep ParentComponent { get; private set; }
        
    /// <summary>
    /// The type of this access step
    /// </summary>
    public ILanguageType AccessStepType { get; }


    /// <summary>
    /// The name of this access step
    /// </summary>
    public abstract string AccessName { get; }


    /// <summary>
    /// True if this step generates a value having a language type
    /// </summary>
    public bool HasType => AccessStepType != null;

    /// <summary>
    /// True if this is the first step in the access process (i.e. this step has no parent components)
    /// </summary>
    public bool IsFirstComponent => this is ValueAccessStepAsRootSymbol;

    /// <summary>
    /// True if this access step is done using a single key of type K
    /// </summary>
    /// <typeparam name="TK"></typeparam>
    /// <returns></returns>
    public bool IsByKey<TK>() where TK : IComparable<TK> { return this is ValueAccessStepByKey<TK>; }

    /// <summary>
    /// True if this access step is done using a list of keys of type K
    /// </summary>
    /// <typeparam name="TK"></typeparam>
    /// <returns></returns>
    public bool IsByKeyList<TK>() where TK : IComparable<TK> { return this is ValueAccessStepByKeyList<TK>; }

    /// <summary>
    /// True if this access step is done using a language symbol component
    /// </summary>
    public bool IsBySymbol => this is ValueAccessStepByLValue;


    protected ValueAccessStep()
    {
        //ParentComponent = null;
        AccessStepType = null;
    }

    //protected ValueAccessStep(ValueAccessStep parent_component)
    //{
    //    ParentComponent = parent_component;
    //    ComponentType = null;
    //}

    protected ValueAccessStep(ILanguageType componentType)
    {
        //ParentComponent = null;
        AccessStepType = componentType;
    }

    //protected ValueAccessStep(ValueAccessStep parent_component, ILanguageType component_type)
    //{
    //    ParentComponent = parent_component;
    //    ComponentType = component_type;
    //}

        
    public abstract ValueAccessStep Duplicate();

    ///// <summary>
    ///// Replace the ParentComponent member by the given parent component
    ///// </summary>
    ///// <param name="parent_component"></param>
    //public ValueAccessStep ReplaceParentComponent(ValueAccessStep parent_component)
    //{
    //    this.ParentComponent = parent_component;

    //    return this;
    //}

    public override string ToString()
    {
        return AccessName;
    }
}