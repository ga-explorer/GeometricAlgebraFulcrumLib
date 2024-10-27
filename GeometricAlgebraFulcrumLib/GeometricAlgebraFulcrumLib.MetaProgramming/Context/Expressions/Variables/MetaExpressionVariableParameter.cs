using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.HeadSpecs;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions.Variables;

public sealed class MetaExpressionVariableParameter :
    MetaExpressionVariableBase,
    IMetaExpressionVariableParameter
{
    public static MetaExpressionVariableParameter Create(MetaContext context, MetaExpressionHeadSpecsVariable headSpecs)
    {
        return new MetaExpressionVariableParameter(context, headSpecs);
    }

    public static MetaExpressionVariableParameter Create(MetaContext context, string variableName)
    {
        return new MetaExpressionVariableParameter(
            context,
            MetaExpressionHeadSpecsVariable.Create(variableName)
        );
    }


    public override IMetaExpression RhsExpression
        => this;

    public override string RhsExpressionText
        => VariableHeadSpecs.VariableName;

    public override bool IsParameterVariable
        => true;

    public override bool IsComputedVariableOrComposite
        => false;

    public override bool IsIntermediateVariable
        => false;

    public override bool IsOutputVariable
        => false;

    public override bool IsNumberOrParameter
        => true;

    public override bool IsComputedVariable
        => false;

    public override int MaxComputationLevel
        => 0;

    public override IEnumerable<IMetaExpressionVariableParameter> VariableParameterExpressions
    {
        get { yield return this; }
    }

    public override IEnumerable<IMetaExpressionVariableComputed> VariableComputedExpressions
        => Enumerable.Empty<IMetaExpressionVariableComputed>();



    private MetaExpressionVariableParameter(MetaContext context, MetaExpressionHeadSpecsVariable headSpecs)
        : base(context, headSpecs)
    {
    }


    public override IMetaExpression CopyToContext(MetaContext context)
    {
        return context.ImportCopy(this);
    }
    
    public override bool MergeEnabled 
        => false;

    public override bool HasDependingVariables
        => DependingVariablesCache.Count > 0;

    public override bool IsOutputOrHasDependingVariables
        => DependingVariablesCache.Count > 0;


    public override IMetaExpression GetScalarValue(bool useRhsScalarValue)
    {
        return this;
    }

    public override string GetTextDescription()
    {
        var isUsedText = HasDependingVariables
            ? "    Used"
            : "Not Used";

        return $"{isUsedText} Parameter       '{ExternalName}': {VariableHeadSpecs.VariableName}";
    }

    public override bool UpdateExternalName(string externalName)
    {
        //Debug.Assert(
        //    //ExternalName != "tmpVar46"
        //    string.IsNullOrEmpty(_externalName)
        //);

        var externalNameOld = _externalName;

        if (string.IsNullOrEmpty(externalName))
            _externalName = string.Empty;

        else if (string.IsNullOrEmpty(_externalName))
            _externalName = externalName;

        return _externalName != externalNameOld;
    }

    public void SetStateFrom(IMetaExpressionVariableParameter variable)
    {
        _externalName = variable.ExternalName;
        RhsExpressionValue = variable.RhsExpressionValue;
    }

    public override void ClearDependencyData()
    {
        DependingVariablesCache.Clear();
    }
}