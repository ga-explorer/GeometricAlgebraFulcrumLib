using AngouriMath;
using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Evaluators;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Numbers;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public abstract class MetaExpressionVariableBase :
    IMetaExpressionVariable
{
    protected HashSet<IMetaExpressionVariableComputed> DependingVariablesCache { get; }
        = new HashSet<IMetaExpressionVariableComputed>();

    public MetaContext Context { get; }

    public IMetaExpressionHeadSpecs HeadSpecs
        => VariableHeadSpecs;

    public IMetaExpressionHeadSpecsAtomic AtomicHeadSpecs
        => VariableHeadSpecs;

    public MetaExpressionHeadSpecsVariable VariableHeadSpecs { get; }

    public IEnumerable<IMetaExpressionVariableComputed> DependingVariables
        => DependingVariablesCache;

    public IEnumerable<IMetaExpressionVariableComputed> DirectDependingVariables
        => DependingVariablesCache.Where(v =>
            v.RhsVariables.Select(u => u.InternalName).Contains(InternalName)
        );

    public IEnumerable<IMetaExpressionVariableComputed> DirectDependingIntermediateVariables
        => DependingVariablesCache.Where(v =>
            v.IsIntermediateVariable &&
            v.RhsVariables.Any(u => u.InternalName == InternalName)
        );

    public abstract bool HasDependingVariables { get; }

    public abstract bool IsOutputOrHasDependingVariables { get; }

    public IMetaExpressionVariableComputed LastDependingVariable
        => DependingVariablesCache.MaxBy(
            v => v.ComputationOrder
        );

    public int LastDependingVariableComputationOrder
        => LastDependingVariable?.ComputationOrder ?? -1;

    public string HeadText
        => VariableHeadSpecs.HeadText;

    public bool IsNumberSymbolOrVariable
        => true;

    public bool IsNumber
        => false;

    public bool IsLiteralNumber
        => false;

    public bool IsSymbolicNumber
        => false;

    public bool IsVariable
        => true;

    public bool IsFunction
        => false;

    public bool IsArrayAccess
        => false;

    public bool IsOperator
        => false;

    public bool IsAtomic
        => true;

    public bool IsComposite
        => false;

    public int ComputationsCount
        => 0;

    public IEnumerable<IMetaExpression> Expressions
    {
        get { yield return this; }
    }

    public IEnumerable<IMetaExpression> SubExpressions
        => Enumerable.Empty<IMetaExpression>();

    public IEnumerable<IMetaExpressionAtomic> AtomicExpressions
    {
        get { yield return this; }
    }

    public IEnumerable<IMetaExpressionAtomic> AtomicSubExpressions
        => Enumerable.Empty<IMetaExpressionAtomic>();

    public IEnumerable<IMetaExpressionComposite> CompositeExpressions
        => Enumerable.Empty<IMetaExpressionComposite>();

    public IEnumerable<IMetaExpressionComposite> CompositeSubExpressions
        => Enumerable.Empty<IMetaExpressionComposite>();

    public IEnumerable<IMetaExpressionNumber> NumberExpressions
        => Enumerable.Empty<IMetaExpressionNumber>();

    public IEnumerable<IMetaExpressionNumber> NumberSubExpressions
        => Enumerable.Empty<IMetaExpressionNumber>();

    public IEnumerable<IMetaExpressionVariable> VariableExpressions
    {
        get { yield return this; }
    }

    public IEnumerable<IMetaExpressionVariable> VariableSubExpressions
        => Enumerable.Empty<IMetaExpressionVariable>();

    public abstract IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions { get; }

    public IEnumerable<IMetaExpressionVariableParameter> VariableParameterSubExpressions
        => Enumerable.Empty<IMetaExpressionVariableParameter>();

    public abstract IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions { get; }

    public IEnumerable<IMetaExpressionVariableComputed> VariableComputedSubExpressions
        => Enumerable.Empty<IMetaExpressionVariableComputed>();

    public int AtomicExpressionId { get; }

    public string InternalName
        => VariableHeadSpecs.VariableName;


    protected string _externalName = string.Empty;
    public string ExternalName
        => string.IsNullOrEmpty(_externalName)
            ? InternalName
            : _externalName;

    public abstract IMetaExpression RhsExpression { get; }

    public abstract string RhsExpressionText { get; }

    public double RhsExpressionValue { get; protected set; }

    public abstract bool IsParameterVariable { get; }

    public abstract bool IsComputedVariableOrComposite { get; }

    public abstract bool IsIntermediateVariable { get; }

    public abstract bool IsOutputVariable { get; }

    public abstract bool IsNumberOrParameter { get; }

    public abstract bool IsComputedVariable { get; }

    public abstract int MaxComputationLevel { get; }


    protected MetaExpressionVariableBase(MetaContext context, MetaExpressionHeadSpecsVariable headSpecs)
    {
        Context = context;
        AtomicExpressionId = Context.GetNextAtomicExpressionId();
        VariableHeadSpecs = headSpecs;
        RhsExpressionValue = 0d;
    }


    public IMetaExpression Simplify(IMetaExpressionEvaluator symbolicEvaluator)
    {
        return this;
    }

    public abstract string GetTextDescription();

    public abstract bool UpdateExternalName(string externalName);

    public Tuple<bool, IMetaExpression> ReplaceAllExpressionByExpression(IMetaExpression oldExpr, IMetaExpression newExpr)
    {
        return MetaExpressionUtils.Equals(this, oldExpr)
            ? new Tuple<bool, IMetaExpression>(true, newExpr)
            : new Tuple<bool, IMetaExpression>(false, this);
    }

    public Tuple<bool, IMetaExpression> ReplaceAllExpressionByVariable(IMetaExpression oldExpr, string variableName, MetaContext context)
    {
        var newExpr = context.GetVariable(variableName);

        return MetaExpressionUtils.Equals(this, oldExpr)
            ? new Tuple<bool, IMetaExpression>(true, newExpr)
            : new Tuple<bool, IMetaExpression>(false, this);
    }

    public Tuple<bool, IMetaExpression> ReplaceAllVariableByVariable(string oldVariableName, string newVariableName, MetaContext context)
    {
        var oldExpr = context.GetVariable(oldVariableName);
        var newExpr = context.GetVariable(newVariableName);

        return MetaExpressionUtils.Equals(this, oldExpr)
            ? new Tuple<bool, IMetaExpression>(true, newExpr)
            : new Tuple<bool, IMetaExpression>(false, this);
    }

    public abstract IMetaExpression CopyToContext(MetaContext context);

    public Entity ToAngouriMathEntity()
    {
        return MathS.Var(InternalName);
    }

    public SteExpression ToSimpleTextExpression()
    {
        return SteExpression.CreateVariable(InternalName);
    }

    public abstract IMetaExpression GetScalarValue(bool useRhsScalarValue);


    public void SetRhsExpressionValue(double number)
    {
        RhsExpressionValue = number;
    }

    public void AddDependingVariable(IMetaExpressionVariableComputed computedVar)
    {
        DependingVariablesCache.Add(computedVar);
    }

    public abstract void ClearDependencyData();

    public override string ToString()
    {
        return VariableHeadSpecs.VariableName;
    }
}